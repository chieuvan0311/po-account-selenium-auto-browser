using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PAYONEER.RandomData
{
    public class Answer_Question_Cate_08_ABC
    {
        public string Get_3_Letters() 
        {
            List<string> list_21 = new List<string>();
            list_21.Add("a");
            list_21.Add("b");
            list_21.Add("c");
            list_21.Add("d");
            list_21.Add("e");
            list_21.Add("g");
            list_21.Add("h");
            list_21.Add("i");
            list_21.Add("k");
            list_21.Add("l");
            list_21.Add("m");
            list_21.Add("n");
            list_21.Add("o");
            list_21.Add("p");
            list_21.Add("q");
            list_21.Add("r");
            list_21.Add("s");
            list_21.Add("t");
            list_21.Add("u");
            list_21.Add("v");
            list_21.Add("x");

            string first_letter = list_21[new Random().Next(0, 20)];
            Thread.Sleep(RdTimes.T_50());

            string second_letter = list_21[new Random().Next(0, 20)];
            Thread.Sleep(RdTimes.T_50());

            string third_letter = list_21[new Random().Next(0, 20)];
            Thread.Sleep(RdTimes.T_50());

            return first_letter + second_letter + third_letter;
        }
    }
}
