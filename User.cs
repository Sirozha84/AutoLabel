using System;

namespace AutoLabel
{
    class User
    {
        public string Name;
        public string Code;
        public byte Rule;
        public byte[] TPAAccess = { 0, 0, 0, 0, 0, 0 };
        public User(string name, string code, string rule, string tpaa)
        {
            Name = name;
            Code = code;
            Rule = Convert.ToByte(rule);
            for (int i = 0; i < 6; i++)
                if (tpaa[i] == '0') TPAAccess[i] = 0;
                else TPAAccess[i] = 1;
        }
    }
}
