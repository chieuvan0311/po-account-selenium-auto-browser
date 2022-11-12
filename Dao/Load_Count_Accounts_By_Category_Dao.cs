using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Dao
{
    public class Load_Count_Accounts_By_Category_Dao
    {
        PayoneerDbContext db = null;
        public Load_Count_Accounts_By_Category_Dao()
        {
            db = new PayoneerDbContext();
        }

        public List<Account> Get_Accounts (string category)
        {
            List<Account> listAcc = null;
            if (category == "Tất cả")
            {
                listAcc = db.Accounts.ToList();
            }
            else if (category == "Set Email")
            {
                listAcc = db.Accounts.Where(x => x.Change_Email_Info_All != true).ToList();
            }
            else if (category == "Set PO")
            {
                listAcc = db.Accounts.Where(x => x.Change_Email_Info_All == true && (x.Change_AccPassword != true || x.Add_AccQuestion != true)).ToList();
            }
            else if (category == "Forward Email")
            {
                listAcc = db.Accounts.Where(x => x.Add_NewForwardEmail != true).ToList();
            }
            else if (category == "Up Link")
            {
                listAcc = db.Accounts.Where(x => x.Up_Link_Status != true).ToList();
            }
            else if (category == "Đã set - all")
            {
                listAcc = db.Accounts.Where(x => x.Change_Email_Info_All == true && x.Up_Link_Status == true).ToList();
            }
            return listAcc;
        }


        public int Count_Accounts(string category)
        {
            List<Account> listAcc = null;
            if (category == "Tất cả")
            {
                listAcc = db.Accounts.ToList();
            }
            else if (category == "Set Email")
            {
                listAcc = db.Accounts.Where(x => x.Change_Email_Info_All != true).ToList();
            }
            else if (category == "Set PO")
            {
                listAcc = db.Accounts.Where(x => x.Change_Email_Info_All == true && (x.Change_AccPassword != true || x.Add_AccQuestion != true)).ToList();
            }
            else if (category == "Forward Email")
            {
                listAcc = db.Accounts.Where(x => x.Add_NewForwardEmail != true).ToList();
            }
            else if (category == "Up Link")
            {
                listAcc = db.Accounts.Where(x => x.Up_Link_Status != true).ToList();
            }
            else if (category == "Đã set - all")
            {
                listAcc = db.Accounts.Where(x => x.Change_Email_Info_All == true && x.Up_Link_Status == true).ToList();
            }
            return listAcc.Count;
        }
    }
}
