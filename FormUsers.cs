using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            //Отменяем изменения путём перезагрузки старых данных
            Data.LoadUsers();
            Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listBox1.SelectedIndex);
            Data.DrawUsersList(listBox1);
            listBox1_SelectedIndexChanged(null, null);
            buttonsave.Visible = true;
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            Data.DrawUsersList(listBox1);
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("1", listBox1);
            buttonsave.Visible = true;
        }

        private void buttonRules_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("255", listBox1);
            buttonsave.Visible = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonKey.Visible = listBox1.SelectedIndex >= 0;
            buttonDelete.Visible = listBox1.SelectedIndex >= 0;
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            buttonsave.Visible = false;
        }

        private void buttonKey_Click(object sender, EventArgs e)
        {
            FormKey key = new FormKey();
            key.ShowDialog();
            if (key.Code != "")
            {
                Data.Users[listBox1.SelectedIndex].Code = key.Code;
                Data.DrawUsersList(listBox1);
                buttonsave.Visible = true;
                buttonKey.Visible = false;
                buttonDelete.Visible = false;
            }
        }
    }
}
