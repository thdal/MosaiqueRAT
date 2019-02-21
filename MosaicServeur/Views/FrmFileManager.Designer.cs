namespace Serveur.Views
{
    partial class FrmFileManager
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
            this.tcFileManager = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblDrive = new System.Windows.Forms.Label();
            this.cboDrives = new System.Windows.Forms.ComboBox();
            this.lblRemotePath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripLblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lvDirectory = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcFileManager.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcFileManager
            // 
            this.tcFileManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcFileManager.Controls.Add(this.tabPage1);
            this.tcFileManager.Controls.Add(this.tabPage2);
            this.tcFileManager.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tcFileManager.Location = new System.Drawing.Point(12, 12);
            this.tcFileManager.Name = "tcFileManager";
            this.tcFileManager.SelectedIndex = 0;
            this.tcFileManager.Size = new System.Drawing.Size(776, 426);
            this.tcFileManager.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvDirectory);
            this.tabPage1.Controls.Add(this.txtPath);
            this.tabPage1.Controls.Add(this.lblRemotePath);
            this.tabPage1.Controls.Add(this.cboDrives);
            this.tabPage1.Controls.Add(this.lblDrive);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File Explorer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transfers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblDrive
            // 
            this.lblDrive.AutoSize = true;
            this.lblDrive.Location = new System.Drawing.Point(6, 11);
            this.lblDrive.Name = "lblDrive";
            this.lblDrive.Size = new System.Drawing.Size(39, 13);
            this.lblDrive.TabIndex = 0;
            this.lblDrive.Text = "Drive :";
            // 
            // cboDrives
            // 
            this.cboDrives.FormattingEnabled = true;
            this.cboDrives.Location = new System.Drawing.Point(51, 8);
            this.cboDrives.Name = "cboDrives";
            this.cboDrives.Size = new System.Drawing.Size(199, 21);
            this.cboDrives.TabIndex = 1;
            this.cboDrives.SelectedIndexChanged += new System.EventHandler(this.cboDrives_SelectedIndexChanged);
            // 
            // lblRemotePath
            // 
            this.lblRemotePath.AutoSize = true;
            this.lblRemotePath.Location = new System.Drawing.Point(279, 11);
            this.lblRemotePath.Name = "lblRemotePath";
            this.lblRemotePath.Size = new System.Drawing.Size(78, 13);
            this.lblRemotePath.TabIndex = 2;
            this.lblRemotePath.Text = "Remote Path :";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(363, 8);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(342, 22);
            this.txtPath.TabIndex = 3;
            this.txtPath.Text = "\\";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripLblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusS";
            // 
            // stripLblStatus
            // 
            this.stripLblStatus.Name = "stripLblStatus";
            this.stripLblStatus.Size = new System.Drawing.Size(131, 17);
            this.stripLblStatus.Text = "Status: Loading drives...";
            // 
            // lvDirectory
            // 
            this.lvDirectory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSize,
            this.colType});
            this.lvDirectory.FullRowSelect = true;
            this.lvDirectory.GridLines = true;
            this.lvDirectory.Location = new System.Drawing.Point(9, 36);
            this.lvDirectory.Name = "lvDirectory";
            this.lvDirectory.Size = new System.Drawing.Size(753, 355);
            this.lvDirectory.TabIndex = 4;
            this.lvDirectory.UseCompatibleStateImageBehavior = false;
            this.lvDirectory.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 203;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 251;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 420;
            // 
            // FrmFileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tcFileManager);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmFileManager";
            this.Text = "File Manager";
            this.Load += new System.EventHandler(this.FrmFileManager_Load);
            this.tcFileManager.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcFileManager;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblRemotePath;
        private System.Windows.Forms.ComboBox cboDrives;
        private System.Windows.Forms.Label lblDrive;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripLblStatus;
        private System.Windows.Forms.ListView lvDirectory;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colType;
    }
}