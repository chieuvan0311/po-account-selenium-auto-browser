using OpenQA.Selenium;
using PAYONEER.ChromeDrivers;
using PAYONEER.DataConnection;
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
    public class Get_Code_From_Gmail_To_Gmail
    {
        PayoneerDbContext db = null;
        public Get_Code_From_Gmail_To_Gmail() 
        {
            db = new PayoneerDbContext();
        }
        public string Get_Code (string search_keyword) 
        {
            UndectedChromeDriver gmail_driver = null;
            string code = "";
            try
            {
                string fw_email_profileId = db.Admins.Where(x => x.Name == "Forward_Email_Gmail").FirstOrDefault().Password;
                var result = new Open_Profiles().Open(fw_email_profileId);
                gmail_driver = result.Driver;
                gmail_driver.Get("https://mail.google.com/mail");
                Thread.Sleep(RdTimes.T_1000());
                var search_box = gmail_driver.FindElement(By.XPath("//div[@id='gs_lc50']//input"));
                Thread.Sleep(RdTimes.T_1000());
                search_box.SendKeys(search_keyword);
                Thread.Sleep(RdTimes.T_500());
                var search_BTN = gmail_driver.FindElement(By.XPath("//form[@id='aso_search_form_anchor']//button[@class='gb_mf gb_nf']"));
                Thread.Sleep(RdTimes.T_400());
                search_BTN.Click();


                //StringWriter strWriteLine_02 = new StringWriter();
                //strWriteLine_02.WriteLine("- Chỉ click 'Ok' khi code đã về");
                //strWriteLine_02.WriteLine("- Lưu ý: Cần check thời gian tin nhắn mới với thời gian hiện tại");
                //MessageBox.Show(strWriteLine_02.ToString(), "Form hướng dẫn", MessageBoxButtons.OK,
                //                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);


                Thread.Sleep(RdTimes.T_800());
                var code_email_BTN_count = gmail_driver.FindElements(By.XPath("//div[@id=':1']//div[@class='ae4 UI UJ nH oy8Mbf id']//table[@role='grid']//tbody//tr[@role='row']"));

                Thread.Sleep(RdTimes.T_1500());
                var code_email_BTN = code_email_BTN_count[0];
                Thread.Sleep(RdTimes.T_1000());
                code_email_BTN.Click();
                Thread.Sleep(RdTimes.T_1000());

                var get_code_element = gmail_driver.FindElement(By.XPath("//div[@class='nH V8djrc byY']//div[@class='nH']//div[@class='ha']//h2"));
                //nH V8djrc byY
                Thread.Sleep(RdTimes.T_600()); 
                string h2_text = get_code_element.Text;
                Thread.Sleep(RdTimes.T_400());

                for (int i = 2; i <= 8; i++)
                {
                    code += h2_text[i].ToString();
                }

                var num9 = h2_text[9].ToString();
                if(num9 == "0" || num9 == "1" || num9 == "2" || num9 == "3" || num9 == "4" || num9 == "5" || num9 == "6" || num9 == "7" || num9 == "8" || num9 == "9") 
                {
                    code += num9;
                }

                var num10 = h2_text[10].ToString();
                if (num10 == "0" || num10 == "1" || num10 == "2" || num10 == "3" || num10 == "4" || num10 == "5" || num10 == "6" || num10 == "7" || num10 == "8" || num10 == "9")
                {
                    code += num10;
                }

                Thread.Sleep(RdTimes.T_500());
                //MessageBox.Show(code, "Get Code", MessageBoxButtons.YesNo,
                //                                MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                try { gmail_driver.Close(); } catch { }
                try { gmail_driver.Quit(); } catch { }
                try { gmail_driver.Dispose(); } catch { }
            }
            catch
            {
                try { gmail_driver.Close(); } catch { }
                try { gmail_driver.Quit(); } catch { }
                try { gmail_driver.Dispose(); } catch { }
            }        

            return code; 
        }
    }
}
