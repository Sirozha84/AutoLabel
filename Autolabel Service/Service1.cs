using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AutoLabel_Server;

namespace Autolabel_Service
{
    public partial class Service1 : ServiceBase
    {
        string path;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            path = Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString() + "\\";
            File.AppendAllText(path + "servtest.txt", DateTime.Now.ToString() + " - Служба запущена\n");
            File.AppendAllText(path + "servtest.txt", DateTime.Now.ToString() + " - " + path + "\n");
            Server.Main(null);
        }

        protected override void OnStop()
        {
            File.AppendAllText(path + "servtest.txt", DateTime.Now.ToString() + " - Служба остановлена\n");
        }
    }
}
