using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
            Data.UsersLoad();
            DrawList();
            buttonsave.Visible = false;
        }

        //Заполняем список пользователей
        void DrawList()
        {
            checkBoxOn.Checked = Data.AccessControl;
            Data.UserListDraw(listView1);
            listView1_SelectedIndexChanged(null, null);
            buttonsave.Visible = true;
        }

        //Кнопка назад
        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка удаления
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listView1.SelectedIndices[0]);
            DrawList();
        }

        //Кнопка нового пользователя
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("1");
            DrawList();
        }

        //Кнопка нового админа
        private void buttonRules_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("255");
            DrawList();
        }

        //Кнопка сохранения
        private void buttonsave_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            buttonsave.Visible = false;
            Net.Log("Изменение списка пользователей на терминале");
        }

        //Кнопка изменения ключа
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

        //Изменение выделения списка
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool sel = listView1.SelectedIndices.Count > 0;
            if (sel)
            {
                User u = Data.Users[listView1.SelectedIndices[0]];
                checkP1.Checked = u.TPAAccess[0];
                checkP2.Checked = u.TPAAccess[1];
                checkP3.Checked = u.TPAAccess[2];
                checkP4.Checked = u.TPAAccess[3];
                checkP5.Checked = u.TPAAccess[4];
                checkP6.Checked = u.TPAAccess[5];
                checkP7.Checked = u.TPAAccess[6];
                checkK1.Checked = u.TPAAccess[7];
                checkK2.Checked = u.TPAAccess[8];
                checkR1.Checked = u.TPAAccess[9];
            }
            else
            {
                checkP1.Checked = false;
                checkP2.Checked = false;
                checkP3.Checked = false;
                checkP4.Checked = false;
                checkP5.Checked = false;
                checkP6.Checked = false;
                checkP7.Checked = false;
                checkK1.Checked = false;
                checkK2.Checked = false;
                checkR1.Checked = false;
            }
            checkP1.Visible = sel;
            checkP2.Visible = sel;
            checkP3.Visible = sel;
            checkP4.Visible = sel;
            checkP5.Visible = sel;
            checkP6.Visible = sel;
            checkP7.Visible = sel;
            checkK1.Visible = sel;
            checkK2.Visible = sel;
            checkR1.Visible = sel;
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
            listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text = u.StringWidthTPA();
            buttonsave.Visible = true;
        }

        //Изменение привязки
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ChangeTPA(0, checkP1.Checked); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { ChangeTPA(1, checkP2.Checked); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { ChangeTPA(2, checkP3.Checked); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { ChangeTPA(3, checkP4.Checked); }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { ChangeTPA(4, checkP5.Checked); }
        private void checkBox6_CheckedChanged(object sender, EventArgs e) { ChangeTPA(5, checkP6.Checked); }
        private void CheckP7_CheckedChanged(object sender, EventArgs e) { ChangeTPA(6, checkP7.Checked); }
        private void checkBox7_CheckedChanged(object sender, EventArgs e) { ChangeTPA(7, checkK1.Checked); }
        private void checkBox8_CheckedChanged(object sender, EventArgs e) { ChangeTPA(8, checkK2.Checked); }
        private void checkBox9_CheckedChanged(object sender, EventArgs e) { ChangeTPA(9, checkR1.Checked); }

        //Кнопка удаление ключа
        private void buttonKeyDel_Click(object sender, EventArgs e)
        {
            Data.Users[listView1.SelectedIndices[0]].Code = "";
            DrawList();
        }

        //Включение/выключение проверки привязок
        private void checkBoxOn_CheckedChanged(object sender, EventArgs e)
        {
            Data.AccessControl = checkBoxOn.Checked;
            buttonsave.Visible = true;
        }


    }
}
