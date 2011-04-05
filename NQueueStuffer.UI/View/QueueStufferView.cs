using System;
using System.Drawing;
using System.Windows.Forms;
using NQueueStuffer.UI.Controller;

namespace NQueueStuffer.UI.View
{
	public partial class QueueStufferView : Form, IQueueStufferView
	{
		private IQueueStufferController _controller;
		private string _assemblyFile = null;

		public QueueStufferView(IQueueStufferController controller, NqsSettingItem settingItem)
		{
			InitializeComponent();

			_assemblyFile = settingItem.AssemblyPath;
			_controller = controller;
			_controller.Initialize(this);
			_controller.GetTypesFromAssembly(settingItem.AssemblyPath, settingItem.SelectedTypeName);
			tbQueueName.Text = settingItem.QueueName;
			tbMessage.Text = settingItem.MessageContent;
		}

		public QueueStufferView(IQueueStufferController controller, string defaultQueueName, string assemblyFile)
		{
			InitializeComponent();

			_assemblyFile = assemblyFile;
			tbQueueName.Text = defaultQueueName;
			_controller = controller;
			_controller.Initialize(this);
			_controller.GetTypesFromAssembly(assemblyFile);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Dll files (*.dll)|*.dll";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				var filename = openFileDialog1.FileName;
				_assemblyFile = filename;
				_controller.GetTypesFromAssembly(filename);
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			_controller.StuffMessageToQueue(tbMessage.Text, tbQueueName.Text);
		}

		private void listBoxMessageTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			var messageType = (Type)listBoxMessageTypes.SelectedItem;

			_controller.MessageTypeSelectionChanged(messageType);
		}

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
				return new NqsSettingItem()
						{
							AssemblyPath = _assemblyFile,
							MessageContent = tbMessage.Text,
							QueueName = tbQueueName.Text,
							SelectedTypeName = type != null ? type.Name : null,
							SelectedType = type
						};
			}
		}

		public void SetStatus(string message)
		{
			lblStatus.Text = message;
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

		private bool _tbMessageTextChangedProcessing = false;
		private bool _tbMessageIsDirty = false;
		private void tbMessage_TextChanged(object sender, EventArgs e)
		{
			if (_tbMessageTextChangedProcessing)
			{
				_tbMessageIsDirty = true;
				return;
			}

			var tb = sender as RichTextBox;
			if (tb == null)
				return;

			_tbMessageTextChangedProcessing = true;

			HighlightXml(tbMessage);

			_tbMessageTextChangedProcessing = false;

			if (_tbMessageIsDirty)
			{
				_tbMessageIsDirty = false;
				tbMessage_TextChanged(sender, e);
			}
		}



		protected enum SymbolPosition
		{
			Unknown,
			StartNodeName,
			EndNode,
			AttributeName,
			AttributeValue,
			NodeValue
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
			int oldCursor = rtb.SelectionStart;
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
		}
	}
}
