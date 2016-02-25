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
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
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
            FormPrint formprint = new FormPrint(num);
            formprint.ShowDialog();
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
            Data.Load();
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

        private void timerTime_Tick(object sender, EventArgs e)
        {
            labelClock.Text = DateTime.Now.ToString("HH:mm");
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            //Обновлялка каждые 10 секунд, на случай, если данные были изменены удалённо
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
            timerRefresh.Enabled = false; //Останавливаем автоматическое обновление
            formprop.ShowDialog();
            timerRefresh.Enabled = true; //Запускаем автоматическое обновление
            RefreshMain();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsersPC form = new FormUsersPC();
            timerRefresh.Enabled = false; //Останавливаем автоматическое обновление
            form.ShowDialog();
            timerRefresh.Enabled = true; //Запускаем автоматическое обновление
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
            FormLogEditor form = new FormLogEditor();
            timerRefresh.Enabled = false; //Останавливаем автоматическое обновление
            form.ShowDialog();
            timerRefresh.Enabled = true; //Запускаем автоматическое обновление
        }
    }
}
