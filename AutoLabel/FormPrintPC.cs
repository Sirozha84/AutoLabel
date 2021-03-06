﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormPrintPC : Form
    {
        public int NumMachine;  //Номер ТПА
        Line lab;              //Ссылка на ТПА
        int box;                //Номер короба
        int count = 1;          //Количество коробов
        bool CountSelect = false;   //Выбираем ли мы количество коробов?
        string User = "";

        public FormPrintPC(int num)
        {
            InitializeComponent();
            lab = Data.lines[num];
            NumMachine = num;
            Data.UsersLoad();
            labelNum.Text = lab.name;
            box = lab.boxNum;
            FillUsers();
            //Если это колпак, выбор пользователя убираем
            if (num >= Data.firstType1 & num <= Data.lastType1)
            {
                comboBoxUser.Enabled = false;
                buttonPrint.Enabled = true;
                checkBoxFixUsers.Enabled = false;
            }
            //Далее надо выяснить мелкие это коробки или крупные, и в зависимости от этого вывести второй нумератор
            try
            {
                if (Data.lines[NumMachine].AllowSelectCount())
                {
                    label2.Enabled = true;
                    textBoxCount.Enabled = true;
                    buttonCountDec.Enabled = true;
                    buttonCountInc.Enabled = true;
                    CountSelect = true;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            DrawNum();
        }

        //Кнопка печати
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            int c = 0;
            if (CountSelect) c = count;
            lab.Print(box, User, c);
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
            box = lab.boxNum;
            DrawNum();
        }

        //Рисование номера
        void DrawNum()
        {
            //Номер короба
            textBoxNum.Text = box.ToString();
            if (box < lab.boxNum)
            {
                textBoxNum.ForeColor = Color.Red;
                buttonMax.Enabled = true;
            }
            else
            {
                textBoxNum.ForeColor = Color.Black;
                buttonMax.Enabled = false;
            }
            buttonDec.Enabled = box > 1;
            //Количество коробов
            if (CountSelect)
            {
                textBoxCount.Text = count.ToString();
                buttonCountDec.Enabled = count > 1;
                buttonCountInc.Enabled = count < Data.MaxLabels(NumMachine);
                buttonCountMax.Enabled = count < Data.MaxLabels(NumMachine);
            }
        }

        //Запрет печати 
        void AccessDenied()
        {
            FormError er = new FormError("Печать запрещена");
            er.ShowDialog();
            Close();
        }

        //Смена пользователя
        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Data.isTerminal & !Data.AccessTest(comboBoxUser.SelectedItem.ToString(), NumMachine))
                AccessDenied();
            buttonPrint.Enabled = true;
            User = comboBoxUser.SelectedItem.ToString();
        }

        //Кнопка "<"
        private void buttonCountDec_Click(object sender, EventArgs e)
        {
            if (count > 1)
            {
                count--;
                DrawNum();
            }
        }

        //Кнопка ">"
        private void buttonCountInc_Click(object sender, EventArgs e)
        {
            if (count < Data.MaxLabels(NumMachine))
            {
                count++;
                DrawNum();
            }
        }

        //Кнопка ">|"
        private void buttonCountMax_Click(object sender, EventArgs e)
        {
            count = Data.MaxLabels(NumMachine);
            DrawNum();
        }

        //Кнопка "Закрыть"
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Ручное редактирование номера короба
        private void textBoxNum_TextChanged(object sender, EventArgs e)
        {
            try { box = Convert.ToInt32(textBoxNum.Text); }
            catch { box = lab.boxNum; }
            if (box < 1) box = 1;
            if (box > 999) box = 999;
            if (textBoxNum.Text != "")
                DrawNum();
        }

        //Ручное редактирование количества коробов
        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            try { count = Convert.ToInt32(textBoxCount.Text); }
            catch { count = 1; }
            if (count < 1) count = 1;
            if (count > Data.MaxLabels(NumMachine)) count = Data.MaxLabels(NumMachine);
            if (textBoxCount.Text != "")
                DrawNum();
        }

        private void checkBoxFixUsers_CheckedChanged(object sender, EventArgs e)
        {
            FillUsers();
        }

        //Заполнение списка пользователей
        void FillUsers()
        {
            //Заполним комбобокс пользователями
            comboBoxUser.Items.Clear();
            foreach (User u in Data.users)
            {
                if (!checkBoxFixUsers.Checked || u.TPAAccess[NumMachine])
                    comboBoxUser.Items.Add(u.Name);
            }
            //Если в списке только один чувак, его сразу и выберем и делаем кнопку печати активной
            if (comboBoxUser.Items.Count == 1)
            {
                comboBoxUser.SelectedIndex = 0;
            }
            buttonPrint.Enabled = (comboBoxUser.Items.Count == 1);
        }
    }
}
