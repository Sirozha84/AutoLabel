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
    public partial class FormUsersPC : Form
    {
        public FormUsersPC()
        {
            InitializeComponent();
        }

        private void FormUsersPC_Load(object sender, EventArgs e)
        {
            Data.DrawUsersList(listBoxUsers);
        }

        private void buttonNewPacker_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("1", listBoxUsers);
        }

        private void buttonNewAdmin_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("255", listBoxUsers);
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonKey.Enabled = listBoxUsers.SelectedIndex >= 0;
            buttonDel.Enabled = listBoxUsers.SelectedIndex >= 0;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listBoxUsers.SelectedIndex);
            Data.DrawUsersList(listBoxUsers);
            listBoxUsers_SelectedIndexChanged(null, null);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            Close();
        }

        private void FormUsersPC_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.LoadUsers();
        }

        private void buttonKey_Click(object sender, EventArgs e)
        {
            FormKey key = new FormKey();
            key.ShowDialog();
            if (key.Code != "")
            {
                Data.Users[listBoxUsers.SelectedIndex].Code = key.Code;
                Data.DrawUsersList(listBoxUsers);
                listBoxUsers_SelectedIndexChanged(null, null);
            }
        }
    }
}
