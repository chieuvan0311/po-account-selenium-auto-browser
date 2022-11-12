using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PAYONEER.RandomData
{
    public class Answer_Question_Cate_07_Age
    {
        public string Get_Age_22_35 () 
        {
            Thread.Sleep(RdTimes.T_50());
            string age = new Random().Next(22, 35).ToString();
            return age;
        }
    }
}
