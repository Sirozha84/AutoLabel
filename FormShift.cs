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
    public partial class FormShift : Form
    {
        public FormShift()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Data.ShiftChange(Data.Shifts[0]);
            Close();
        }

        private void buttonShift2_Click(object sender, EventArgs e)
        {
            Data.ShiftChange(Data.Shifts[1]);
            Close();
        }

        private void buttonShift3_Click(object sender, EventArgs e)
        {
            Data.ShiftChange(Data.Shifts[2]);
            Close();
        }

        private void buttonShift4_Click(object sender, EventArgs e)
        {
            Data.ShiftChange(Data.Shifts[3]);
            Close();
        }
    }
}
