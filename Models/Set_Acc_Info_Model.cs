using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Models
{
    public class Set_Acc_Info_Model
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string AccPassword { get; set; }
        public string AccQuestion1 { get; set; }
        public string AccQuestion2 { get; set; }
        public string AccQuestion3 { get; set; }
        public string EmailPassword { get; set; }
        public string Email_2FA { get; set; }
        public string RecoveryEmail { get; set; }
        public string Forward_Email { get; set; }
        public string EmailType { get; set; }

        public string Proxy { get; set; }
        public bool Canvas { get; set; }

        public bool? Profile { get; set; }
        public string ProfileID { get; set; }
        public string ProfileName { get; set; }

        public bool? Remove_OldRecoveryEmail { get; set; }
        public bool? Add_NewRecoveryEmail { get; set; }
        public bool? Remove_OldForwardEmail { get; set; }
        public bool? Add_NewForwardEmail { get; set; }
        public bool? Change_EmailPassword { get; set; }
        public bool? Change_Email_Info_All { get; set; }
        public bool? Change_AccPassword { get; set; }
        public bool? Add_AccQuestion { get; set; }
        public bool? Up_Link_Status { get; set; }
        public bool? Change_Acc_Info_All { get; set; }
    }
}
