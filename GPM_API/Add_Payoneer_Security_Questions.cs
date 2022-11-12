using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PAYONEER.ChromeDrivers;
using PAYONEER.DataConnection;
using PAYONEER.Forms;
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
    public class Add_Payoneer_Security_Questions
    {
        PayoneerDbContext db = null;
        public Add_Payoneer_Security_Questions()
        {
            db = new PayoneerDbContext();
        }

        public Set_Acc_Result_Model Add_Questions(Account Acc, UndectedChromeDriver receive_driver)
        {
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            UndectedChromeDriver driver = receive_driver;
            result.Status = false;
            driver.Get("https://myaccount.payoneer.com/ma/settings/securitysettings");
            Thread.Sleep(RdTimes.T_5000());

            var changeBTN = driver.FindElement(By.XPath("//section[@id='security-questions-wrapper']//div[@data-tid='edit-button']"));
            Thread.Sleep(RdTimes.T_1000());
            changeBTN.Click();
           
            Thread.Sleep(RdTimes.T_1000());
            var question_01_Box = driver.FindElement(By.XPath("//section[@id='security-questions-wrapper']//input[@name='Question1.SecurityAnswer']"));
            Thread.Sleep(RdTimes.T_500());
            var question_02_Box = driver.FindElement(By.XPath("//section[@id='security-questions-wrapper']//input[@name='Question2.SecurityAnswer']"));
            Thread.Sleep(RdTimes.T_500());
            var question_03_Box = driver.FindElement(By.XPath("//section[@id='security-questions-wrapper']//input[@name='Question3.SecurityAnswer']"));
            Thread.Sleep(RdTimes.T_500());    

            string question_01 = "";
            string question_02 = "";
            string question_03 = "";
            string answer_01 = "";
            string answer_02 = "";
            string answer_03 = "";

            Thread.Sleep(RdTimes.T_200());
            var questionDiv = driver.FindElements(By.XPath("//section[@id='security-questions-wrapper']//div[@class='react-selectize-search-field-and-selected-values']//div//div"));
            Thread.Sleep(RdTimes.T_1000());
            question_01 = questionDiv[0].Text;
            Thread.Sleep(RdTimes.T_500());
            question_02 = questionDiv[1].Text;
            Thread.Sleep(RdTimes.T_500());
            question_03 = questionDiv[2].Text;
            Thread.Sleep(RdTimes.T_500());
            answer_01 = new Get_Answer_By_Question().Get_Answer(question_01);
            Thread.Sleep(RdTimes.T_300());
            answer_02 = new Get_Answer_By_Question().Get_Answer(question_02);
            Thread.Sleep(RdTimes.T_300());
            answer_03 = new Get_Answer_By_Question().Get_Answer(question_03);

            //StringWriter question_answer = new StringWriter();
            //question_answer.WriteLine(question_01);
            //question_answer.WriteLine("=> " + answer_01);
            //question_answer.WriteLine("");
            //question_answer.WriteLine(question_02);
            //question_answer.WriteLine("=> " + answer_02);
            //question_answer.WriteLine("");
            //question_answer.WriteLine(question_03);
            //question_answer.WriteLine("=> " + answer_03);

            //MessageBox.Show(question_answer.ToString(), "Form hướng dẫn", MessageBoxButtons.OK,
            //                            MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

            Thread.Sleep(RdTimes.T_1000());

            for (int i = 0; i < answer_01.Length; i++)
            {
                question_01_Box.SendKeys(answer_01[i].ToString());
                Thread.Sleep(RdTimes.T_10_20());
            }
            Thread.Sleep(RdTimes.T_1000());

            for (int i = 0; i < answer_02.Length; i++)
            {
                question_02_Box.SendKeys(answer_02[i].ToString());
                Thread.Sleep(RdTimes.T_10_20());
            }
            Thread.Sleep(RdTimes.T_1000());

            Thread.Sleep(RdTimes.T_1000());
            for (int i = 0; i < answer_03.Length; i++)
            {
                question_03_Box.SendKeys(answer_03[i].ToString());
                Thread.Sleep(RdTimes.T_10_20());
            }
            Thread.Sleep(RdTimes.T_1000());
            var submit_BTN = driver.FindElement(By.XPath("//section[@id='security-questions-wrapper']//div[@class='editable-card-footer']//button[@type='submit']"));
            Thread.Sleep(RdTimes.T_800());


            submit_BTN.Click();

            string random_question = "1--" + question_01 + "--" + answer_01 + "--2--" + question_02 + "--" + answer_02 + "--3--"
                + question_03 + "--" + answer_03 + "--";
            var db_Acc = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
            db_Acc.Random_Question += random_question;
            db.SaveChanges();

            Thread.Sleep(RdTimes.T_500());
            driver.SwitchTo().NewWindow(WindowType.Tab);
            Thread.Sleep(RdTimes.T_700());

            bool hold_on = false;
            var result_01 = new Gmail_Login().Login(Acc, driver, hold_on);
            if (result_01.Status == true)
            {
                result_01.Driver.Get("https://mail.google.com/mail");
                Thread.Sleep(RdTimes.T_2000());
                string search_email = "Your verification code from Payoneer";
                var search_box = driver.FindElement(By.XPath("//div[@id='gs_lc50']//input"));
                Thread.Sleep(RdTimes.T_1000());
                for (int i = 0; i < search_email.Length; i++)
                {
                    search_box.SendKeys(search_email[i].ToString());
                    Thread.Sleep(RdTimes.T_3_8());
                }
                Thread.Sleep(RdTimes.T_1000());
                var search_BTN = driver.FindElement(By.XPath("//form[@id='aso_search_form_anchor']//button[@class='gb_mf gb_nf']"));
                Thread.Sleep(RdTimes.T_900());
                search_BTN.Click();

                StringWriter strWriteLine_02 = new StringWriter();
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

                    Clipboard.SetText(code);

                    StringWriter strWriteLine_04 = new StringWriter();
                    strWriteLine_04.WriteLine("- Lưu ý: chỉ click YES sau khi nhập code thành công, YES = lưu, NO = không lưu, Tắt = không lưu");
                    strWriteLine_04.WriteLine("- Đã copy code: " + code);
                    strWriteLine_04.WriteLine(" ");
                    strWriteLine_04.WriteLine(question_01);
                    strWriteLine_04.WriteLine("=>" + answer_01);
                    strWriteLine_04.WriteLine(" ");
                    strWriteLine_04.WriteLine(question_02);
                    strWriteLine_04.WriteLine("=>" + answer_02);
                    strWriteLine_04.WriteLine(" ");
                    strWriteLine_04.WriteLine(question_03);
                    strWriteLine_04.WriteLine("=>" + answer_03);

                    DialogResult save_change = MessageBox.Show(strWriteLine_04.ToString(), "Form hướng dẫn", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    if (save_change == DialogResult.Yes)
                    {
                        var db_Acc_01 = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                        if (!string.IsNullOrEmpty(db_Acc_01.AccQuestion1) || !string.IsNullOrEmpty(db_Acc_01.AccQuestion2) || !string.IsNullOrEmpty(db_Acc_01.AccQuestion3))
                        {
                            db_Acc_01.Old_AccQuestion += db_Acc_01.AccQuestion1 + "--" + db_Acc_01.AccQuestion2 + "--" + db_Acc_01.AccQuestion3 + "||";
                        }
                        db_Acc_01.AccQuestion1 = question_01 + "--" + answer_01;
                        db_Acc_01.AccQuestion2 = question_02 + "--" + answer_02;
                        db_Acc_01.AccQuestion3 = question_03 + "--" + answer_03;
                        db_Acc_01.Add_AccQuestion = true;
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
                    StringWriter copy_02 = new StringWriter();
                    copy_02.WriteLine(question_01);
                    copy_02.WriteLine("=>" + answer_01);
                    copy_02.WriteLine(" ");
                    copy_02.WriteLine(question_02);
                    copy_02.WriteLine("=>" + answer_02);
                    copy_02.WriteLine(" ");
                    copy_02.WriteLine(question_03);
                    copy_02.WriteLine("=>" + answer_03);
                    Clipboard.SetText(copy_02.ToString());

                    StringWriter strWriteLine_05 = new StringWriter();
                    strWriteLine_05.WriteLine("- Tự động lấy code Failed, cần thực hiện thủ công");
                    strWriteLine_05.WriteLine("- Lưu ý: chỉ click YES sau khi nhập code thành công, YES = lưu, NO = không lưu, Tắt = không lưu");
                    strWriteLine_05.WriteLine("- Đã copy câu hỏi + trả lời");
                    strWriteLine_05.WriteLine(" ");
                    strWriteLine_05.WriteLine(question_01);
                    strWriteLine_05.WriteLine("=>" + answer_01);
                    strWriteLine_05.WriteLine(" ");
                    strWriteLine_05.WriteLine(question_02);
                    strWriteLine_05.WriteLine("=>" + answer_02);
                    strWriteLine_05.WriteLine(" ");
                    strWriteLine_05.WriteLine(question_03);
                    strWriteLine_05.WriteLine("=>" + answer_03);

                    DialogResult save_change = MessageBox.Show(strWriteLine_05.ToString(), "Form hướng dẫn", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    if (save_change == DialogResult.Yes)
                    {
                        var db_Acc_01 = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                        if (!string.IsNullOrEmpty(db_Acc_01.AccQuestion1) || !string.IsNullOrEmpty(db_Acc_01.AccQuestion2) || !string.IsNullOrEmpty(db_Acc_01.AccQuestion3))
                        {
                            db_Acc_01.Old_AccQuestion += db_Acc_01.AccQuestion1 + "--" + db_Acc_01.AccQuestion2 + "--" + db_Acc_01.AccQuestion3 + "||";
                        }
                        db_Acc_01.AccQuestion1 = question_01 + "--" + answer_01;
                        db_Acc_01.AccQuestion2 = question_02 + "--" + answer_02;
                        db_Acc_01.AccQuestion3 = question_03 + "--" + answer_03;
                        db_Acc_01.Add_AccQuestion = true;
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

            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            result.Driver = driver;
            return result;
        }
    }
}
