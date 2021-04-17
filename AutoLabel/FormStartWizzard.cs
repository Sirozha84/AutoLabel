using System;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace AutoLabel
{
    public partial class FormStartWizzard : Form
    {
        public FormStartWizzard()
        {
            InitializeComponent();
            textBoxServer.Text = "Localhost";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (new PrinterSettings() { PrinterName = printer }.IsDefaultPrinter)
                    textBoxPrinter.Text = printer;//.ToString();
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = textBoxServer.Text != "";
        }
        private void buttonPrnSel_Click(object sender, EventArgs e)
        {
            PrintDialog diag = new PrintDialog();
            if (diag.ShowDialog() == DialogResult.Cancel) return;
            textBoxPrinter.Text = diag.PrinterSettings.PrinterName;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.server = textBoxServer.Text;
            Settings.printer = textBoxPrinter.Text;
            Settings.Save();
            DialogResult = DialogResult.OK;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            
        }
    }
}
