using System.Collections.Generic;

namespace AutoLabel
{
    class StaticDir
    {
        /// <summary>
        /// Список весов колпака исходя из линии, капец, костыль на костыле :-(
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        public static List<string> KolpakWeights(string Line)
        {
            List<string> list = new List<string>();
            if (Line == "C1")
            {
                list.Add("1,95±0,1");
                list.Add("2,15±0,1");
                list.Add("3,15±0,1");
            }
            if (Line == "C2")
            {
                list.Add("2,35±0,1");
                list.Add("2,5±0,1");
            }
            return list;
        }
    }
}
