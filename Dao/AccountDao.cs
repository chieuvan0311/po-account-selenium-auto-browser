using Newtonsoft.Json.Linq;
using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Dao
{
    public class AccountDao
    {
        PayoneerDbContext db = null;
        public AccountDao()
        {
            db = new PayoneerDbContext();
        }

        //Cập nhật
        public void Update_Account_Profiles(List<JObject> list)
        {
            foreach (JObject profile in list)
            {
                Account Acc = new Account();
                Acc.ProfileID = Convert.ToString(profile["id"]);
                Acc.ProfileName = Convert.ToString(profile["name"]);
                Acc.ProfilePath = Convert.ToString(profile["path"]);
                Acc.Email = Convert.ToString(profile["name"]);
                db.Accounts.Add(Acc);
                db.SaveChanges();
            }
        }
        public void Update_Account_Proxy(int id, string proxy) 
        {
            db.Accounts.Where(x => x.ID == id).FirstOrDefault().Proxy = proxy;
            db.SaveChanges();
        }
        public void Update_Account_ProfileId(int id, string profileId)
        {
            db.Accounts.Where(x => x.ID == id).FirstOrDefault().ProfileID = profileId;
            db.SaveChanges();
        }
        public void Update_PO_Password (int id, string password)
        {
            var account = db.Accounts.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(account.AccPassword)) { account.Old_AccPassword += account.AccPassword + "--"; }
            account.AccPassword = password;
            db.SaveChanges();
        }
        public void Update_Email_Password(int id, string password)
        {
            var account = db.Accounts.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(account.EmailPassword)) { account.Old_EmailPassword += account.EmailPassword + "--"; }
            account.EmailPassword = password;
            db.SaveChanges();
        }
        //Get Info
        public List<Account> Get_All_Accounts()
        {
            return db.Accounts.ToList();
        }
        public Account Get_Account_ById(int id)
        {
            return db.Accounts.Where(x => x.ID == id).FirstOrDefault();
        }
        public bool Check_Email (string email) 
        {
            var account = db.Accounts.FirstOrDefault(x => x.Email == email);
            if(account != null) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public Account Get_Account_By_Email(string email) 
        {
            return db.Accounts.Where(x => x.Email == email).FirstOrDefault();
        }
        public void Delete_Account (int id)
        {
            var account = db.Accounts.Where(x => x.ID == id).FirstOrDefault();
            db.Accounts.Remove(account);
            db.SaveChanges();
        }
    }
}
