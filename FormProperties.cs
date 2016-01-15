using System;
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
            //Режим для ПК
            if (!Data.IsMachine)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                buttonquitprogram.Visible = false;
            }
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
            buttonClear.Visible = true;
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
        private void comboBoxLimit_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }

        private void buttonPrinterSelect_Click(object sender, EventArgs e)
        {
            Data.PrintSetup();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (!Data.PrintSelected())  //Ой, ну и бред же :-( но работать будет
            {
                Data.PrintSetup();
            }
            if (Data.PrintSelected())
            {
                FormReports rep = new FormReports();
                rep.ShowDialog();
                rep.Dispose();
            }
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            FormUsers form = new FormUsers();
            form.ShowDialog();
        }

        private void textBoxOther_Click(object sender, EventArgs e)
        {
            FormKeyboardLetter key = new FormKeyboardLetter("Введите прочие дополнения");
            if (key.ShowDialog() == DialogResult.OK)
            {
                textBoxOther.Text = key.Str;
                MakeSaveEnable();
            }
        }

        private void textBoxNumber_Click_1(object sender, EventArgs e)
        {
            FormKeyboardNums key = new FormKeyboardNums("Введите номер партии");
            if (key.ShowDialog() == DialogResult.OK)
            {
                textBoxNumber.Text = key.Str;
                MakeSaveEnable();
            }
        }

        /// <summary>
        /// Очистка полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            comboBoxWeight.SelectedItem = null;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.SelectedItem = null;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.SelectedItem = null;
            textBoxNumber.Text = "";
            comboBoxAntiType.SelectedItem = "";
            comboBoxAntiCount.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            textBoxOther.Text = "";
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AutoLabel\n" +
                            "Версия: " + Program.Version + "\n" +
                            "Автор: Сергей Гордеев\n" +
                            "Телефон техподдержки: +7 (965) 917-31-43\n" +
                            "Сайт автора: http://www.sg-software.ru",
                            "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
