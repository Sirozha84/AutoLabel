namespace AutoLabel
{
    partial class FormUsers
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
            this.labelformname = new System.Windows.Forms.Label();
            this.buttonquit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonsave = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonPageUp = new System.Windows.Forms.Button();
            this.buttonPageDown = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonRules = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelformname
            // 
            this.labelformname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelformname.AutoSize = true;
            this.labelformname.BackColor = System.Drawing.Color.Navy;
            this.labelformname.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelformname.ForeColor = System.Drawing.Color.White;
            this.labelformname.Location = new System.Drawing.Point(268, 15);
            this.labelformname.Name = "labelformname";
            this.labelformname.Size = new System.Drawing.Size(460, 73);
            this.labelformname.TabIndex = 7;
            this.labelformname.Text = "Пользователи";
            // 
            // buttonquit
            // 
            this.buttonquit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonquit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonquit.ForeColor = System.Drawing.Color.White;
            this.buttonquit.Location = new System.Drawing.Point(12, 12);
            this.buttonquit.Name = "buttonquit";
            this.buttonquit.Size = new System.Drawing.Size(250, 76);
            this.buttonquit.TabIndex = 2;
            this.buttonquit.Text = "< Назад";
            this.buttonquit.UseVisualStyleBackColor = true;
            this.buttonquit.Click += new System.EventHandler(this.buttonquit_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Navy;
            this.panel2.Controls.Add(this.buttonsave);
            this.panel2.Controls.Add(this.labelformname);
            this.panel2.Controls.Add(this.buttonquit);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(956, 100);
            this.panel2.TabIndex = 10;
            // 
            // buttonsave
            // 
            this.buttonsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonsave.ForeColor = System.Drawing.Color.White;
            this.buttonsave.Location = new System.Drawing.Point(690, 12);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(250, 76);
            this.buttonsave.TabIndex = 8;
            this.buttonsave.Text = "Сохранить";
            this.buttonsave.UseVisualStyleBackColor = true;
            this.buttonsave.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.Color.Black;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 55;
            this.listBox1.Items.AddRange(new object[] {
            ""});
            this.listBox1.Location = new System.Drawing.Point(73, 146);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(731, 279);
            this.listBox1.TabIndex = 11;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // buttonPageUp
            // 
            this.buttonPageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPageUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPageUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPageUp.ForeColor = System.Drawing.Color.White;
            this.buttonPageUp.Location = new System.Drawing.Point(810, 146);
            this.buttonPageUp.Name = "buttonPageUp";
            this.buttonPageUp.Size = new System.Drawing.Size(76, 76);
            this.buttonPageUp.TabIndex = 12;
            this.buttonPageUp.Text = "<<<";
            this.buttonPageUp.UseVisualStyleBackColor = true;
            // 
            // buttonPageDown
            // 
            this.buttonPageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPageDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPageDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPageDown.ForeColor = System.Drawing.Color.White;
            this.buttonPageDown.Location = new System.Drawing.Point(810, 228);
            this.buttonPageDown.Name = "buttonPageDown";
            this.buttonPageDown.Size = new System.Drawing.Size(76, 76);
            this.buttonPageDown.TabIndex = 13;
            this.buttonPageDown.Text = ">>>";
            this.buttonPageDown.UseVisualStyleBackColor = true;
            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNew.ForeColor = System.Drawing.Color.White;
            this.buttonNew.Location = new System.Drawing.Point(73, 473);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(250, 76);
            this.buttonNew.TabIndex = 14;
            this.buttonNew.Text = "Новый";
            this.buttonNew.UseVisualStyleBackColor = true;
            // 
            // buttonRules
            // 
            this.buttonRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRules.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRules.ForeColor = System.Drawing.Color.White;
            this.buttonRules.Location = new System.Drawing.Point(329, 473);
            this.buttonRules.Name = "buttonRules";
            this.buttonRules.Size = new System.Drawing.Size(250, 76);
            this.buttonRules.TabIndex = 15;
            this.buttonRules.Text = "Доступ";
            this.buttonRules.UseVisualStyleBackColor = true;
            this.buttonRules.Visible = false;
            this.buttonRules.Click += new System.EventHandler(this.buttonRules_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(585, 473);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(250, 76);
            this.buttonDelete.TabIndex = 16;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Visible = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // FormUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(954, 636);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonRules);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.buttonPageDown);
            this.Controls.Add(this.buttonPageUp);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormUsers";
            this.Text = "FormUsers";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormUsers_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelformname;
        private System.Windows.Forms.Button buttonquit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonsave;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonPageUp;
        private System.Windows.Forms.Button buttonPageDown;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonRules;
        private System.Windows.Forms.Button buttonDelete;
    }
}