using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Dao
{
    public class AdminDao
    {
        PayoneerDbContext db = null;
        public AdminDao() 
        {
            db = new PayoneerDbContext();
        }

        public bool Update_GMP_API_URL(string api_url) 
        {          
            db.Admins.Where(x => x.Name == "API_URL").FirstOrDefault().Password = api_url;
            db.SaveChanges();
            return true;
        }

        public bool Update_Beeliant_ProfileId (string profileId)
        {
            db.Admins.Where(x => x.Name == "admin@beeliant.com").FirstOrDefault().Password = profileId;
            db.SaveChanges();
            return true;
        }

        public bool Update_Google_Sheet_ID (string link)
        {
            db.Admins.Where(x => x.Name == "Google_Sheet_Link").FirstOrDefault().Password = link;
            db.SaveChanges();
            return true;
        }

        public bool Update_Database_Sheet_Name (string name)
        {
            db.Admins.Where(x => x.Name == "Database_Sheet_Name").FirstOrDefault().Password = name;
            db.SaveChanges();
            return true;
        }

        public bool Update_Backup_Info_Sheet_Name(string name)
        {
            db.Admins.Where(x => x.Name == "Del_Accounts_Sheet_Name").FirstOrDefault().Password = name;
            db.SaveChanges();
            return true;
        }
    }
}
