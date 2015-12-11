﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormProperties : Form
    {
        public FormProperties()
        {
            InitializeComponent();
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonquitprogram_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void FormProperties_Load(object sender, EventArgs e)
        {
            //Заполнение комбобоксов списками
            textBoxNumber.Text = "";
            //textBoxCurrent.Text = "";
            comboBoxWeight.DataSource = Data.Weights;
            comboBoxWeight.SelectedItem = null;
            comboBoxType.DataSource = Data.Types;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.DataSource = Data.Materials;
            comboBoxMaterial.SelectedItem = null;
            comboBoxColor.DataSource = Data.Colors;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.DataSource = Data.Quantitys;
            comboBoxCount.SelectedItem = null;
            comboBoxAntiType.DataSource = Data.AntiTypes;
            comboBoxAntiType.SelectedItem = "";
            comboBoxAntiCount.DataSource = Data.AntiCounts;
            comboBoxAntiCount.SelectedItem = null;
            comboBoxLimit.DataSource = Data.Limits;
            comboBoxLimit.SelectedItem = null;
            buttonsave.Visible = false;
        }

        //Выбор ТПА из списка (сбрасываем несохранённые поля и заполняим их текущими)
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];

            //comboBoxType.Enabled = true;
            if (l.Weight != "") comboBoxWeight.SelectedItem = l.Weight; else comboBoxWeight.SelectedItem = null;
            if (l.Material != "") comboBoxMaterial.SelectedItem = l.Material; else comboBoxMaterial.SelectedItem = null;
            if (l.PColor != "") comboBoxColor.SelectedItem = l.PColor; else comboBoxColor.SelectedItem = null;
            if (l.Count != "") comboBoxCount.SelectedItem = l.Count; else comboBoxCount.SelectedItem = null;
            if (l.Type != "") comboBoxType.SelectedItem = l.Type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = l.PartNum;
            if (l.AntistaticType != "") comboBoxAntiType.SelectedItem = l.AntistaticType; else comboBoxAntiType.SelectedItem = null;
            if (l.AntistaticCount != "") comboBoxAntiCount.SelectedItem = l.AntistaticCount; else comboBoxAntiCount.SelectedItem = null;
            if (l.Limit != "") comboBoxLimit.SelectedItem = l.Limit; else comboBoxLimit.SelectedItem = null;
            textBoxOther.Text = l.Other;

            //if (l.Count != "") comboBoxCount
            comboBoxWeight.Visible = true;
            comboBoxType.Visible = true;
            comboBoxMaterial.Visible = true;
            comboBoxColor.Visible = true;
            comboBoxCount.Visible = true;
            textBoxNumber.Visible = true;
            comboBoxLimit.Visible = true;
            comboBoxAntiType.Visible = true;
            comboBoxAntiCount.Visible = true;
            textBoxOther.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            buttonsave.Visible = false;
        }

        //Сохранение параметров
        private void buttonsave_Click(object sender, EventArgs e)
        {
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];
            //Сброс номера
            l.CurrentNum = 1;
            //Запоминание полей (10)
            if (comboBoxWeight.SelectedItem != null) l.Weight = comboBoxWeight.SelectedItem.ToString(); else l.Weight = "";
            if (comboBoxType.SelectedItem != null) l.Type = comboBoxType.SelectedItem.ToString(); else l.Type = "";
            if (comboBoxMaterial.SelectedItem != null) l.Material = comboBoxMaterial.SelectedItem.ToString(); else l.Material = "";
            if (comboBoxColor.SelectedItem != null) l.PColor = comboBoxColor.SelectedItem.ToString(); else l.PColor = "";
            if (comboBoxCount.SelectedItem != null) l.Count = comboBoxCount.SelectedItem.ToString(); else l.Count = "";
            l.PartNum = textBoxNumber.Text;
            if (comboBoxAntiType.SelectedItem != null) l.AntistaticType = comboBoxAntiType.SelectedItem.ToString(); else l.AntistaticType = "";
            if (comboBoxAntiCount.SelectedItem != null) l.AntistaticCount = comboBoxAntiCount.SelectedItem.ToString(); else l.AntistaticCount = "";
            if (comboBoxLimit.SelectedItem != null) l.Limit = comboBoxLimit.SelectedItem.ToString(); else l.Limit = "";
            l.Other = textBoxOther.Text;

            l.Save();
            buttonsave.Visible = false;
        }

        void MakeSaveEnable()
        {
            if (comboBoxTPA.SelectedIndex >= 0)
                buttonsave.Visible = true;
        }

        private void comboBoxCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxFirstBox_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxNumber_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxNumber_Click(object sender, EventArgs e)
        {

        }

        private void buttonPrinterSelect_Click(object sender, EventArgs e)
        {
            Data.PrintSetup();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon :-)");
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            FormUsers form = new FormUsers();
            form.Show();
        }

        private void textBoxOther_Click(object sender, EventArgs e)
        {
            FormKeyboardLetter key = new FormKeyboardLetter("Введите прочие дополнения");
            if (key.ShowDialog() == DialogResult.OK)
                textBoxOther .Text = key.Str;
        }
    }
}
