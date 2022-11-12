using Newtonsoft.Json.Linq;
using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAYONEER.GPM_API
{
    public class Check_API
    {
        PayoneerDbContext db = null;
        public Check_API()
        {
            db = new PayoneerDbContext();
        }

        public bool Check () 
        {
            bool result = false; 
            string api_url = db.Admins.Where(x => x.Name == "API_URL").FirstOrDefault().Password;
            GPMLoginAPI api = new GPMLoginAPI();

            JObject createdResult = api.Create("Check API");
            string createdProfileId = null;
            if (createdResult != null)
            {
                bool status = Convert.ToBoolean(createdResult["status"]);
                if (status == true)
                {
                    createdProfileId = Convert.ToString(createdResult["profile_id"]);
                    api.Delete(createdProfileId);

                    StringWriter strWriteLine_01 = new StringWriter();
                    strWriteLine_01.WriteLine("- Đã kết nối API với GMP-Login!");
                    strWriteLine_01.WriteLine(" ");
                    strWriteLine_01.WriteLine("- API URL: " + api_url);

                    MessageBox.Show(strWriteLine_01.ToString(), "Check kết nối API");
                    result = true;
                }
            }
            else
            {
                StringWriter strWriteLine_02 = new StringWriter();
                strWriteLine_02.WriteLine("- Chưa kết nối API với GMP-Login, chưa bật GMP-Login hoặc API URL không đúng");
                strWriteLine_02.WriteLine(" ");
                strWriteLine_02.WriteLine("- API URL: " + api_url);

                MessageBox.Show(strWriteLine_02.ToString(), "Check kết nối API");
            }
            return result;
        }

        public bool Start ()
        {
            bool result = false;
            string api_url = db.Admins.Where(x => x.Name == "API_URL").FirstOrDefault().Password;
            GPMLoginAPI api = new GPMLoginAPI();

            JObject createdResult = api.Create("Check API");
            string createdProfileId = null;
            if (createdResult != null)
            {
                bool status = Convert.ToBoolean(createdResult["status"]);
                if(status == true) 
                {
                    createdProfileId = Convert.ToString(createdResult["profile_id"]);
                    api.Delete(createdProfileId);
                    result = true; 
                }
                else 
                {
                    try 
                    {
                        createdProfileId = Convert.ToString(createdResult["profile_id"]);
                        api.Delete(createdProfileId);
                    }
                    catch { }
                }
            }
            else
            {
                StringWriter strWriteLine_02 = new StringWriter();
                strWriteLine_02.WriteLine("- Chưa kết nối API với GMP-Login, chưa bật GMP-Login hoặc API URL không đúng");
                strWriteLine_02.WriteLine(" ");
                strWriteLine_02.WriteLine("- API URL: " + api_url);
                MessageBox.Show(strWriteLine_02.ToString(), "Check kết nối API");
            }

            return result;
        }
    }
}
