using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace AutoLabel
{
    public partial class FormListEdit : Form
    {
        string FileName;

        public FormListEdit(string name, string file)
        {
            InitializeComponent();
            Text = name;
            FileName = file;
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("ListRead");
                        writer.Write(file);
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End") listBox1.Items.Add(s);
                        } while (s != "End");
                    }
                }
            }
            catch { }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("ListWrite");
                        writer.Write(FileName);
                        foreach (string s in listBox1.Items)
                            writer.Write(s);
                        writer.Write("End");
                    }
                }
            }
            catch { }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDel.Enabled = listBox1.SelectedIndex >= 0;
            buttonUp.Enabled = listBox1.SelectedIndex > 0;
            buttonDown.Enabled = listBox1.SelectedIndex >= 0 &
                listBox1.SelectedIndex < listBox1.Items.Count - 1;
            if (listBox1.SelectedIndex >= 0)
                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled = textBox1.Text != "";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
            listBox1_SelectedIndexChanged(null, null);
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            string temp = listBox1.Items[listBox1.SelectedIndex].ToString();
            listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex - 1].ToString();
            listBox1.Items[listBox1.SelectedIndex - 1] = temp;
            listBox1.SelectedIndex--;
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            string temp = listBox1.Items[listBox1.SelectedIndex].ToString();
            listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex + 1].ToString();
            listBox1.Items[listBox1.SelectedIndex + 1] = temp;
            listBox1.SelectedIndex++;
        }
    }
}
