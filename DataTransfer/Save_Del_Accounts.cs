using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.DataTransfer
{
    public class Save_Del_Accounts
    {
        PayoneerDbContext db = null;

        public Save_Del_Accounts() 
        {
            db = new PayoneerDbContext();
        }
        public void Save (List<Account> list_account)
        {
            foreach(var account in list_account) 
            {
                Del_Account_Save del_account = new Del_Account_Save();

                del_account.ID = account.ID;
                del_account.Email = account.Email;
                del_account.SessionResult = account.SessionResult;
                del_account.AccPassword = account.AccPassword;
                del_account.AccQuestion1 = account.AccQuestion1;
                del_account.AccQuestion2 = account.AccQuestion2;
                del_account.AccQuestion3 = account.AccQuestion3;
                del_account.EmailPassword = account.EmailPassword;
                del_account.RecoveryEmail = account.RecoveryEmail;
                del_account.Forward_Email = account.Forward_Email;
                del_account.Email_2FA = account.Email_2FA;
                del_account.Proxy = account.Proxy;
                del_account.Profile = account.Profile;
                del_account.ProfileID = account.ProfileID;
                del_account.ProfileName = account.ProfileName;
                del_account.ProfilePath = account.ProfilePath;
                del_account.ProfileCreatedTime = account.ProfileCreatedTime;
                del_account.Change_Acc_Info_All = account.Change_Acc_Info_All;
                del_account.Remove_OldForwardEmail = account.Remove_OldForwardEmail;
                del_account.Add_NewForwardEmail = account.Add_NewForwardEmail;
                del_account.Remove_OldRecoveryEmail = account.Remove_OldRecoveryEmail;
                del_account.Add_NewRecoveryEmail = account.Add_NewRecoveryEmail;
                del_account.Change_EmailPassword = account.Change_EmailPassword;
                del_account.Change_Email_Info_All = account.Change_Email_Info_All;
                del_account.Change_AccPassword = account.Change_AccPassword;
                del_account.Add_AccQuestion = account.Add_AccQuestion;
                del_account.Up_Link_Status = account.Up_Link_Status;
                del_account.Old_AccPassword = account.Old_AccPassword;
                del_account.Old_EmailPassword = account.Old_EmailPassword;
                del_account.Old_RecoveryEmail = account.Old_RecoveryEmail;
                del_account.Old_AccQuestion = account.Old_AccQuestion;
                del_account.EmailType = account.EmailType;
                del_account.AccType = account.AccType;
                del_account.Random_AccPassword = account.Random_AccPassword;
                del_account.Random_EmailPassword = account.Random_EmailPassword;
                del_account.Random_Question = account.Random_Question;

                db.Del_Account_Save.Add(del_account);
            }
            db.SaveChanges();
        }
    }
}
