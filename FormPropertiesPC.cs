using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormPropertiesPC : Form
    {
        public FormPropertiesPC()
        {
            InitializeComponent();
            foreach (Label l in Data.Labels)
                comboBoxTPA.Items.Add(l.TPAName);
        }

        //Кнопка назад
        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка выхода
        private void buttonquitprogram_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //Выбор ТПА из списка (сбрасываем несохранённые поля и заполняим их текущими)
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Заполнение полей из ТПА
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];
            //Заполнение выпадаю0щих списков
            if (l.TPAType == 0)
            {
                comboBoxWeight.DataSource = Data.Weights0;
                comboBoxColor.DataSource = Data.Colors0;
                comboBoxCount.DataSource = Data.Quantitys0;
                label11.Text = "Прочие дополнения:";
            }
            else
            {
                comboBoxWeight.DataSource = Data.Weights1;
                comboBoxColor.DataSource = Data.Colors1;
                comboBoxCount.DataSource = Data.Quantitys1;
                label11.Text = "Код:";
            }
            comboBoxType.DataSource = Data.Types;
            comboBoxMaterial.DataSource = Data.Materials;
            comboBoxAntistatic.DataSource = Data.Antistatics;
            comboBoxColorants.DataSource = Data.Colorants;
            comboBoxLimit.DataSource = Data.Limits;
            comboBoxWeight.SelectedItem = null;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.SelectedItem = null;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.SelectedItem = null;
            comboBoxColorants.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            textBoxNumber.Text = "";
            comboBoxAntistatic.SelectedItem = "";
            if (l.Weight != "") comboBoxWeight.SelectedItem = l.Weight; else comboBoxWeight.SelectedItem = null;
            if (l.Material != "") comboBoxMaterial.SelectedItem = l.Material; else comboBoxMaterial.SelectedItem = null;
            if (l.PColor != "") comboBoxColor.SelectedItem = l.PColor; else comboBoxColor.SelectedItem = null;
            if (l.Count != "") comboBoxCount.SelectedItem = l.Count; else comboBoxCount.SelectedItem = null;
            if (l.Type != "") comboBoxType.SelectedItem = l.Type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = l.PartNum;
            if (l.AntistaticType != "") comboBoxAntistatic.SelectedItem = l.AntistaticType; else comboBoxAntistatic.SelectedItem = null;
            if (l.AntistaticCount != "") comboBoxColorants.SelectedItem = l.AntistaticCount; else comboBoxColorants.SelectedItem = null;
            if (l.Limit != "") comboBoxLimit.SelectedItem = l.Limit; else comboBoxLimit.SelectedItem = null;
            textBoxOther.Text = l.Other;
            comboBoxWeight.Enabled = true;
            comboBoxType.Enabled = (l.TPAType == 0);
            comboBoxMaterial.Enabled = (l.TPAType == 0);
            comboBoxColor.Enabled = true;
            comboBoxCount.Enabled = true;
            textBoxNumber.Enabled = true;
            comboBoxLimit.Enabled = (l.TPAType == 0);
            comboBoxAntistatic.Enabled = (l.TPAType == 0);
            comboBoxColorants.Enabled = (l.TPAType == 0);
            textBoxOther.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = (l.TPAType == 0);
            label3.Enabled = true;
            label4.Enabled = true;
            label6.Enabled = (l.TPAType == 0);
            label7.Enabled = true;
            label8.Enabled = (l.TPAType == 0);
            label9.Enabled = (l.TPAType == 0);
            label10.Enabled = (l.TPAType == 0);
            label11.Enabled = true;
            buttonSave.Enabled = false;
            buttonClear.Enabled = true;
        }

        //Сохранение параметров
        private void buttonSave_Click(object sender, EventArgs e)
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
            if (comboBoxAntistatic.SelectedItem != null) l.AntistaticType = comboBoxAntistatic.SelectedItem.ToString(); else l.AntistaticType = "";
            if (comboBoxColorants.SelectedItem != null) l.AntistaticCount = comboBoxColorants.SelectedItem.ToString(); else l.AntistaticCount = "";
            if (comboBoxLimit.SelectedItem != null) l.Limit = comboBoxLimit.SelectedItem.ToString(); else l.Limit = "";
            l.Other = textBoxOther.Text;
            l.Save();
            buttonSave.Enabled = false;
        }

        //Делаем кнопку сохранения видимой
        void MakeSaveEnable()
        {
            if (comboBoxTPA.SelectedIndex >= 0)
                buttonSave.Enabled = true;
        }

        //При изменении любого поля делаем кнопку сохранения видимой
        private void comboBoxCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxFirstBox_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxNumber_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxLimit_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxOther_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }

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
            comboBoxAntistatic.SelectedItem = "";
            comboBoxColorants.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            textBoxOther.Text = "";
        }

        //Кнопка Закрытия
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
