namespace Serveur.Views
{
    partial class FrmRemoteWebcam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteWebcam));
            this.tsFrmWbc = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.cboWebcams = new System.Windows.Forms.ToolStripComboBox();
            this.cboResolutions = new System.Windows.Forms.ToolStripComboBox();
            this.pbWebcam = new System.Windows.Forms.PictureBox();
            this.tsFrmWbc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebcam)).BeginInit();
            this.SuspendLayout();
            // 
            // tsFrmWbc
            // 
            this.tsFrmWbc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop,
            this.cboWebcams,
            this.cboResolutions});
            this.tsFrmWbc.Location = new System.Drawing.Point(0, 0);
            this.tsFrmWbc.Name = "tsFrmWbc";
            this.tsFrmWbc.Size = new System.Drawing.Size(635, 25);
            this.tsFrmWbc.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(35, 22);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(35, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cboWebcams
            // 
            this.cboWebcams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWebcams.Name = "cboWebcams";
            this.cboWebcams.Size = new System.Drawing.Size(121, 25);
            this.cboWebcams.SelectedIndexChanged += new System.EventHandler(this.cboWebcams_SelectedIndexChanged);
            // 
            // cboResolutions
            // 
            this.cboResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResolutions.Name = "cboResolutions";
            this.cboResolutions.Size = new System.Drawing.Size(121, 25);
            // 
            // pbWebcam
            // 
            this.pbWebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbWebcam.Location = new System.Drawing.Point(0, 25);
            this.pbWebcam.Name = "pbWebcam";
            this.pbWebcam.Size = new System.Drawing.Size(635, 289);
            this.pbWebcam.TabIndex = 1;
            this.pbWebcam.TabStop = false;
            // 
            // FrmRemoteWebcam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 314);
            this.Controls.Add(this.pbWebcam);
            this.Controls.Add(this.tsFrmWbc);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmRemoteWebcam";
            this.Text = "Remote Webcam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRemoteWebcam_FormClosing);
            this.Load += new System.EventHandler(this.FrmRemoteWebcam_Load);
            this.tsFrmWbc.ResumeLayout(false);
            this.tsFrmWbc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebcam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsFrmWbc;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripComboBox cboWebcams;
        private System.Windows.Forms.ToolStripComboBox cboResolutions;
        private System.Windows.Forms.PictureBox pbWebcam;
    }
}