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
    public partial class FormKeyboardNums : Form
    {
        public string Str = "";

        public FormKeyboardNums(string str)
        {
            //label = str;
            InitializeComponent();
            label1.Text = str;
        }

        public FormKeyboardNums()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Str = textBoxEdit.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxEdit.Text += "9";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBoxEdit.Text.Length < 1) return;
            textBoxEdit.Text = textBoxEdit.Text.Substring(0, textBoxEdit.Text.Length - 1);
        }

        private void textBoxEdit_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
