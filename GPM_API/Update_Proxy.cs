using PAYONEER.DataConnection;
using PAYONEER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.GPM_API
{
    public class Update_Proxy
    {
        PayoneerDbContext db = null;
        public Update_Proxy()
        {
            db = new PayoneerDbContext();
        }

        public Set_Acc_Result_Model Update (Account Acc)
        {
            Set_Acc_Result_Model result = new Set_Acc_Result_Model();
            var db_Account = db.Accounts.Where(x => x.ID == Acc.ID).FirstOrDefault();
            GPMLoginAPI api = new GPMLoginAPI();
            result.Status = api.UpdateProxy(Acc.ProfileID, Acc.Proxy);
            return result;
        }
    }
}
