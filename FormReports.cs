using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
            //Заполнение списка последних смен
            foreach (string sh in Shift.LogName)
                comboBoxShift.Items.Add(sh);
            comboBoxShift.SelectedIndex = 0;
            comboBoxShop.SelectedIndex = 0;
        }

        //Кнопка закрытия
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка отчёта "Журнал"
        private void buttonLog_Click(object sender, EventArgs e)
        {
            Reports.Log(comboBoxShift.SelectedItem.ToString(), 0);
            Close();
        }

        //Кнопка отчёта "Обший"
        private void buttonMianRepotr_Click(object sender, EventArgs e)
        {
            Reports.General(comboBoxShift.SelectedItem.ToString(), 0);
            Close();
        }

        //Кнопка отчёта "По партиям"
        private void buttonReportByPart_Click(object sender, EventArgs e)
        {
            Reports.ByPart(comboBoxShift.SelectedItem.ToString(), 0);
            Close();
        }

        //Кнопка отчёта "По ТПА"
        private void buttonByTPA_Click(object sender, EventArgs e)
        {
            Reports.ByTPA(comboBoxShift.SelectedItem.ToString(), 0);
            Close();
        }
    }
}
