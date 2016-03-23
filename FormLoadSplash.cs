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
    public partial class FormLoadSplash : Form
    {
        public FormLoadSplash()
        {
            InitializeComponent();
        }

        public FormLoadSplash(string message)
        {
            InitializeComponent();
            timer1.Enabled = false;
            label1.Text = message;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Data.Loading) Close();
        }
    }
}
