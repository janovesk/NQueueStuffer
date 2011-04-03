namespace NQueueStuffer.UI.View
{
    partial class QueueStufferView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tbMessage = new System.Windows.Forms.RichTextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.tbQueueName = new System.Windows.Forms.TextBox();
			this.lblQueueName = new System.Windows.Forms.Label();
			this.btnLoad = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.listBoxMessageTypes = new System.Windows.Forms.ListBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbMessage
			// 
			this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbMessage.Location = new System.Drawing.Point(26, 260);
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.Size = new System.Drawing.Size(426, 314);
			this.tbMessage.TabIndex = 0;
			this.tbMessage.Text = "";
			this.tbMessage.TextChanged += new System.EventHandler(this.tbMessage_TextChanged);
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Location = new System.Drawing.Point(307, 591);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(145, 23);
			this.btnSend.TabIndex = 1;
			this.btnSend.Text = "Stuff message in queue";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// tbQueueName
			// 
			this.tbQueueName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbQueueName.Location = new System.Drawing.Point(96, 593);
			this.tbQueueName.Name = "tbQueueName";
			this.tbQueueName.Size = new System.Drawing.Size(195, 20);
			this.tbQueueName.TabIndex = 2;
			this.tbQueueName.Text = "error";
			// 
			// lblQueueName
			// 
			this.lblQueueName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblQueueName.AutoSize = true;
			this.lblQueueName.Location = new System.Drawing.Point(26, 596);
			this.lblQueueName.Name = "lblQueueName";
			this.lblQueueName.Size = new System.Drawing.Size(68, 13);
			this.lblQueueName.TabIndex = 3;
			this.lblQueueName.Text = "Queue name";
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(26, 22);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(178, 23);
			this.btnLoad.TabIndex = 4;
			this.btnLoad.Text = "Load assembly with messagetypes";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// listBoxMessageTypes
			// 
			this.listBoxMessageTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxMessageTypes.FormattingEnabled = true;
			this.listBoxMessageTypes.Location = new System.Drawing.Point(29, 52);
			this.listBoxMessageTypes.Name = "listBoxMessageTypes";
			this.listBoxMessageTypes.Size = new System.Drawing.Size(423, 186);
			this.listBoxMessageTypes.Sorted = true;
			this.listBoxMessageTypes.TabIndex = 5;
			this.listBoxMessageTypes.SelectedIndexChanged += new System.EventHandler(this.listBoxMessageTypes_SelectedIndexChanged);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(26, 633);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 6;
			// 
			// QueueStufferView
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(478, 655);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.listBoxMessageTypes);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.lblQueueName);
			this.Controls.Add(this.tbQueueName);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.tbMessage);
			this.Name = "QueueStufferView";
			this.Text = "NQueueStuffer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.QueueStufferView_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.QueueStufferView_DragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tbMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbQueueName;
        private System.Windows.Forms.Label lblQueueName;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox listBoxMessageTypes;
        private System.Windows.Forms.Label lblStatus;
    }
}

