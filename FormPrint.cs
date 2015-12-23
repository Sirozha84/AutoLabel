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
        bool CostomNum = true; //Можно ли менять номер вручную?

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
                if (user == null)
                {
                    if (key.Code != "")
                    {
                        FormError err = new FormError();
                        err.ShowDialog();
                    }
                    Close();
                }
                else
                {
                    comboBoxUser.SelectedItem = user.Name;
                    //Если это упаковщик - замораживаем комбобокс и возможность выбрать номер
                    if (user.Rule == 1)
                    {
                        comboBoxUser.Enabled = false;
                        CostomNum = false;
                    }
                    buttonPrint.Visible = true;
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

        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonPrint.Visible = true;
        }

        private void textBoxNum_Click(object sender, EventArgs e)
        {
            if (!CostomNum) return;
            //Нам разрешено поменять номер короба вручную
            FormKeyboardNums key = new FormKeyboardNums("Введите номер короба");
            key.ShowDialog();
            if (key.DialogResult == DialogResult.OK)
            {
                Data.Labels[NumMachine].CurrentNum = Convert.ToInt32(key.Str);
                box = Data.Labels[NumMachine].CurrentNum;
                DrawNum();
            }
        }
    }
}
