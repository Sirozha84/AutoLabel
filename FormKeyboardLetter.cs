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
    public partial class FormKeyboardLetter : Form
    {
        public FormKeyboardLetter(string Lbl)
        {
            InitializeComponent();
            label1.Text = Lbl;
            Str = "";
        }

        public string Str;

        public FormKeyboardLetter()
        {
            InitializeComponent();
        }


        void AddLetter(string l)
        {
            if (Big())
                Str += l.ToUpper();
            else
                Str += l;
            textBoxEdit.Text = Str;
        }

        private void button7_Click(object sender, EventArgs e) { AddLetter("й"); }
        private void button8_Click(object sender, EventArgs e) { AddLetter("ц"); }
        private void button9_Click(object sender, EventArgs e) { AddLetter("у"); }
        private void button12_Click(object sender, EventArgs e) { AddLetter("к"); }
        private void button16_Click(object sender, EventArgs e) { AddLetter("е"); }
        private void button17_Click(object sender, EventArgs e) { AddLetter("н"); }
        private void button13_Click(object sender, EventArgs e) { AddLetter("г"); }
        private void button10_Click(object sender, EventArgs e) { AddLetter("ш"); }
        private void button21_Click(object sender, EventArgs e) { AddLetter("щ"); }
        private void button22_Click(object sender, EventArgs e) { AddLetter("з"); }
        private void button24_Click(object sender, EventArgs e) { AddLetter("х"); }
        private void button20_Click(object sender, EventArgs e) { AddLetter("ъ"); }
        private void button4_Click(object sender, EventArgs e) { AddLetter("ф"); }
        private void button5_Click(object sender, EventArgs e) { AddLetter("ы"); }
        private void button6_Click(object sender, EventArgs e) { AddLetter("в"); }
        private void button19_Click(object sender, EventArgs e) { AddLetter("а"); }
        private void button23_Click(object sender, EventArgs e) { AddLetter("п"); }
        private void button18_Click(object sender, EventArgs e) { AddLetter("р"); }
        private void button26_Click(object sender, EventArgs e) { AddLetter("о"); }
        private void button25_Click(object sender, EventArgs e) { AddLetter("л"); }
        private void button27_Click(object sender, EventArgs e) { AddLetter("д"); }
        private void button33_Click(object sender, EventArgs e) { AddLetter("ж"); }
        private void button29_Click(object sender, EventArgs e) { AddLetter("э"); }
        private void button1_Click(object sender, EventArgs e) { AddLetter(" "); }
        private void button2_Click(object sender, EventArgs e) { AddLetter("я"); }
        private void button3_Click(object sender, EventArgs e) { AddLetter("ч"); }
        private void button28_Click(object sender, EventArgs e) { AddLetter("с"); }
        private void button14_Click(object sender, EventArgs e) { AddLetter("м"); }
        private void button30_Click(object sender, EventArgs e) { AddLetter("и"); }
        private void button15_Click(object sender, EventArgs e) { AddLetter("т"); }
        private void button31_Click(object sender, EventArgs e) { AddLetter("ь"); }
        private void button11_Click(object sender, EventArgs e) { AddLetter("б"); }
        private void button32_Click(object sender, EventArgs e) { AddLetter("ю"); }
        private void button34_Click(object sender, EventArgs e) { AddLetter("."); }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (Str.Length == 0) DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        bool Big()
        {
            if (Str.Length == 0) return true;
            return Str[Str.Length - 1] == ' ' | Str[Str.Length - 1] == '.';
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (Str.Length == 0) return;
            Str = Str.Substring(0, Str.Length - 1);
            textBoxEdit.Text = Str;
        }
    }
}
