namespace MosaiqueServeur.Views
{
    partial class FrmRegValueEditString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegValueEditString));
            this.label1 = new System.Windows.Forms.Label();
            this.valueNameTxtBox = new System.Windows.Forms.TextBox();
            this.valueDataTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value name:";
            // 
            // valueNameTxtBox
            // 
            this.valueNameTxtBox.BackColor = System.Drawing.SystemColors.Window;
            this.valueNameTxtBox.Location = new System.Drawing.Point(13, 26);
            this.valueNameTxtBox.Name = "valueNameTxtBox";
            this.valueNameTxtBox.Size = new System.Drawing.Size(341, 20);
            this.valueNameTxtBox.TabIndex = 1;
            // 
            // valueDataTxtBox
            // 
            this.valueDataTxtBox.Location = new System.Drawing.Point(13, 70);
            this.valueDataTxtBox.Name = "valueDataTxtBox";
            this.valueDataTxtBox.Size = new System.Drawing.Size(341, 20);
            this.valueDataTxtBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value data:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(279, 96);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(198, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.okButton_Click);
            // 
            // FrmRegValueEditString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(366, 129);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.valueDataTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.valueNameTxtBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRegValueEditString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit String";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox valueNameTxtBox;
        private System.Windows.Forms.TextBox valueDataTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button2;
    }
}