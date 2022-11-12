using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.RandomData
{
    public class Answer_Question_Cate_01_Date
    {
        public string Get_Date() 
        {
            string month_pre = new Random().Next(1, 12).ToString();
            string date_pre = new Random().Next(1, 28).ToString();

            string month = null;
            string date = null;

            if(month_pre.Length == 1)
            {
                month = "0" + month_pre;
            }
            else 
            {
                month = month_pre;
            }

            if (date_pre.Length == 1)
            {
                date = "0" + date_pre;
            }
            else
            {
                date = date_pre;
            }

            return month + @"/" + date;
        }
    }
}
