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
            Data.users.ForEach(u => comboBoxUser.Items.Add(u.Name));
            comboBoxUser.SelectedItem = null;
            numericUpDownCount.Maximum = Data.MaxLabels(comboBoxTPA.SelectedIndex);
            foreach (Line l in Data.lines)
                comboBoxTPA.Items.Add(l.name);
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
            Line l = Data.lines[comboBoxTPA.SelectedIndex];
            if (l.weight != "") comboBoxWeight.SelectedItem = l.weight; else comboBoxWeight.SelectedItem = null;
            if (l.material != "") comboBoxMaterial.SelectedItem = l.material; else comboBoxMaterial.SelectedItem = null;
            if (l.color != "") comboBoxColor.SelectedItem = l.color; else comboBoxColor.SelectedItem = null;
            if (l.count != "") comboBoxCount.SelectedItem = l.count; else comboBoxCount.SelectedItem = null;
            if (l.type != "") comboBoxType.SelectedItem = l.type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = l.partNum;
            if (l.antistatic != "") comboBoxAntistatic.SelectedItem = l.antistatic; else comboBoxAntistatic.SelectedItem = null;
            if (l.colorant != "") comboBoxColorant.SelectedItem = l.colorant; else comboBoxColorant.SelectedItem = null;
            if (l.life != "") comboBoxLimit.SelectedItem = l.life; else comboBoxLimit.SelectedItem = null;
            comboBoxOther.Text = l.addition;
            textBoxBoxNum.Text = l.boxNum.ToString();
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
            TPAType = Data.lines[comboBoxTPA.SelectedIndex].lineType;
            label4.Text = Data.WeightOrLogo(Data.lines[comboBoxTPA.SelectedIndex]);

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
                Line l = new Line(comboBoxTPA.SelectedItem.ToString(), 0, true);

                //Выбираем какую этикетку печатаем
                if (comboBoxTPA.SelectedIndex >= Data.firstType1 & comboBoxTPA.SelectedIndex <= Data.lastType1) l.lineType = 1;
                if (comboBoxTPA.SelectedIndex >= Data.firstType2 & comboBoxTPA.SelectedIndex <= Data.lastType2) l.lineType = 2;

                if (comboBoxWeight.SelectedItem != null) l.weight = comboBoxWeight.SelectedItem.ToString();
                if (comboBoxType.SelectedItem != null) l.type = comboBoxType.SelectedItem.ToString(); else l.type = "";
                if (comboBoxMaterial.SelectedItem != null) l.material = comboBoxMaterial.SelectedItem.ToString(); else l.material = "";
                if (comboBoxColor.SelectedItem != null) l.color = comboBoxColor.SelectedItem.ToString(); else l.color = "";
                if (comboBoxCount.SelectedItem != null) l.count = comboBoxCount.SelectedItem.ToString(); else l.count = "";
                l.partNum = textBoxNumber.Text;
                if (comboBoxAntistatic.SelectedItem != null) l.antistatic = comboBoxAntistatic.SelectedItem.ToString(); else l.antistatic = "";
                if (comboBoxColorant.SelectedItem != null) l.colorant = comboBoxColorant.SelectedItem.ToString(); else l.colorant = "";
                if (comboBoxLimit.SelectedItem != null) l.life = comboBoxLimit.SelectedItem.ToString(); else l.life = "";
                l.addition = comboBoxOther.Text;
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
