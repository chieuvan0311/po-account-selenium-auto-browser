using PAYONEER.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAYONEER.Forms
{
    public partial class fSetColumn : Form
    {
        public delegate void Send_Back(Column_Set_Status_Model status);
        public Send_Back sendStatus;
        public fSetColumn(Column_Set_Status_Model receive_status)
        {
            InitializeComponent();
            if(receive_status != null) 
            {
                STT.Checked = receive_status.STT;
                Email.Checked = receive_status.Email;
                SessionResult.Checked = receive_status.SessionResult;
                AccPassword.Checked = receive_status.AccPassword;
                AccQuestion1.Checked = receive_status.AccQuestion1;

                AccQuestion2.Checked = receive_status.AccQuestion2;
                AccQuestion3.Checked = receive_status.AccQuestion3;
                EmailPassword.Checked = receive_status.EmailPassword;
                RecoveryEmail.Checked = receive_status.RecoveryEmail;
                Forward_Email.Checked = receive_status.Forward_Email;

                Email_2FA.Checked = receive_status.Email_2FA;
                Proxy.Checked = receive_status.Proxy;
                Profile.Checked = receive_status.Profile;
                ProfileID.Checked = receive_status.ProfileID;
                Change_Acc_Info_All.Checked = receive_status.Change_Acc_Info_All;

                Remove_OldForwardEmail.Checked = receive_status.Remove_OldForwardEmail;
                Add_NewForwardEmail.Checked = receive_status.Add_NewForwardEmail;
                Remove_OldRecoveryEmail.Checked = receive_status.Remove_OldRecoveryEmail;
                Add_NewRecoveryEmail.Checked = receive_status.Add_NewRecoveryEmail;
                Change_EmailPassword.Checked = receive_status.Change_EmailPassword;

                Change_AccPassword.Checked = receive_status.Change_AccPassword;
                Add_AccQuestion.Checked = receive_status.Add_AccQuestion;
                Up_Link_Status.Checked = receive_status.Up_Link_Status;
                Old_AccPassword.Checked = receive_status.Old_AccPassword;
                Old_EmailPassword.Checked = receive_status.Old_EmailPassword;

                Old_RecoveryEmail.Checked = receive_status.Old_RecoveryEmail;
                Old_AccQuestion.Checked = receive_status.Old_AccQuestion;
                Random_AccPassword.Checked = receive_status.Random_AccPassword;
                Random_EmailPassword.Checked = receive_status.Random_EmailPassword;
                Random_Question.Checked = receive_status.Random_Question;
            }
        }

        private void check_all_01_Click(object sender, EventArgs e)
        {
            STT.Checked = true;
            Email.Checked = true;
            SessionResult.Checked = true;
            AccPassword.Checked = true;
            AccQuestion1.Checked = true;
        }

        private void check_all_02_Click(object sender, EventArgs e)
        {
            AccQuestion2.Checked = true;
            AccQuestion3.Checked = true;
            EmailPassword.Checked = true;
            RecoveryEmail.Checked = true;
            Forward_Email.Checked = true;
        }

        private void check_all_03_Click(object sender, EventArgs e)
        {
            Email_2FA.Checked = true;
            Proxy.Checked = true;
            Profile.Checked = true;
            ProfileID.Checked = true;
            Change_Acc_Info_All.Checked = true;
        }

        private void check_all_04_Click(object sender, EventArgs e)
        {
            Remove_OldForwardEmail.Checked = true;
            Add_NewForwardEmail.Checked = true;
            Remove_OldRecoveryEmail.Checked = true;
            Add_NewRecoveryEmail.Checked = true;
            Change_EmailPassword.Checked = true;
        }

        private void check_all_05_Click(object sender, EventArgs e)
        {
            Change_AccPassword.Checked = true;
            Add_AccQuestion.Checked = true;
            Up_Link_Status.Checked = true;
            Old_AccPassword.Checked = true;
            Old_EmailPassword.Checked = true;
        }

        private void check_all_06_Click(object sender, EventArgs e)
        {
            Old_RecoveryEmail.Checked = true;
            Old_AccQuestion.Checked = true;
            Random_AccPassword.Checked = true;
            Random_EmailPassword.Checked = true;
            Random_Question.Checked = true;
        }

        private void uncheck_all_01_Click(object sender, EventArgs e)
        {
            STT.Checked = false;
            Email.Checked = false;
            SessionResult.Checked = false;
            AccPassword.Checked = false;
            AccQuestion1.Checked = false;
        }

        private void uncheck_all_02_Click(object sender, EventArgs e)
        {
            AccQuestion2.Checked = false;
            AccQuestion3.Checked = false;
            EmailPassword.Checked = false;
            RecoveryEmail.Checked = false;
            Forward_Email.Checked = false;
        }

        private void uncheck_all_03_Click(object sender, EventArgs e)
        {
            Email_2FA.Checked = false;
            Proxy.Checked = false;
            Profile.Checked = false;
            ProfileID.Checked = false;
            Change_Acc_Info_All.Checked = false;
        }

        private void uncheck_all_04_Click(object sender, EventArgs e)
        {
            Remove_OldForwardEmail.Checked = false;
            Add_NewForwardEmail.Checked = false;
            Remove_OldRecoveryEmail.Checked = false;
            Add_NewRecoveryEmail.Checked = false;
            Change_EmailPassword.Checked = false;
        }

        private void uncheck_all_05_Click(object sender, EventArgs e)
        {
            Change_AccPassword.Checked = false;
            Add_AccQuestion.Checked = false;
            Up_Link_Status.Checked = false;
            Old_AccPassword.Checked = false;
            Old_EmailPassword.Checked = false;
        }

        private void uncheck_all_06_Click(object sender, EventArgs e)
        {
            Old_RecoveryEmail.Checked = false;
            Old_AccQuestion.Checked = false;
            Random_AccPassword.Checked = false;
            Random_EmailPassword.Checked = false;
            Random_Question.Checked = false;
        }

        private void set_email_info_Click(object sender, EventArgs e)
        {
            STT.Checked = true;
            Email.Checked = true;
            SessionResult.Checked = true;
            AccPassword.Checked = false;
            AccQuestion1.Checked = false;

            AccQuestion2.Checked = false;
            AccQuestion3.Checked = false;
            EmailPassword.Checked = true;
            RecoveryEmail.Checked = true;
            Forward_Email.Checked = true;

            Email_2FA.Checked = true;
            Proxy.Checked = false;
            Profile.Checked = false;
            ProfileID.Checked = false;
            Change_Acc_Info_All.Checked = false;

            Remove_OldForwardEmail.Checked = true;
            Add_NewForwardEmail.Checked = true;
            Remove_OldRecoveryEmail.Checked = true;
            Add_NewRecoveryEmail.Checked = true;
            Change_EmailPassword.Checked = true;

            Change_AccPassword.Checked = false;
            Add_AccQuestion.Checked = false;
            Up_Link_Status.Checked = false;
            Old_AccPassword.Checked = false;
            Old_EmailPassword.Checked = false;

            Old_RecoveryEmail.Checked = false;
            Old_AccQuestion.Checked = false;
            Random_AccPassword.Checked = false;
            Random_EmailPassword.Checked = false;
            Random_Question.Checked = false;
        }

        private void check_stt_email_result_Click(object sender, EventArgs e)
        {
            STT.Checked = true;
            Email.Checked = true;
            SessionResult.Checked = true;
            AccPassword.Checked = false;
            AccQuestion1.Checked = false;

            AccQuestion2.Checked = false;
            AccQuestion3.Checked = false;
            EmailPassword.Checked = false;
            RecoveryEmail.Checked = false;
            Forward_Email.Checked = false;

            Email_2FA.Checked = false;
            Proxy.Checked = false;
            Profile.Checked = false;
            ProfileID.Checked = false;
            Change_Acc_Info_All.Checked = false;

            Remove_OldForwardEmail.Checked = false;
            Add_NewForwardEmail.Checked = false;
            Remove_OldRecoveryEmail.Checked = false;
            Add_NewRecoveryEmail.Checked = false;
            Change_EmailPassword.Checked = false;

            Change_AccPassword.Checked = false;
            Add_AccQuestion.Checked = false;
            Up_Link_Status.Checked = false;
            Old_AccPassword.Checked = false;
            Old_EmailPassword.Checked = false;

            Old_RecoveryEmail.Checked = false;
            Old_AccQuestion.Checked = false;
            Random_AccPassword.Checked = false;
            Random_EmailPassword.Checked = false;
            Random_Question.Checked = false;
        }

        private void set_po_info_Click(object sender, EventArgs e)
        {
            STT.Checked = true;
            Email.Checked = true;
            SessionResult.Checked = true;
            AccPassword.Checked = true;
            AccQuestion1.Checked = true;

            AccQuestion2.Checked = true;
            AccQuestion3.Checked = true;
            EmailPassword.Checked = false;
            RecoveryEmail.Checked = false;
            Forward_Email.Checked = false;

            Email_2FA.Checked = false;
            Proxy.Checked = false;
            Profile.Checked = false;
            ProfileID.Checked = false;
            Change_Acc_Info_All.Checked = false;

            Remove_OldForwardEmail.Checked = false;
            Add_NewForwardEmail.Checked = false;
            Remove_OldRecoveryEmail.Checked = false;
            Add_NewRecoveryEmail.Checked = false;
            Change_EmailPassword.Checked = false;

            Change_AccPassword.Checked = true;
            Add_AccQuestion.Checked = true;
            Up_Link_Status.Checked = true;
            Old_AccPassword.Checked = false;
            Old_EmailPassword.Checked = false;

            Old_RecoveryEmail.Checked = false;
            Old_AccQuestion.Checked = false;
            Random_AccPassword.Checked = false;
            Random_EmailPassword.Checked = false;
            Random_Question.Checked = false;
        }

        private void profile_info_Click(object sender, EventArgs e)
        {
            STT.Checked = true;
            Email.Checked = true;
            SessionResult.Checked = true;
            AccPassword.Checked = false;
            AccQuestion1.Checked = false;

            AccQuestion2.Checked = false;
            AccQuestion3.Checked = false;
            EmailPassword.Checked = false;
            RecoveryEmail.Checked = false;
            Forward_Email.Checked = false;

            Email_2FA.Checked = false;
            Proxy.Checked = true;
            Profile.Checked = true;
            ProfileID.Checked = true;
            Change_Acc_Info_All.Checked = false;

            Remove_OldForwardEmail.Checked = false;
            Add_NewForwardEmail.Checked = false;
            Remove_OldRecoveryEmail.Checked = false;
            Add_NewRecoveryEmail.Checked = false;
            Change_EmailPassword.Checked = false;

            Change_AccPassword.Checked = false;
            Add_AccQuestion.Checked = false;
            Up_Link_Status.Checked = false;
            Old_AccPassword.Checked = false;
            Old_EmailPassword.Checked = false;

            Old_RecoveryEmail.Checked = false;
            Old_AccQuestion.Checked = false;
            Random_AccPassword.Checked = false;
            Random_EmailPassword.Checked = false;
            Random_Question.Checked = false;
        }

        private void set_acc_all_Click(object sender, EventArgs e)
        {
            STT.Checked = true;
            Email.Checked = true;
            SessionResult.Checked = true;
            AccPassword.Checked = false;
            AccQuestion1.Checked = false;

            AccQuestion2.Checked = false;
            AccQuestion3.Checked = false;
            EmailPassword.Checked = false;
            RecoveryEmail.Checked = false;
            Forward_Email.Checked = false;

            Email_2FA.Checked = false;
            Proxy.Checked = false;
            Profile.Checked = true;
            ProfileID.Checked = false;
            Change_Acc_Info_All.Checked = true;

            Remove_OldForwardEmail.Checked = true;
            Add_NewForwardEmail.Checked = true;
            Remove_OldRecoveryEmail.Checked = true;
            Add_NewRecoveryEmail.Checked = true;
            Change_EmailPassword.Checked = true;

            Change_AccPassword.Checked = true;
            Add_AccQuestion.Checked = true;
            Up_Link_Status.Checked = true;
            Old_AccPassword.Checked = false;
            Old_EmailPassword.Checked = false;

            Old_RecoveryEmail.Checked = false;
            Old_AccQuestion.Checked = false;
            Random_AccPassword.Checked = false;
            Random_EmailPassword.Checked = false;
            Random_Question.Checked = false;
        }

        private void btn_set_up_Click(object sender, EventArgs e)
        {
            Column_Set_Status_Model status = new Column_Set_Status_Model();
            status.STT = STT.Checked;
            status.Email = Email.Checked;
            status.SessionResult = SessionResult.Checked;
            status.AccPassword = AccPassword.Checked;
            status.AccQuestion1 = AccQuestion1.Checked;

            status.AccQuestion2 = AccQuestion2.Checked;
            status.AccQuestion3 = AccQuestion3.Checked;
            status.EmailPassword = EmailPassword.Checked;
            status.RecoveryEmail = RecoveryEmail.Checked;
            status.Forward_Email = Forward_Email.Checked;

            status.Email_2FA = Email_2FA.Checked;
            status.Proxy = Proxy.Checked;
            status.Profile = Profile.Checked;
            status.ProfileID = ProfileID.Checked;
            status.Change_Acc_Info_All = Change_Acc_Info_All.Checked;

            status.Remove_OldForwardEmail = Remove_OldForwardEmail.Checked;
            status.Add_NewForwardEmail = Add_NewForwardEmail.Checked;
            status.Remove_OldRecoveryEmail = Remove_OldRecoveryEmail.Checked;
            status.Add_NewRecoveryEmail = Add_NewRecoveryEmail.Checked;
            status.Change_EmailPassword = Change_EmailPassword.Checked;

            status.Change_AccPassword = Change_AccPassword.Checked;
            status.Add_AccQuestion = Add_AccQuestion.Checked;
            status.Up_Link_Status = Up_Link_Status.Checked;
            status.Old_AccPassword = Old_AccPassword.Checked;
            status.Old_EmailPassword = Old_EmailPassword.Checked;

            status.Old_RecoveryEmail = Old_RecoveryEmail.Checked;
            status.Old_AccQuestion = Old_AccQuestion.Checked;
            status.Random_AccPassword = Random_AccPassword.Checked;
            status.Random_EmailPassword = Random_EmailPassword.Checked;
            status.Random_Question = Random_Question.Checked;

            sendStatus(status);
            this.Close();
        }
    }
}
