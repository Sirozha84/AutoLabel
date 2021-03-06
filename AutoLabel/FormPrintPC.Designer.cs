﻿namespace AutoLabel
{
    partial class FormPrintPC
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
            this.labelNum = new System.Windows.Forms.Label();
            this.LabelPacker = new System.Windows.Forms.Label();
            this.textBoxNum = new System.Windows.Forms.TextBox();
            this.buttonMax = new System.Windows.Forms.Button();
            this.buttonDec = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.comboBoxUser = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.buttonCountInc = new System.Windows.Forms.Button();
            this.buttonCountDec = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCountMax = new System.Windows.Forms.Button();
            this.checkBoxFixUsers = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelNum
            // 
            this.labelNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNum.Location = new System.Drawing.Point(9, 9);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(318, 36);
            this.labelNum.TabIndex = 2;
            this.labelNum.Text = "ТПА: ";
            this.labelNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelPacker
            // 
            this.LabelPacker.AutoSize = true;
            this.LabelPacker.Location = new System.Drawing.Point(59, 51);
            this.LabelPacker.Name = "LabelPacker";
            this.LabelPacker.Size = new System.Drawing.Size(72, 13);
            this.LabelPacker.TabIndex = 3;
            this.LabelPacker.Text = "Упаковщик: ";
            this.LabelPacker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNum
            // 
            this.textBoxNum.Location = new System.Drawing.Point(163, 99);
            this.textBoxNum.Name = "textBoxNum";
            this.textBoxNum.Size = new System.Drawing.Size(50, 20);
            this.textBoxNum.TabIndex = 7;
            this.textBoxNum.TabStop = false;
            this.textBoxNum.Text = "0";
            this.textBoxNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxNum.TextChanged += new System.EventHandler(this.textBoxNum_TextChanged);
            // 
            // buttonMax
            // 
            this.buttonMax.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonMax.Location = new System.Drawing.Point(219, 99);
            this.buttonMax.Name = "buttonMax";
            this.buttonMax.Size = new System.Drawing.Size(20, 20);
            this.buttonMax.TabIndex = 6;
            this.buttonMax.Text = ">";
            this.buttonMax.UseVisualStyleBackColor = false;
            this.buttonMax.Click += new System.EventHandler(this.buttonMax_Click);
            // 
            // buttonDec
            // 
            this.buttonDec.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDec.Location = new System.Drawing.Point(137, 99);
            this.buttonDec.Name = "buttonDec";
            this.buttonDec.Size = new System.Drawing.Size(20, 20);
            this.buttonDec.TabIndex = 5;
            this.buttonDec.Text = "<";
            this.buttonDec.UseVisualStyleBackColor = false;
            this.buttonDec.Click += new System.EventHandler(this.buttonDec_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Короб:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrint.Enabled = false;
            this.buttonPrint.Location = new System.Drawing.Point(171, 169);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(75, 23);
            this.buttonPrint.TabIndex = 6;
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // comboBoxUser
            // 
            this.comboBoxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUser.FormattingEnabled = true;
            this.comboBoxUser.Location = new System.Drawing.Point(137, 48);
            this.comboBoxUser.Name = "comboBoxUser";
            this.comboBoxUser.Size = new System.Drawing.Size(178, 21);
            this.comboBoxUser.TabIndex = 15;
            this.comboBoxUser.SelectedIndexChanged += new System.EventHandler(this.comboBoxUser_SelectedIndexChanged);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Enabled = false;
            this.textBoxCount.Location = new System.Drawing.Point(163, 125);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(50, 20);
            this.textBoxCount.TabIndex = 7;
            this.textBoxCount.TabStop = false;
            this.textBoxCount.Text = "0";
            this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxCount_TextChanged);
            // 
            // buttonCountInc
            // 
            this.buttonCountInc.Enabled = false;
            this.buttonCountInc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCountInc.Location = new System.Drawing.Point(219, 125);
            this.buttonCountInc.Name = "buttonCountInc";
            this.buttonCountInc.Size = new System.Drawing.Size(20, 20);
            this.buttonCountInc.TabIndex = 6;
            this.buttonCountInc.Text = ">";
            this.buttonCountInc.UseVisualStyleBackColor = false;
            this.buttonCountInc.Click += new System.EventHandler(this.buttonCountInc_Click);
            // 
            // buttonCountDec
            // 
            this.buttonCountDec.Enabled = false;
            this.buttonCountDec.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCountDec.Location = new System.Drawing.Point(137, 125);
            this.buttonCountDec.Name = "buttonCountDec";
            this.buttonCountDec.Size = new System.Drawing.Size(20, 20);
            this.buttonCountDec.TabIndex = 5;
            this.buttonCountDec.Text = "<";
            this.buttonCountDec.UseVisualStyleBackColor = false;
            this.buttonCountDec.Click += new System.EventHandler(this.buttonCountDec_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(17, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Количество коробов:\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(252, 169);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonCountMax
            // 
            this.buttonCountMax.Enabled = false;
            this.buttonCountMax.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCountMax.Location = new System.Drawing.Point(245, 125);
            this.buttonCountMax.Name = "buttonCountMax";
            this.buttonCountMax.Size = new System.Drawing.Size(20, 20);
            this.buttonCountMax.TabIndex = 17;
            this.buttonCountMax.Text = ">|";
            this.buttonCountMax.UseVisualStyleBackColor = false;
            this.buttonCountMax.Click += new System.EventHandler(this.buttonCountMax_Click);
            // 
            // checkBoxFixUsers
            // 
            this.checkBoxFixUsers.AutoSize = true;
            this.checkBoxFixUsers.Checked = true;
            this.checkBoxFixUsers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFixUsers.Location = new System.Drawing.Point(137, 76);
            this.checkBoxFixUsers.Name = "checkBoxFixUsers";
            this.checkBoxFixUsers.Size = new System.Drawing.Size(134, 17);
            this.checkBoxFixUsers.TabIndex = 18;
            this.checkBoxFixUsers.Text = "Только привязанные";
            this.checkBoxFixUsers.UseVisualStyleBackColor = true;
            this.checkBoxFixUsers.CheckedChanged += new System.EventHandler(this.checkBoxFixUsers_CheckedChanged);
            // 
            // FormPrintPC
            // 
            this.AcceptButton = this.buttonPrint;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(339, 204);
            this.Controls.Add(this.checkBoxFixUsers);
            this.Controls.Add(this.buttonCountMax);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.buttonCountInc);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.buttonMax);
            this.Controls.Add(this.textBoxNum);
            this.Controls.Add(this.buttonCountDec);
            this.Controls.Add(this.comboBoxUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonDec);
            this.Controls.Add(this.LabelPacker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrintPC";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Печать этикетки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelNum;
        private System.Windows.Forms.Label LabelPacker;
        private System.Windows.Forms.TextBox textBoxNum;
        private System.Windows.Forms.Button buttonMax;
        private System.Windows.Forms.Button buttonDec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.ComboBox comboBoxUser;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button buttonCountInc;
        private System.Windows.Forms.Button buttonCountDec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCountMax;
        private System.Windows.Forms.CheckBox checkBoxFixUsers;
    }
}