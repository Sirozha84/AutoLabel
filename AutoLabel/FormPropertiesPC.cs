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
            foreach (Line l in Data.lines)
                comboBoxTPA.Items.Add(l.name);
            Data.ListsLoad();
            form.Close();
        }

        //Выбор ТПА из списка (сбрасываем несохранённые поля и заполняим их текущими)
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Заполнение полей из ТПА
            curLab = Data.lines[comboBoxTPA.SelectedIndex];
            //Заполнение выпадающих списков
            if (curLab.lineType == 0)
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
            if (curLab.lineType == 1)
            {
                comboBoxWeight.DataSource = StaticDir.KolpakWeights(comboBoxTPA.Text);
                comboBoxColor.DataSource = Data.Colors1;
                comboBoxCount.DataSource = Data.Quantitys1;
                //comboBoxType.DataSource = 
                comboBoxMaterial.DataSource = Data.Materials1;
                comboBoxAntistatic.DataSource = Data.Antistatics1;
                comboBoxColorants.DataSource = Data.Colorants1;
            }
            if (curLab.lineType == 2)
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
            comboBoxType.Enabled = (curLab.lineType != 2);
            comboBoxWeight_SelectedIndexChanged(null, null); //Для веса для С1 и С2 особоые условия :-( сука
            comboBoxMaterial.Enabled = (curLab.lineType != 2);
            comboBoxColor.Enabled = true;
            comboBoxCount.Enabled = (curLab.lineType != 2);
            textBoxNumber.Enabled = (curLab.lineType != 2);
            comboBoxAntistatic.Enabled = (curLab.lineType != 2);
            comboBoxColorants.Enabled = true;
            comboBoxLimit.Enabled = (curLab.lineType == 0);
            comboBoxOther.Enabled = (curLab.lineType != 2);
            textBoxBox.Enabled = true;
            textBoxKN.Enabled = (curLab.lineType == 0);
            textBoxKP.Enabled = (curLab.lineType == 0);
            textBoxDN.Enabled = (curLab.lineType == 0);
            textBoxDP.Enabled = (curLab.lineType == 0);

            //Видимость подписей полей
            label3.Enabled = true;
            label2.Enabled = (curLab.lineType != 2);
            label6.Enabled = (curLab.lineType != 2);
            label7.Enabled = true;
            label4.Enabled = (curLab.lineType != 2);
            label1.Enabled = (curLab.lineType != 2);
            label8.Enabled = (curLab.lineType != 2);
            label9.Enabled = true;
            label10.Enabled = (curLab.lineType == 0);
            label11.Enabled = (curLab.lineType != 2);
            label12.Enabled = true;
            labelKN.Enabled = (curLab.lineType == 0);
            labelKP.Enabled = (curLab.lineType == 0);
            labelDN.Enabled = (curLab.lineType == 0);
            labelDP.Enabled = (curLab.lineType == 0);

            //Заполнение полей
            if (curLab.weight != "") comboBoxWeight.SelectedItem = curLab.weight; else comboBoxWeight.SelectedItem = null;
            if (curLab.material != "") comboBoxMaterial.SelectedItem = curLab.material; else comboBoxMaterial.SelectedItem = null;
            if (curLab.color != "") comboBoxColor.SelectedItem = curLab.color; else comboBoxColor.SelectedItem = null;
            if (curLab.count != "") comboBoxCount.SelectedItem = curLab.count; else comboBoxCount.SelectedItem = null;
            if (curLab.type != "") comboBoxType.SelectedItem = curLab.type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = curLab.partNum;
            if (curLab.antistatic != "") comboBoxAntistatic.SelectedItem = curLab.antistatic; else comboBoxAntistatic.SelectedItem = null;
            if (curLab.colorant != "") comboBoxColorants.SelectedItem = curLab.colorant; else comboBoxColorants.SelectedItem = null;
            if (curLab.life != "") comboBoxLimit.SelectedItem = curLab.life; else comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = curLab.addition;
            textBoxBox.Text = "1";
            label3.Text = Data.WeightOrLogo(curLab);
            textBoxKN.Text = comboBoxColorants.Text;
            //Доступность кнопок
            buttonRepeat.Enabled = (curLab.lineType == 0);
            buttonSave.Enabled = false;
            buttonClear.Enabled = true;
        }

        //Сохранение параметров
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Line l = Data.lines[comboBoxTPA.SelectedIndex];
            //Запоминание полей (10)
            if (comboBoxWeight.SelectedItem != null) l.weight = comboBoxWeight.SelectedItem.ToString(); else l.weight = "";
            if (comboBoxType.SelectedItem != null) l.type = comboBoxType.SelectedItem.ToString(); else l.type = "";
            if (comboBoxMaterial.SelectedItem != null) l.material = comboBoxMaterial.SelectedItem.ToString(); else l.material = "";
            if (comboBoxColor.SelectedItem != null) l.color = comboBoxColor.SelectedItem.ToString(); else l.color = "";
            if (comboBoxCount.SelectedItem != null) l.count = comboBoxCount.SelectedItem.ToString(); else l.count = "";
            l.partNum = textBoxNumber.Text;
            if (comboBoxAntistatic.SelectedItem != null) l.antistatic = comboBoxAntistatic.SelectedItem.ToString(); else l.antistatic = "";
            if (comboBoxColorants.SelectedItem != null) l.colorant = comboBoxColorants.SelectedItem.ToString(); else l.colorant = "";
            if (comboBoxLimit.SelectedItem != null) l.life = comboBoxLimit.SelectedItem.ToString(); else l.life = "";
            l.addition = comboBoxOther.Text;
            try { l.boxNum = Convert.ToInt32(textBoxBox.Text); }
            catch { l.boxNum = 1; }
            l.Save();
            buttonSave.Enabled = false;
            Net.Log("Изменение параметров ТПА на ПК");

            //Печать производственного задания
            if (curLab.lineType == 0)
                l.PrintProductionTask(textBoxKN.Text, textBoxKP.Text, textBoxDN.Text, textBoxDP.Text);
        }
        //Повторная печать производственного задания
        private void buttonRepeat_Click(object sender, EventArgs e)
        {
            Data.lines[comboBoxTPA.SelectedIndex].PrintProductionTask(textBoxKN.Text, textBoxKP.Text, textBoxDN.Text, textBoxDP.Text);
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
            if (curLab.lineType == 1)
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
