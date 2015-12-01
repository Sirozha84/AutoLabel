using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormProperties : Form
    {
        public FormProperties()
        {
            InitializeComponent();
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonquitprogram_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormProperties_Load(object sender, EventArgs e)
        {
            //Заполнение комбобоксов списками
            comboBoxType.DataSource = Data.Types;
            comboBoxWeight.DataSource = Data.Weights;
            comboBoxCount.DataSource = Data.Counts;
            buttonsave.Enabled = false;
        }

        private void comboBoxTPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (comboBoxTPA.SelectedIndex<3)
                comboBoxCount.SelectedItem = "0";
            else
                comboBoxCount.SelectedItem = "10896";*/
            buttonsave.Enabled = false;
        }


        void MakeSaveEnable()
        {
            buttonsave.Enabled = true;
        }

        private void comboBoxCount_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxFirstBox_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxNumber_TextChanged(object sender, EventArgs e) { MakeSaveEnable(); }
        private void textBoxNumber_Click(object sender, EventArgs e)
        {
            FormKeyboardNums key = new FormKeyboardNums("Введите номер партии");
            key.ShowDialog();
            textBoxNumber.Text = "0 тест";
        }

        private void textBoxFirstBox_Click(object sender, EventArgs e)
        {
            FormKeyboardNums key = new FormKeyboardNums("Введите номер первой коробки");
            key.ShowDialog();
            textBoxFirstBox.Text = "1 тест";
        }
    }
}
