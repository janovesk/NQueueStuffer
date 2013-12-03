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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueueStufferView));
            this.tbMessage = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbQueueName = new System.Windows.Forms.TextBox();
            this.lblQueueName = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBoxMessageTypes = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.checkLockSelection = new System.Windows.Forms.CheckBox();
            this.tbMessageTimer = new System.Windows.Forms.Timer(this.components);
            this.cmbQueueName = new System.Windows.Forms.ComboBox();
            this.btnRefreshQueues = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(26, 260);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(426, 322);
            this.tbMessage.TabIndex = 0;
            this.tbMessage.Text = "";
            this.tbMessage.TextChanged += new System.EventHandler(this.tbMessage_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(307, 585);
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
            this.tbQueueName.Location = new System.Drawing.Point(96, 582);
            this.tbQueueName.Name = "tbQueueName";
            this.tbQueueName.Size = new System.Drawing.Size(195, 20);
            this.tbQueueName.TabIndex = 2;
            this.tbQueueName.Text = "error";
            // 
            // lblQueueName
            // 
            this.lblQueueName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblQueueName.AutoSize = true;
            this.lblQueueName.Location = new System.Drawing.Point(26, 585);
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
            this.lblStatus.Location = new System.Drawing.Point(26, 641);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 6;
            // 
            // checkLockSelection
            // 
            this.checkLockSelection.AutoSize = true;
            this.checkLockSelection.Location = new System.Drawing.Point(225, 25);
            this.checkLockSelection.Name = "checkLockSelection";
            this.checkLockSelection.Size = new System.Drawing.Size(131, 17);
            this.checkLockSelection.TabIndex = 7;
            this.checkLockSelection.Text = "Lock current selection";
            this.checkLockSelection.UseVisualStyleBackColor = true;
            this.checkLockSelection.CheckStateChanged += new System.EventHandler(this.checkLockSelection_CheckStateChanged);
            // 
            // tbMessageTimer
            // 
            this.tbMessageTimer.Tick += new System.EventHandler(this.tbMessageTimer_Tick);
            // 
            // cmbQueueName
            // 
            this.cmbQueueName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbQueueName.FormattingEnabled = true;
            this.cmbQueueName.Location = new System.Drawing.Point(96, 608);
            this.cmbQueueName.Name = "cmbQueueName";
            this.cmbQueueName.Size = new System.Drawing.Size(195, 21);
            this.cmbQueueName.TabIndex = 8;
            this.cmbQueueName.SelectedIndexChanged += new System.EventHandler(this.cmbQueueName_SelectedIndexChanged);
            // 
            // btnRefreshQueues
            // 
            this.btnRefreshQueues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshQueues.Location = new System.Drawing.Point(307, 608);
            this.btnRefreshQueues.Name = "btnRefreshQueues";
            this.btnRefreshQueues.Size = new System.Drawing.Size(145, 23);
            this.btnRefreshQueues.TabIndex = 9;
            this.btnRefreshQueues.Text = "Refresh Queues";
            this.btnRefreshQueues.UseVisualStyleBackColor = true;
            this.btnRefreshQueues.Click += new System.EventHandler(this.btnRefreshQueues_Click);
            // 
            // QueueStufferView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 663);
            this.Controls.Add(this.btnRefreshQueues);
            this.Controls.Add(this.cmbQueueName);
            this.Controls.Add(this.checkLockSelection);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.listBoxMessageTypes);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblQueueName);
            this.Controls.Add(this.tbQueueName);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueueStufferView";
            this.ShowInTaskbar = false;
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
		private System.Windows.Forms.CheckBox checkLockSelection;
		private System.Windows.Forms.Timer tbMessageTimer;
        private System.Windows.Forms.ComboBox cmbQueueName;
        private System.Windows.Forms.Button btnRefreshQueues;
    }
}

