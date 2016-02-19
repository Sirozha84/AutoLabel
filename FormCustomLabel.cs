using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormCustomLabel : Form
    {
        public FormCustomLabel()
        {
            InitializeComponent();
            Data.UsersLoad();
            //Заполнение комбобоксов списками
            foreach (Label l in Data.Labels)
                comboBoxTPA.Items.Add(l.TPAName);
            textBoxNumber.Text = "";
            comboBoxWeight.DataSource = Data.Weights0;
            comboBoxWeight.SelectedItem = null;
            comboBoxType.DataSource = Data.Types;
            comboBoxType.SelectedItem = null;
            comboBoxMaterial.DataSource = Data.Materials;
            comboBoxMaterial.SelectedItem = null;
            comboBoxColor.DataSource = Data.Colors0;
            comboBoxColor.SelectedItem = null;
            comboBoxCount.DataSource = Data.Quantitys0;
            comboBoxCount.SelectedItem = null;
            comboBoxAntiType.DataSource = Data.Antistatics;
            comboBoxAntiType.SelectedItem = "";
            comboBoxAntiCount.DataSource = Data.Colorants;
            comboBoxAntiCount.SelectedItem = null;
            comboBoxLimit.DataSource = Data.Limits;
            comboBoxLimit.SelectedItem = null;
            Data.Users.ForEach(u => comboBoxUser.Items.Add(u.Name));
            comboBoxUser.SelectedItem = null;
            numericUpDownCount.Maximum = Data.MaxLabels;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Заполнение полей из ТПА
            Label l = Data.Labels[comboBoxTPA.SelectedIndex];
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
            if (Data.LittleBox(l.Count)) numericUpDownCount.Value = 1;
            else numericUpDownCount.Value = 0;
            textBoxDate.Text = Data.DateToString();
            textBoxTime.Text = DateTime.Now.ToString("hh:mm");
            comboBoxShift.SelectedItem = Data.Shift;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxTPA.SelectedItem = null;
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
            textBoxBoxNum.Text = "";
            comboBoxUser.SelectedItem = null;
            textBoxDate.Text = "";
            textBoxTime.Text = "";
            comboBoxShift.SelectedItem = null;
            button1.Enabled = false;
        }

        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //Печать
            try
            {
                Label l = new Label(comboBoxTPA.SelectedItem.ToString(), 0);
                if (comboBoxWeight.SelectedItem != null) l.Weight = comboBoxWeight.SelectedItem.ToString();
                if (comboBoxType.SelectedItem != null) l.Type = comboBoxType.SelectedItem.ToString(); else l.Type = "";
                if (comboBoxMaterial.SelectedItem != null) l.Material = comboBoxMaterial.SelectedItem.ToString(); else l.Material = "";
                if (comboBoxColor.SelectedItem != null) l.PColor = comboBoxColor.SelectedItem.ToString(); else l.PColor = "";
                if (comboBoxCount.SelectedItem != null) l.Count = comboBoxCount.SelectedItem.ToString(); else l.Count = "";
                l.PartNum = textBoxNumber.Text;
                if (comboBoxAntiType.SelectedItem != null) l.AntistaticType = comboBoxAntiType.SelectedItem.ToString(); else l.AntistaticType = "";
                if (comboBoxAntiCount.SelectedItem != null) l.AntistaticCount = comboBoxAntiCount.SelectedItem.ToString(); else l.AntistaticCount = "";
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
