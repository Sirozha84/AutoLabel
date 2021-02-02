using System;

namespace AutoLabel
{
    class User
    {
        public string Name;
        public string Code;
        public byte Rule;
        public bool[] TPAAccess = new bool[Data.lineCount];

        public User(string name, string code, string rule)
        {
            Name = name;
            Code = code;
            Rule = Convert.ToByte(rule);
            for (int i = 0; i < Data.lineCount; i++)
                TPAAccess[i] = false;
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="code">Код ключа</param>
        /// <param name="rule">Уровень прав доступа</param>
        /// <param name="tpaa">Привязка к ТПА</param>
        public User(string name, string code, string rule, string tpaa)
        {
            //Добавляем "ноликов" на случай, если внезапно добавились линии
            while (tpaa.Length < Data.lineCount) tpaa += "0";

            Name = name;
            Code = code;
            Rule = Convert.ToByte(rule);
            for (int i = 0; i < Data.lineCount; i++)
                TPAAccess[i] = (tpaa[i] == '1');
        }

        /// <summary>
        /// Рисуем красивую строчку с перечнем привязанных ТПА
        /// </summary>
        /// <param name="u">Пользователь</param>
        /// <returns></returns>
        public string StringWidthTPA()
        {
            string tpas = "";
            int tc = 0;
            foreach (bool b in TPAAccess) if (b) tc++; //Узнаём количество тпа
            int tca = 0;
            for (int i = 0; i < TPAAccess.Length; i++)
            {
                if (TPAAccess[i])
                {
                    if (i <= 8) tpas += (i + 1).ToString();
                    if (i >= 9 & i <= 10) tpas += "К" + (i - 8).ToString();
                    if (i >= 11) tpas += "РП";
                    tca++;
                    if (tca < tc) tpas += ", ";
                }
            }
            return tpas;
        }
    }
}
