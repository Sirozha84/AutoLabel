﻿using System;
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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            //Режим для ПК
            labelVersion.Text = "Версия: " + Program.Version;
            if (!Data.IsMachine)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                labelVersion.Text += " (Режим ПК)";
                labelClock.Visible = false;
                buttonCustomLabel.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Data.Init();
            RefreshMain();
            //Так... небольшая защита от кидалова :-)
            //Заплатить обещали, но на всякий случай пока оставлю...
            if (DateTime.Now> new DateTime(2016, 3, 17))
            {
                MessageBox.Show("Программа устарела, требуется обновление. Дальнейшая работа невозможна.");
                Environment.Exit(0);
            }
            if (DateTime.Now > new DateTime(2016, 2, 14))
                MessageBox.Show("Программа устарела, требуется обновление. C 17 марта 2016 года работа прекратится.");
        }

        private void button1_Click_1(object sender, EventArgs e) { Print(0); }
        private void button2_Click(object sender, EventArgs e) { Print(1); }
        private void button3_Click(object sender, EventArgs e) { Print(2); }
        private void button4_Click(object sender, EventArgs e) { Print(3); }
        private void button5_Click(object sender, EventArgs e) { Print(4); }
        private void button6_Click(object sender, EventArgs e) { Print(5); }

        void Print(int num)
        {
            if (Data.Labels[num].PartNum == null | 
                Data.Labels[num].PartNum == "" |
                Data.Labels[num].Count == "") return;
            FormPrint formprint = new FormPrint();
            formprint.NumMachine = num;
            formprint.ShowDialog();
            RefreshMain();
        }

        //Параметры
        private void buttonProperties_Click(object sender, EventArgs e)
        {
            if (Data.GetKey(255) == 255)
            {
                FormProperties formprop = new FormProperties();
                formprop.ShowDialog();
                RefreshMain();
            }
        }

        //Выбор новой смены
        private void buttonShift_Click(object sender, EventArgs e)
        {
            if (Data.GetKey(255) == 255)
            {
                FormShift shift = new FormShift();
                shift.ShowDialog();
                RefreshMain();
            }
        }

        /// <summary>
        /// Обновление кнопочек на главном окне в соответствии на настройками
        /// </summary>
        void RefreshMain()
        {
            Data.Load();
            buttonShift.Text = Data.Shift;
            label1.Text = Data.Labels[0].LabelUnderButton();
            label2.Text = Data.Labels[1].LabelUnderButton();
            label3.Text = Data.Labels[2].LabelUnderButton();
            label4.Text = Data.Labels[3].LabelUnderButton();
            label5.Text = Data.Labels[4].LabelUnderButton();
            label6.Text = Data.Labels[5].LabelUnderButton();
            SetColor(button1, 0);
            SetColor(button2, 1);
            SetColor(button3, 2);
            SetColor(button4, 3);
            SetColor(button5, 4);
            SetColor(button6, 5);
        }

        /// <summary>
        /// Задание цвета для кнопки
        /// </summary>
        /// <param name="but">Кнопку</param>
        /// <param name="lab">Лейбл</param>
        void SetColor(Button but, int tpa)
        {
            but.Visible = true;
            switch (Data.Labels[tpa].PColor)
            {
                case "Бесцветный":
                    but.BackColor = Color.FromArgb(0,0,32);
                    but.ForeColor = Color.LightSkyBlue;
                    break;
                case "Матовый":
                    but.BackColor = Color.LightGray;
                    but.ForeColor = Color.DarkGray;
                    break;
                case "Белый":
                    but.BackColor = Color.White;
                    but.ForeColor = Color.Black;
                    break;
                case "Оранжевый":
                    but.BackColor = Color.Orange;
                    but.ForeColor = Color.White;
                    break;
                case "Зелёный":
                    but.BackColor = Color.Green;
                    but.ForeColor = Color.White;
                    break;
                case "Синий":
                    but.BackColor = Color.Blue;
                    but.ForeColor = Color.White;
                    break;
                case "Бирюзовый":
                    but.BackColor = Color.Teal;
                    but.ForeColor = Color.White;
                    break;
                case "Красный":
                    but.BackColor = Color.Red;
                    but.ForeColor = Color.White;
                    break;
                case "Коричневый":
                    but.BackColor = Color.FromArgb(64, 32, 0);
                    but.ForeColor = Color.White;
                    break;
                case "Чёрный":
                    but.BackColor = Color.Black;
                    but.ForeColor = Color.FromArgb(32,32,32);
                    break;
                default:
                    but.Visible = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelClock.Text = DateTime.Now.ToString("HH:mm");
        }

        private void buttonCustomLabel_Click(object sender, EventArgs e)
        {
            FormCustomLabel form = new FormCustomLabel();
            form.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //Обновлялка каждые 10 секунд, на случай, если данные были изменены удалённо
            RefreshMain();
        }        
    }
}
