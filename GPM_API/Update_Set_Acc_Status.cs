using PAYONEER.DataConnection;
using PAYONEER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.GPM_API
{
    public class Update_Set_Acc_Status
    {
        PayoneerDbContext db = null;
        public Update_Set_Acc_Status() 
        {
            db = new PayoneerDbContext();
        }

        public bool Update (Account Acc) 
        {
            bool result = false;
            if (Acc.Remove_OldForwardEmail == true && Acc.Remove_OldRecoveryEmail == true && Acc.Add_NewRecoveryEmail == true &&
                Acc.Change_EmailPassword == true)
            {
                Acc.Change_Email_Info_All = true;
                db.SaveChanges();
            }

            if (Acc.Remove_OldForwardEmail == true && Acc.Remove_OldRecoveryEmail == true && Acc.Add_NewRecoveryEmail == true &&
                Acc.Change_EmailPassword == true && Acc.Change_AccPassword == true && Acc.Add_AccQuestion == true && Acc.Up_Link_Status == true)
            {
                Acc.Change_Acc_Info_All = true;
                Acc.AccType = "Đã đổi thông tin + uplink";
                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}

