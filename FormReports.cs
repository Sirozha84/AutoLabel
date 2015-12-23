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
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            foreach (string sh in Data.LogName)
                comboBoxShift.Items.Add(sh);
            comboBoxShift.SelectedIndex = 0;
        }
    }
}
