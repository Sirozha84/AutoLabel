using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormCustomLabel : Form
    {
        public int TPAType;

        public FormCustomLabel()
        {
            InitializeComponent();
            FormLoadSplash form = new FormLoadSplash("Загрузка...");
            form.Show();
            Application.DoEvents();
            Data.UsersLoad();
            Data.ListsLoad();
            //Заполнение комбобоксов списками
            Data.Users.ForEach(u => comboBoxUser.Items.Add(u.Name));
            comboBoxUser.SelectedItem = null;
            numericUpDownCount.Maximum = Data.MaxLabels(comboBoxTPA.SelectedIndex);
            foreach (Label l in Data.Labels)
                comboBoxTPA.Items.Add(l.TPAName);
            textBoxNumber.Text = "";
            ListFill();
            comboBoxLimit.DataSource = Data.Limits;
            comboBoxLimit.SelectedItem = null;
            form.Close();
        }

        void ListFill()
        {
            if (TPAType == 0)
            {
                comboBoxType.DataSource = Data.Types0;
                comboBoxWeight.DataSource = Data.Weights0;
                comboBoxColor.DataSource = Data.Colors0;
                comboBoxCount.DataSource = Data.Quantitys0;
                comboBoxMaterial.DataSource = Data.Materials0;
                comboBoxAntistatic.DataSource = Data.Antistatics0;
                comboBoxColorant.DataSource = Data.Colorants0;
                comboBoxOther.DataSource = Data.Others;
            }
            if (TPAType == 1)
            {
                comboBoxType.DataSource = Data.Types1;
                //comboBoxWeight.DataSource = Data.Weights1;
                comboBoxWeight.DataSource = StaticDir.KolpakWeights(comboBoxTPA.Text);
                comboBoxColor.DataSource = Data.Colors1;
                comboBoxCount.DataSource = Data.Quantitys1;
                comboBoxMaterial.DataSource = Data.Materials1;
                comboBoxAntistatic.DataSource = Data.Antistatics1;
                comboBoxColorant.DataSource = Data.Colorants1;
            }
            if (TPAType == 2)
            {
                comboBoxWeight.DataSource = Data.Weights2;
                comboBoxColor.DataSource = Data.Colors2;
            }
            comboBoxColorant.SelectedItem = null;
            comboBoxAntistatic.SelectedItem = "";
            comboBoxMaterial.SelectedItem = null;
            comboBoxType.SelectedItem = null;
            comboBoxWeight.SelectedItem = null;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.SelectedItem = null;
            comboBoxOther.SelectedItem = null;
        }

        //Кнопка заполнения полей из ТПА
        private void FillFromTPA()
        {
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];
            if (l.Weight != "") comboBoxWeight.SelectedItem = l.Weight; else comboBoxWeight.SelectedItem = null;
            if (l.Material != "") comboBoxMaterial.SelectedItem = l.Material; else comboBoxMaterial.SelectedItem = null;
            if (l.PColor != "") comboBoxColor.SelectedItem = l.PColor; else comboBoxColor.SelectedItem = null;
            if (l.Count != "") comboBoxCount.SelectedItem = l.Count; else comboBoxCount.SelectedItem = null;
            if (l.Type != "") comboBoxType.SelectedItem = l.Type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = l.PartNum;
            if (l.Antistatic != "") comboBoxAntistatic.SelectedItem = l.Antistatic; else comboBoxAntistatic.SelectedItem = null;
            if (l.Colorant != "") comboBoxColorant.SelectedItem = l.Colorant; else comboBoxColorant.SelectedItem = null;
            if (l.Limit != "") comboBoxLimit.SelectedItem = l.Limit; else comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = l.Other;
            textBoxBoxNum.Text = l.CurrentNum.ToString();
            if (l.AllowSelectCount()) numericUpDownCount.Value = 1;
            else numericUpDownCount.Value = 0;
            textBoxDate.Text = Shift.Date;
            textBoxTime.Text = DateTime.Now.ToString("HH:mm");
            comboBoxShift.SelectedItem = Shift.Current;
        }

        //Кнопка очистки
        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxWeight.SelectedItem = null;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.SelectedItem = null;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.SelectedItem = null;
            textBoxNumber.Text = "";
            comboBoxAntistatic.SelectedItem = "";
            comboBoxColorant.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = null;
            textBoxBoxNum.Text = "";
            comboBoxUser.SelectedItem = null;
            textBoxDate.Text = "";
            textBoxTime.Text = "";
            comboBoxShift.SelectedItem = null;
        }

        //Комбобокс выбора ТПА
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            TPAType = Data.Labels[comboBoxTPA.SelectedIndex].TPAType;
            label4.Text = Data.WeightOrLogo(Data.Labels[comboBoxTPA.SelectedIndex]);

            //Видимость полей
            comboBoxType.Enabled = (TPAType != 2);
            comboBoxMaterial.Enabled = (TPAType != 2);
            comboBoxCount.Enabled = (TPAType != 2);
            textBoxNumber.Enabled = (TPAType != 2);
            comboBoxAntistatic.Enabled = (TPAType != 2);
            comboBoxColorant.Enabled = (TPAType != 2);
            comboBoxLimit.Enabled = (TPAType == 0);
            comboBoxOther.Enabled = (TPAType != 2);
            textBoxTime.Enabled = (TPAType != 2);

            //Видимость подписей полей
            label3.Enabled = (TPAType != 2);
            label6.Enabled = (TPAType != 2);
            label12.Enabled = (TPAType != 2);
            label2.Enabled = (TPAType != 2);
            label8.Enabled = (TPAType != 2);
            label9.Enabled = (TPAType != 2);
            label10.Enabled = (TPAType == 0);
            label11.Enabled = (TPAType != 2);
            label14.Enabled = (TPAType != 2);

            ListFill();
            FillFromTPA();
            numericUpDownCount.Maximum = Data.MaxLabels(comboBoxTPA.SelectedIndex);
        }

        //Кнопка печати
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Label l = new Label(comboBoxTPA.SelectedItem.ToString(), 0);

                //Выбираем какую этикетку печатаем
                if (comboBoxTPA.SelectedIndex >= 6 & comboBoxTPA.SelectedIndex <= 7) l.TPAType = 1;
                if (comboBoxTPA.SelectedIndex >= 8) l.TPAType = 2;

                if (comboBoxWeight.SelectedItem != null) l.Weight = comboBoxWeight.SelectedItem.ToString();
                if (comboBoxType.SelectedItem != null) l.Type = comboBoxType.SelectedItem.ToString(); else l.Type = "";
                if (comboBoxMaterial.SelectedItem != null) l.Material = comboBoxMaterial.SelectedItem.ToString(); else l.Material = "";
                if (comboBoxColor.SelectedItem != null) l.PColor = comboBoxColor.SelectedItem.ToString(); else l.PColor = "";
                if (comboBoxCount.SelectedItem != null) l.Count = comboBoxCount.SelectedItem.ToString(); else l.Count = "";
                l.PartNum = textBoxNumber.Text;
                if (comboBoxAntistatic.SelectedItem != null) l.Antistatic = comboBoxAntistatic.SelectedItem.ToString(); else l.Antistatic = "";
                if (comboBoxColorant.SelectedItem != null) l.Colorant = comboBoxColorant.SelectedItem.ToString(); else l.Colorant = "";
                if (comboBoxLimit.SelectedItem != null) l.Limit = comboBoxLimit.SelectedItem.ToString(); else l.Limit = "";
                l.Other = comboBoxOther.Text;
                l.Print(Convert.ToInt32(textBoxBoxNum.Text), comboBoxUser.SelectedItem.ToString(),
                    (int)numericUpDownCount.Value, textBoxDate.Text, textBoxTime.Text,
                    comboBoxShift.SelectedItem.ToString(), true);
            }
            catch
            {
                MessageBox.Show("Не все поля правильно заполнены.");
            }
        }
    }
}
