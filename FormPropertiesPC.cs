using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormPropertiesPC : Form
    {
        public FormPropertiesPC()
        {
            InitializeComponent();
            FormLoadSplash form = new FormLoadSplash("Загрузка...");
            form.Show();
            Application.DoEvents();
            foreach (Label l in Data.Labels)
                comboBoxTPA.Items.Add(l.TPAName);
            Data.ListsLoad();
            form.Close();
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
                comboBoxType.DataSource = Data.Types0;
                comboBoxMaterial.DataSource = Data.Materials0;
                comboBoxAntistatic.DataSource = Data.Antistatics0;
                comboBoxColorants.DataSource = Data.Colorants0;
                //Временно, пока не будет констант
                comboBoxOther.Items.Clear();
                comboBoxOther.Items.Add("Coca-cola");
                comboBoxOther.Items.Add("АЯН");
                comboBoxOther.Items.Add("ИЗРМВ");
            }
            if (l.TPAType == 1)
            {
                comboBoxWeight.DataSource = Data.Weights1;
                comboBoxColor.DataSource = Data.Colors1;
                comboBoxCount.DataSource = Data.Quantitys1;
                comboBoxType.DataSource = Data.Types1;
                comboBoxMaterial.DataSource = Data.Materials1;
                comboBoxAntistatic.DataSource = Data.Antistatics1;
                comboBoxColorants.DataSource = Data.Colorants1;
                //Временно, пока не будет констант
                comboBoxOther.Items.Clear();
            }
            if (l.TPAType == 2)
            {
                comboBoxWeight.DataSource = Data.Weights2;
                comboBoxColor.DataSource = Data.Colors2;
            }
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
            if (l.Antistatic != "") comboBoxAntistatic.SelectedItem = l.Antistatic; else comboBoxAntistatic.SelectedItem = null;
            if (l.Colorant != "") comboBoxColorants.SelectedItem = l.Colorant; else comboBoxColorants.SelectedItem = null;
            if (l.Limit != "") comboBoxLimit.SelectedItem = l.Limit; else comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = l.Other;
            textBoxBox.Text = "1";
            label3.Text = Data.WeightOrLogo(l);

            //Видимость полей
            comboBoxWeight.Enabled = true;
            comboBoxType.Enabled = (l.TPAType == 0);
            comboBoxMaterial.Enabled = (l.TPAType != 2);
            comboBoxColor.Enabled = true;
            comboBoxCount.Enabled = (l.TPAType != 2);
            textBoxNumber.Enabled = (l.TPAType != 2);
            comboBoxAntistatic.Enabled = (l.TPAType != 2);
            comboBoxColorants.Enabled = (l.TPAType != 2);
            comboBoxLimit.Enabled = (l.TPAType == 0);
            comboBoxOther.Enabled = (l.TPAType != 2);
            textBoxBox.Enabled = true;

            //Видимость подписей полей
            label3.Enabled = true;
            label2.Enabled = (l.TPAType == 0);
            label6.Enabled = (l.TPAType != 2);
            label7.Enabled = true;
            label4.Enabled = (l.TPAType != 2);
            label1.Enabled = (l.TPAType != 2);
            label8.Enabled = (l.TPAType != 2);
            label9.Enabled = (l.TPAType != 2);
            label10.Enabled = (l.TPAType == 0);
            label11.Enabled = (l.TPAType != 2);
            label12.Enabled = true;
            buttonSave.Enabled = false;
            buttonClear.Enabled = true;
        }

        //Сохранение параметров
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];
            //Запоминание полей (10)
            if (comboBoxWeight.SelectedItem != null) l.Weight = comboBoxWeight.SelectedItem.ToString(); else l.Weight = "";
            if (comboBoxType.SelectedItem != null) l.Type = comboBoxType.SelectedItem.ToString(); else l.Type = "";
            if (comboBoxMaterial.SelectedItem != null) l.Material = comboBoxMaterial.SelectedItem.ToString(); else l.Material = "";
            if (comboBoxColor.SelectedItem != null) l.PColor = comboBoxColor.SelectedItem.ToString(); else l.PColor = "";
            if (comboBoxCount.SelectedItem != null) l.Count = comboBoxCount.SelectedItem.ToString(); else l.Count = "";
            l.PartNum = textBoxNumber.Text;
            if (comboBoxAntistatic.SelectedItem != null) l.Antistatic = comboBoxAntistatic.SelectedItem.ToString(); else l.Antistatic = "";
            if (comboBoxColorants.SelectedItem != null) l.Colorant = comboBoxColorants.SelectedItem.ToString(); else l.Colorant = "";
            if (comboBoxLimit.SelectedItem != null) l.Limit = comboBoxLimit.SelectedItem.ToString(); else l.Limit = "";
            l.Other = comboBoxOther.Text;
            try { l.CurrentNum = Convert.ToInt32(textBoxBox.Text); }
            catch { l.CurrentNum = 1; }
            l.Save();
            buttonSave.Enabled = false;
            Net.Log("Изменение параметров ТПА на ПК");
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
        private void comboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxNumber_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxLimit_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxOther_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxBox_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }

        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeSaveEnable();
            comboBoxType.SelectedItem = Data.Conformity(comboBoxWeight.Text);
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
            comboBoxAntistatic.SelectedItem = "";
            comboBoxColorants.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = "";
        }

        //Кнопка Закрытия
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
