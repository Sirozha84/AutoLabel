using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoLabel
{
    class User
    {
        public string Name;
        public string Code;
        public byte Rule;
        public User(string name, string code, string rule)
        {
            Name = name;
            Code = code;
            Rule = Convert.ToByte(rule);
        }
    }
}
