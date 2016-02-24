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
            Data.UsersLoad();
            //Заполнение комбобоксов списками
            Data.Users.ForEach(u => comboBoxUser.Items.Add(u.Name));
            comboBoxUser.SelectedItem = null;
            numericUpDownCount.Maximum = Data.MaxLabels;
            foreach (Label l in Data.Labels)
                comboBoxTPA.Items.Add(l.TPAName);
            textBoxNumber.Text = "";
            comboBoxType.DataSource = Data.Types;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.DataSource = Data.Materials;
            comboBoxMaterial.SelectedItem = null;
            ListFill();
            comboBoxAntistatic.DataSource = Data.Antistatics;
            comboBoxAntistatic.SelectedItem = "";
            comboBoxColorant.DataSource = Data.Colorants;
            comboBoxColorant.SelectedItem = null;
            comboBoxLimit.DataSource = Data.Limits;
            comboBoxLimit.SelectedItem = null;
        }

        void ListFill()
        {
            if (TPAType == 0) comboBoxWeight.DataSource = Data.Weights0;
            else comboBoxWeight.DataSource = Data.Weights1;
            comboBoxWeight.SelectedItem = null;
            if (TPAType == 0) comboBoxColor.DataSource = Data.Colors0;
            else comboBoxColor.DataSource = Data.Colors1;
            comboBoxColor.SelectedItem = null;
            if (TPAType == 0) comboBoxCount.DataSource = Data.Quantitys0;
            else comboBoxCount.DataSource = Data.Quantitys1;
            comboBoxCount.SelectedItem = null;
        }

        //Кнопка заполнения полей из ТПА
        private void button1_Click(object sender, EventArgs e)
        {
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];
            if (l.Weight != "") comboBoxWeight.SelectedItem = l.Weight; else comboBoxWeight.SelectedItem = null;
            if (l.Material != "") comboBoxMaterial.SelectedItem = l.Material; else comboBoxMaterial.SelectedItem = null;
            if (l.PColor != "") comboBoxColor.SelectedItem = l.PColor; else comboBoxColor.SelectedItem = null;
            if (l.Count != "") comboBoxCount.SelectedItem = l.Count; else comboBoxCount.SelectedItem = null;
            if (l.Type != "") comboBoxType.SelectedItem = l.Type; else comboBoxType.SelectedItem = null;
            textBoxNumber.Text = l.PartNum;
            if (l.AntistaticType != "") comboBoxAntistatic.SelectedItem = l.AntistaticType; else comboBoxAntistatic.SelectedItem = null;
            if (l.AntistaticCount != "") comboBoxColorant.SelectedItem = l.AntistaticCount; else comboBoxColorant.SelectedItem = null;
            if (l.Limit != "") comboBoxLimit.SelectedItem = l.Limit; else comboBoxLimit.SelectedItem = null;
            textBoxOther.Text = l.Other;
            textBoxBoxNum.Text = l.CurrentNum.ToString();
            if (l.AllowSelectCount()) numericUpDownCount.Value = 1;
            else numericUpDownCount.Value = 0;
            textBoxDate.Text = Data.DateToString();
            textBoxTime.Text = DateTime.Now.ToString("HH:mm");
            comboBoxShift.SelectedItem = Data.Shift;
        }

        //Кнопка очистки
        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxTPA.SelectedItem = null;
            comboBoxWeight.SelectedItem = null;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.SelectedItem = null;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.SelectedItem = null;
            textBoxNumber.Text = "";
            comboBoxAntistatic.SelectedItem = "";
            comboBoxColorant.SelectedItem = null;
            comboBoxLimit.SelectedItem = null;
            textBoxOther.Text = "";
            textBoxBoxNum.Text = "";
            comboBoxUser.SelectedItem = null;
            textBoxDate.Text = "";
            textBoxTime.Text = "";
            comboBoxShift.SelectedItem = null;
            button1.Enabled = false;
        }

        //Комбобокс выбора ТПА
        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            TPAType = 0;
            if (comboBoxTPA.SelectedIndex > 5) TPAType = 1;
            label3.Enabled = (TPAType == 0);
            comboBoxType.Enabled = (TPAType == 0);
            label6.Enabled = (TPAType == 0);
            comboBoxMaterial.Enabled = (TPAType == 0);
            label8.Enabled = (TPAType == 0);
            comboBoxAntistatic.Enabled = (TPAType == 0);
            label9.Enabled = (TPAType == 0);
            comboBoxColorant.Enabled = (TPAType == 0);
            label10.Enabled = (TPAType == 0);
            comboBoxLimit.Enabled = (TPAType == 0);
            if (TPAType == 0) label11.Text = "Прочие дополнения:";
            else label11.Text = "Код:";
            ListFill();
        }

        //КНопка печати
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Label l = new Label(comboBoxTPA.SelectedItem.ToString(), 0);

                //Временно сделаю так... надо подумать как сделать лучше
                if (comboBoxTPA.SelectedIndex > 5) l.TPAType = 1;

                if (comboBoxWeight.SelectedItem != null) l.Weight = comboBoxWeight.SelectedItem.ToString();
                if (comboBoxType.SelectedItem != null) l.Type = comboBoxType.SelectedItem.ToString(); else l.Type = "";
                if (comboBoxMaterial.SelectedItem != null) l.Material = comboBoxMaterial.SelectedItem.ToString(); else l.Material = "";
                if (comboBoxColor.SelectedItem != null) l.PColor = comboBoxColor.SelectedItem.ToString(); else l.PColor = "";
                if (comboBoxCount.SelectedItem != null) l.Count = comboBoxCount.SelectedItem.ToString(); else l.Count = "";
                l.PartNum = textBoxNumber.Text;
                if (comboBoxAntistatic.SelectedItem != null) l.AntistaticType = comboBoxAntistatic.SelectedItem.ToString(); else l.AntistaticType = "";
                if (comboBoxColorant.SelectedItem != null) l.AntistaticCount = comboBoxColorant.SelectedItem.ToString(); else l.AntistaticCount = "";
                if (comboBoxLimit.SelectedItem != null) l.Limit = comboBoxLimit.SelectedItem.ToString(); else l.Limit = "";
                l.Other = textBoxOther.Text;
                l.Print(Convert.ToInt32(textBoxBoxNum.Text), comboBoxUser.SelectedItem.ToString(),
                    (int)numericUpDownCount.Value, textBoxDate.Text, textBoxTime.Text,
                    comboBoxShift.SelectedItem.ToString());
            }
            catch
            {
                MessageBox.Show("Не все поля правильно заполнены.");
            }
        }
    }
}
