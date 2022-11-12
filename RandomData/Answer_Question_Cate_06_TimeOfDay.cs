using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PAYONEER.RandomData
{
    public class Answer_Question_Cate_06_TimeOfDay
    {
        public string Get_Time_Of_Date() 
        {
            string return_time = "";
            Thread.Sleep(RdTimes.T_100());
            string time = new Random().Next(0, 23).ToString();

            if(time.Length == 1) 
            {
                return_time = "0" + time;
            }
            else 
            {
                return_time = time;
            }

            return return_time;
        }
    }
}
