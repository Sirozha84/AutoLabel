using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormPropertiesPC : Form
    {
        Line curLab;

        public FormPropertiesPC()
        {
            InitializeComponent();
            FormLoadSplash form = new FormLoadSplash("Загрузка...");
            form.Show();
            Application.DoEvents();
            foreach (Line l in Data.Labels)
                comboBoxTPA.Items.Add(l.TPAName);
            Data.ListsLoad();
            form.Close();
        }

        //Выбор ТПА из списка (сбрасываем несохранённые поля и заполняим их текущими)
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Заполнение полей из ТПА
            curLab = Data.Labels[comboBoxTPA.SelectedIndex];
            //Заполнение выпадающих списков
            if (curLab.TPAType == 0)
            {
                comboBoxWeight.DataSource = Data.Weights0;
                comboBoxColor.DataSource = Data.Colors0;
                comboBoxCount.DataSource = Data.Quantitys0;
                comboBoxType.DataSource = Data.Types0;
                comboBoxMaterial.DataSource = Data.Materials0;
                comboBoxAntistatic.DataSource = Data.Antistatics0;
                comboBoxColorants.DataSource = Data.Colorants0;
                comboBoxOther.DataSource = Data.Others;
            }
            if (curLab.TPAType == 1)
            {
                comboBoxWeight.DataSource = StaticDir.KolpakWeights(comboBoxTPA.Text);
                comboBoxColor.DataSource = Data.Colors1;
                comboBoxCount.DataSource = Data.Quantitys1;
                //comboBoxType.DataSource = 
                comboBoxMaterial.DataSource = Data.Materials1;
                comboBoxAntistatic.DataSource = Data.Antistatics1;
                comboBoxColorants.DataSource = Data.Colorants1;
            }
            if (curLab.TPAType == 2)
            {
                comboBoxWeight.DataSource = Data.Weights2;
                comboBoxColor.DataSource = Data.Colors2;
                comboBoxColorants.DataSource = Data.Colorants2;
            }

            //Очистка полей
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
            textBoxKN.Text = "";
            textBoxKP.Text = "";
            textBoxDN.Text = "";
            textBoxDP.Text = "";

            //Видимость полей
            comboBoxWeight.Enabled = true;
            comboBoxType.Enabled = (curLab.TPAType != 2);
            comboBoxWeight_SelectedIndexChanged(null, null); //Для веса для С1 и С2 особоые условия :-( сука
            comboBoxMaterial.Enabled = (curLab.TPAType != 2);
            comboBoxColor.Enabled = true;
            comboBoxCount.Enabled = (curLab.TPAType != 2);
            textBoxNumber.Enabled = (curLab.TPAType != 2);
            comboBoxAntistatic.Enabled = (curLab.TPAType != 2);
            comboBoxColorants.Enabled = true;
            comboBoxLimit.Enabled = (curLab.TPAType == 0);
            comboBoxOther.Enabled = (curLab.TPAType != 2);
            textBoxBox.Enabled = true;
            textBoxKN.Enabled = (curLab.TPAType == 0);
            textBoxKP.Enabled = (curLab.TPAType == 0);
            textBoxDN.Enabled = (curLab.TPAType == 0);
            textBoxDP.Enabled = (curLab.TPAType == 0);

            //Видимость подписей полей
            label3.Enabled = true;
            label2.Enabled = (curLab.TPAType != 2);
            label6.Enabled = (curLab.TPAType != 2);
            label7.Enabled = true;
            label4.Enabled = (curLab.TPAType != 2);
            label1.Enabled = (curLab.TPAType != 2);
            label8.Enabled = (curLab.TPAType != 2);
            label9.Enabled = true;
            label10.Enabled = (curLab.TPAType == 0);
            label11.Enabled = (curLab.TPAType != 2);
            label12.Enabled = true;
            labelKN.Enabled = (curLab.TPAType == 0);
            labelKP.Enabled = (curLab.TPAType == 0);
            labelDN.Enabled = (curLab.TPAType == 0);
            labelDP.Enabled = (curLab.TPAType == 0);

            //Заполнение полей
            if (curLab.Weight != "") comboBoxWeight.SelectedItem = curLab.Weight; else comboBoxWeight.SelectedItem = null;
            if (curLab.Material != "") comboBoxMaterial.SelectedItem = curLab.Material; else comboBoxMaterial.SelectedItem = null;
            if (curLab.PColor != "") comboBoxColor.SelectedItem = curLab.PColor; else comboBoxColor.SelectedItem = null;
            if (curLab.Count != "") comboBoxCount.SelectedItem = curLab.Count; else comboBoxCount.SelectedItem = null;
            if (curLab.Type != "") comboBoxType.SelectedItem = curLab.Type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = curLab.PartNum;
            if (curLab.Antistatic != "") comboBoxAntistatic.SelectedItem = curLab.Antistatic; else comboBoxAntistatic.SelectedItem = null;
            if (curLab.Colorant != "") comboBoxColorants.SelectedItem = curLab.Colorant; else comboBoxColorants.SelectedItem = null;
            if (curLab.Limit != "") comboBoxLimit.SelectedItem = curLab.Limit; else comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = curLab.Other;
            textBoxBox.Text = "1";
            label3.Text = Data.WeightOrLogo(curLab);
            textBoxKN.Text = comboBoxColorants.Text;
            //Доступность кнопок
            buttonRepeat.Enabled = (curLab.TPAType == 0);
            buttonSave.Enabled = false;
            buttonClear.Enabled = true;
        }

        //Сохранение параметров
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Line l = Data.Labels[comboBoxTPA.SelectedIndex];
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

            //Печать производственного задания
            if (curLab.TPAType == 0)
                l.PrintProductionTask(textBoxKN.Text, textBoxKP.Text, textBoxDN.Text, textBoxDP.Text);
        }
        //Повторная печать производственного задания
        private void buttonRepeat_Click(object sender, EventArgs e)
        {
            Data.Labels[comboBoxTPA.SelectedIndex].PrintProductionTask(textBoxKN.Text, textBoxKP.Text, textBoxDN.Text, textBoxDP.Text);
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
        private void comboBoxColorants_SelectedIndexChanged(object sender, EventArgs e) { textBoxKN.Text = comboBoxColorants.Text; MakeSaveEnable(); }
        private void comboBoxOther_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxOther_TextUpdate(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxBox_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }

        /// <summary>
        /// Выбор веса из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (curLab.TPAType == 1)
            {
                comboBoxType.DataSource = Conformity.WeightToType(comboBoxWeight.Text, comboBoxTPA.Text);
                comboBoxType.Enabled = comboBoxType.Items.Count > 1;
                comboBoxCount.SelectedItem = Conformity.WeightToCount(comboBoxWeight.Text, comboBoxTPA.Text);
            }
            comboBoxColorants.SelectedItem = Conformity.NameAndColorToCode(comboBoxWeight.Text, comboBoxColor.Text);
            MakeSaveEnable();
        }
        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxColorants.SelectedItem = Conformity.NameAndColorToCode(comboBoxWeight.Text, comboBoxColor.Text);
            MakeSaveEnable();
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
