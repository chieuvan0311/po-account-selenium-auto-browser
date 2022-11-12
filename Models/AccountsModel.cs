using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Models
{
    public class AccountsModel
    {
        public int STT { get; set; }    // Index 0
        public int ID { get; set; }    // Index 1
        public string Email { get; set; }    // Index 2
        [DisplayName("Kết quả phiên")]
        public string SessionResult { get; set; }    // Index 3

        [DisplayName("Mật khẩu PO")]
        public string AccPassword { get; set; }    // Index 4
        [DisplayName("Câu hỏi 1")]
        public string AccQuestion1 { get; set; }    // Index 5
        [DisplayName("Câu hỏi 2")]
        public string AccQuestion2 { get; set; }    // Index 6
        [DisplayName("Câu hỏi 3")]
        public string AccQuestion3 { get; set; }    // Index 7


        [DisplayName("Mật khẩu Email")]
        public string EmailPassword { get; set; }    // Index 8
        [DisplayName("Email KP")]
        public string RecoveryEmail { get; set; }    // Index 9
        [DisplayName("Email fw")]
        public string Forward_Email { get; set; }    // Index 10
        [DisplayName("Email 2FA")]
        public string Email_2FA { get; set; }    // Index 11

        public string Proxy { get; set; }    // Index 12

        public string Profile { get; set; }    // Index 13      
        [DisplayName("Profile ID")]
        public string ProfileID { get; set; }    // Index 14
        [DisplayName("Tên Profile")]
        public string ProfileName { get; set; }    // Index 15 - Ẩn
        [DisplayName("Thư mục Profile")]
        public string ProfilePath { get; set; }    // Index 16 - Ẩn
        [DisplayName("Thời gian tạo Profile")]
        public string ProfileCreatedTime { get; set; }    // Index 17 - Ẩn

        [DisplayName("Set Acc all")]
        public string Change_Acc_Info_All { get; set; }    // Index 18 

        [DisplayName("Xóa Email fw cũ")]
        public string Remove_OldForwardEmail { get; set; }    // Index 19
        [DisplayName("Thêm Email fw mới")]
        public string Add_NewForwardEmail { get; set; }    // Index 20

        [DisplayName("Xóa Email KP cũ")]
        public string Remove_OldRecoveryEmail { get; set; }    // Index 21
        [DisplayName("Thêm Email KP mới")]
        public string Add_NewRecoveryEmail { get; set; }    // Index 22
        
        
        [DisplayName("Đổi mk Email")]
        public string Change_EmailPassword { get; set; }    // Index 23 
        public string Change_Email_Info_All { get; set; }    // Index 24 - Ẩn

        [DisplayName("Đổi mk PO")]
        public string Change_AccPassword { get; set; }    // Index 25
        [DisplayName("Thêm câu hỏi BM")]    
        public string Add_AccQuestion { get; set; }    // Index 26
        [DisplayName("Up link")]
        public string Up_Link_Status { get; set; }  // Index 27


        [DisplayName("Mk Acc cũ")]
        public string Old_AccPassword { get; set; }     // Index 28
        [DisplayName("Mk Email cũ")]
        public string Old_EmailPassword { get; set; }    // Index 29
        [DisplayName("Email KP cũ")]
        public string Old_RecoveryEmail { get; set; }   // Index 30
        [DisplayName("Câu hỏi cũ")]
        public string Old_AccQuestion { get; set; }  // Index 31



        public string EmailType { get; set; }     // Index 32 - Ẩn
        public string AccType { get; set; }    // Index 33 - Ẩn

        [DisplayName("Mk PO random")]
        public string Random_AccPassword { get; set; } // Index 34
        [DisplayName("Mk Email random")]
        public string Random_EmailPassword { get; set; } // Index 35
        [DisplayName("Câu hỏi random")]
        public string Random_Question { get; set; } // Index 36

    }
}
