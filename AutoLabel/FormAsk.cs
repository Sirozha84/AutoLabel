using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormAsk : Form
    {
        public FormAsk(string question)
        {
            InitializeComponent();
            labelQuestion.Text = question;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
