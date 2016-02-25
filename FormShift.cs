using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormShift : Form
    {
        public FormShift()
        {
            InitializeComponent();
        }

        //Кнопка отмены
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка смены 1
        private void buttonShift1_Click(object sender, EventArgs e)
        {
            Shift.Change(Shift.Names[0]);
            Close();
        }

        //Кнопка смены 2
        private void buttonShift2_Click(object sender, EventArgs e)
        {
            Shift.Change(Shift.Names[1]);
            Close();
        }

        //Кнопка смены 3
        private void buttonShift3_Click(object sender, EventArgs e)
        {
            Shift.Change(Shift.Names[2]);
            Close();
        }

        //Кнопка смены 4
        private void buttonShift4_Click(object sender, EventArgs e)
        {
            Shift.Change(Shift.Names[3]);
            Close();
        }
    }
}
