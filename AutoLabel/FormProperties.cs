using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormProperties : Form
    {
        Line curLab;

        public FormProperties()
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

        //Кнопка назад
        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка выхода
        private void buttonquitprogram_Click(object sender, EventArgs e)
        {
            Log.Write("Выход из программы (через параметры)");
            Environment.Exit(0);
        }

        //Выбор ТПА из списка (сбрасываем несохранённые поля и заполняим их текущими)
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Заполнение полей из ТПА
            curLab = Data.lines[comboBoxTPA.SelectedIndex];
            //Заполнение выпадаю0щих списков
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
                //comboBoxType.DataSource = Data.Types1;
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
            buttonsave.Visible = false;
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

            //Видимость полей
            comboBoxWeight.Visible = true;
            comboBoxType.Visible = (curLab.lineType != 2);
            comboBoxWeight_SelectedIndexChanged(null, null); //Для веса для С1 и С2 особоые условия :-( сука
            comboBoxMaterial.Visible = (curLab.lineType != 2);
            comboBoxColor.Visible = true;
            comboBoxCount.Visible = (curLab.lineType != 2);
            textBoxNumber.Visible = (curLab.lineType != 2);
            comboBoxAntistatic.Visible = (curLab.lineType != 2);
            comboBoxColorants.Visible = true;
            comboBoxLimit.Visible = (curLab.lineType == 0);
            comboBoxOther.Visible = (curLab.lineType != 2);
            textBoxBox.Visible = true;

            //Видимость подписей полей
            label3.Visible = true;
            label2.Visible = (curLab.lineType != 2);
            label6.Visible = (curLab.lineType != 2);
            label7.Visible = true;
            label4.Visible = (curLab.lineType != 2);
            label1.Visible = (curLab.lineType != 2);
            label8.Visible = (curLab.lineType != 2);
            label9.Visible = true;
            label10.Visible = (curLab.lineType == 0);
            label11.Visible = (curLab.lineType != 2);
            label12.Visible = true;
            buttonsave.Visible = false;
            buttonClear.Visible = true;
        }

        //Сохранение параметров
        private void buttonsave_Click(object sender, EventArgs e)
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
            //Сохранение
            l.Save();
            buttonsave.Visible = false;
            Net.Log("Изменение параметров ТПА на терминале");
        }

        //Делаем кнопку сохранения видимой
        void MakeSaveEnable()
        {
            if (comboBoxTPA.SelectedIndex >= 0)
                buttonsave.Visible = true;
        }

        //При изменении любого поля делаем кнопку сохранения видимой
        private void comboBoxCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxFirstBox_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxLimit_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxAntiCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }

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

        //Кнопка выбора принтера
        private void buttonPrinterSelect_Click(object sender, EventArgs e)
        {
            Data.PrintSetup();
        }

        //Кнопка отчётов
        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (!Data.PrintSelected()) Data.PrintSetup();
            if (Data.PrintSelected())
            {
                FormReports rep = new FormReports();
                rep.ShowDialog();
            }
        }

        //Кнопка пользователей
        private void buttonUsers_Click(object sender, EventArgs e)
        {
            FormUsers form = new FormUsers();
            form.ShowDialog();
        }

        //Строка прочих дополнений
        private void comboBoxOther_Click(object sender, EventArgs e)
        {
            FormKeyboardLetter key = new FormKeyboardLetter("Введите прочие дополнения");
            if (key.ShowDialog() == DialogResult.OK)
            {
                comboBoxOther.Text = key.Str;
                MakeSaveEnable();
            }
        }

        //Строчка номера партии
        private void textBoxNumber_Click_1(object sender, EventArgs e)
        {
            FormKeyboardNums key = new FormKeyboardNums("Введите номер партии");
            if (key.ShowDialog() == DialogResult.OK)
            {
                textBoxNumber.Text = key.Str;
                MakeSaveEnable();
            }
        }

        //Строчка номера короба
        private void textBoxBox_Click(object sender, EventArgs e)
        {
            FormKeyboardNums key = new FormKeyboardNums("Введите номер короба");
            if (key.ShowDialog() == DialogResult.OK)
            {
                textBoxBox.Text = key.Str;
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
            comboBoxAntistatic.SelectedItem = "";
            comboBoxColorants.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = "";
        }

        //Кнопка "о программе"
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            Program.About();
        }
    }
}
