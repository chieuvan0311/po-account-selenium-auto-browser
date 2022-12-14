using OpenQA.Selenium;
using PAYONEER.ChromeDrivers;
using PAYONEER.DataConnection;
using PAYONEER.Goolge_Sheets;
using PAYONEER.Models;
using PAYONEER.RandomData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAYONEER.GPM_API
{
    public class Add_New_Recovery_Email_Gmail_Beeliant
    {
        PayoneerDbContext db = null;
        public Add_New_Recovery_Email_Gmail_Beeliant() 
        {
            db = new PayoneerDbContext();
        }

        public Set_Acc_Result_Model Add (Account Acc, UndectedChromeDriver receive_driver, bool hold_on)
        {
            UndectedChromeDriver driver = receive_driver;
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            result.Status = false;

            driver.Get("https://myaccount.google.com/recovery/email?");
            Thread.Sleep(RdTimes.T_3000());
            var passwordBox = driver.FindElement(By.XPath("//div[@id='password']//input[@type='password']"));
            for (int i = 0; i < Acc.EmailPassword.Length; i++)
            {
                passwordBox.SendKeys(Acc.EmailPassword[i].ToString());
                Thread.Sleep(RdTimes.T_5_13());
            }
            Thread.Sleep(RdTimes.T_1000());
            var passwordNext = driver.FindElement(By.XPath("//div[@id='passwordNext']//button"));
            Thread.Sleep(RdTimes.T_1000());
            passwordNext.Click();

            Thread.Sleep(RdTimes.T_1500());

            var recoveryEmailBox = driver.FindElement(By.Id("i5"));
            Thread.Sleep(RdTimes.T_2000());

            recoveryEmailBox.Clear();
            Thread.Sleep(RdTimes.T_800());

            string bee_recovery_email = Acc.Email.Split('@')[0] + "@beeliant.com";
            for (int i = 0; i < bee_recovery_email.Length; i++)
            {
                recoveryEmailBox.SendKeys(bee_recovery_email[i].ToString());
                Thread.Sleep(RdTimes.T_5_13());
            }
            Thread.Sleep(RdTimes.T_1000());

            var recoveryEmail_Next = driver.FindElements(By.XPath("//div[@class='N1UXxf']//div[@class='VfPpkd-dgl2Hf-ppHlrf-sM5MNb']//button"))[1];
            Thread.Sleep(RdTimes.T_1500());
            recoveryEmail_Next.Click();
            Thread.Sleep(RdTimes.T_1500());
            var input_code_Box = driver.FindElement(By.XPath("//div[@class='VfPpkd-P5QLlc']//div[@class='Bsb9rf']//input[@jsname='YPqjbf']"));
            Thread.Sleep(RdTimes.T_300());
            driver.Manage().Window.Minimize();
            Thread.Sleep(RdTimes.T_900());
            string code = new Get_Code_From_Beeliant().Get_Code(bee_recovery_email, hold_on);
            driver.Manage().Window.Maximize();
            Thread.Sleep(RdTimes.T_600());
            if (!string.IsNullOrEmpty(code))
            {
                for (int co = 0; co < code.Length; co++)
                {
                    input_code_Box.SendKeys(code[co].ToString());
                    Thread.Sleep(RdTimes.T_5_13());
                }
            }
                   
            var codeNext = driver.FindElements(By.XPath("//div[@class='VfPpkd-P5QLlc']//div[@class='VfPpkd-T0kwCb']//button"))[1];
            Thread.Sleep(RdTimes.T_1500());
            codeNext.Click();
            Thread.Sleep(RdTimes.T_500());

            if (hold_on == true)
            {
                MessageBox.Show("Chỉ click OK sau nhập code thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }

            IWebElement checkagain = null;
            try { checkagain = driver.FindElement(By.Id("i5")); } catch { }
            if (checkagain != null)
            {
                var db_account = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                if (!string.IsNullOrEmpty(db_account.RecoveryEmail)) 
                {
                    db_account.Old_RecoveryEmail += db_account.RecoveryEmail + "--";
                }                        
                db_account.RecoveryEmail = bee_recovery_email;
                db_account.Remove_OldRecoveryEmail = true;
                db_account.Add_NewRecoveryEmail = true;
                db.SaveChanges();
                        
                var update_set_acc_all = new Update_Set_Acc_Status().Update(db_account);
                result.SetAcc_All_Status = update_set_acc_all;
                result.Status = true;               
            }                             
            result.Driver = driver;
            return result;
        }
    }

}
