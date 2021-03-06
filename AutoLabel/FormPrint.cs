﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormPrint : Form
    {
        public int NumMachine;  //Номер ТПА
        Line lab;              //Ссылка на ТПА
        int box;                //Номер короба
        int count = 1;          //Количество коробов
        bool ThisAdmin = true;  //Админ ли это?
        bool CountSelect = false;   //Выбираем ли мы количество коробов?
        int timer;              //Таймер для автозакрывания окна

        public FormPrint(int num)
        {
            InitializeComponent();
            lab = Data.lines[num];
            NumMachine = num;
            Data.UsersLoad();
            labelNum.Text = lab.name;
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            //Заполним комбобокс пользователями
            comboBoxUser.Items.Clear();
            foreach (User u in Data.users) comboBoxUser.Items.Add(u.Name);
            box = lab.boxNum;
            FormKey key = new FormKey();
            key.ShowDialog();
            if (key.Code == "")
            {
                //Маленько кривинько, но стираем сформированный список и заполняем его только гостями
                //Причём только теми, у кого есть доступ для этой ТПА
                comboBoxUser.Items.Clear();
                foreach (User u in Data.users)
                    if (u.Code == "")
                        if (Data.accessControl)
                        {
                            if (u.TPAAccess[NumMachine])
                                comboBoxUser.Items.Add(u.Name);
                        }
                        else
                            comboBoxUser.Items.Add(u.Name);
                ThisAdmin = false;
                //Ну а если список гостей пуст, значит запрещаем печать на этой ТПА
                if (comboBoxUser.Items.Count == 0) AccessDenied();
                //А еееесли в списке только один чувак, его сразу и выберем
                if (comboBoxUser.Items.Count == 1)
                    comboBoxUser.SelectedIndex = 0;
            }
            else
            {
                //Далее план такой: ищем в базе ключ, если его нет - уходим
                User user = Data.users.Find(u => u.Code == key.Code);
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
                        ThisAdmin = false;
                    }
                    buttonPrint.Visible = true;
                }
            }
            //Далее надо выяснить мелкие это коробки или крупные, и в зависимости от этого вывести второй нумератор
            if (Data.lines[NumMachine].AllowSelectCount())
            {
                label2.Visible = true;
                textBoxCount.Visible = true;
                buttonCountDec.Visible = true;
                buttonCountInc.Visible = true;
                CountSelect = true;
            }
            DrawNum();
            TimerStart();
            //Делаем кнопки <,> видимыми, если это админ
            /*if (ThisAdmin)
            {
                buttonDec.Visible = true;
                buttonMax.Visible = true;
            }*/
        }

        //Кнопка закрытия
        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка печати
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (box != lab.boxNum)
                if (!Data.Ask("Эта этикетка уже напечатана.\nУверены что хотите повторить?")) return;
            int c = 0;
            if (CountSelect) c = count;
            lab.Print(box, comboBoxUser.SelectedItem.ToString(), c);
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
            TimerStart();
        }

        //Кнопка последняя
        private void buttonMax_Click(object sender, EventArgs e)
        {
            box = lab.boxNum;
            DrawNum();
            TimerStart();
        }

        //Рисование номера
        void DrawNum()
        {
            //Номер короба
            textBoxNum.Text = box.ToString();
            if (box < lab.boxNum)
            {
                textBoxNum.ForeColor = Color.Tomato;
                buttonMax.Visible = true & ThisAdmin;
            }
            else
            {
                textBoxNum.ForeColor = Color.White;
                buttonMax.Visible = false;
            }
            buttonDec.Visible = box > 1 & ThisAdmin;
            //Количество коробов
            if (CountSelect)
            {
                textBoxCount.Text = count.ToString();
                buttonCountDec.Visible = count > 1;
                buttonCountInc.Visible = count < Data.MaxLabels(NumMachine);
                buttonCountMax.Visible = count < Data.MaxLabels(NumMachine);
            }
        }

        //Запрет печати 
        void AccessDenied()
        {
            FormError er = new FormError("Печать запрещена");
            er.ShowDialog();
            Close();
        }

        //Смена пользователя, проверка его доступа к ТПА
        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Data.isTerminal & !Data.AccessTest(comboBoxUser.SelectedItem.ToString(), NumMachine))
                AccessDenied();
            buttonPrint.Visible = true;
            TimerStart();
        }

        private void textBoxNum_Click(object sender, EventArgs e)
        {
            if (!ThisAdmin) return;
            //Нам разрешено поменять номер короба вручную
            timer1.Enabled = false;
            FormKeyboardNums key = new FormKeyboardNums("Введите номер короба");
            key.ShowDialog();
            if (key.DialogResult == DialogResult.OK)
            {
                lab.boxNum = Convert.ToInt32(key.Str);
                lab.Save();
                box = lab.boxNum;
                DrawNum();
            }
            TimerStart();
        }

        //Кнопка "<"
        private void buttonCountDec_Click(object sender, EventArgs e)
        {
            if (count > 1)
            {
                count--;
                DrawNum();
            }
            TimerStart();
        }

        //Кнопка ">"
        private void buttonCountInc_Click(object sender, EventArgs e)
        {
            if (count < Data.MaxLabels(NumMachine))
            {
                count++;
                DrawNum();
            }
            TimerStart();
        }

        //Кнопка ">|"
        private void buttonCountMax_Click(object sender, EventArgs e)
        {
            count = Data.MaxLabels(NumMachine);
            DrawNum();
            TimerStart();
        }

    //Таймер для закрывания окна
    private void timer1_Tick(object sender, EventArgs e)
        {
            timer--;
            if (timer > 10)
                buttonquit.Text = "< Назад";
            else
                buttonquit.Text = "< Назад (" + timer.ToString() + ")";
            if (timer == 0) Dispose();
        }

        void TimerStart()
        {
            timer = 60;
            timer1.Enabled = true;
        }
    }
}
