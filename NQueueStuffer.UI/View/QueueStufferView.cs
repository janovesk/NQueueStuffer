using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NQueueStuffer.UI.Controller;

namespace NQueueStuffer.UI.View
{
	public partial class QueueStufferView : Form, IQueueStufferView
	{
		private IQueueStufferController _controller;
		private string _assemblyFile = null;
		private bool _tbMessageTextChangedProcessing = false;
		private const float MessageUpdateInterval = 1.5f;
		private DateTime _lastMessageUpdateTime = DateTime.MaxValue;
	    private List<string> _queueNames;

		#region Ctors

		public QueueStufferView(IQueueStufferController controller, NqsSettingItem settingItem)
		{
			InitializeComponent();

			tbMessageTimer.Start();

			_assemblyFile = settingItem.AssemblyPath;
			_controller = controller;
			_controller.Initialize(this);
			_controller.GetTypesFromAssembly(settingItem.AssemblyPath, settingItem.SelectedTypeName);
			tbQueueName.Text = settingItem.QueueName;
			tbMessage.Text = settingItem.MessageContent;
			if (settingItem.IsLocked.HasValue  && settingItem.IsLocked.Value)
			{
				checkLockSelection.CheckState = CheckState.Checked;
			}
			SetupTitle();
			tbMessage_TextChanged(tbMessage, EventArgs.Empty);
            PopulateQueueNames();
		}

		public QueueStufferView(IQueueStufferController controller, string defaultQueueName, string assemblyFile)
		{
			InitializeComponent();

			tbMessageTimer.Start();

			_assemblyFile = assemblyFile;
			tbQueueName.Text = defaultQueueName;
			_controller = controller;
			_controller.Initialize(this);
			_controller.GetTypesFromAssembly(assemblyFile);
			SetupTitle();
			tbMessage_TextChanged(tbMessage, EventArgs.Empty);
            PopulateQueueNames();
		}

		#endregion

		#region Public interface

		public void SetMessagesTypes(Type[] types, Type selectedType = null)
		{
			listBoxMessageTypes.DataSource = types;
			listBoxMessageTypes.DisplayMember = "Name";
			if (selectedType != null)
			{
				listBoxMessageTypes.SelectedItem = selectedType;
			}

		}

		public void ShowSerializedMessageType(string serializedMessageType, Type messageType)
		{
			tbMessage.Text = serializedMessageType;
			HighlightXml(tbMessage);
			SetStatus(string.Format("Message of type {0} has been loaded.", messageType.Name));
		}

		public NqsSettingItem SelectedSettings
		{
			get
			{
				var type = listBoxMessageTypes.SelectedItem as Type;
				var item = new NqsSettingItem()
						{
							AssemblyPath = _assemblyFile,
							MessageContent = tbMessage.Text,
							QueueName = tbQueueName.Text,
							SelectedTypeName = type != null ? type.Name : null,
							SelectedType = type
						};

				switch(checkLockSelection.CheckState)
				{
					case CheckState.Checked:
						item.IsLocked = true;
						break;
					case CheckState.Unchecked:
						item.IsLocked = false;
						break;
					case CheckState.Indeterminate:
						item.IsLocked = null;
						break;
				}

				return item;
			}
		}

		public bool MessageIsDirty
		{
			get { return (DateTime.Now - _lastMessageUpdateTime).TotalMilliseconds > 1000*MessageUpdateInterval; }
		}

		public void SetStatus(string message)
		{
			lblStatus.Text = message;
		}

		#endregion

		#region Implementation

        private void PopulateQueueNames()
        {
            _queueNames = _controller.GetQueueNames();

            cmbQueueName.Items.Clear();

            foreach (string queueName in _queueNames)
            {
                cmbQueueName.Items.Add(queueName);
            }
        }

		private void SetupTitle()
		{
			this.Text = Path.GetFileNameWithoutExtension(_assemblyFile);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Dll files (*.dll)|*.dll";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				var filename = openFileDialog1.FileName;
				_assemblyFile = filename;
				_controller.GetTypesFromAssembly(filename);
				SetupTitle();
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			try
			{
				_controller.StuffMessageToQueue(tbMessage.Text, tbQueueName.Text);
				SetStatus(String.Format("Message {0} stuffed into queue {1} successfully.", listBoxMessageTypes.SelectedValue, tbQueueName.Text));
			}
			catch (Exception exception)
			{
				SetStatus(String.Format("Error stuffing {0} message into queue {1}: {2}", listBoxMessageTypes.SelectedValue, tbQueueName.Text, exception.Message));
			}

		}

		private void listBoxMessageTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			var messageType = (Type)listBoxMessageTypes.SelectedItem;

			_controller.MessageTypeSelectionChanged(messageType);
		}

		private void QueueStufferView_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void QueueStufferView_DragDrop(object sender, DragEventArgs e)
		{
			var filenames = e.Data.GetData(DataFormats.FileDrop) as string[];

			if (filenames != null && filenames.Length > 0)
			{
				_controller.GetTypesFromAssembly(filenames[0]);
			}
		}

		private delegate void UpdateMessageColorsDelegate();
		private void UpdateMessageColors()
		{
			if (InvokeRequired)
			{
				UpdateMessageColorsDelegate del = UpdateMessageColors;
				Invoke(del);
			}
			else
			{
				_lastMessageUpdateTime = DateTime.MaxValue;
				_tbMessageTextChangedProcessing = true;
				HighlightXml(tbMessage);
				_tbMessageTextChangedProcessing = false;
			}
		}

		private void tbMessage_TextChanged(object sender, EventArgs e)
		{
			if (!_tbMessageTextChangedProcessing)
				_lastMessageUpdateTime = DateTime.Now;
		}

		private void checkLockSelection_CheckStateChanged(object sender, EventArgs e)
		{
			bool enabled = checkLockSelection.CheckState != CheckState.Checked;
			btnLoad.Enabled = enabled;
			tbMessage.Enabled = enabled;
			listBoxMessageTypes.Enabled = enabled;
			tbQueueName.Enabled = enabled;
		}

		public class HighlightColors
		{
			public static Color HC_NODE = Color.Firebrick;
			public static Color HC_STRING = Color.Blue;
			public static Color HC_ATTRIBUTE = Color.Red;
			public static Color HC_COMMENT = Color.GreenYellow;
			public static Color HC_INNERTEXT = Color.Black;
		}

		public static void HighlightXml(RichTextBox rtb)
		{
			if (rtb.IsDisposed)
				return;

			int oldCursor = rtb.SelectionStart;
			Color oldBackColor = rtb.SelectionBackColor;
			rtb.SelectionBackColor = Color.Transparent;

			int k = 0;

			string str = rtb.Text;

			int st, en;
			int lasten = -1;
			while (k < str.Length)
			{
				st = str.IndexOf('<', k);

				if (st < 0)
					break;

				if (lasten > 0)
				{
					rtb.Select(lasten + 1, st - lasten - 1);
					rtb.SelectionColor = HighlightColors.HC_INNERTEXT;
				}

				en = str.IndexOf('>', st + 1);
				if (en < 0)
					break;

				k = en + 1;
				lasten = en;

				if (str[st + 1] == '!')
				{
					rtb.Select(st + 1, en - st - 1);
					rtb.SelectionColor = HighlightColors.HC_COMMENT;
					continue;

				}
				String nodeText = str.Substring(st + 1, en - st - 1);


				bool inString = false;

				int lastSt = -1;
				int state = 0;
				/* 0 = before node name
				 * 1 = in node name
				   2 = after node name
				   3 = in attribute
				   4 = in string
				   */
				int startNodeName = 0, startAtt = 0;
				for (int i = 0; i < nodeText.Length; ++i)
				{
					if (nodeText[i] == '"')
						inString = !inString;

					if (inString && nodeText[i] == '"')
						lastSt = i;
					else
						if (nodeText[i] == '"')
						{
							rtb.Select(lastSt + st + 2, i - lastSt - 1);
							rtb.SelectionColor = HighlightColors.HC_STRING;
						}

					switch (state)
					{
						case 0:
							if (!Char.IsWhiteSpace(nodeText, i))
							{
								startNodeName = i;
								state = 1;
							}
							break;
						case 1:
							if (Char.IsWhiteSpace(nodeText, i))
							{
								rtb.Select(startNodeName + st, i - startNodeName + 1);
								rtb.SelectionColor = HighlightColors.HC_NODE;
								state = 2;
							}
							break;
						case 2:
							if (!Char.IsWhiteSpace(nodeText, i))
							{
								startAtt = i;
								state = 3;
							}
							break;

						case 3:
							if (Char.IsWhiteSpace(nodeText, i) || nodeText[i] == '=')
							{
								rtb.Select(startAtt + st, i - startAtt + 1);
								rtb.SelectionColor = HighlightColors.HC_ATTRIBUTE;
								state = 4;
							}
							break;
						case 4:
							if (nodeText[i] == '"' && !inString)
								state = 2;
							break;


					}

				}
				if (state == 1)
				{
					rtb.Select(st + 1, nodeText.Length);
					rtb.SelectionColor = HighlightColors.HC_NODE;
				}


			}

			rtb.SelectionStart = Math.Min(oldCursor, rtb.TextLength-1);
			rtb.SelectionLength = 0;
			rtb.SelectionBackColor = oldBackColor;
		}

		#endregion

		private void tbMessageTimer_Tick(object sender, EventArgs e)
		{
			if (MessageIsDirty)
			{
				UpdateMessageColors();
			}
		}

        private void cmbQueueName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbQueueName.Text = ((ComboBox) sender).SelectedItem.ToString();
        }

        private void btnRefreshQueues_Click(object sender, EventArgs e)
        {
            string selectedQueue = tbQueueName.Text;
            PopulateQueueNames();

            if(_queueNames.Contains(selectedQueue))
            {
                tbQueueName.Text = selectedQueue;
            }
            else
            {
                tbQueueName.Text = string.Empty;
            }
        }
	}
}
