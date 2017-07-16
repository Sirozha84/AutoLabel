using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormError : Form
    {
        public FormError()
        {
            InitializeComponent();
        }

        public FormError(string message)
        {
            InitializeComponent();
            label1.Text = message;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
