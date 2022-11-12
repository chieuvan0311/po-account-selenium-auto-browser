using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.Models
{
    public class Add_New_Account_Model
    {
        public int STT { get; set; }    // Index 0
        public string Email { get; set; }    // Index 1

        [DisplayName("Mật khẩu PO")]
        public string AccPassword { get; set; }    // Index 2

        [DisplayName("Mật khẩu Email")]
        public string EmailPassword { get; set; }    // Index 3
        [DisplayName("Email KP")]
        public string RecoveryEmail { get; set; }    // Index 4
        [DisplayName("Email fw")]
        public string Forward_Email { get; set; }    // Index 5
        [DisplayName("Email 2FA")]
        public string Email_2FA { get; set; }    // Index 6
        public string Proxy { get; set; }    // Index 7

        [DisplayName("Đổi thông tin")]
        public string Change_Info { get; set; }
        [DisplayName("Kêt quả")]
        public string Result { get; set; }    // Index 8
    }
}
