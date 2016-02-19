using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormKey : Form
    {
        public string Code;
        int s = 0;
        int timer = 60;    //Таймер для автоматического закрытия

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer--;
            if (timer <= 10) buttonCancel.Text = "Отмена (" + timer.ToString() + ")";
            if (timer == 0) Close();
        }
    }
}
