namespace Serveur.Views
{
    partial class FrmSendMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSendMessageBox));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pbDanger = new System.Windows.Forms.PictureBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pbCross = new System.Windows.Forms.PictureBox();
            this.pbIntero = new System.Windows.Forms.PictureBox();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.pbNone = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDanger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIntero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNone)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbNone);
            this.groupBox1.Controls.Add(this.pbCross);
            this.groupBox1.Controls.Add(this.pbInfo);
            this.groupBox1.Controls.Add(this.pbIntero);
            this.groupBox1.Controls.Add(this.pbDanger);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Icon";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Location = new System.Drawing.Point(12, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 52);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Title";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtMsg);
            this.groupBox3.Location = new System.Drawing.Point(12, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 136);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message";
            // 
            // pbDanger
            // 
            this.pbDanger.Image = ((System.Drawing.Image)(resources.GetObject("pbDanger.Image")));
            this.pbDanger.Location = new System.Drawing.Point(12, 19);
            this.pbDanger.Name = "pbDanger";
            this.pbDanger.Size = new System.Drawing.Size(64, 64);
            this.pbDanger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDanger.TabIndex = 0;
            this.pbDanger.TabStop = false;
            this.pbDanger.Click += new System.EventHandler(this.pbDanger_Click);
            this.pbDanger.Paint += new System.Windows.Forms.PaintEventHandler(this.pbDanger_Paint);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(302, 315);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pbCross
            // 
            this.pbCross.Image = ((System.Drawing.Image)(resources.GetObject("pbCross.Image")));
            this.pbCross.Location = new System.Drawing.Point(292, 19);
            this.pbCross.Name = "pbCross";
            this.pbCross.Size = new System.Drawing.Size(64, 64);
            this.pbCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCross.TabIndex = 1;
            this.pbCross.TabStop = false;
            this.pbCross.Click += new System.EventHandler(this.pbCross_Click);
            this.pbCross.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCross_Paint);
            // 
            // pbIntero
            // 
            this.pbIntero.Image = ((System.Drawing.Image)(resources.GetObject("pbIntero.Image")));
            this.pbIntero.Location = new System.Drawing.Point(152, 19);
            this.pbIntero.Name = "pbIntero";
            this.pbIntero.Size = new System.Drawing.Size(64, 64);
            this.pbIntero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbIntero.TabIndex = 2;
            this.pbIntero.TabStop = false;
            this.pbIntero.Click += new System.EventHandler(this.pbIntero_Click);
            this.pbIntero.Paint += new System.Windows.Forms.PaintEventHandler(this.pbIntero_Paint);
            // 
            // pbInfo
            // 
            this.pbInfo.Image = ((System.Drawing.Image)(resources.GetObject("pbInfo.Image")));
            this.pbInfo.Location = new System.Drawing.Point(222, 19);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(64, 64);
            this.pbInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInfo.TabIndex = 3;
            this.pbInfo.TabStop = false;
            this.pbInfo.Click += new System.EventHandler(this.pbInfo_Click);
            this.pbInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.pbInfo_Paint);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(6, 19);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(353, 20);
            this.txtTitle.TabIndex = 0;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(6, 19);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(353, 111);
            this.txtMsg.TabIndex = 0;
            // 
            // pbNone
            // 
            this.pbNone.Image = ((System.Drawing.Image)(resources.GetObject("pbNone.Image")));
            this.pbNone.Location = new System.Drawing.Point(82, 19);
            this.pbNone.Name = "pbNone";
            this.pbNone.Size = new System.Drawing.Size(64, 64);
            this.pbNone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbNone.TabIndex = 4;
            this.pbNone.TabStop = false;
            this.pbNone.Click += new System.EventHandler(this.pbNone_Click);
            this.pbNone.Paint += new System.Windows.Forms.PaintEventHandler(this.pbNone_Paint);
            // 
            // FrmSendMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 350);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSendMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Message Box";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDanger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIntero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pbDanger;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox pbInfo;
        private System.Windows.Forms.PictureBox pbIntero;
        private System.Windows.Forms.PictureBox pbCross;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.PictureBox pbNone;
    }
}