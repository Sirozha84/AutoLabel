using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

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
                panel1.Visible = false;
                timerMessage.Enabled = false;
            }
            labelVersion.Text += "     Сервер: " + Net.HostName;
            MenuEnable(false);
            Data.Init();
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
        private void button9_Click(object sender, EventArgs e) { Print(8); }

        void Print(int num)
        {
            StopRefresh();
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
            StartRefresh();
        }

        //Кнопка параметров
        private void buttonProperties_Click(object sender, EventArgs e)
        {
            StopRefresh();
            if (Data.GetKey(255) == 255)
            {
                FormProperties form = new FormProperties();
                form.ShowDialog();
            }
            StartRefresh();
        }

        //Выбор новой смены
        private void buttonShift_Click(object sender, EventArgs e)
        {
            StopRefresh();
            if (Data.GetKey(255) == 255)
            {
                FormShift shift = new FormShift();
                shift.ShowDialog();
                RefreshMain();
            }
            StartRefresh();
        }

        /// <summary>
        /// Обновление кнопочек на главном окне в соответствии на настройками
        /// </summary>
        void RefreshMain()
        {
            labelClock.Text = DateTime.Now.ToString("HH:mm");
            labelClock.Visible = true;
            if (Data.IsMachine)
            {
                labelformname.Text = "Выбор ТПА";
                buttonShift.Text = Shift.Current;
            }
            else
            {
                labelformname.Text = Shift.Current;
                смена1ToolStripMenuItem.Checked = Shift.Current == Shift.Names[0];
                смена2ToolStripMenuItem.Checked = Shift.Current == Shift.Names[1];
                смена3ToolStripMenuItem.Checked = Shift.Current == Shift.Names[2];
                смена4ToolStripMenuItem.Checked = Shift.Current == Shift.Names[3];
            }
            try
            {
                label1.Text = Data.Labels[0].LabelUnderButton();
                label2.Text = Data.Labels[1].LabelUnderButton();
                label3.Text = Data.Labels[2].LabelUnderButton();
                label4.Text = Data.Labels[3].LabelUnderButton();
                label5.Text = Data.Labels[4].LabelUnderButton();
                label6.Text = Data.Labels[5].LabelUnderButton();
                label7.Text = Data.Labels[6].LabelUnderButton();
                label8.Text = Data.Labels[7].LabelUnderButton();
                label9.Text = Data.Labels[8].LabelUnderButton();
                Data.SetColor(button1, 0);
                Data.SetColor(button2, 1);
                Data.SetColor(button3, 2);
                Data.SetColor(button4, 3);
                Data.SetColor(button5, 4);
                Data.SetColor(button6, 5);
                Data.SetColor(button7, 6);
                Data.SetColor(button8, 7);
                Data.SetColor(button9, 8);
                tableLayoutPanel1.Visible = true;
                labelProblem.Visible = false;
            }
            catch { }
        }

        void DrawError()
        {
            tableLayoutPanel1.Visible = false;
            labelProblem.Visible = true;
        }

        //Таймер для обновления внешнего вида (на случай если из вне поменяли параметры)
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                try
                {
                    if (Net.Test())
                    {
                        Data.Load();
                        //labelMessage.Text = Net.LoadMessage(); //Обновление бегущей строки
                        Invoke(new Action(() =>
                        {
                            MenuEnable(true);
                            RefreshMain();
                        }));
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            MenuEnable(false);
                            DrawError();
                        }));
                    }
                }
                catch { }
            }).Start();
            labelMessage.Text = Net.LoadMessage();
        }

        void MenuEnable(bool enable)
        {
            if (Data.IsMachine)
            {
                buttonProperties.Visible = enable;
                buttonShift.Visible = enable;
                buttonQuit.Visible = !enable;
            }
            else
            {
                константыToolStripMenuItem.Enabled = enable;
                сменаToolStripMenuItem.Enabled = enable;
                параметрыToolStripMenuItem.Enabled = enable;
                отчётыToolStripMenuItem.Enabled = enable;
                правкаЖурналаToolStripMenuItem.Enabled = enable;
                этикеткаСПроизвольнымиПолямиToolStripMenuItem.Enabled = enable;
            }
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
            StopRefresh();
            FormPropertiesPC formprop = new FormPropertiesPC();
            formprop.ShowDialog();
            StartRefresh();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRefresh();
            FormUsersPC form = new FormUsersPC();
            form.ShowDialog();
            StartRefresh();
        }

        private void отчётыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRefresh();
            FormReports form = new FormReports();
            form.ShowDialog();
            StartRefresh();
        }

        private void принтерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRefresh();
            Data.PrintSetup();
            StartRefresh();
        }

        private void этикеткаСПроизвольнымиПолямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRefresh();
            FormCustomLabel form = new FormCustomLabel();
            form.ShowDialog();
            StartRefresh();
        }

        void ChangeShift(string shift)
        {
            StopRefresh();
            if (MessageBox.Show("Подтверждаете заступление новой смены?", "Новая смена", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
                Shift.Change(shift);
            StartRefresh();
        }

        private void правкаЖурналаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRefresh();
            FormLog form = new FormLog();
            form.ShowDialog();
            StartRefresh();
        }

        //Таймер для обновления
        int anim = 0;
        private void timerMessage_Tick(object sender, EventArgs e)
        {
            if (Data.Loading)
            {
                anim++;
                if (anim > 10)
                {
                    anim = 0;
                    labelStatus.Text += ".";
                    if (labelStatus.Text.Length > 3)
                        labelStatus.Text = "";
                }
            }
            else
                labelStatus.Text = "";
            labelMessage.Left -= 2;
            if (labelMessage.Left < -labelMessage.Width)
                labelMessage.Left = this.Width;
        }

        void EditList(string name, string file)
        {
            StopRefresh();
            FormListEdit form = new FormListEdit(name, file);
            form.ShowDialog();
            StartRefresh();
        }
        #region Скучная фигня
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
            EditList("Список сроков годности преформы", "Limits");
        }

        private void списокПрочихДополненийПреформыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список прочих дополнений преформы", "Others");
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

        private void списокЛоготиповРотопринтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список логотипов ротопринт", "Weights2");
        }

        private void списокЦветовРотопринтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditList("Список цветов ротопринт", "Colors2");
        }
        #endregion
        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FormHelp help = new FormHelp()) { help.ShowDialog(); }
        }

        private void собщениеБегущейСтрокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerRefresh.Enabled = true;
            FormMessageEdit form = new FormMessageEdit();
            form.ShowDialog();
        }

        //Остановка обновления и вывод сплеша ожидания, если необходимо
        private void StopRefresh()
        {
            timerRefresh.Enabled = false;
            if (Data.Loading)
            {
                FormLoadSplash form = new FormLoadSplash();
                form.ShowDialog();
            }
        }

        //Возобновление обновления
        void StartRefresh()
        {
            timerRefresh.Enabled = true;
            RefreshMain();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Net.TestСompatibility();
            timerRefresh_Tick(null, null);
            timerRefresh.Enabled = true;

        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void статистикаПоЭтикеткамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRefresh();
            FormStat form = new FormStat();
            form.ShowDialog();
            StartRefresh();
        }
    }
}
