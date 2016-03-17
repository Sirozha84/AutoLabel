using System;
using System.Drawing;
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
            if (Data.IsMachine)
            {
                panel2.Location = new Point(0, 0);
                labelVersion.Location = new Point(12, 110);
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                labelVersion.Text += "     Режим оператора";
                buttonShift.Visible = false;
                buttonProperties.Visible = false;
                timerMessage.Enabled = false;
                panel1.Visible = false;
            }
            Data.Init();
            RefreshMain();
        }

        //Большие кнопки
        private void button1_Click(object sender, EventArgs e) { Print(0); }
        private void button2_Click(object sender, EventArgs e) { Print(1); }
        private void button3_Click(object sender, EventArgs e) { Print(2); }
        private void button4_Click(object sender, EventArgs e) { Print(3); }
        private void button5_Click(object sender, EventArgs e) { Print(4); }
        private void button6_Click(object sender, EventArgs e) { Print(5); }
        private void button7_Click(object sender, EventArgs e) { Print(6); }
        private void button8_Click(object sender, EventArgs e) { Print(7); }

        void Print(int num)
        {
            if (Data.Labels[num].PartNum == null | 
                Data.Labels[num].PartNum == "" |
                Data.Labels[num].Count == "") return;
            if (Data.IsMachine)
            {
                FormPrint formprint = new FormPrint(num);
                formprint.ShowDialog();
            }
            else
            {
                FormPrintPC formprint = new FormPrintPC(num);
                formprint.ShowDialog();
            }
            RefreshMain();
        }

        //Кнопка параметров
        private void buttonProperties_Click(object sender, EventArgs e)
        {
            if (Data.GetKey(255) == 255)
            {
                FormProperties form = new FormProperties();
                form.ShowDialog();
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
            labelClock.Text = DateTime.Now.ToString("HH:mm");
            if (Net.Test())
            {
                Data.Load();
                tableLayoutPanel1.Visible = true;
                labelProblem.Visible = false;
                timerRefresh.Interval = 3000;
                if (Data.IsMachine)
                    buttonShift.Text = Shift.Current;
                else
                {
                    labelformname.Text = Shift.Current;
                    смена1ToolStripMenuItem.Checked = Shift.Current == Shift.Names[0];
                    смена2ToolStripMenuItem.Checked = Shift.Current == Shift.Names[1];
                    смена3ToolStripMenuItem.Checked = Shift.Current == Shift.Names[2];
                    смена4ToolStripMenuItem.Checked = Shift.Current == Shift.Names[3];
                }
                label1.Text = Data.Labels[0].LabelUnderButton();
                label2.Text = Data.Labels[1].LabelUnderButton();
                label3.Text = Data.Labels[2].LabelUnderButton();
                label4.Text = Data.Labels[3].LabelUnderButton();
                label5.Text = Data.Labels[4].LabelUnderButton();
                label6.Text = Data.Labels[5].LabelUnderButton();
                label7.Text = Data.Labels[6].LabelUnderButton();
                label8.Text = Data.Labels[7].LabelUnderButton();
                Data.SetColor(button1, 0);
                Data.SetColor(button2, 1);
                Data.SetColor(button3, 2);
                Data.SetColor(button4, 3);
                Data.SetColor(button5, 4);
                Data.SetColor(button6, 5);
                Data.SetColor(button7, 6);
                Data.SetColor(button8, 7);
            }
            else
            {
                tableLayoutPanel1.Visible = false;
                labelProblem.Visible = true;
                timerRefresh.Interval = 10000;
            }
        }

        //Таймер для обновления внешнего вида (на случай если из вне поменяли параметры)
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshMain();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.About();
        }

        private void вводДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPropertiesPC formprop = new FormPropertiesPC();
            formprop.ShowDialog();
            RefreshMain();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsersPC form = new FormUsersPC();
            form.ShowDialog();
        }

        private void отчётыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReports form = new FormReports();
            form.ShowDialog();
        }

        private void принтерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.PrintSetup();
        }

        private void этикеткаСПроизвольнымиПолямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomLabel form = new FormCustomLabel();
            form.ShowDialog();
        }

        void ChangeShift(string shift)
        {
            if (MessageBox.Show("Подтверждаете заступление новой смены?", "Новая смена", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
                Shift.Change(shift);
            RefreshMain();
        }

        private void смена1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!смена1ToolStripMenuItem.Checked) ChangeShift(Shift.Names[0]);
        }

        private void смена2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!смена2ToolStripMenuItem.Checked) ChangeShift(Shift.Names[1]);
        }

        private void смена3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!смена3ToolStripMenuItem.Checked) ChangeShift(Shift.Names[2]);
        }

        private void смена4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!смена4ToolStripMenuItem.Checked) ChangeShift(Shift.Names[3]);
        }

        private void правкаЖурналаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLog form = new FormLog();
            form.ShowDialog();
        }

        bool load = true;
        private void timerMessage_Tick(object sender, EventArgs e)
        {
            if (load)
            {
                labelMessage.Text = Net.LoadMessage();
                load = false;
            }
            labelMessage.Location = new Point(labelMessage.Location.X - 2, labelMessage.Location.Y);
            if (labelMessage.Location.X < -labelMessage.Size.Width)
            {
                labelMessage.Location = new Point(Size.Width, labelMessage.Location.Y);
                load = true;
            }
        }

        void EditList(string name, string file)
        {
            FormListEdit form = new FormListEdit(name, file);
            form.ShowDialog();
        }

        private void списокВесовПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список весов преформы", "Weights0");
        }

        private void списокТиповГорловиныПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список типов горловины преформы", "Types0");
        }

        private void списокМатериаловПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список материалов преформы", "Materials0");
        }

        private void списокЦветовПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список цветов преформы", "Colors0");
        }

        private void списокКоличествПреформВКоробеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список количеств преформ", "Quantitys0");
        }

        private void списокТиповАнтистатикаДляПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список типов антистатика для преформы", "Antistatics0");
        }

        private void списокКодовКрасителейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список кодов красителей преформы", "Colorants0");
        }

        private void списокСроковГодностиПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список сроков годности преформы", "Colorants0");
        }

        private void списокВесовКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список весов колпачка", "Weights1");
        }

        private void списокТиповКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список типов колпачка", "Types1");
        }

        private void списокМатериаловКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список материалов колпачка", "Materials1");
        }

        private void списокЦветовКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список цветов колпачка", "Colors1");
        }

        private void списокКоличествКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список количеств колпачка", "Quantitys1");
        }

        private void списокТиповАнтистатикаКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список типов антистатика колпачка", "Antistatics1");
        }

        private void списокКодовКрасителейКолпачкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список типов антистатика колпачка", "Colorants1");
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ожидается в скором времени...\n" +
                "А пока? что непонятно - можно спросить у автора :-)", "AutoLabel");
        }
    }
}
