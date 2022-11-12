using PAYONEER.DataConnection;
using PAYONEER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.DataTransfer
{
    public class Accounts_Table
    {
        public List<AccountsModel> Get_Accounts_List (List<Account> list) 
        {
            List<AccountsModel> listAcc = new List<AccountsModel>();
            if(list.Count > 0) 
            {
                for (int i = 0; i < list.Count; i++)
                {
                    AccountsModel Acc = new AccountsModel();
                    Acc.STT = i + 1;
                    Acc.ID = list[i].ID;
                    Acc.Email = list[i].Email;
                    Acc.SessionResult = list[i].SessionResult;

                    Acc.AccPassword = list[i].AccPassword;
                    Acc.AccQuestion1 = list[i].AccQuestion1;
                    Acc.AccQuestion2 = list[i].AccQuestion2;
                    Acc.AccQuestion3 = list[i].AccQuestion3;

                    Acc.EmailPassword = list[i].EmailPassword;
                    Acc.RecoveryEmail = list[i].RecoveryEmail;
                    Acc.Forward_Email = list[i].Forward_Email;
                    Acc.Email_2FA = list[i].Email_2FA;

                    Acc.Proxy = list[i].Proxy;
                    if (list[i].Profile == true) { Acc.Profile = "YES"; } else { Acc.Profile = "NO"; }
                    Acc.ProfileID = list[i].ProfileID;
                    Acc.ProfileName = list[i].ProfileName;
                    Acc.ProfilePath = list[i].ProfilePath;
                    Acc.ProfileCreatedTime = list[i].ProfileCreatedTime;

                    if (list[i].Change_Acc_Info_All == true) { Acc.Change_Acc_Info_All = "YES"; } else { Acc.Change_Acc_Info_All = "NO"; }
                    if (list[i].Remove_OldRecoveryEmail == true) { Acc.Remove_OldRecoveryEmail = "YES"; } else { Acc.Remove_OldRecoveryEmail = "NO"; }
                    if (list[i].Add_NewRecoveryEmail == true) { Acc.Add_NewRecoveryEmail = "YES"; } else { Acc.Add_NewRecoveryEmail = "NO"; }
                    if (list[i].Remove_OldForwardEmail == true) { Acc.Remove_OldForwardEmail = "YES"; } else { Acc.Remove_OldForwardEmail = "NO"; }
                    if (list[i].Add_NewForwardEmail == true) { Acc.Add_NewForwardEmail = "YES"; } else { Acc.Add_NewForwardEmail = "NO"; }
                    if (list[i].Change_EmailPassword == true) { Acc.Change_EmailPassword = "YES"; } else { Acc.Change_EmailPassword = "NO"; }
                    if (list[i].Change_Email_Info_All == true) { Acc.Change_Email_Info_All = "YES"; } else { Acc.Change_Email_Info_All = "NO"; }
                    if (list[i].Change_AccPassword == true) { Acc.Change_AccPassword = "YES"; } else { Acc.Change_AccPassword = "NO"; }
                    if (list[i].Add_AccQuestion == true) { Acc.Add_AccQuestion = "YES"; } else { Acc.Add_AccQuestion = "NO"; }
                    if (list[i].Up_Link_Status == true) { Acc.Up_Link_Status = "YES"; } else { Acc.Up_Link_Status = "NO"; }

                    Acc.Old_AccPassword = list[i].Old_AccPassword;
                    Acc.Old_EmailPassword = list[i].Old_EmailPassword;
                    Acc.Old_RecoveryEmail = list[i].Old_RecoveryEmail;
                    Acc.Old_AccQuestion = list[i].Old_AccQuestion;
;

                    Acc.EmailType = list[i].EmailType;
                    Acc.AccType = list[i].AccType;

                    Acc.Random_AccPassword = list[i].Random_AccPassword;
                    Acc.Random_EmailPassword = list[i].Random_EmailPassword;
                    Acc.Random_Question = list[i].Random_Question; 
 
                    listAcc.Add(Acc);
                }
            }
            return listAcc;
        }
    }
}
