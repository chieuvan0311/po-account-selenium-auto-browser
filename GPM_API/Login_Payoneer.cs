using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PAYONEER.ChromeDrivers;
using PAYONEER.DataConnection;
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
    public class Login_Payoneer
    { 
        public Set_Acc_Result_Model Login (Account Acc, UndectedChromeDriver receive_driver)
        {
            UndectedChromeDriver driver = receive_driver;
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            result.Status = false;
            try
            {
                driver.Get("https://myaccount.payoneer.com/ma");
                Thread.Sleep(RdTimes.T_7000());
                IWebElement checklogin_01 = null;
                IWebElement checklogin_02 = null;

                try {checklogin_01 = driver.FindElement(By.XPath("//div[@id='container']//div[@class='myaccount-layout']")); } catch { }
                try { checklogin_02 = driver.FindElement(By.Id("username")); } catch { }

                int next_step_01 = 0;
                int next_step_02 = 0;
                if (checklogin_01 != null)
                {
                    result.Status = true;
                }
                else if (checklogin_02 != null)
                {
                    var emailBox = driver.FindElement(By.Id("username"));
                    Thread.Sleep(RdTimes.T_600());
                    for (int i = 0; i < Acc.Email.Length; i++)
                    {
                        emailBox.SendKeys(Acc.Email[i].ToString());
                        Thread.Sleep(RdTimes.T_5_13());
                    }
                    Thread.Sleep(RdTimes.T_700());
                    var passwordBox = driver.FindElement(By.XPath("//div[@id='app']//form//input[@name='password']"));
                    Thread.Sleep(RdTimes.T_400());
                    for (int i = 0; i < Acc.AccPassword.Length; i++)
                    {
                        passwordBox.SendKeys(Acc.AccPassword[i].ToString());
                        Thread.Sleep(RdTimes.T_5_13());
                    }
                    Thread.Sleep(RdTimes.T_1000());
                    var loginBTN = driver.FindElement(By.Id("login_button"));
                    Thread.Sleep(RdTimes.T_500());
                    loginBTN.Click();
                    Thread.Sleep(RdTimes.T_7000());

                    IWebElement checkagain = null;
                    try
                    {
                        checkagain = driver.FindElement(By.XPath("//div[@id='container']//div[@class='myaccount-layout']"));
                        Thread.Sleep(RdTimes.T_1000());
                        if (checkagain != null)
                        {
                            result.Status = true;
                        }
                    }
                    catch
                    {
                        StringWriter copy_01 = new StringWriter();
                        copy_01.Write(Acc.Email);
                        copy_01.Write("|");
                        copy_01.Write(Acc.AccPassword);
                        Clipboard.SetText(copy_01.ToString());

                        StringWriter strWriteLine_01 = new StringWriter();
                        strWriteLine_01.WriteLine("- Cần đăng nhập PO thủ công");
                        strWriteLine_01.WriteLine("- Nếu xuất hiện nút 'Not Now' -> click vào 'Not Now'");
                        strWriteLine_01.WriteLine("- Đã copy email + mật khẩu");
                        strWriteLine_01.WriteLine(" ");
                        strWriteLine_01.WriteLine("- Lưu ý: chỉ click 'Ok sau khi đã đăng nhập thành công");
                        MessageBox.Show(strWriteLine_01.ToString(), "Form hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        result.Status = true;
                    }
                }
                else 
                {
                    driver.Get("https://myaccount.payoneer.com/ma");
                    Thread.Sleep(RdTimes.T_7000());
                    IWebElement checklogin_03 = null;
                    IWebElement checklogin_04 = null;

                    try
                    {
                        checklogin_03 = driver.FindElement(By.XPath("//div[@id='container']//div[@class='myaccount-layout']"));
                        next_step_01 = 1;
                    }
                    catch { next_step_01 = 2; }

                    try
                    {
                        checklogin_04 = driver.FindElement(By.Id("username"));
                        next_step_02 = 1;
                    }
                    catch { next_step_02 = 2; }
                }

                //Trường hợp lỗi 404
                if (next_step_01 == 1) 
                {
                    result.Status = true;
                }
                else if (next_step_02 == 1)
                {                   
                    var emailBox = driver.FindElement(By.Id("username"));
                    Thread.Sleep(RdTimes.T_600());
                    for (int i = 0; i < Acc.Email.Length; i++)
                    {
                        emailBox.SendKeys(Acc.Email[i].ToString());
                        Thread.Sleep(RdTimes.T_5_13());
                    }
                    Thread.Sleep(RdTimes.T_700());
                    var passwordBox = driver.FindElement(By.XPath("//div[@id='app']//form//input[@name='password']"));
                    Thread.Sleep(RdTimes.T_400());
                    for (int i = 0; i < Acc.AccPassword.Length; i++)
                    {
                        passwordBox.SendKeys(Acc.AccPassword[i].ToString());
                        Thread.Sleep(RdTimes.T_5_13());
                    }
                    Thread.Sleep(RdTimes.T_1000());
                    var loginBTN = driver.FindElement(By.Id("login_button"));
                    Thread.Sleep(RdTimes.T_800());
                    loginBTN.Click();
                    Thread.Sleep(RdTimes.T_7000());
                    IWebElement checkagain = null;
                    try
                    {
                        checkagain = driver.FindElement(By.XPath("//div[@id='container']//div[@class='myaccount-layout']"));
                        Thread.Sleep(RdTimes.T_1000());
                        if (checkagain != null) 
                        {
                            result.Status = true;
                        }
                    }
                    catch 
                    {
                        StringWriter copy_02 = new StringWriter();
                        copy_02.Write(Acc.Email);
                        copy_02.Write("|");
                        copy_02.Write(Acc.AccPassword);
                        Clipboard.SetText(copy_02.ToString());

                        StringWriter strWriteLine_02 = new StringWriter();
                        strWriteLine_02.WriteLine("- Cần đăng nhập PO Thủ công");
                        strWriteLine_02.WriteLine("- Nếu xuất hiện nút 'Not Now' -> click vào 'Not Now'");
                        strWriteLine_02.WriteLine("- Đã copy email + mật khẩu");
                        strWriteLine_02.WriteLine(" ");
                        strWriteLine_02.WriteLine("- Lưu ý: chỉ click 'Ok sau khi đã đăng nhập thành công");
                        MessageBox.Show(strWriteLine_02.ToString(), "Form hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        result.Status = true;
                    }
                }
                else if(next_step_01 == 2 && next_step_02 == 2)
                {
                    StringWriter copy_03 = new StringWriter();
                    copy_03.Write(Acc.Email);
                    copy_03.Write("|");
                    copy_03.Write(Acc.AccPassword);
                    Clipboard.SetText(copy_03.ToString());

                    StringWriter strWriteLine_03 = new StringWriter();
                    strWriteLine_03.WriteLine("- Cần đăng nhập PO bằng tay");
                    strWriteLine_03.WriteLine("- Đã copy email + mật khẩu");
                    strWriteLine_03.WriteLine(" ");
                    strWriteLine_03.WriteLine("- Lưu ý: chỉ click 'Ok sau khi đã đăng nhập thành công");
                    MessageBox.Show(strWriteLine_03.ToString(), "Form hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    result.Status = true;
                }
            } 
            catch { }

            result.Driver = driver;
            return result;
        }
    }
}
