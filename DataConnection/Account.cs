namespace PAYONEER.DataConnection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(500)]
        public string SessionResult { get; set; }

        [StringLength(50)]
        public string AccPassword { get; set; }

        [StringLength(1000)]
        public string AccQuestion1 { get; set; }

        [StringLength(1000)]
        public string AccQuestion2 { get; set; }

        [StringLength(1000)]
        public string AccQuestion3 { get; set; }

        [StringLength(50)]
        public string EmailPassword { get; set; }

        [StringLength(50)]
        public string RecoveryEmail { get; set; }

        [StringLength(50)]
        public string Forward_Email { get; set; }

        [StringLength(50)]
        public string Email_2FA { get; set; }

        [StringLength(100)]
        public string Proxy { get; set; }

        public bool? Profile { get; set; }

        [StringLength(250)]
        public string ProfileID { get; set; }

        [StringLength(50)]
        public string ProfileName { get; set; }

        [StringLength(250)]
        public string ProfilePath { get; set; }

        [StringLength(100)]
        public string ProfileCreatedTime { get; set; }

        public bool? Change_Acc_Info_All { get; set; }

        public bool? Remove_OldRecoveryEmail { get; set; }

        public bool? Add_NewRecoveryEmail { get; set; }

        public bool? Remove_OldForwardEmail { get; set; }

        public bool? Add_NewForwardEmail { get; set; }

        public bool? Change_EmailPassword { get; set; }

        public bool? Change_Email_Info_All { get; set; }

        public bool? Change_AccPassword { get; set; }

        public bool? Add_AccQuestion { get; set; }

        public bool? Up_Link_Status { get; set; }

        [StringLength(500)]
        public string Old_AccPassword { get; set; }

        [StringLength(500)]
        public string Old_EmailPassword { get; set; }

        [StringLength(500)]
        public string Old_RecoveryEmail { get; set; }

        [StringLength(3000)]
        public string Old_AccQuestion { get; set; }

        [StringLength(50)]
        public string EmailType { get; set; }

        [StringLength(50)]
        public string AccType { get; set; }

        [StringLength(500)]
        public string Random_AccPassword { get; set; }

        [StringLength(500)]
        public string Random_EmailPassword { get; set; }

        [StringLength(3000)]
        public string Random_Question { get; set; }

        public bool? Canvas_Profiles { get; set; }
    }
}
