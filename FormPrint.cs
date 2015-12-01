using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormPrint : Form
    {
        public int NumMachine;
        int box;
        public FormPrint()
        {
            InitializeComponent();
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            labelNum.Text = "ТПА: " + (NumMachine + 1).ToString();
            LabelPacker.Text = "Упаковщик: Иванов И. И.";
            box = Data.Labels[NumMachine].CurrentNum;
            DrawNum();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            Data.Labels[NumMachine].Print(box);
            Close();
        }

        private void buttonDec_Click(object sender, EventArgs e)
        {
            if (box > 1)
            {
                box--;
                DrawNum();
            }
        }

        private void buttonMax_Click(object sender, EventArgs e)
        {
            box = Data.Labels[NumMachine].CurrentNum;
            DrawNum();
        }

        void DrawNum()
        {
            textBoxNum.Text = box.ToString();
            if (box < Data.Labels[NumMachine].CurrentNum)
            {
                textBoxNum.ForeColor = Color.Red;
                buttonMax.Visible = true;
            }
            else
            {
                textBoxNum.ForeColor = Color.White;
                buttonMax.Visible = false;
            }
            buttonDec.Visible = box > 1;
        }
    }
}
