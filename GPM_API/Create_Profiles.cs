using Newtonsoft.Json.Linq;
using PAYONEER.DataConnection;
using PAYONEER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAYONEER.GPM_API
{
    public class Create_Profiles
    {
        PayoneerDbContext db = null;
        public Create_Profiles() 
        {
            db = new PayoneerDbContext();
        }

        public Set_Acc_Result_Model Create(Account Acc)
        {
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            var db_Account = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
            result.Status = false;
            GPMLoginAPI api = new GPMLoginAPI();
            bool canvas = true;
            if(Acc.Canvas_Profiles == false) { canvas = false; }
            JObject createdResult = api.Create(Acc.Email, Acc.Proxy, canvas);
            //string createdProfileId = null;
            if (createdResult != null)
            {
                bool status = Convert.ToBoolean(createdResult["status"]);
                if (status == true) 
                {
                    db_Account.ProfileID = Convert.ToString(createdResult["profile_id"]);
                    db_Account.ProfileName = Acc.Email;
                    db_Account.Profile = true;
                    db.SaveChanges();
                    //trả về giao diện
                    result.Status = true;
                    result.Value_01 = Convert.ToString(createdResult["profile_id"]);
                }             
            }
            else 
            {
                MessageBox.Show("Kết nối GMP-Login lỗi!", "Thông báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            return result;
        }
    }
}
