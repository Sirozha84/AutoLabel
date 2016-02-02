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
        public int NumMachine;  //Номер ТПА
        int box;                //Номер короба
        int count = 1;          //Количество коробов
        bool CustomNum = true;  //Можно ли менять номер вручную?
        bool CountSelect = false;   //Выбираем ли мы количество коробов?

        public FormPrint()
        {
            InitializeComponent();
            Data.LoadUsers();
            //Режим для ПК
            if (!Data.IsMachine)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                StartPosition = FormStartPosition.CenterParent;
            }
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
            if (Data.IsMachine)
            {
                FormKey key = new FormKey();
                key.ShowDialog();
                if (key.Code == "")
                {
                    //Маленько кривинько, но стираем сформированный список и заполняем его только гостями
                    comboBoxUser.Items.Clear();
                    foreach (User u in Data.Users) if (u.Code == "") comboBoxUser.Items.Add(u.Name);
                    CustomNum = false;
                }
                else
                {
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
                            CustomNum = false;
                        }
                        buttonPrint.Visible = true;
                    }
                }
            }
            //Далее надо выяснить мелкие это коробки или крупные, и в зависимости от этого вывести второй нумератор
            if (Data.LittleBox(NumMachine))
            {
                label2.Visible = true;
                textBoxCount.Visible = true;
                buttonCountDec.Visible = true;
                buttonCountInc.Visible = true;
                CountSelect = true;
            }
            DrawNum();
        }

        //Кнопка печати
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            int c = 0;
            if (CountSelect) c = count;
            Data.Labels[NumMachine].Print(box, comboBoxUser.SelectedItem.ToString(), c);
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
            //Номер короба
            textBoxNum.Text = box.ToString();
            if (box < Data.Labels[NumMachine].CurrentNum)
            {
                textBoxNum.ForeColor = Color.Tomato;
                buttonMax.Visible = true;
            }
            else
            {
                textBoxNum.ForeColor = Color.White;
                buttonMax.Visible = false;
            }
            buttonDec.Visible = box > 1;
            //Количество коробов
            if (CountSelect)
            {
                textBoxCount.Text = count.ToString();
                buttonCountDec.Visible = count > 1;
                buttonCountInc.Visible = count < Data.MaxLabels;
            }
        }

        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Data.AccessTest(comboBoxUser.SelectedItem.ToString(), NumMachine))
            {
                labelAccessDenied.Visible = false;
                buttonPrint.Visible = true;
            }
            else
            {
                buttonPrint.Visible = false;
                labelAccessDenied.Visible = true;
            }
        }

        private void textBoxNum_Click(object sender, EventArgs e)
        {
            if (!CustomNum) return;
            //Нам разрешено поменять номер короба вручную
            FormKeyboardNums key = new FormKeyboardNums("Введите номер короба");
            key.ShowDialog();
            if (key.DialogResult == DialogResult.OK)
            {
                Data.Labels[NumMachine].CurrentNum = Convert.ToInt32(key.Str);
                Data.Labels[NumMachine].Save();
                box = Data.Labels[NumMachine].CurrentNum;
                DrawNum();
            }
        }

        private void buttonCountDec_Click(object sender, EventArgs e)
        {
            if (count > 1)
            {
                count--;
                DrawNum();
            }
        }

        private void buttonCountInc_Click(object sender, EventArgs e)
        {
            if (count < Data.MaxLabels)
            {
                count++;
                DrawNum();
            }
        }
    }
}
