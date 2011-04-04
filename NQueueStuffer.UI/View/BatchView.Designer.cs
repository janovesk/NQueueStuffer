namespace NQueueStuffer.UI.View
{
	partial class BatchView
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
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btnStartBatch = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tbDelay = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.rtbStatus = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(98, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(297, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please ensure you have edited message templates \r\nand queue names for all opened " +
    "windows!";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(15, 100);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(448, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// btnStartBatch
			// 
			this.btnStartBatch.Location = new System.Drawing.Point(122, 147);
			this.btnStartBatch.Name = "btnStartBatch";
			this.btnStartBatch.Size = new System.Drawing.Size(75, 23);
			this.btnStartBatch.TabIndex = 2;
			this.btnStartBatch.Text = "Start!";
			this.btnStartBatch.UseVisualStyleBackColor = true;
			this.btnStartBatch.Click += new System.EventHandler(this.btnStartBatch_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(264, 147);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(16, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(131, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Delay between messages:";
			// 
			// tbDelay
			// 
			this.tbDelay.Location = new System.Drawing.Point(153, 60);
			this.tbDelay.Name = "tbDelay";
			this.tbDelay.Size = new System.Drawing.Size(49, 20);
			this.tbDelay.TabIndex = 5;
			this.tbDelay.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(208, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "sec";
			// 
			// rtbStatus
			// 
			this.rtbStatus.Location = new System.Drawing.Point(15, 183);
			this.rtbStatus.Name = "rtbStatus";
			this.rtbStatus.Size = new System.Drawing.Size(448, 135);
			this.rtbStatus.TabIndex = 7;
			this.rtbStatus.Text = "";
			// 
			// BatchView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(475, 329);
			this.Controls.Add(this.rtbStatus);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbDelay);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStartBatch);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BatchView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Batch Message Stuffing";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button btnStartBatch;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbDelay;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox rtbStatus;
	}
}