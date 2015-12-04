namespace AutoLabel
{
    partial class FormProperties
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
            this.buttonquit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonsave = new System.Windows.Forms.Button();
            this.labelformname = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxTPA = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCount = new System.Windows.Forms.ComboBox();
            this.comboBoxWeight = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.buttonquitprogram = new System.Windows.Forms.Button();
            this.buttonPrinterSelect = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonUsers = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.panel2.Size = new System.Drawing.Size(978, 100);
            this.panel2.TabIndex = 9;
            // 
            // buttonsave
            // 
            this.buttonsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonsave.ForeColor = System.Drawing.Color.White;
            this.buttonsave.Location = new System.Drawing.Point(712, 12);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(250, 76);
            this.buttonsave.TabIndex = 8;
            this.buttonsave.Text = "Сохранить";
            this.buttonsave.UseVisualStyleBackColor = true;
            this.buttonsave.Visible = false;
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
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
            this.labelformname.Size = new System.Drawing.Size(722, 73);
            this.labelformname.TabIndex = 7;
            this.labelformname.Text = "Установка параметров";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxTPA, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxCount, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxWeight, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNumber, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(218, 106);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(665, 546);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // comboBoxTPA
            // 
            this.comboBoxTPA.BackColor = System.Drawing.Color.Black;
            this.comboBoxTPA.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxTPA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTPA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTPA.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxTPA.ForeColor = System.Drawing.Color.White;
            this.comboBoxTPA.FormattingEnabled = true;
            this.comboBoxTPA.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBoxTPA.Location = new System.Drawing.Point(269, 3);
            this.comboBoxTPA.Name = "comboBoxTPA";
            this.comboBoxTPA.Size = new System.Drawing.Size(393, 63);
            this.comboBoxTPA.TabIndex = 14;
            this.comboBoxTPA.SelectedIndexChanged += new System.EventHandler(this.comboBoxTPA_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 46);
            this.label5.TabIndex = 13;
            this.label5.Text = "ТПА:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCount
            // 
            this.comboBoxCount.BackColor = System.Drawing.Color.Black;
            this.comboBoxCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCount.ForeColor = System.Drawing.Color.White;
            this.comboBoxCount.FormattingEnabled = true;
            this.comboBoxCount.Location = new System.Drawing.Point(269, 275);
            this.comboBoxCount.Name = "comboBoxCount";
            this.comboBoxCount.Size = new System.Drawing.Size(393, 63);
            this.comboBoxCount.TabIndex = 12;
            this.comboBoxCount.Visible = false;
            this.comboBoxCount.SelectedIndexChanged += new System.EventHandler(this.comboBoxCount_SelectedIndexChanged);
            // 
            // comboBoxWeight
            // 
            this.comboBoxWeight.BackColor = System.Drawing.Color.Black;
            this.comboBoxWeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxWeight.ForeColor = System.Drawing.Color.White;
            this.comboBoxWeight.FormattingEnabled = true;
            this.comboBoxWeight.Location = new System.Drawing.Point(269, 207);
            this.comboBoxWeight.Name = "comboBoxWeight";
            this.comboBoxWeight.Size = new System.Drawing.Size(393, 63);
            this.comboBoxWeight.TabIndex = 11;
            this.comboBoxWeight.Visible = false;
            this.comboBoxWeight.SelectedIndexChanged += new System.EventHandler(this.comboBoxWeight_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер партии:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 49);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тип горловины:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 49);
            this.label3.TabIndex = 2;
            this.label3.Text = "Вес:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 49);
            this.label4.TabIndex = 3;
            this.label4.Text = "Количество:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // comboBoxType
            // 
            this.comboBoxType.BackColor = System.Drawing.Color.Black;
            this.comboBoxType.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxType.ForeColor = System.Drawing.Color.White;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(269, 139);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(393, 63);
            this.comboBoxType.TabIndex = 10;
            this.comboBoxType.Visible = false;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.BackColor = System.Drawing.Color.Black;
            this.textBoxNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNumber.ForeColor = System.Drawing.Color.White;
            this.textBoxNumber.Location = new System.Drawing.Point(269, 71);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(393, 62);
            this.textBoxNumber.TabIndex = 16;
            this.textBoxNumber.Visible = false;
            this.textBoxNumber.Click += new System.EventHandler(this.textBoxNumber_Click);
            this.textBoxNumber.TextChanged += new System.EventHandler(this.textBoxNumber_TextChanged);
            // 
            // buttonquitprogram
            // 
            this.buttonquitprogram.BackColor = System.Drawing.Color.Black;
            this.buttonquitprogram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonquitprogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonquitprogram.ForeColor = System.Drawing.Color.White;
            this.buttonquitprogram.Location = new System.Drawing.Point(12, 131);
            this.buttonquitprogram.Name = "buttonquitprogram";
            this.buttonquitprogram.Size = new System.Drawing.Size(127, 30);
            this.buttonquitprogram.TabIndex = 11;
            this.buttonquitprogram.Text = "Выход";
            this.buttonquitprogram.UseVisualStyleBackColor = false;
            this.buttonquitprogram.Click += new System.EventHandler(this.buttonquitprogram_Click);
            // 
            // buttonPrinterSelect
            // 
            this.buttonPrinterSelect.BackColor = System.Drawing.Color.Black;
            this.buttonPrinterSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrinterSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrinterSelect.ForeColor = System.Drawing.Color.White;
            this.buttonPrinterSelect.Location = new System.Drawing.Point(12, 95);
            this.buttonPrinterSelect.Name = "buttonPrinterSelect";
            this.buttonPrinterSelect.Size = new System.Drawing.Size(127, 30);
            this.buttonPrinterSelect.TabIndex = 12;
            this.buttonPrinterSelect.Text = "Выбор принтера";
            this.buttonPrinterSelect.UseVisualStyleBackColor = false;
            this.buttonPrinterSelect.Click += new System.EventHandler(this.buttonPrinterSelect_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.BackColor = System.Drawing.Color.Black;
            this.buttonReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReport.ForeColor = System.Drawing.Color.White;
            this.buttonReport.Location = new System.Drawing.Point(12, 58);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(127, 30);
            this.buttonReport.TabIndex = 13;
            this.buttonReport.Text = "Отчёты";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.buttonUsers);
            this.panel1.Controls.Add(this.buttonPrinterSelect);
            this.panel1.Controls.Add(this.buttonquitprogram);
            this.panel1.Controls.Add(this.buttonReport);
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 577);
            this.panel1.TabIndex = 14;
            // 
            // buttonUsers
            // 
            this.buttonUsers.BackColor = System.Drawing.Color.Black;
            this.buttonUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonUsers.ForeColor = System.Drawing.Color.White;
            this.buttonUsers.Location = new System.Drawing.Point(12, 22);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(127, 30);
            this.buttonUsers.TabIndex = 14;
            this.buttonUsers.Text = "Пользователи";
            this.buttonUsers.UseVisualStyleBackColor = false;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // FormProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(974, 676);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProperties";
            this.Text = "FormProperties";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormProperties_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonquit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelformname;
        private System.Windows.Forms.Button buttonsave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonquitprogram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.ComboBox comboBoxCount;
        private System.Windows.Forms.ComboBox comboBoxWeight;
        private System.Windows.Forms.ComboBox comboBoxTPA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Button buttonPrinterSelect;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonUsers;
    }
}