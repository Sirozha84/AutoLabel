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
            //Заполним комбобокс пользователями
            comboBoxUser.Items.Clear();
            foreach (User u in Data.Users) comboBoxUser.Items.Add(u.Name);
            box = Data.Labels[NumMachine].CurrentNum;
            DrawNum();
            if (Data.UseKeys)
            {
                FormKey key = new FormKey();
                key.ShowDialog();
                if (key.Code == "") Close();
                //Далее план такой: ищем в базе ключ, если его нет - уходим
                User user = Data.Users.Find(u => u.Code == key.Code);
                //Если находим - выводим в комбобокс
                if (user == null) Close();
                else
                {
                    //Выбираем нужного в комбобоксе
                    comboBoxUser.SelectedItem = user.Name;
                    //Если это упаковщик - замораживаем комбобокс
                    if (user.Rule == 0) comboBoxUser.Enabled = false;
                }
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            Data.Labels[NumMachine].Print(box, comboBoxUser.SelectedItem.ToString());
            Close();
        }

        //Кнопка меньше
        private void buttonDec_Click(object sender, EventArgs e)
        {
            if (box > 1)
            {
                box--;
                DrawNum();
            }
        }

        //Кнопка последняя
        private void buttonMax_Click(object sender, EventArgs e)
        {
            box = Data.Labels[NumMachine].CurrentNum;
            DrawNum();
        }

        //Рисование номера
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
