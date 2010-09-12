using System;
using System.Windows.Forms;
using NQueueStuffer.UI.Controller;

namespace NQueueStuffer.UI.View
{
    public partial class QueueStufferView : Form, IQueueStufferView 
    {
        private IQueueStufferController _controller; 
       

        public QueueStufferView(IQueueStufferController controller, string defaultQueueName)
        {
            InitializeComponent();
            tbQueueName.Text = defaultQueueName;
            openFileDialog1.Filter = "Dll files (*.dll)|*.dll";
            _controller = controller; 
            _controller.Initialize(this);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;
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

        public void SetMessagesTypes(Type[] types)
        {
            listBoxMessageTypes.DataSource = types;
            listBoxMessageTypes.DisplayMember = "Name";
        }

        public void ShowSerializedMessageType(string serializedMessageType, Type messageType)
        {
            tbMessage.Text = serializedMessageType;
            SetStatus(string.Format("Message of type {0} has been loaded.", messageType.Name));
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
            
            if(filenames != null && filenames.Length > 0)
            {
                _controller.GetTypesFromAssembly(filenames[0]);
            }
        }
    }
}
