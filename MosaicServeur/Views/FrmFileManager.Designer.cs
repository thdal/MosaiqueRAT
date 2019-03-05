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
            this.components = new System.ComponentModel.Container();
            this.tcFileManager = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvDirectory = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsDirectory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListDirectory = new System.Windows.Forms.ImageList(this.components);
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblRemotePath = new System.Windows.Forms.Label();
            this.cboDrives = new System.Windows.Forms.ComboBox();
            this.lblDrive = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDwnldFolder = new System.Windows.Forms.Button();
            this.lvTransfers = new System.Windows.Forms.ListView();
            this.colUniqID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTransferType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsTransfers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripLblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcFileManager.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.cmsDirectory.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cmsTransfers.SuspendLayout();
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
            this.tcFileManager.Size = new System.Drawing.Size(631, 389);
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
            this.tabPage1.Size = new System.Drawing.Size(623, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File Explorer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvDirectory
            // 
            this.lvDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDirectory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSize,
            this.colType});
            this.lvDirectory.ContextMenuStrip = this.cmsDirectory;
            this.lvDirectory.FullRowSelect = true;
            this.lvDirectory.GridLines = true;
            this.lvDirectory.Location = new System.Drawing.Point(3, 36);
            this.lvDirectory.Name = "lvDirectory";
            this.lvDirectory.Size = new System.Drawing.Size(614, 308);
            this.lvDirectory.SmallImageList = this.imgListDirectory;
            this.lvDirectory.TabIndex = 4;
            this.lvDirectory.UseCompatibleStateImageBehavior = false;
            this.lvDirectory.View = System.Windows.Forms.View.Details;
            this.lvDirectory.DoubleClick += new System.EventHandler(this.lvDirectory_DoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 165;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 114;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 253;
            // 
            // cmsDirectory
            // 
            this.cmsDirectory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem});
            this.cmsDirectory.Name = "cmsDirectory";
            this.cmsDirectory.Size = new System.Drawing.Size(129, 26);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // imgListDirectory
            // 
            this.imgListDirectory.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgListDirectory.ImageSize = new System.Drawing.Size(16, 16);
            this.imgListDirectory.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(340, 8);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(250, 22);
            this.txtPath.TabIndex = 3;
            this.txtPath.Text = "\\";
            // 
            // lblRemotePath
            // 
            this.lblRemotePath.AutoSize = true;
            this.lblRemotePath.Location = new System.Drawing.Point(256, 11);
            this.lblRemotePath.Name = "lblRemotePath";
            this.lblRemotePath.Size = new System.Drawing.Size(78, 13);
            this.lblRemotePath.TabIndex = 2;
            this.lblRemotePath.Text = "Remote Path :";
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
            // lblDrive
            // 
            this.lblDrive.AutoSize = true;
            this.lblDrive.Location = new System.Drawing.Point(6, 11);
            this.lblDrive.Name = "lblDrive";
            this.lblDrive.Size = new System.Drawing.Size(39, 13);
            this.lblDrive.TabIndex = 0;
            this.lblDrive.Text = "Drive :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDwnldFolder);
            this.tabPage2.Controls.Add(this.lvTransfers);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(623, 363);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transfers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDwnldFolder
            // 
            this.btnDwnldFolder.Location = new System.Drawing.Point(6, 334);
            this.btnDwnldFolder.Name = "btnDwnldFolder";
            this.btnDwnldFolder.Size = new System.Drawing.Size(135, 23);
            this.btnDwnldFolder.TabIndex = 1;
            this.btnDwnldFolder.Text = "Download Folder";
            this.btnDwnldFolder.UseVisualStyleBackColor = true;
            this.btnDwnldFolder.Click += new System.EventHandler(this.btnDwnldFolder_Click);
            // 
            // lvTransfers
            // 
            this.lvTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTransfers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUniqID,
            this.colTransferType,
            this.colStatus,
            this.colFilename});
            this.lvTransfers.ContextMenuStrip = this.cmsTransfers;
            this.lvTransfers.FullRowSelect = true;
            this.lvTransfers.GridLines = true;
            this.lvTransfers.Location = new System.Drawing.Point(0, 0);
            this.lvTransfers.Name = "lvTransfers";
            this.lvTransfers.Size = new System.Drawing.Size(623, 325);
            this.lvTransfers.TabIndex = 0;
            this.lvTransfers.UseCompatibleStateImageBehavior = false;
            this.lvTransfers.View = System.Windows.Forms.View.Details;
            // 
            // colUniqID
            // 
            this.colUniqID.Text = "ID";
            this.colUniqID.Width = 91;
            // 
            // colTransferType
            // 
            this.colTransferType.Text = "Transfer Type";
            this.colTransferType.Width = 95;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 158;
            // 
            // colFilename
            // 
            this.colFilename.Text = "FileName";
            this.colFilename.Width = 224;
            // 
            // cmsTransfers
            // 
            this.cmsTransfers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelToolStripMenuItem});
            this.cmsTransfers.Name = "cmsTransfers";
            this.cmsTransfers.Size = new System.Drawing.Size(111, 26);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripLblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 404);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(655, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusS";
            // 
            // stripLblStatus
            // 
            this.stripLblStatus.Name = "stripLblStatus";
            this.stripLblStatus.Size = new System.Drawing.Size(131, 17);
            this.stripLblStatus.Text = "Status: Loading drives...";
            // 
            // FrmFileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 426);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tcFileManager);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmFileManager";
            this.Text = "File Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFileManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmFileManager_Load);
            this.tcFileManager.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.cmsDirectory.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.cmsTransfers.ResumeLayout(false);
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
        private System.Windows.Forms.ImageList imgListDirectory;
        private System.Windows.Forms.ContextMenuStrip cmsDirectory;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ListView lvTransfers;
        private System.Windows.Forms.ColumnHeader colTransferType;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colFilename;
        private System.Windows.Forms.ContextMenuStrip cmsTransfers;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colUniqID;
        private System.Windows.Forms.Button btnDwnldFolder;
    }
}