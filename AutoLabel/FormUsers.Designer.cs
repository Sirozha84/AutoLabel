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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUsers));
            this.labelformname = new System.Windows.Forms.Label();
            this.buttonquit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonsave = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonNewAdmin = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonKey = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonKeyDel = new System.Windows.Forms.Button();
            this.checkP1 = new System.Windows.Forms.CheckBox();
            this.checkP2 = new System.Windows.Forms.CheckBox();
            this.checkP3 = new System.Windows.Forms.CheckBox();
            this.checkP4 = new System.Windows.Forms.CheckBox();
            this.checkP5 = new System.Windows.Forms.CheckBox();
            this.checkP6 = new System.Windows.Forms.CheckBox();
            this.checkBoxOn = new System.Windows.Forms.CheckBox();
            this.checkK1 = new System.Windows.Forms.CheckBox();
            this.checkK2 = new System.Windows.Forms.CheckBox();
            this.checkR1 = new System.Windows.Forms.CheckBox();
            this.checkP7 = new System.Windows.Forms.CheckBox();
            this.checkP8 = new System.Windows.Forms.CheckBox();
            this.checkP9 = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelformname
            // 
            this.labelformname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelformname.AutoSize = true;
            this.labelformname.BackColor = System.Drawing.Color.SlateGray;
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
            this.buttonquit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonquit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonquit.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonquit.ForeColor = System.Drawing.Color.White;
            this.buttonquit.Location = new System.Drawing.Point(12, 12);
            this.buttonquit.Name = "buttonquit";
            this.buttonquit.Size = new System.Drawing.Size(250, 76);
            this.buttonquit.TabIndex = 2;
            this.buttonquit.Text = "< Назад";
            this.buttonquit.UseVisualStyleBackColor = false;
            this.buttonquit.Click += new System.EventHandler(this.buttonquit_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.SlateGray;
            this.panel2.Controls.Add(this.buttonsave);
            this.panel2.Controls.Add(this.labelformname);
            this.panel2.Controls.Add(this.buttonquit);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1128, 100);
            this.panel2.TabIndex = 10;
            // 
            // buttonsave
            // 
            this.buttonsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonsave.ForeColor = System.Drawing.Color.White;
            this.buttonsave.Location = new System.Drawing.Point(862, 12);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(250, 76);
            this.buttonsave.TabIndex = 8;
            this.buttonsave.Text = "Записать";
            this.buttonsave.UseVisualStyleBackColor = false;
            this.buttonsave.Visible = false;
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNew.ForeColor = System.Drawing.Color.White;
            this.buttonNew.Location = new System.Drawing.Point(929, 153);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(183, 69);
            this.buttonNew.TabIndex = 14;
            this.buttonNew.Text = "Новый упаковщик";
            this.buttonNew.UseVisualStyleBackColor = false;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonNewAdmin
            // 
            this.buttonNewAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonNewAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNewAdmin.ForeColor = System.Drawing.Color.White;
            this.buttonNewAdmin.Location = new System.Drawing.Point(929, 228);
            this.buttonNewAdmin.Name = "buttonNewAdmin";
            this.buttonNewAdmin.Size = new System.Drawing.Size(183, 69);
            this.buttonNewAdmin.TabIndex = 15;
            this.buttonNewAdmin.Text = "Новый администратор";
            this.buttonNewAdmin.UseVisualStyleBackColor = false;
            this.buttonNewAdmin.Click += new System.EventHandler(this.buttonRules_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(929, 453);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(183, 69);
            this.buttonDelete.TabIndex = 16;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Visible = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonKey
            // 
            this.buttonKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonKey.ForeColor = System.Drawing.Color.White;
            this.buttonKey.Location = new System.Drawing.Point(929, 303);
            this.buttonKey.Name = "buttonKey";
            this.buttonKey.Size = new System.Drawing.Size(183, 69);
            this.buttonKey.TabIndex = 17;
            this.buttonKey.Text = "Изменить ключ";
            this.buttonKey.UseVisualStyleBackColor = false;
            this.buttonKey.Visible = false;
            this.buttonKey.Click += new System.EventHandler(this.buttonKey_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.Black;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 153);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(901, 747);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Пользователь";
            this.columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Права";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ключ";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Привязка к ТПА";
            this.columnHeader4.Width = 320;
            // 
            // buttonKeyDel
            // 
            this.buttonKeyDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKeyDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonKeyDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKeyDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonKeyDel.ForeColor = System.Drawing.Color.White;
            this.buttonKeyDel.Location = new System.Drawing.Point(929, 378);
            this.buttonKeyDel.Name = "buttonKeyDel";
            this.buttonKeyDel.Size = new System.Drawing.Size(183, 69);
            this.buttonKeyDel.TabIndex = 19;
            this.buttonKeyDel.Text = "Изъять ключ";
            this.buttonKeyDel.UseVisualStyleBackColor = false;
            this.buttonKeyDel.Visible = false;
            this.buttonKeyDel.Click += new System.EventHandler(this.buttonKeyDel_Click);
            // 
            // checkP1
            // 
            this.checkP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP1.AutoSize = true;
            this.checkP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP1.ForeColor = System.Drawing.Color.White;
            this.checkP1.Location = new System.Drawing.Point(929, 542);
            this.checkP1.Name = "checkP1";
            this.checkP1.Size = new System.Drawing.Size(74, 41);
            this.checkP1.TabIndex = 20;
            this.checkP1.Text = "П1";
            this.checkP1.UseVisualStyleBackColor = true;
            this.checkP1.CheckedChanged += new System.EventHandler(this.checkP1_change);
            // 
            // checkP2
            // 
            this.checkP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP2.AutoSize = true;
            this.checkP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP2.ForeColor = System.Drawing.Color.White;
            this.checkP2.Location = new System.Drawing.Point(1022, 542);
            this.checkP2.Name = "checkP2";
            this.checkP2.Size = new System.Drawing.Size(76, 41);
            this.checkP2.TabIndex = 21;
            this.checkP2.Text = "П2";
            this.checkP2.UseVisualStyleBackColor = true;
            this.checkP2.CheckedChanged += new System.EventHandler(this.checkP2_change);
            // 
            // checkP3
            // 
            this.checkP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP3.AutoSize = true;
            this.checkP3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP3.ForeColor = System.Drawing.Color.White;
            this.checkP3.Location = new System.Drawing.Point(929, 589);
            this.checkP3.Name = "checkP3";
            this.checkP3.Size = new System.Drawing.Size(76, 41);
            this.checkP3.TabIndex = 22;
            this.checkP3.Text = "П3";
            this.checkP3.UseVisualStyleBackColor = true;
            this.checkP3.CheckedChanged += new System.EventHandler(this.checkP3_change);
            // 
            // checkP4
            // 
            this.checkP4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP4.AutoSize = true;
            this.checkP4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP4.ForeColor = System.Drawing.Color.White;
            this.checkP4.Location = new System.Drawing.Point(1022, 589);
            this.checkP4.Name = "checkP4";
            this.checkP4.Size = new System.Drawing.Size(76, 41);
            this.checkP4.TabIndex = 23;
            this.checkP4.Text = "П4";
            this.checkP4.UseVisualStyleBackColor = true;
            this.checkP4.CheckedChanged += new System.EventHandler(this.checkP4_change);
            // 
            // checkP5
            // 
            this.checkP5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP5.AutoSize = true;
            this.checkP5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP5.ForeColor = System.Drawing.Color.White;
            this.checkP5.Location = new System.Drawing.Point(929, 636);
            this.checkP5.Name = "checkP5";
            this.checkP5.Size = new System.Drawing.Size(76, 41);
            this.checkP5.TabIndex = 24;
            this.checkP5.Text = "П5";
            this.checkP5.UseVisualStyleBackColor = true;
            this.checkP5.CheckedChanged += new System.EventHandler(this.checkP5_change);
            // 
            // checkP6
            // 
            this.checkP6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP6.AutoSize = true;
            this.checkP6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP6.ForeColor = System.Drawing.Color.White;
            this.checkP6.Location = new System.Drawing.Point(1022, 636);
            this.checkP6.Name = "checkP6";
            this.checkP6.Size = new System.Drawing.Size(76, 41);
            this.checkP6.TabIndex = 25;
            this.checkP6.Text = "П6";
            this.checkP6.UseVisualStyleBackColor = true;
            this.checkP6.CheckedChanged += new System.EventHandler(this.checkP6_change);
            // 
            // checkBoxOn
            // 
            this.checkBoxOn.AutoSize = true;
            this.checkBoxOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxOn.ForeColor = System.Drawing.Color.White;
            this.checkBoxOn.Location = new System.Drawing.Point(12, 106);
            this.checkBoxOn.Name = "checkBoxOn";
            this.checkBoxOn.Size = new System.Drawing.Size(744, 41);
            this.checkBoxOn.TabIndex = 26;
            this.checkBoxOn.Text = "Контроль привязки пользователей к ТПА включен";
            this.checkBoxOn.UseVisualStyleBackColor = true;
            this.checkBoxOn.CheckedChanged += new System.EventHandler(this.checkBoxOn_CheckedChanged);
            // 
            // checkK1
            // 
            this.checkK1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkK1.AutoSize = true;
            this.checkK1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkK1.ForeColor = System.Drawing.Color.White;
            this.checkK1.Location = new System.Drawing.Point(929, 788);
            this.checkK1.Name = "checkK1";
            this.checkK1.Size = new System.Drawing.Size(73, 41);
            this.checkK1.TabIndex = 27;
            this.checkK1.Text = "К1";
            this.checkK1.UseVisualStyleBackColor = true;
            this.checkK1.Visible = false;
            this.checkK1.CheckedChanged += new System.EventHandler(this.checkK1_change);
            // 
            // checkK2
            // 
            this.checkK2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkK2.AutoSize = true;
            this.checkK2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkK2.ForeColor = System.Drawing.Color.White;
            this.checkK2.Location = new System.Drawing.Point(1022, 788);
            this.checkK2.Name = "checkK2";
            this.checkK2.Size = new System.Drawing.Size(75, 41);
            this.checkK2.TabIndex = 28;
            this.checkK2.Text = "К2";
            this.checkK2.UseVisualStyleBackColor = true;
            this.checkK2.Visible = false;
            this.checkK2.CheckedChanged += new System.EventHandler(this.checkK2_change);
            // 
            // checkR1
            // 
            this.checkR1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkR1.AutoSize = true;
            this.checkR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkR1.ForeColor = System.Drawing.Color.White;
            this.checkR1.Location = new System.Drawing.Point(929, 850);
            this.checkR1.Name = "checkR1";
            this.checkR1.Size = new System.Drawing.Size(185, 41);
            this.checkR1.TabIndex = 29;
            this.checkR1.Text = "Ротопринт";
            this.checkR1.UseVisualStyleBackColor = true;
            this.checkR1.Visible = false;
            this.checkR1.CheckedChanged += new System.EventHandler(this.checkR1_change);
            // 
            // checkP7
            // 
            this.checkP7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP7.AutoSize = true;
            this.checkP7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP7.ForeColor = System.Drawing.Color.White;
            this.checkP7.Location = new System.Drawing.Point(929, 683);
            this.checkP7.Name = "checkP7";
            this.checkP7.Size = new System.Drawing.Size(76, 41);
            this.checkP7.TabIndex = 30;
            this.checkP7.Text = "П7";
            this.checkP7.UseVisualStyleBackColor = true;
            this.checkP7.CheckedChanged += new System.EventHandler(this.checkP7_change);
            // 
            // checkP8
            // 
            this.checkP8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP8.AutoSize = true;
            this.checkP8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP8.ForeColor = System.Drawing.Color.White;
            this.checkP8.Location = new System.Drawing.Point(1022, 683);
            this.checkP8.Name = "checkP8";
            this.checkP8.Size = new System.Drawing.Size(76, 41);
            this.checkP8.TabIndex = 31;
            this.checkP8.Text = "П8";
            this.checkP8.UseVisualStyleBackColor = true;
            this.checkP8.CheckedChanged += new System.EventHandler(this.checkP8_change);
            // 
            // checkP9
            // 
            this.checkP9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP9.AutoSize = true;
            this.checkP9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkP9.ForeColor = System.Drawing.Color.White;
            this.checkP9.Location = new System.Drawing.Point(929, 730);
            this.checkP9.Name = "checkP9";
            this.checkP9.Size = new System.Drawing.Size(76, 41);
            this.checkP9.TabIndex = 32;
            this.checkP9.Text = "П9";
            this.checkP9.UseVisualStyleBackColor = true;
            this.checkP9.CheckedChanged += new System.EventHandler(this.checkP9_change);
            // 
            // FormUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1126, 912);
            this.Controls.Add(this.checkP9);
            this.Controls.Add(this.checkP8);
            this.Controls.Add(this.checkP7);
            this.Controls.Add(this.checkR1);
            this.Controls.Add(this.checkK2);
            this.Controls.Add(this.checkK1);
            this.Controls.Add(this.checkBoxOn);
            this.Controls.Add(this.checkP6);
            this.Controls.Add(this.checkP5);
            this.Controls.Add(this.checkP4);
            this.Controls.Add(this.checkP3);
            this.Controls.Add(this.checkP2);
            this.Controls.Add(this.checkP1);
            this.Controls.Add(this.buttonKeyDel);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonKey);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonNewAdmin);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUsers";
            this.ShowInTaskbar = false;
            this.Text = "Пользователи";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelformname;
        private System.Windows.Forms.Button buttonquit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonsave;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonNewAdmin;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonKey;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonKeyDel;
        private System.Windows.Forms.CheckBox checkP1;
        private System.Windows.Forms.CheckBox checkP2;
        private System.Windows.Forms.CheckBox checkP3;
        private System.Windows.Forms.CheckBox checkP4;
        private System.Windows.Forms.CheckBox checkP5;
        private System.Windows.Forms.CheckBox checkP6;
        private System.Windows.Forms.CheckBox checkBoxOn;
        private System.Windows.Forms.CheckBox checkK1;
        private System.Windows.Forms.CheckBox checkK2;
        private System.Windows.Forms.CheckBox checkR1;
        private System.Windows.Forms.CheckBox checkP7;
        private System.Windows.Forms.CheckBox checkP8;
        private System.Windows.Forms.CheckBox checkP9;
    }
}