using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormProperties : Form
    {
        public FormProperties()
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
            Log.Write("Выход из программы (через параметры)");
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
                comboBoxOther.DataSource = Data.Others;
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
            }
            if (l.TPAType == 2)
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
            comboBoxWeight.Visible = true;
            comboBoxType.Visible = (l.TPAType != 2);
            comboBoxType.Enabled = (l.TPAType == 0);
            comboBoxMaterial.Visible = (l.TPAType != 2);
            comboBoxColor.Visible = true;
            comboBoxCount.Visible = (l.TPAType != 2);
            textBoxNumber.Visible = (l.TPAType != 2);
            comboBoxAntistatic.Visible = (l.TPAType != 2);
            comboBoxColorants.Visible = true;
            comboBoxLimit.Visible = (l.TPAType == 0);
            comboBoxOther.Visible = (l.TPAType != 2);
            textBoxBox.Visible = true;

            //Видимость подписей полей
            label3.Visible = true;
            label2.Visible = (l.TPAType != 2);
            label6.Visible = (l.TPAType != 2);
            label7.Visible = true;
            label4.Visible = (l.TPAType != 2);
            label1.Visible = (l.TPAType != 2);
            label8.Visible = (l.TPAType != 2);
            label9.Visible = true;
            label10.Visible = (l.TPAType == 0);
            label11.Visible = (l.TPAType != 2);
            label12.Visible = true;
            buttonsave.Visible = false;
            buttonClear.Visible = true;
        }

        //Сохранение параметров
        private void buttonsave_Click(object sender, EventArgs e)
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
        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeSaveEnable();
            comboBoxType.SelectedItem = Data.Conformity(comboBoxWeight.Text);
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
