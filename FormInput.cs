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
    public partial class FormInput: Form
    {
        public string Str;

        public FormInput()
        {
            InitializeComponent();
        }

        public FormInput(string Query)
        {
            InitializeComponent();
            label1.Text = Query;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Str = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
