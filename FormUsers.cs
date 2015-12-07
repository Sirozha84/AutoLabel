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
            Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //Удаление
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            DrawList();
        }

        /// <summary>
        /// Вывод списка пользователей
        /// </summary>
        void DrawList()
        {
            listBox1.Items.Clear();
            foreach (User u in Data.Users)
            {
                string str = u.Name;
                if (u.Rule == 255) str += "     администратор";
                if (u.Code == "") str += "     (без ключа)";
                listBox1.Items.Add(str);
            }
        }

        private void buttonRules_Click(object sender, EventArgs e)
        {
            //Доступ
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            FormKeyboardLetter keyboard = new FormKeyboardLetter("Введите имя пользователя");
            if (keyboard.ShowDialog() == DialogResult.Cancel) return;
            //Добавление нового пользователя
            string name = keyboard.Str;
            FormKey key = new FormKey();
            key.ShowDialog();
            string code = key.Code;
            Data.Users.Add(new User(keyboard.Str, code, "0"));
            buttonsave.Visible = true;
            DrawList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool edbuttons = listBox1.SelectedIndex >= 0;
            buttonRules.Visible = edbuttons;
            buttonDelete.Visible = edbuttons;
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            Data.Save();
            Close();
        }
    }
}
