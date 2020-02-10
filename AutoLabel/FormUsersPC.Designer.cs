namespace AutoLabel
{
    partial class FormUsersPC
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkP1 = new System.Windows.Forms.CheckBox();
            this.checkP2 = new System.Windows.Forms.CheckBox();
            this.checkP3 = new System.Windows.Forms.CheckBox();
            this.checkP4 = new System.Windows.Forms.CheckBox();
            this.checkP5 = new System.Windows.Forms.CheckBox();
            this.checkP6 = new System.Windows.Forms.CheckBox();
            this.checkBoxOn = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonNewUser = new System.Windows.Forms.Button();
            this.buttonNewAdmin = new System.Windows.Forms.Button();
            this.buttonSetKey = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonKeyDel = new System.Windows.Forms.Button();
            this.checkK1 = new System.Windows.Forms.CheckBox();
            this.checkK2 = new System.Windows.Forms.CheckBox();
            this.checkR1 = new System.Windows.Forms.CheckBox();
            this.checkP7 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 41);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(436, 485);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Пользователь";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Права";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ключ";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Привязка к ТПА";
            this.columnHeader4.Width = 190;
            // 
            // checkP1
            // 
            this.checkP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP1.AutoSize = true;
            this.checkP1.Enabled = false;
            this.checkP1.Location = new System.Drawing.Point(457, 228);
            this.checkP1.Name = "checkP1";
            this.checkP1.Size = new System.Drawing.Size(89, 17);
            this.checkP1.TabIndex = 8;
            this.checkP1.Text = "Преформа 1";
            this.checkP1.UseVisualStyleBackColor = true;
            this.checkP1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkP2
            // 
            this.checkP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP2.AutoSize = true;
            this.checkP2.Enabled = false;
            this.checkP2.Location = new System.Drawing.Point(457, 251);
            this.checkP2.Name = "checkP2";
            this.checkP2.Size = new System.Drawing.Size(89, 17);
            this.checkP2.TabIndex = 9;
            this.checkP2.Text = "Преформа 2";
            this.checkP2.UseVisualStyleBackColor = true;
            this.checkP2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkP3
            // 
            this.checkP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP3.AutoSize = true;
            this.checkP3.Enabled = false;
            this.checkP3.Location = new System.Drawing.Point(457, 274);
            this.checkP3.Name = "checkP3";
            this.checkP3.Size = new System.Drawing.Size(89, 17);
            this.checkP3.TabIndex = 10;
            this.checkP3.Text = "Преформа 3";
            this.checkP3.UseVisualStyleBackColor = true;
            this.checkP3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkP4
            // 
            this.checkP4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP4.AutoSize = true;
            this.checkP4.Enabled = false;
            this.checkP4.Location = new System.Drawing.Point(457, 297);
            this.checkP4.Name = "checkP4";
            this.checkP4.Size = new System.Drawing.Size(89, 17);
            this.checkP4.TabIndex = 11;
            this.checkP4.Text = "Преформа 4";
            this.checkP4.UseVisualStyleBackColor = true;
            this.checkP4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkP5
            // 
            this.checkP5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP5.AutoSize = true;
            this.checkP5.Enabled = false;
            this.checkP5.Location = new System.Drawing.Point(457, 320);
            this.checkP5.Name = "checkP5";
            this.checkP5.Size = new System.Drawing.Size(89, 17);
            this.checkP5.TabIndex = 12;
            this.checkP5.Text = "Преформа 5";
            this.checkP5.UseVisualStyleBackColor = true;
            this.checkP5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkP6
            // 
            this.checkP6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP6.AutoSize = true;
            this.checkP6.Enabled = false;
            this.checkP6.Location = new System.Drawing.Point(457, 343);
            this.checkP6.Name = "checkP6";
            this.checkP6.Size = new System.Drawing.Size(89, 17);
            this.checkP6.TabIndex = 13;
            this.checkP6.Text = "Преформа 6";
            this.checkP6.UseVisualStyleBackColor = true;
            this.checkP6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBoxOn
            // 
            this.checkBoxOn.AutoSize = true;
            this.checkBoxOn.Location = new System.Drawing.Point(12, 12);
            this.checkBoxOn.Name = "checkBoxOn";
            this.checkBoxOn.Size = new System.Drawing.Size(285, 17);
            this.checkBoxOn.TabIndex = 0;
            this.checkBoxOn.Text = "Контроль привязки пользователей к ТПА включен";
            this.checkBoxOn.UseVisualStyleBackColor = true;
            this.checkBoxOn.CheckedChanged += new System.EventHandler(this.checkBoxOn_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(497, 538);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(416, 538);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 18;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.AutoSize = true;
            this.labelName.Enabled = false;
            this.labelName.Location = new System.Drawing.Point(454, 202);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(62, 13);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Не выбран";
            // 
            // buttonNewUser
            // 
            this.buttonNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewUser.Location = new System.Drawing.Point(457, 41);
            this.buttonNewUser.Name = "buttonNewUser";
            this.buttonNewUser.Size = new System.Drawing.Size(115, 23);
            this.buttonNewUser.TabIndex = 2;
            this.buttonNewUser.Text = "Новый упаковщик";
            this.buttonNewUser.UseVisualStyleBackColor = true;
            this.buttonNewUser.Click += new System.EventHandler(this.buttonNewUser_Click);
            // 
            // buttonNewAdmin
            // 
            this.buttonNewAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewAdmin.Location = new System.Drawing.Point(457, 70);
            this.buttonNewAdmin.Name = "buttonNewAdmin";
            this.buttonNewAdmin.Size = new System.Drawing.Size(115, 23);
            this.buttonNewAdmin.TabIndex = 3;
            this.buttonNewAdmin.Text = "Новый админ";
            this.buttonNewAdmin.UseVisualStyleBackColor = true;
            this.buttonNewAdmin.Click += new System.EventHandler(this.buttonNewAdmin_Click);
            // 
            // buttonSetKey
            // 
            this.buttonSetKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetKey.Enabled = false;
            this.buttonSetKey.Location = new System.Drawing.Point(457, 99);
            this.buttonSetKey.Name = "buttonSetKey";
            this.buttonSetKey.Size = new System.Drawing.Size(115, 23);
            this.buttonSetKey.TabIndex = 4;
            this.buttonSetKey.Text = "Изменить ключ";
            this.buttonSetKey.UseVisualStyleBackColor = true;
            this.buttonSetKey.Click += new System.EventHandler(this.buttonSetKey_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDel.Enabled = false;
            this.buttonDel.Location = new System.Drawing.Point(457, 157);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(115, 23);
            this.buttonDel.TabIndex = 6;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonKeyDel
            // 
            this.buttonKeyDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKeyDel.Enabled = false;
            this.buttonKeyDel.Location = new System.Drawing.Point(457, 128);
            this.buttonKeyDel.Name = "buttonKeyDel";
            this.buttonKeyDel.Size = new System.Drawing.Size(115, 23);
            this.buttonKeyDel.TabIndex = 5;
            this.buttonKeyDel.Text = "Изъять ключ";
            this.buttonKeyDel.UseVisualStyleBackColor = true;
            this.buttonKeyDel.Click += new System.EventHandler(this.buttonKeyDel_Click);
            // 
            // checkK1
            // 
            this.checkK1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkK1.AutoSize = true;
            this.checkK1.Enabled = false;
            this.checkK1.Location = new System.Drawing.Point(457, 399);
            this.checkK1.Name = "checkK1";
            this.checkK1.Size = new System.Drawing.Size(72, 17);
            this.checkK1.TabIndex = 15;
            this.checkK1.Text = "Колпак 1";
            this.checkK1.UseVisualStyleBackColor = true;
            this.checkK1.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // checkK2
            // 
            this.checkK2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkK2.AutoSize = true;
            this.checkK2.Enabled = false;
            this.checkK2.Location = new System.Drawing.Point(457, 422);
            this.checkK2.Name = "checkK2";
            this.checkK2.Size = new System.Drawing.Size(72, 17);
            this.checkK2.TabIndex = 16;
            this.checkK2.Text = "Колпак 2";
            this.checkK2.UseVisualStyleBackColor = true;
            this.checkK2.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // checkR1
            // 
            this.checkR1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkR1.AutoSize = true;
            this.checkR1.Enabled = false;
            this.checkR1.Location = new System.Drawing.Point(457, 455);
            this.checkR1.Name = "checkR1";
            this.checkR1.Size = new System.Drawing.Size(79, 17);
            this.checkR1.TabIndex = 17;
            this.checkR1.Text = "Ротопринт";
            this.checkR1.UseVisualStyleBackColor = true;
            this.checkR1.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // checkP7
            // 
            this.checkP7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP7.AutoSize = true;
            this.checkP7.Enabled = false;
            this.checkP7.Location = new System.Drawing.Point(457, 366);
            this.checkP7.Name = "checkP7";
            this.checkP7.Size = new System.Drawing.Size(89, 17);
            this.checkP7.TabIndex = 14;
            this.checkP7.Text = "Преформа 7";
            this.checkP7.UseVisualStyleBackColor = true;
            this.checkP7.CheckedChanged += new System.EventHandler(this.CheckP7_CheckedChanged);
            // 
            // FormUsersPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 573);
            this.Controls.Add(this.checkP7);
            this.Controls.Add(this.checkR1);
            this.Controls.Add(this.checkK2);
            this.Controls.Add(this.checkK1);
            this.Controls.Add(this.buttonKeyDel);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonSetKey);
            this.Controls.Add(this.buttonNewAdmin);
            this.Controls.Add(this.buttonNewUser);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxOn);
            this.Controls.Add(this.checkP6);
            this.Controls.Add(this.checkP5);
            this.Controls.Add(this.checkP4);
            this.Controls.Add(this.checkP3);
            this.Controls.Add(this.checkP2);
            this.Controls.Add(this.checkP1);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 532);
            this.Name = "FormUsersPC";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Пользователи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox checkP1;
        private System.Windows.Forms.CheckBox checkP2;
        private System.Windows.Forms.CheckBox checkP3;
        private System.Windows.Forms.CheckBox checkP4;
        private System.Windows.Forms.CheckBox checkP5;
        private System.Windows.Forms.CheckBox checkP6;
        private System.Windows.Forms.CheckBox checkBoxOn;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonNewUser;
        private System.Windows.Forms.Button buttonNewAdmin;
        private System.Windows.Forms.Button buttonSetKey;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonKeyDel;
        private System.Windows.Forms.CheckBox checkK1;
        private System.Windows.Forms.CheckBox checkK2;
        private System.Windows.Forms.CheckBox checkR1;
        private System.Windows.Forms.CheckBox checkP7;
    }
}