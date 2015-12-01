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

        public FormPrint()
        {
            InitializeComponent();
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            labelNum.Text = "ТПА: " + (NumMachine + 1).ToString();
            LabelPacker.Text = "Упаковщик: Иванов И. И.";
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ТПА:\t\t" + (NumMachine + 1).ToString() +
                "\nУпаковщик:\tИванов И. И." +
                "\nНомер короба:\t0" + 
                "\nДата и время:\t"+DateTime.Now.ToString());
            //Далее, если печатался максимальный номер, увеличиваем его и выходим
            this.Close();
        }
    }
}
