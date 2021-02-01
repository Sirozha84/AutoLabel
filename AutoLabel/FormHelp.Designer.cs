namespace AutoLabel
{
    partial class FormHelp
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textSetup = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textTips = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textHistory = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 561);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textSetup);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Настройка";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textSetup
            // 
            this.textSetup.BackColor = System.Drawing.Color.White;
            this.textSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textSetup.Location = new System.Drawing.Point(3, 3);
            this.textSetup.Name = "textSetup";
            this.textSetup.ReadOnly = true;
            this.textSetup.Size = new System.Drawing.Size(770, 517);
            this.textSetup.TabIndex = 1;
            this.textSetup.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textTips);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 523);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Подсказки";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textTips
            // 
            this.textTips.BackColor = System.Drawing.Color.White;
            this.textTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textTips.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textTips.Location = new System.Drawing.Point(3, 3);
            this.textTips.Name = "textTips";
            this.textTips.ReadOnly = true;
            this.textTips.Size = new System.Drawing.Size(770, 517);
            this.textTips.TabIndex = 2;
            this.textTips.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textHistory);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Список изменений";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textHistory
            // 
            this.textHistory.BackColor = System.Drawing.Color.White;
            this.textHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textHistory.Location = new System.Drawing.Point(3, 3);
            this.textHistory.Name = "textHistory";
            this.textHistory.ReadOnly = true;
            this.textHistory.Size = new System.Drawing.Size(770, 517);
            this.textHistory.TabIndex = 2;
            this.textHistory.Text = "";
            // 
            // FormHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormHelp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справка";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox textSetup;
        private System.Windows.Forms.RichTextBox textHistory;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox textTips;
    }
}