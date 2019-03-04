namespace Serveur.Views
{
    partial class FrmSystemInformation
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
            this.lvSysInfo = new System.Windows.Forms.ListView();
            this.colCpt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvSysInfo
            // 
            this.lvSysInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCpt,
            this.colVal});
            this.lvSysInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSysInfo.FullRowSelect = true;
            this.lvSysInfo.GridLines = true;
            this.lvSysInfo.Location = new System.Drawing.Point(0, 0);
            this.lvSysInfo.Name = "lvSysInfo";
            this.lvSysInfo.Size = new System.Drawing.Size(468, 345);
            this.lvSysInfo.TabIndex = 0;
            this.lvSysInfo.UseCompatibleStateImageBehavior = false;
            this.lvSysInfo.View = System.Windows.Forms.View.Details;
            // 
            // colCpt
            // 
            this.colCpt.Text = "Component";
            this.colCpt.Width = 187;
            // 
            // colVal
            // 
            this.colVal.Text = "Value";
            this.colVal.Width = 239;
            // 
            // FrmSystemInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 345);
            this.Controls.Add(this.lvSysInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmSystemInformation";
            this.Text = "FrmSystemInformation";
            this.Load += new System.EventHandler(this.FrmSystemInformation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSysInfo;
        private System.Windows.Forms.ColumnHeader colCpt;
        private System.Windows.Forms.ColumnHeader colVal;
    }
}