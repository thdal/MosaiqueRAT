namespace Serveur.Views
{
    partial class FrmKeyLogger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKeyLogger));
            this.lvLogs = new System.Windows.Forms.ListView();
            this.colLogs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGetLogs = new System.Windows.Forms.Button();
            this.wbLogViewver = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // lvLogs
            // 
            this.lvLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogs});
            this.lvLogs.FullRowSelect = true;
            this.lvLogs.GridLines = true;
            this.lvLogs.Location = new System.Drawing.Point(12, 41);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(121, 397);
            this.lvLogs.TabIndex = 0;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            this.lvLogs.View = System.Windows.Forms.View.Details;
            this.lvLogs.ItemActivate += new System.EventHandler(this.lvLogs_ItemActivate);
            // 
            // colLogs
            // 
            this.colLogs.Text = "Logs";
            this.colLogs.Width = 117;
            // 
            // btnGetLogs
            // 
            this.btnGetLogs.Location = new System.Drawing.Point(12, 12);
            this.btnGetLogs.Name = "btnGetLogs";
            this.btnGetLogs.Size = new System.Drawing.Size(121, 23);
            this.btnGetLogs.TabIndex = 1;
            this.btnGetLogs.Text = "Get Logs";
            this.btnGetLogs.UseVisualStyleBackColor = true;
            this.btnGetLogs.Click += new System.EventHandler(this.btnGetLogs_Click);
            // 
            // wbLogViewver
            // 
            this.wbLogViewver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbLogViewver.Location = new System.Drawing.Point(139, 41);
            this.wbLogViewver.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLogViewver.Name = "wbLogViewver";
            this.wbLogViewver.Size = new System.Drawing.Size(649, 397);
            this.wbLogViewver.TabIndex = 2;
            // 
            // FrmKeyLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wbLogViewver);
            this.Controls.Add(this.btnGetLogs);
            this.Controls.Add(this.lvLogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmKeyLogger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KeyLogger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKeylogger_FormClosing);
            this.Load += new System.EventHandler(this.FrmKeylogger_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.Button btnGetLogs;
        private System.Windows.Forms.WebBrowser wbLogViewver;
        private System.Windows.Forms.ColumnHeader colLogs;
    }
}