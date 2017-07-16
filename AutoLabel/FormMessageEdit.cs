using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace AutoLabel
{
    public partial class FormMessageEdit: Form
    {
        public string Str;

        public FormMessageEdit()
        {
            InitializeComponent();
            textBox1.Text = Net.LoadMessage();
            textBox1.Select(textBox1.Text.Length, 0);
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
                        writer.Write("MessageWrite");
                        writer.Write(textBox1.Text);
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
    }
}
