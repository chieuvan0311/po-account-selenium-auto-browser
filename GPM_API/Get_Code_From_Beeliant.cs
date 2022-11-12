using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    public class Get_Code_From_Beeliant
    {
        PayoneerDbContext db = null;
        public Get_Code_From_Beeliant()
        {
            db = new PayoneerDbContext();
        }

        public string Get_Code(string recoveryEmail, bool hold_on)
        {
            string confirm_code = "";
            GPMLoginAPI api = new GPMLoginAPI();
            var beeliant = db.Admins.Where(x => x.Name == "admin@beeliant.com").FirstOrDefault();
            JObject startedResult = api.Start(beeliant.Password);

            if (startedResult != null)
            {
                string browserLocation = Convert.ToString(startedResult["browser_location"]);
                string seleniumRemoteDebugAddress = Convert.ToString(startedResult["selenium_remote_debug_address"]);
                string gpmDriverPath = Convert.ToString(startedResult["selenium_driver_location"]);

                // Init selenium
                FileInfo gpmDriverFileInfo = new FileInfo(gpmDriverPath);

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(gpmDriverFileInfo.DirectoryName, gpmDriverFileInfo.Name);
                service.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.BinaryLocation = browserLocation;
                options.DebuggerAddress = seleniumRemoteDebugAddress;

                UndectedChromeDriver beeliant_driver = new UndectedChromeDriver(service, options);

                beeliant_driver.Get("https://mail.yandex.ru/");
                Thread.Sleep(RdTimes.T_4000());

                var search_box = beeliant_driver.FindElement(By.XPath("//div[@id='js-apps-container']//div[@class='search-input__text-bubble-container']//span//input"));
                Thread.Sleep(RdTimes.T_500());
                for (int irce_02 = 0; irce_02 < recoveryEmail.Length; irce_02++)
                {
                    var letter = recoveryEmail[irce_02].ToString();
                    search_box.SendKeys(letter);
                    Thread.Sleep(RdTimes.T_3_8());
                }
                Thread.Sleep(RdTimes.T_1500());

                var search_btn = beeliant_driver.FindElement(By.XPath("//div[@id='js-apps-container']//form[@class='search-input__form']//button"));
                Thread.Sleep(RdTimes.T_1000());
                search_btn.Click();
                Thread.Sleep(RdTimes.T_3000());

                var new_email_btn_03 = beeliant_driver.FindElement(By.XPath("//div[@id='js-apps-container']//div[@class='ns-view-container-desc mail-MessagesList js-messages-list']//div[@data-key='box=messages-item-box']"));
                Thread.Sleep(RdTimes.T_1000());
                try { new_email_btn_03.Click(); } catch (Exception) { }
                Thread.Sleep(RdTimes.T_3000());

                //var confirm_code_span = beeliant_driver.FindElement(By.XPath("//div[@id='js-apps-container']//main[@class='mail-Layout-Content']//tbody//span"));
                var confirm_code_span = beeliant_driver.FindElements(By.XPath("//div[@id='js-apps-container']//tbody//td//div[@class='8d761f5edc830eb1mdv2rw']//div//div"))[1];
                Thread.Sleep(RdTimes.T_1000());
                confirm_code = confirm_code_span.Text;
                Thread.Sleep(RdTimes.T_500());
                if(hold_on != true) 
                {
                    beeliant_driver.Close();
                    beeliant_driver.Quit();
                    beeliant_driver.Dispose();
                }          
            }
            return confirm_code;
        }
    }
}
