using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    public partial class FormStat : Form
    {
        public FormStat()
        {
            InitializeComponent();
        }

        private void FormStat_Load(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Settings.server, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("StatRead");
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End")
                            {
                                string[] str = new string[2];
                                str[0] = s;
                                str[1] = reader.ReadInt32().ToString();
                                ListViewItem st = new ListViewItem(str);
                                listView1.Items.Add(st);
                            }
                        } while (s != "End");
                    }
                }
            }
            catch { }
            listView1.EndUpdate();
        }
    }
}
