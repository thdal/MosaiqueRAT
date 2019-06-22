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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemInformation));
            this.lvSysInfo = new System.Windows.Forms.ListView();
            this.colCpt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvSysInfo
            // 
            this.lvSysInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSysInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCpt,
            this.colVal});
            this.lvSysInfo.FullRowSelect = true;
            this.lvSysInfo.GridLines = true;
            this.lvSysInfo.Location = new System.Drawing.Point(14, 14);
            this.lvSysInfo.Name = "lvSysInfo";
            this.lvSysInfo.Size = new System.Drawing.Size(573, 379);
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
            this.colVal.Width = 259;
            // 
            // FrmSystemInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(593, 407);
            this.Controls.Add(this.lvSysInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSystemInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "System Information";
            this.Load += new System.EventHandler(this.FrmSystemInformation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSysInfo;
        private System.Windows.Forms.ColumnHeader colCpt;
        private System.Windows.Forms.ColumnHeader colVal;
    }
}