namespace Client.Views
{
    partial class FrmRemoteChat
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
            this.txtWrite = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.rtxtChat = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtWrite
            // 
            this.txtWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWrite.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWrite.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtWrite.Location = new System.Drawing.Point(12, 230);
            this.txtWrite.Multiline = true;
            this.txtWrite.Name = "txtWrite";
            this.txtWrite.Size = new System.Drawing.Size(445, 75);
            this.txtWrite.TabIndex = 1;
            this.txtWrite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWrite_KeyDown);
            this.txtWrite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWrite_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(382, 311);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtxtChat
            // 
            this.rtxtChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtChat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rtxtChat.Location = new System.Drawing.Point(12, 12);
            this.rtxtChat.Name = "rtxtChat";
            this.rtxtChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.rtxtChat.Size = new System.Drawing.Size(445, 212);
            this.rtxtChat.TabIndex = 4;
            this.rtxtChat.Text = "";
            // 
            // FrmRemoteChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 346);
            this.Controls.Add(this.rtxtChat);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtWrite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmRemoteChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRemoteChat_FormClosing);
            this.Load += new System.EventHandler(this.FrmRemoteChat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtWrite;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox rtxtChat;
    }
}