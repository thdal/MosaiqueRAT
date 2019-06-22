namespace Serveur.Views
{
    partial class FrmRemoteShell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteShell));
            this.txtConsoleOutput = new System.Windows.Forms.RichTextBox();
            this.txtConsoleInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtConsoleOutput
            // 
            this.txtConsoleOutput.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtConsoleOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsoleOutput.Location = new System.Drawing.Point(1, 2);
            this.txtConsoleOutput.Name = "txtConsoleOutput";
            this.txtConsoleOutput.Size = new System.Drawing.Size(638, 275);
            this.txtConsoleOutput.TabIndex = 2;
            this.txtConsoleOutput.Text = "";
            this.txtConsoleOutput.TextChanged += new System.EventHandler(this.txtConsoleOutput_TextChanged);
            // 
            // txtConsoleInput
            // 
            this.txtConsoleInput.BackColor = System.Drawing.Color.Black;
            this.txtConsoleInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsoleInput.ForeColor = System.Drawing.Color.White;
            this.txtConsoleInput.Location = new System.Drawing.Point(1, 283);
            this.txtConsoleInput.Name = "txtConsoleInput";
            this.txtConsoleInput.Size = new System.Drawing.Size(638, 16);
            this.txtConsoleInput.TabIndex = 3;
            this.txtConsoleInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConsoleInput_KeyDown);
            this.txtConsoleInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsoleInput_KeyPress);
            // 
            // FrmRemoteShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(638, 301);
            this.Controls.Add(this.txtConsoleInput);
            this.Controls.Add(this.txtConsoleOutput);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRemoteShell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remote Shell";
            this.Load += new System.EventHandler(this.FrmRemoteShell_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox txtConsoleOutput;
        public System.Windows.Forms.TextBox txtConsoleInput;
    }
}