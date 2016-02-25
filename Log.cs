using System;
using System.IO;

namespace AutoLabel
{
    class Log
    {

        const string FileName = "Logs\\Log.txt";

        /// <summary>
        /// Запись в общий лог
        /// </summary>
        /// <param name="str"></param>
        public static void Write(string str)
        {
            try
            {
                StreamWriter file = File.AppendText(FileName);
                file.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm - " + Environment.MachineName + " - " + str));
                file.Close();
            }
            catch { }
        }

        public static void Error(string str)
        {
            System.Windows.Forms.MessageBox.Show(str, "Ошибка");
            Write("Ошибка! " + str);
        }
    }
}
