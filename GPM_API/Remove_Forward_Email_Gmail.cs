﻿using OpenQA.Selenium;
using PAYONEER.ChromeDrivers;
using PAYONEER.DataConnection;
using PAYONEER.Goolge_Sheets;
using PAYONEER.Models;
using PAYONEER.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAYONEER.GPM_API
{
    public class Remove_Forward_Email_Gmail
    {
        PayoneerDbContext db = null;
        public Remove_Forward_Email_Gmail()
        {
            db = new PayoneerDbContext();
        }

        public Set_Acc_Result_Model Remove (Account Acc, UndectedChromeDriver receive_driver) 
        {
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            result.Status = false;
            UndectedChromeDriver driver = receive_driver;
            try
            {
                IWebElement unverify_check = null;
                driver.Get("https://mail.google.com/mail/u/0/#settings/fwdandpop");
                Thread.Sleep(RdTimes.T_6000());
                try
                {
                    unverify_check = driver.FindElement(By.XPath("//div[@class='nH Tv1JD']//div[@class='nH r4']//table//table//td//span[@act='removeAddr']"));
                }
                catch { }
                Thread.Sleep(RdTimes.T_1000());
                if (unverify_check != null)
                {
                    var del_element = driver.FindElements(By.XPath("//div[@class='nH Tv1JD']//div[@class='nH r4']//table//table//td//span[@act='removeAddr']"));
                    Thread.Sleep(RdTimes.T_1000());
                    int count_elemnt = del_element.Count();
                    Thread.Sleep(RdTimes.T_700());
                    for (int i = 0; i < count_elemnt; i++)
                    {
                        var del_element_01 = driver.FindElement(By.XPath("//div[@class='nH Tv1JD']//div[@class='nH r4']//table//table//td//span[@act='removeAddr']"));
                        Thread.Sleep(RdTimes.T_1500());
                        del_element_01.Click();
                        Thread.Sleep(RdTimes.T_1500());
                        driver.FindElement(By.XPath("//div[@role='alertdialog']//button[@name='ok']")).Click();
                        Thread.Sleep(RdTimes.T_900());
                    }
                }

                Thread.Sleep(RdTimes.T_1000());
                var checkElement01 = driver.FindElements(By.XPath("//table[@class='cf bA1']//tbody//td//span//select//option"));
                Thread.Sleep(RdTimes.T_500());
                int count = checkElement01.Count;
                if (count > 4)
                {
                    do
                    {
                        driver.FindElements(By.XPath("//table[@class='cf bA1']//tbody//td//span//select"))[0].Click();
                        Thread.Sleep(RdTimes.T_700());
                        driver.FindElements(By.XPath("//table[@class='cf bA1']//tbody//td//span//select//option"))[count - 5].Click();
                        Thread.Sleep(RdTimes.T_600());
                        driver.FindElement(By.XPath("//div[@role='alertdialog']//button[@name='ok']")).Click();
                        Thread.Sleep(RdTimes.T_1500());
                        count = driver.FindElements(By.XPath("//table[@class='cf bA1']//tbody//td//span//select//option")).Count;
                        Thread.Sleep(RdTimes.T_1500());
                    }
                    while (count > 4);
                    var db_account = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                    db_account.Remove_OldForwardEmail = true;
                    if (!string.IsNullOrEmpty(Acc.Forward_Email)) { db_account.Forward_Email = null; }
                    db.SaveChanges();
                    result.Status = true;

                    var update_set_acc_all = new Update_Set_Acc_Status().Update(db_account);
                    result.SetAcc_All_Status = update_set_acc_all;
                }
                else
                {
                    var db_account = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
                    db_account.Remove_OldForwardEmail = true;
                    db.SaveChanges();
                    result.Status = true;

                    var update_set_acc_all = new Update_Set_Acc_Status().Update(db_account);
                    result.SetAcc_All_Status = update_set_acc_all;
                }
            }
            catch{ }
            //Trả về kết quả
            result.Driver = driver;
            return result;
        }
    }
}
