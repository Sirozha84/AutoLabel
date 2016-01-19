﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormShift : Form
    {
        public FormShift()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Data.Shift != comboBoxShift.SelectedItem.ToString())
                Data.ShiftChange(comboBoxShift.SelectedItem.ToString());
            Close();
        }

        private void FormShift_Load(object sender, EventArgs e)
        {
            comboBoxShift.SelectedItem = Data.Shift;
        }
    }
}
