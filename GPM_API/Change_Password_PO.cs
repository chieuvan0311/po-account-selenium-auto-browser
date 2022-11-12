using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    public class Change_Password_PO
    {
        PayoneerDbContext db = null;
        public Change_Password_PO() 
        {
            db = new PayoneerDbContext();
        }

        public Set_Acc_Result_Model Change_Password (Account Acc, UndectedChromeDriver receive_driver) 
        {
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            UndectedChromeDriver driver = receive_driver;
            result.Status = false;

            driver.Get("https://myaccount.payoneer.com/ma/settings/securitysettings");
            Thread.Sleep(RdTimes.T_5000());
            
            var changeBTN = driver.FindElement(By.XPath("//section[@id='password-wrapper']//div[@data-tid='edit-button']"));
            Thread.Sleep(RdTimes.T_1500());
            changeBTN.Click();
        
            Thread.Sleep(RdTimes.T_800());
            var currentPassword_Box = driver.FindElement(By.XPath("//section[@id='password-wrapper']//input[@name='CurrentPassword']"));
            Thread.Sleep(RdTimes.T_500());
            for (int i = 0; i < Acc.AccPassword.Length; i++)
            {
                currentPassword_Box.SendKeys(Acc.AccPassword[i].ToString());
                Thread.Sleep(RdTimes.T_5_13());
            }
            Thread.Sleep(RdTimes.T_600());

            var newPassword_Box = driver.FindElement(By.XPath("//section[@id='password-wrapper']//input[@name='NewPassword']"));
            Thread.Sleep(RdTimes.T_1500());
            
            Thread.Sleep(RdTimes.T_300());

            string new_password = new Get_Random_Password().Get_One_Random_Password();
            
            for (int i = 0; i < new_password.Length; i++)
            {
                newPassword_Box.SendKeys(new_password[i].ToString());
                Thread.Sleep(RdTimes.T_5_13());
            }
            Thread.Sleep(RdTimes.T_1000());

            var submitBTN = driver.FindElement(By.XPath("//section[@id='password-wrapper']//form[@class='password-form']//button[@type='submit']"));
            Thread.Sleep(RdTimes.T_1000());
            submitBTN.Click();

            var db_Acc = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
            db_Acc.Random_AccPassword += new_password + "--";
            db.SaveChanges();
            Thread.Sleep(RdTimes.T_900());

            IWebElement check_sign_out = null;
            try
            {
                check_sign_out = driver.FindElements(By.XPath("//div[@class='dialog-portal password-change-modal']//div[@class='dialog__content']//div[@class='editable-card-footer']//button"))[0];
            }
            catch { }
            Thread.Sleep(RdTimes.T_1000());
            if (check_sign_out != null)
            {
                check_sign_out.Click();
            }
            Thread.Sleep(RdTimes.T_1000());

            StringWriter strWriteLine_02 = new StringWriter();

            //Get code
            driver.SwitchTo().NewWindow(WindowType.Tab);
            Thread.Sleep(RdTimes.T_300());
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(RdTimes.T_500());

            bool hold_on = false;
            var result_01 = new Gmail_Login().Login(Acc, driver, hold_on);
            if (result_01.Status == true)
            {
                result_01.Driver.Get("https://mail.google.com/mail");
                Thread.Sleep(RdTimes.T_3000());
                string search_email = "Your verification code from Payoneer";
                var search_box = driver.FindElement(By.XPath("//div[@id='gs_lc50']//input"));
                Thread.Sleep(RdTimes.T_600());
                for (int i = 0; i < search_email.Length; i++)
                {
                    search_box.SendKeys(search_email[i].ToString());
                    Thread.Sleep(RdTimes.T_3_8());
                }
                Thread.Sleep(RdTimes.T_300());
                var search_BTN = driver.FindElement(By.XPath("//form[@id='aso_search_form_anchor']//button[@class='gb_mf gb_nf']"));
                Thread.Sleep(RdTimes.T_400());
                search_BTN.Click();


                

                strWriteLine_02.WriteLine("- Chỉ click 'Ok' khi code đã về");
                strWriteLine_02.WriteLine("- Lưu ý: Cần check thời gian tin nhắn mới với thời gian hiện tại");

                MessageBox.Show(strWriteLine_02.ToString(), "Form hướng dẫn", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                try
                {
                    Thread.Sleep(RdTimes.T_500());
                    var code_email_BTN_count = driver.FindElements(By.XPath("//div[@id=':1']//div[@class='ae4 UI nH oy8Mbf id']//table[@role='grid']//tbody//tr[@role='row']"));
                    int addgg = code_email_BTN_count.Count;
                    var code_email_BTN = driver.FindElements(By.XPath("//div[@id=':1']//div[@class='ae4 UI nH oy8Mbf id']//table[@role='grid']//tbody//tr[@role='row']"))[0];
                    Thread.Sleep(RdTimes.T_1000());
                    code_email_BTN.Click();
                    Thread.Sleep(RdTimes.T_1000());

                    var get_code_element = driver.FindElements(By.XPath("//div[@class='nH aHU']//div[@class='nH hx']//div[@role='list']//div[@role='listitem']"));
                    Thread.Sleep(RdTimes.T_1000());
                    int counting = get_code_element.Count;
                    Thread.Sleep(RdTimes.T_1000());
                    string code = driver.FindElements(By.XPath("//div[@class='nH aHU']//div[@class='nH hx']//div[@role='list']//div[@role='listitem']//tbody//p//b"))[counting - 1].Text;
                    Thread.Sleep(RdTimes.T_500());

                    Clipboard.SetText(new_password);

                    StringWriter strWriteLine_04 = new StringWriter();
                    strWriteLine_04.WriteLine("- Lưu ý: chỉ click YES sau khi nhập code thành công, YES = lưu mật khẩu mới, NO = không lưu, Tắt = không lưu");
                    strWriteLine_04.WriteLine("");
                    strWriteLine_04.WriteLine("- Đã copy mật khẩu mới: " + new_password);
                    strWriteLine_04.WriteLine("");
                    strWriteLine_04.WriteLine("- Code: " + code);

                    DialogResult save_change = MessageBox.Show(strWriteLine_04.ToString(), "Form hướng dẫn", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    if (save_change == DialogResult.Yes)
                    {
                        var db_Acc_01 = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                        db_Acc_01.Old_AccPassword += db_Acc_01.AccPassword + "--";
                        db_Acc_01.AccPassword = new_password;
                        db_Acc_01.Change_AccPassword = true;
                        db.SaveChanges();
                        var update_set_acc_all = new Update_Set_Acc_Status().Update(db_Acc_01);
                        result.SetAcc_All_Status = update_set_acc_all;
                        result.Status = true;
                    }
                    else
                    {
                        result.Status = false;
                    }
                }
                catch
                {
                    StringWriter strWriteLine_05 = new StringWriter();
                    strWriteLine_05.WriteLine("- Tự động lấy code Failed, cần thực hiện thủ công");
                    strWriteLine_05.WriteLine("");
                    strWriteLine_05.WriteLine("- Lưu ý: chỉ click YES sau khi nhập code thành công, YES = lưu mật khẩu mới, NO = không lưu, Tắt = không lưu");
                    strWriteLine_05.WriteLine("");
                    strWriteLine_05.WriteLine("- Đã copy mật khẩu mới: " + new_password);

                    Clipboard.SetText(new_password);

                    DialogResult save_change = MessageBox.Show(strWriteLine_05.ToString(), "Form hướng dẫn", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    if (save_change == DialogResult.Yes)
                    {
                        var db_Acc_01 = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                        db_Acc_01.Old_AccPassword += db_Acc_01.AccPassword + "--";
                        db_Acc_01.AccPassword = new_password;
                        db_Acc_01.Change_AccPassword = true;
                        db.SaveChanges();
                        var update_set_acc_all = new Update_Set_Acc_Status().Update(db_Acc_01);
                        result.SetAcc_All_Status = update_set_acc_all;
                        result.Status = true;
                    }
                    else
                    {
                        result.Status = false;
                    }
                }
            }
            Thread.Sleep(RdTimes.T_500());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            result.Driver = driver;
            return result;
        }
    }
}
