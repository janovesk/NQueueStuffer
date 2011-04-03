using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Castle.Windsor;
using NQueueStuffer.UI.Controller;

namespace NQueueStuffer.UI.View
{
	public partial class MdiForm : Form
	{
		private readonly IWindsorContainer _container = null;
		private readonly IList<QueueStufferView> _views = new List<QueueStufferView>();
		private readonly Font _selectedViewFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
		private readonly Font _viewFont = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular); 

		public MdiForm(IWindsorContainer container)
		{
			InitializeComponent();
			_container = container;
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Dll files (*.dll)|*.dll";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				var filename = openFileDialog1.FileName;
				var controller = _container.Resolve<IQueueStufferController>();
				var view = new QueueStufferView(controller, ConfigurationManager.AppSettings["defaultQueueName"], filename);
				AddView(view);
			}

		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "NQueueStuffer (*.nqs)|*.nqs";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				var filename = openFileDialog1.FileName;
				List<NqsSettingItem> settings = null;
				try
				{
					var loader = new NqsSettingsLoader(filename);
					settings = loader.Load();
				}
				catch
				{
					SetMessage("Invalid file supplied!", MessageType.Error);
					return;
				}


				while (_views.Count > 0)
				{
					RemoveView(_views[0], true);
					
				}

				string error = String.Empty;
				foreach (var settingItem in settings)
				{
					try
					{
						var controller = _container.Resolve<IQueueStufferController>();
						var view = new QueueStufferView(controller, settingItem);
						AddView(view);
					}
					catch
					{
						error += String.Format("Error adding assembly {0}", settingItem.AssemblyPath);
					}
				}
				if (error.Length > 0)
				{
					SetMessage(error, MessageType.Error);
				}
				else
				{
					SetMessage(String.Format("Successfully loaded {0} assemblies.", settings.Count), MessageType.Message);
				}
			}

		}

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "NQueueStuffer (*.nqs)|*.nqs";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				var settings = new List<NqsSettingItem>();
				try
				{
					settings = MdiChildren.Select(f => (f as IQueueStufferView).SelectedSettings).ToList();
					var loader = new NqsSettingsLoader(saveFileDialog1.FileName);
					loader.Save(settings);
				}
				catch  (Exception ex)
				{
					SetMessage(String.Format("Error saving assemblies! ({0})", ex.Message), MessageType.Error);
				}
				SetMessage(String.Format("Successfully saved {0} assemblies.", settings.Count), MessageType.Message);
			}
			
		}

		private void AddView(QueueStufferView view)
		{
			_views.Add(view);
			view.MdiParent = this;
			view.Show();
			ActivateMdiChild(view);
			view.Closed += view_Closed;
			view.Activated += view_Activated;
			var toolStripItem = toolStrip1.Items.Add((_views.IndexOf(view) + 1).ToString());
			toolStripItem.ToolTipText = Path.GetFileNameWithoutExtension(view.SelectedSettings.AssemblyPath);
			toolStripItem.Click += activateToolStripButton_Click; 
			toolStrip1.Items.Add(toolStripItem);
			toolStripItem.PerformClick();
		}

		private bool _toolStripButtonClickProcessing = false;
		void view_Activated(object sender, EventArgs e)
		{
			if (!_toolStripButtonClickProcessing)
			{
				var view = sender as QueueStufferView;
				var index = _views.IndexOf(view);
				var seletedItem = FindButtonByText((index + 1).ToString());
				if (seletedItem != null)
				{
					UpdateToolbox(seletedItem);
				}
			}
		}

		void view_Closed(object sender, EventArgs e)
		{
			var view = sender as QueueStufferView;
			RemoveView(view, true);
		}

		private void activateToolStripButton_Click(object sender, EventArgs e)
		{
			var item = sender as ToolStripItem;
			int index = int.Parse(item.Text) - 1;
			var view = _views[index];

			_toolStripButtonClickProcessing = true;
			view.Activate();
			_toolStripButtonClickProcessing = false;

			UpdateToolbox(item);
		}

		private void UpdateToolbox(ToolStripItem slectedItem)
		{
			foreach (var toolStripItem in toolStrip1.Items.Cast<ToolStripItem>().Where(itm => itm != slectedItem))
			{
				toolStripItem.Font = _viewFont;
				toolStripItem.BackColor = Color.FromKnownColor(KnownColor.Control); ;
			}

			slectedItem.Font = _selectedViewFont;
			slectedItem.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
		}

		private void RemoveView(QueueStufferView child, bool dispose)
		{
			var childIndex = _views.IndexOf(child);
			if (childIndex == -1)
				return;

			//remove toolStripItem.
			var key = (childIndex + 1).ToString();
			ToolStripItem toolItem = FindButtonByText(key);
			if (toolItem != null)
			{
				toolStrip1.Items.Remove(toolItem);
				toolItem.Click -= activateToolStripButton_Click; 
				toolItem.Dispose();
			}

			//shift tooltip keys
			for (int i = childIndex; i < _views.Count; i++)
			{
				var text = (i + 1).ToString();
				var button = FindButtonByText(text);
				if (button != null)
				{
					button.Text = i.ToString();
				}
			}

			//drop child view
			_views.RemoveAt(childIndex);
			if (dispose)
			{
				child.Closed -= view_Closed;
				if (child.Visible)
					child.Close();
				child.Dispose();
			}


			if (ActiveMdiChild != null)
			{
				var index = _views.IndexOf(ActiveMdiChild as QueueStufferView);
				ActivateToolstripButton((index + 1).ToString());
			}
		}

		private ToolStripItem FindButtonByText(string text)
		{
			return toolStrip1.Items.Cast<ToolStripItem>().FirstOrDefault(item => item.Text == text);
		}

		private void ActivateToolstripButton(string key)
		{
			var item = FindButtonByText(key);
			if (item != null)
			{
				item.PerformClick();
			}
		}


		private void SetMessage(string message, MessageType type)
		{
			lblStatus.Text = message;
			switch (type)
			{
				case MessageType.Error:
					lblStatus.ForeColor = Color.Red;
					break;
				case MessageType.Message:
					lblStatus.ForeColor = Color.Black;
					break;
				case MessageType.Warning:
					lblStatus.ForeColor = Color.Orange;
					break;
			}
		}

		private enum MessageType
		{
			Message,
			Warning,
			Error
		}
	}
}
