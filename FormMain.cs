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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Data.Load();
        }

        private void button1_Click_1(object sender, EventArgs e) { Print(0); }

        private void button2_Click(object sender, EventArgs e) { Print(1); }

        private void button3_Click(object sender, EventArgs e) { Print(2); }

        private void button4_Click(object sender, EventArgs e) { Print(3); }

        private void button5_Click(object sender, EventArgs e) { Print(4); }

        private void button6_Click(object sender, EventArgs e) { Print(5); }

        void Print(int num)
        {
            FormPrint formprint = new FormPrint();
            formprint.NumMachine = num;
            formprint.Show();
        }

        private void buttonProperties_Click(object sender, EventArgs e)
        {
            FormProperties formprop = new FormProperties();
            formprop.Show();
        }
    }
}