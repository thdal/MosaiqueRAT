namespace Serveur.Views
{
    partial class FrmPasswordRecovery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPasswordRecovery));
            this.lvPasswords = new System.Windows.Forms.ListView();
            this.colIdentification = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsPr = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbRecoveredAcc = new System.Windows.Forms.GroupBox();
            this.cmsPr.SuspendLayout();
            this.gbRecoveredAcc.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPasswords
            // 
            this.lvPasswords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPasswords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdentification,
            this.colURL,
            this.colUser,
            this.colPass});
            this.lvPasswords.ContextMenuStrip = this.cmsPr;
            this.lvPasswords.FullRowSelect = true;
            this.lvPasswords.GridLines = true;
            this.lvPasswords.Location = new System.Drawing.Point(7, 22);
            this.lvPasswords.Name = "lvPasswords";
            this.lvPasswords.Size = new System.Drawing.Size(629, 335);
            this.lvPasswords.TabIndex = 0;
            this.lvPasswords.UseCompatibleStateImageBehavior = false;
            this.lvPasswords.View = System.Windows.Forms.View.Details;
            // 
            // colIdentification
            // 
            this.colIdentification.Text = "Identification";
            this.colIdentification.Width = 124;
            // 
            // colURL
            // 
            this.colURL.Width = 135;
            // 
            // colUser
            // 
            this.colUser.Width = 134;
            // 
            // colPass
            // 
            this.colPass.Text = "Identification";
            this.colPass.Width = 142;
            // 
            // cmsPr
            // 
            this.cmsPr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToFileToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.cmsPr.Name = "cmsPr";
            this.cmsPr.Size = new System.Drawing.Size(170, 98);
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllToolStripMenuItem,
            this.saveSelectedToolStripMenuItem});
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveToFileToolStripMenuItem.Text = "Save to file";
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.saveAllToolStripMenuItem.Text = "All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // saveSelectedToolStripMenuItem
            // 
            this.saveSelectedToolStripMenuItem.Name = "saveSelectedToolStripMenuItem";
            this.saveSelectedToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.saveSelectedToolStripMenuItem.Text = "Selected";
            this.saveSelectedToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAllToolStripMenuItem,
            this.copySelectedToolStripMenuItem});
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.copyAllToolStripMenuItem.Text = "All";
            // 
            // copySelectedToolStripMenuItem
            // 
            this.copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
            this.copySelectedToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.copySelectedToolStripMenuItem.Text = "Selected";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // gbRecoveredAcc
            // 
            this.gbRecoveredAcc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecoveredAcc.Controls.Add(this.lvPasswords);
            this.gbRecoveredAcc.Location = new System.Drawing.Point(14, 14);
            this.gbRecoveredAcc.Name = "gbRecoveredAcc";
            this.gbRecoveredAcc.Size = new System.Drawing.Size(644, 365);
            this.gbRecoveredAcc.TabIndex = 3;
            this.gbRecoveredAcc.TabStop = false;
            this.gbRecoveredAcc.Text = "Recovered Account";
            // 
            // FrmPasswordRecovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(672, 392);
            this.Controls.Add(this.gbRecoveredAcc);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPasswordRecovery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password Recovery";
            this.Load += new System.EventHandler(this.FrmPasswordRecovery_Load);
            this.cmsPr.ResumeLayout(false);
            this.gbRecoveredAcc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPasswords;
        private System.Windows.Forms.ColumnHeader colIdentification;
        private System.Windows.Forms.ColumnHeader colURL;
        private System.Windows.Forms.ColumnHeader colUser;
        private System.Windows.Forms.ColumnHeader colPass;
        private System.Windows.Forms.GroupBox gbRecoveredAcc;
        private System.Windows.Forms.ContextMenuStrip cmsPr;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}