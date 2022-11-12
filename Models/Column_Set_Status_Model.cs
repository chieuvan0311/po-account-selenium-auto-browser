using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Models
{
    public class Column_Set_Status_Model
    {
        public bool STT { get; set; }
        public bool Email { get; set; }
        public bool SessionResult { get; set; }
        public bool AccPassword { get; set; }
        public bool AccQuestion1 { get; set; }

        public bool AccQuestion2 { get; set; }
        public bool AccQuestion3 { get; set; }
        public bool EmailPassword { get; set; }
        public bool RecoveryEmail { get; set; }
        public bool Forward_Email { get; set; }

        public bool Email_2FA { get; set; }
        public bool Proxy { get; set; }
        public bool Profile { get; set; }
        public bool ProfileID { get; set; }
        public bool Change_Acc_Info_All { get; set; }

        public bool Remove_OldForwardEmail { get; set; }
        public bool Add_NewForwardEmail { get; set; }
        public bool Remove_OldRecoveryEmail { get; set; }
        public bool Add_NewRecoveryEmail { get; set; }
        public bool Change_EmailPassword { get; set; }

        public bool Change_AccPassword { get; set; }
        public bool Add_AccQuestion { get; set; }
        public bool Up_Link_Status { get; set; }
        public bool Old_AccPassword { get; set; }
        public bool Old_EmailPassword { get; set; }

        public bool Old_RecoveryEmail { get; set; }
        public bool Old_AccQuestion { get; set; }
        public bool Random_AccPassword { get; set; }
        public bool Random_EmailPassword { get; set; }
        public bool Random_Question { get; set; }
    }
}
