using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoLabel_Server
{
    public class Line
    {
        public string name;
        public string box;
        public string partNum;
        public string type;
        public string weight;
        public string count;
        public string material;
        public string color;
        public string antistatic;
        public string colorant;
        public string life;
        public string addition;

        public Line() { }
        public Line(string[] s) //Временная функция, нужна только при загрузке с текстового файла
        {
            if (s.Count() < 12) return;
            name = s[0];
            box = s[1];
            partNum = s[2];
            type = s[3];
            weight = s[4];
            count = s[5];
            material = s[6];
            color = s[7];
            antistatic = s[8];
            colorant = s[9];
            life = s[10];
            addition = s[11];
        }
        public void Input(string str)
        {
            string[] s = str.Split('☺');
            if (s.Count() < 12) return;
            name = s[0];
            box = s[1];
            partNum = s[2];
            type = s[3];
            weight = s[4];
            count = s[5];
            material = s[6];
            color = s[7];
            antistatic = s[8];
            colorant = s[9];
            life = s[10];
            addition = s[11];
        }

        public string ToSend()
        {
            string str="";
            str += name + '☺';
            str += box + '☺';
            str += partNum + '☺';
            str += type + '☺';
            str += weight + '☺';
            str += count + '☺';
            str += material + '☺';
            str += color + '☺';
            str += antistatic + '☺';
            str += colorant + '☺';
            str += life + '☺';
            str += addition ;
            return str;
        }
    }
}
