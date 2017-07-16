using System.Collections.Generic;
using System.IO;

namespace AutoLabel_Server
{
    class DropList
    {
        public string Name;
        public List<string> List = new List<string>();
        public DropList(string name)
        {
            Name = name;
            try
            {
                using (StreamReader file = File.OpenText("Lists\\" + Name + ".txt"))
                    while (!file.EndOfStream)
                        List.Add(file.ReadLine());
            }
            catch { }
        }

        public void Save()
        {
            try
            {
                using (StreamWriter file = File.CreateText("Lists\\" + Name + ".txt"))
                    List.ForEach(o => file.WriteLine(o));
            }
            catch { }
        }
    }
}
