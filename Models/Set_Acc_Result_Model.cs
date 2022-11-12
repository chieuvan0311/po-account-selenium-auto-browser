using OpenQA.Selenium.Chrome;
using PAYONEER.ChromeDrivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Models
{
    public class Set_Acc_Result_Model
    {
        public UndectedChromeDriver Driver { get; set; }
        public bool Status { get; set; }
        public bool SetAcc_All_Status { get; set; }
        public string Value_01 { get; set; }
        public string Value_02 { get; set; }
        public string Value_03 { get; set; }
    }
}
