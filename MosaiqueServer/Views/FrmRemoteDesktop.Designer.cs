namespace Serveur.Views
{
    partial class FrmRemoteDesktop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteDesktop));
            this.tsRdp = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.cbMonitors = new System.Windows.Forms.ToolStripComboBox();
            this.pbRdp = new System.Windows.Forms.PictureBox();
            this.tsRdp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRdp)).BeginInit();
            this.SuspendLayout();
            // 
            // tsRdp
            // 
            this.tsRdp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop,
            this.cbMonitors});
            this.tsRdp.Location = new System.Drawing.Point(0, 0);
            this.tsRdp.Name = "tsRdp";
            this.tsRdp.Size = new System.Drawing.Size(477, 25);
            this.tsRdp.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(35, 22);
            this.btnStart.Text = "Start";
            this.btnStart.ToolTipText = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(35, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cbMonitors
            // 
            this.cbMonitors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonitors.Name = "cbMonitors";
            this.cbMonitors.Size = new System.Drawing.Size(121, 25);
            // 
            // pbRdp
            // 
            this.pbRdp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbRdp.Location = new System.Drawing.Point(0, 25);
            this.pbRdp.Name = "pbRdp";
            this.pbRdp.Size = new System.Drawing.Size(477, 276);
            this.pbRdp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRdp.TabIndex = 1;
            this.pbRdp.TabStop = false;
            // 
            // FrmRemoteDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 301);
            this.Controls.Add(this.pbRdp);
            this.Controls.Add(this.tsRdp);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRemoteDesktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mosaic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRemoteDesktop_FormClosing);
            this.Load += new System.EventHandler(this.FrmRemoteDesktop_Load);
            this.tsRdp.ResumeLayout(false);
            this.tsRdp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRdp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsRdp;
        private System.Windows.Forms.ToolStripComboBox cbMonitors;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.PictureBox pbRdp;
    }
}