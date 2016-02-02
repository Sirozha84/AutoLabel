using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
            Data.LoadUsers();
            DrawList();
            buttonsave.Visible = false;
        }

        //Заполняем список пользователей
        void DrawList()
        {
            checkBox7.Checked = Data.AccessControl;
            Data.UserListDraw(listView1);
            listView1_SelectedIndexChanged(null, null);
            buttonsave.Visible = true;
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listView1.SelectedIndices[0]);
            DrawList();
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("0");
            DrawList();
        }

        private void buttonRules_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("255");
            DrawList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                Data.Users[listView1.SelectedIndices[0]].Code = key.Code;
                DrawList();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool sel = listView1.SelectedIndices.Count > 0;
            //if (listView1.SelectedIndices.Count == 0) return;
            if (sel)
            {
                User u = Data.Users[listView1.SelectedIndices[0]];
                checkBox1.Checked = u.TPAAccess[0];
                checkBox2.Checked = u.TPAAccess[1];
                checkBox3.Checked = u.TPAAccess[2];
                checkBox4.Checked = u.TPAAccess[3];
                checkBox5.Checked = u.TPAAccess[4];
                checkBox6.Checked = u.TPAAccess[5];
            }
            else
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
            checkBox1.Visible = sel;
            checkBox2.Visible = sel;
            checkBox3.Visible = sel;
            checkBox4.Visible = sel;
            checkBox5.Visible = sel;
            checkBox6.Visible = sel;
            buttonKey.Visible = sel;
            buttonKeyDel.Visible = sel;
            buttonDelete.Visible = sel;
        }

        //Изменение привязки определённого пользователя к определённому ТПА
        void ChangeTPA(byte num, bool Access)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            User u = Data.Users[listView1.SelectedIndices[0]];
            u.TPAAccess[num] = Access;
            listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text = Data.StringWidthTPA(u);
            buttonsave.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ChangeTPA(0, checkBox1.Checked); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { ChangeTPA(1, checkBox2.Checked); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { ChangeTPA(2, checkBox3.Checked); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { ChangeTPA(3, checkBox4.Checked); }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { ChangeTPA(4, checkBox5.Checked); }
        private void checkBox6_CheckedChanged(object sender, EventArgs e) { ChangeTPA(5, checkBox6.Checked); }

        private void buttonKeyDel_Click(object sender, EventArgs e)
        {
            Data.Users[listView1.SelectedIndices[0]].Code = "";
            DrawList();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Data.AccessControl = checkBox7.Checked;
            buttonsave.Visible = true;
        }
    }
}
