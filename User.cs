using System;

namespace AutoLabel
{
    class User
    {
        public string Name;
        public string Code;
        public byte Rule;
        public bool[] TPAAccess = new bool[6];
        public User(string name, string code, string rule, string tpaa)
        {
            Name = name;
            Code = code;
            Rule = Convert.ToByte(rule);
            for (int i = 0; i < 6; i++)
                TPAAccess[i] = (tpaa[i] == '1');
        }
    }
}
