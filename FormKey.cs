using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormKey : Form
    {
        public string Code;
        int s = 0;

        public FormKey()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Code = "";
            Close();
        }

        private void buttonCancel_KeyDown(object sender, KeyEventArgs e)
        {
            s++;
            Code += e.KeyValue.ToString();
            if (s > 7) Close();
        }

    }
}
