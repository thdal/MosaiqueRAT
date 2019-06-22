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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIntero = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnCross = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnDanger = new System.Windows.Forms.Button();
            this.lblTitleForm = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Location = new System.Drawing.Point(12, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(451, 61);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Title";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(7, 22);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(433, 23);
            this.txtTitle.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMsg);
            this.groupBox3.Location = new System.Drawing.Point(12, 227);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(451, 236);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(7, 22);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(433, 204);
            this.txtMsg.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(342, 469);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(121, 27);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnIntero);
            this.groupBox1.Controls.Add(this.btnInfo);
            this.groupBox1.Controls.Add(this.btnCross);
            this.groupBox1.Controls.Add(this.btnNone);
            this.groupBox1.Controls.Add(this.btnDanger);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Icon";
            // 
            // btnIntero
            // 
            this.btnIntero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntero.Image = ((System.Drawing.Image)(resources.GetObject("btnIntero.Image")));
            this.btnIntero.Location = new System.Drawing.Point(273, 22);
            this.btnIntero.Name = "btnIntero";
            this.btnIntero.Size = new System.Drawing.Size(80, 75);
            this.btnIntero.TabIndex = 4;
            this.btnIntero.UseVisualStyleBackColor = true;
            this.btnIntero.Click += new System.EventHandler(this.btnIntero_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.Location = new System.Drawing.Point(360, 22);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(80, 75);
            this.btnInfo.TabIndex = 3;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnCross
            // 
            this.btnCross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCross.Image = ((System.Drawing.Image)(resources.GetObject("btnCross.Image")));
            this.btnCross.Location = new System.Drawing.Point(98, 22);
            this.btnCross.Name = "btnCross";
            this.btnCross.Size = new System.Drawing.Size(80, 75);
            this.btnCross.TabIndex = 2;
            this.btnCross.UseVisualStyleBackColor = true;
            this.btnCross.Click += new System.EventHandler(this.btnCross_Click);
            // 
            // btnNone
            // 
            this.btnNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNone.Image = ((System.Drawing.Image)(resources.GetObject("btnNone.Image")));
            this.btnNone.Location = new System.Drawing.Point(185, 22);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(80, 75);
            this.btnNone.TabIndex = 1;
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnDanger
            // 
            this.btnDanger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanger.Image = ((System.Drawing.Image)(resources.GetObject("btnDanger.Image")));
            this.btnDanger.Location = new System.Drawing.Point(7, 22);
            this.btnDanger.Name = "btnDanger";
            this.btnDanger.Size = new System.Drawing.Size(80, 75);
            this.btnDanger.TabIndex = 0;
            this.btnDanger.UseVisualStyleBackColor = true;
            this.btnDanger.Click += new System.EventHandler(this.btnDanger_Click);
            // 
            // lblTitleForm
            // 
            this.lblTitleForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitleForm.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitleForm.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblTitleForm.Location = new System.Drawing.Point(0, 0);
            this.lblTitleForm.Name = "lblTitleForm";
            this.lblTitleForm.Size = new System.Drawing.Size(475, 40);
            this.lblTitleForm.TabIndex = 3;
            this.lblTitleForm.Text = "Create your own message box";
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSendMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(475, 508);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblTitleForm);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSendMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Message Box";
            this.Load += new System.EventHandler(this.FrmSendMessageBox_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnIntero;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnCross;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnDanger;
        private System.Windows.Forms.Label lblTitleForm;
    }
}