using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NQueueStuffer.Core;

namespace NQueueStuffer.UI.View
{
	public partial class BatchView : Form
	{
		private bool _executing = false;
		private IList<NqsSettingItem> _items = null;

		private Thread _batchThread = null;

		public BatchView(IList<NqsSettingItem> items)
		{
			InitializeComponent();
			_items = items;

			SetupControls();
		}

		private void SetupControls()
		{
			Closing += new CancelEventHandler(BatchView_Closing);
		}

		void BatchView_Closing(object sender, CancelEventArgs e)
		{
			if (_executing)
			{
				var result = MessageBox.Show(this,"Batch is being executed. Do you want to abort?",
				                             "Abort Batch?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					AbortBatch();
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		private void AbortBatch()
		{
			if (_batchThread != null && _executing)
			{
				_batchThread.Abort();
				_executing = false;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnStartBatch_Click(object sender, EventArgs e)
		{
			_executing = true;
			btnStartBatch.Enabled = false;

			progressBar1.Minimum = 0;
			progressBar1.Maximum = _items.Count;

			var stuffbatch = new StuffBatch() {items = _items.ToList()};
			Int32.TryParse(tbDelay.Text, out stuffbatch.delay);

			rtbStatus.Text = String.Empty;

			_batchThread = new Thread(ExecuteBatch);
			_batchThread.Start(stuffbatch);
		}

		private void ExecuteBatch(object batch)
		{
			var stuffBatch = batch as StuffBatch;
			if (stuffBatch == null)
				return;

			int i = 1;
			foreach (var item in stuffBatch.items)
			{
				try
				{
					IMessageStuffer messageStuffer = new MessageStuffer(item.SelectedType);
					VisualSerializer visualSerializer = new VisualSerializer(item.SelectedType);

					var messages = visualSerializer.GetDeserializedType(item.MessageContent);
					messageStuffer.StuffMessagesToQueue(item.QueueName, messages);

					SetStatus(item, i, null);

				}
				catch (Exception e)
				{
					SetStatus(item, i, e);
				}

				if (stuffBatch.delay > 0)
					Thread.Sleep(1000 * stuffBatch.delay);

				i++;
			}

			ReportBatchFinished();
		}

		private delegate void ReportBatchFinishedDelegate();
		private void ReportBatchFinished()
		{
			if (InvokeRequired)
			{
				ReportBatchFinishedDelegate del = ReportBatchFinished;
				Invoke(del);
			}
			else
			{
				progressBar1.Text = "Batch execution finished!";
				_executing = false;
				btnStartBatch.Enabled = true;
			}
		}

		private delegate void SetStatusDelegate(NqsSettingItem item, int position, Exception error);
		private void SetStatus(NqsSettingItem  item, int posistion, Exception error)
		{
			if (InvokeRequired)
			{
				SetStatusDelegate del = SetStatus;
				Invoke(del, item, posistion, error);
			}
			else
			{
				progressBar1.Value = posistion;
				if (rtbStatus.TextLength == 0)
				{
					rtbStatus.AppendText("Batch Results:\r\n");
				}
				if (error == null)
				{
					SetMessage(String.Format("Message of type {0} has been stuffed into {1} queue \r\n", 
													item.SelectedTypeName,
													item.QueueName), Color.Green);
				}
				else
				{
					SetMessage(String.Format("Error stuffing message of type {0} into {1} queue! \r\n", 
													item.SelectedTypeName,
													item.QueueName), Color.Red);
				}

			}
		}

		private void SetMessage(String message, Color color)
		{
			int start = rtbStatus.TextLength - 1;
			rtbStatus.AppendText(message);
			int length = rtbStatus.TextLength - start;
			rtbStatus.Select(start, length);
			rtbStatus.SelectionColor = color;
		}

		protected class StuffBatch
		{
			public int delay = 0;
			public IList<NqsSettingItem> items;
		}
	}
}
