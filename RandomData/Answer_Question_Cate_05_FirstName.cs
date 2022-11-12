using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.RandomData
{
    public class Answer_Question_Cate_05_FirstName
    {
        public string Get_Name() 
        {
            List<string> first_name = new List<string>();
            first_name.Add("nguyen");  // 01
            first_name.Add("tran");  // 02
            first_name.Add("le");  // 03
            first_name.Add("pham");  // 04
            first_name.Add("hoang");  // 05
            first_name.Add("vu ");  // 06
            first_name.Add("vo");  // 07
            first_name.Add("phan");  // 08
            first_name.Add("truong");  // 09
            first_name.Add("bui");  // 10
            first_name.Add("dang");  // 11
            first_name.Add("do");  // 12
            first_name.Add("ngo");  // 13
            first_name.Add("ho");  // 14
            first_name.Add("duong");  // 15
            first_name.Add("dinh");  // 16

            int random = new Random().Next(0, 15);
            return first_name[random];
        }
    }
}
