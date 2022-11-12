using OfficeOpenXml;
using PAYONEER.Dao;
using PAYONEER.DataConnection;
using PAYONEER.Goolge_Sheets;
using PAYONEER.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAYONEER.Forms
{
    public partial class fAddNewAccounts : Form
    {
        List<Add_New_Account_Model> data = new List<Add_New_Account_Model>();
        PayoneerDbContext db = null;

        public fAddNewAccounts()
        {
            InitializeComponent();
            db = new PayoneerDbContext();
            grvAddNewAccounts.DataSource = data;
        }

        private void btnOpenExcelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Accounts";
            openFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                data = Add_New_Accounts(openFileDialog.FileName);
                grvAddNewAccounts.DataSource = data;
                
            }
        }
        public List<Add_New_Account_Model> Add_New_Accounts(string path)
        {
            List<Add_New_Account_Model> list_table_acc = new List<Add_New_Account_Model>();
            List<string> new_email_repeatation_list = new List<string>();

            List<Account> old_accounts_list = new AccountDao().Get_All_Accounts();

            ExcelPackage excel_file = new ExcelPackage(new FileInfo(path));
            ExcelWorksheet excel_sheet = excel_file.Workbook.Worksheets[0];
            for (int i = 4; i <= excel_sheet.Dimension.End.Row; i++)
            {
                //check trùng với Acc cũ
                Add_New_Account_Model table_acc = new Add_New_Account_Model();
                Account add_database_acc = new Account();

                string check_email = "";
                try { check_email = excel_sheet.Cells[i, 1].Value.ToString(); } catch { }
                if (string.IsNullOrEmpty(check_email)) { continue; }

                bool no_repeatation_01 = true;
                if (old_accounts_list.Count > 0)
                {
                    foreach (var old_account in old_accounts_list)
                    {
                        if (excel_sheet.Cells[i, 1].Value.ToString() == old_account.Email)
                        {
                            no_repeatation_01 = false;
                            break;
                        }
                    }
                }

                if (no_repeatation_01 == false) //Trùng với Acc cũ
                {
                    table_acc.STT = i - 3;
                    table_acc.Email = excel_sheet.Cells[i, 1].Value.ToString();
                    try { table_acc.AccPassword = excel_sheet.Cells[i, 2].Value.ToString(); } catch { }
                    try { table_acc.EmailPassword = excel_sheet.Cells[i, 3].Value.ToString(); } catch { }
                    try { table_acc.Email_2FA = excel_sheet.Cells[i, 4].Value.ToString(); } catch { }
                    try { table_acc.RecoveryEmail = excel_sheet.Cells[i, 5].Value.ToString(); } catch { }
                    try { table_acc.Forward_Email = excel_sheet.Cells[i, 6].Value.ToString(); } catch { }
                    try { table_acc.Proxy = excel_sheet.Cells[i, 7].Value.ToString(); } catch { }

                    string change_info_table = "";
                    try { change_info_table = excel_sheet.Cells[i, 8].Value.ToString(); } catch { }
                    if (change_info_table == "1") { table_acc.Change_Info = "YES"; } else { table_acc.Change_Info = "NO"; }

                    table_acc.Result = "Trùng với Acc cũ!";
                    list_table_acc.Add(table_acc);
                }
                else //Không trùng acc cũ;
                {
                    bool no_repeatation_02 = true;
                    for (int j = 4; j <= excel_sheet.Dimension.End.Row; j++)  //check trùng acc mới
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        else
                        {
                            if (excel_sheet.Cells[i, 1].Value == excel_sheet.Cells[j, 1].Value)
                            {
                                if (new_email_repeatation_list.Count > 0)
                                {
                                    bool add_email_list = true;
                                    foreach (string email in new_email_repeatation_list)
                                    {
                                        if (excel_sheet.Cells[i, 1].Value.ToString() == email)
                                        {
                                            no_repeatation_02 = false; //Không phải là giá trị đầu tiên
                                            add_email_list = false;
                                            break;
                                        }
                                    }
                                    if (add_email_list == true)
                                    {
                                        new_email_repeatation_list.Add(excel_sheet.Cells[j, 1].Value.ToString());
                                    }
                                }
                                else
                                {
                                    new_email_repeatation_list.Add(excel_sheet.Cells[j, 1].Value.ToString());
                                }
                                break;
                            }
                        }
                    }

                    if (no_repeatation_02 == false)
                    {
                        table_acc.STT = i - 3;
                        table_acc.Email = excel_sheet.Cells[i, 1].Value.ToString();
                        try { table_acc.AccPassword = excel_sheet.Cells[i, 2].Value.ToString(); } catch { }
                        try { table_acc.EmailPassword = excel_sheet.Cells[i, 3].Value.ToString(); } catch { }
                        try { table_acc.Email_2FA = excel_sheet.Cells[i, 4].Value.ToString(); } catch { }
                        try { table_acc.RecoveryEmail = excel_sheet.Cells[i, 5].Value.ToString(); } catch { }
                        try { table_acc.Forward_Email = excel_sheet.Cells[i, 6].Value.ToString(); } catch { }
                        try { table_acc.Proxy = excel_sheet.Cells[i, 7].Value.ToString(); } catch { }

                        string change_info_table = "";
                        try { change_info_table = excel_sheet.Cells[i, 8].Value.ToString(); } catch { }
                        if (change_info_table == "1") { table_acc.Change_Info = "YES"; } else { table_acc.Change_Info = "NO"; }

                        table_acc.Result = "Trùng với Acc mới!";
                        list_table_acc.Add(table_acc);
                    }
                    else //Trả kết quả về giao diện
                    {
                        table_acc.STT = i - 3;
                        table_acc.Email = excel_sheet.Cells[i, 1].Value.ToString();
                        try { table_acc.AccPassword = excel_sheet.Cells[i, 2].Value.ToString(); } catch { }
                        try { table_acc.EmailPassword = excel_sheet.Cells[i, 3].Value.ToString(); } catch { }
                        try { table_acc.Email_2FA = excel_sheet.Cells[i, 4].Value.ToString(); } catch { }
                        try { table_acc.RecoveryEmail = excel_sheet.Cells[i, 5].Value.ToString(); } catch { }
                        try { table_acc.Forward_Email = excel_sheet.Cells[i, 6].Value.ToString(); } catch { }
                        try { table_acc.Proxy = excel_sheet.Cells[i, 7].Value.ToString(); } catch { }

                        string change_info_table = "";
                        try { change_info_table = excel_sheet.Cells[i, 8].Value.ToString(); } catch { }
                        if (change_info_table == "1") { table_acc.Change_Info = "YES"; } else { table_acc.Change_Info = "NO"; }

                        table_acc.Result = "Thêm Acc thành công!";
                        list_table_acc.Add(table_acc);

                        //Add vào database
                        add_database_acc.Email = excel_sheet.Cells[i, 1].Value.ToString();
                        try { add_database_acc.AccPassword = excel_sheet.Cells[i, 2].Value.ToString(); } catch { }
                        try { add_database_acc.EmailPassword = excel_sheet.Cells[i, 3].Value.ToString(); } catch { }
                        try { add_database_acc.Email_2FA = excel_sheet.Cells[i, 4].Value.ToString(); } catch { }
                        try { add_database_acc.RecoveryEmail = excel_sheet.Cells[i, 5].Value.ToString(); } catch { }
                        try { add_database_acc.Forward_Email = excel_sheet.Cells[i, 6].Value.ToString(); } catch { }
                        try { add_database_acc.Proxy = excel_sheet.Cells[i, 7].Value.ToString(); } catch { }

                        var new_acc_email = excel_sheet.Cells[i, 1].Value.ToString();
                        string email_type = null;
                        try { email_type = new_acc_email.Split('@')[1]; } catch (Exception ex) { }
                        add_database_acc.EmailType = email_type;

                        string change_info = "";
                        try { change_info = excel_sheet.Cells[i, 8].Value.ToString(); } catch { }
                        if (change_info == "1")
                        {
                            add_database_acc.AccType = "Chờ Set";
                        }
                        else
                        {
                            add_database_acc.AccType = "Đã đổi thông tin + uplink";

                            add_database_acc.Change_Acc_Info_All = true;
                            add_database_acc.Remove_OldRecoveryEmail = true;
                            add_database_acc.Add_NewRecoveryEmail = true;
                            add_database_acc.Remove_OldForwardEmail = true;
                            add_database_acc.Add_NewForwardEmail = true;
                            add_database_acc.Change_EmailPassword = true;
                            add_database_acc.Change_AccPassword = true;
                            add_database_acc.Up_Link_Status = true;
                        }

                        db.Accounts.Add(add_database_acc);
                        db.SaveChanges();
                    }
                }
            }
            return list_table_acc;
        }

        private void google_sheet_BTN_Click(object sender, EventArgs e)
        {
            var new_accounts_list = new Update_Database_To_Google_Sheet().Get_New_Accounts_List();
            if(new_accounts_list.Count > 0) 
            {
                List<Add_New_Account_Model> list_table_acc = new List<Add_New_Account_Model>();
                List<string> new_email_repeatation_list = new List<string>();
                List<Account> old_accounts_list = new AccountDao().Get_All_Accounts();

                for (int i = 0; i < new_accounts_list.Count; i++)
                {
                    //check trùng với Acc cũ
                    Add_New_Account_Model table_acc = new Add_New_Account_Model();
                    Account add_database_acc = new Account();

                    string check_email = "";
                    try { check_email = new_accounts_list[i][0].ToString(); } catch { }
                    if (string.IsNullOrEmpty(check_email)) { continue; }

                    bool no_repeatation_01 = true;
                    if (old_accounts_list.Count > 0)
                    {
                        foreach (var old_account in old_accounts_list)
                        {
                            if (new_accounts_list[i][0].ToString() == old_account.Email)
                            {
                                no_repeatation_01 = false;
                                break;
                            }
                        }
                    }

                    if (no_repeatation_01 == false) //Trùng với Acc cũ
                    {
                        table_acc.STT = i + 1;
                        table_acc.Email = new_accounts_list[i][0].ToString();
                        try { table_acc.AccPassword = new_accounts_list[i][1].ToString(); } catch { }
                        try { table_acc.EmailPassword = new_accounts_list[i][2].ToString(); } catch { }
                        try { table_acc.Email_2FA = new_accounts_list[i][3].ToString(); } catch { }
                        try { table_acc.RecoveryEmail = new_accounts_list[i][4].ToString(); } catch { }
                        try { table_acc.Forward_Email = new_accounts_list[i][5].ToString(); } catch { }
                        try { table_acc.Proxy = new_accounts_list[i][6].ToString(); } catch { }

                        string change_info_table = "";
                        try { change_info_table = new_accounts_list[i][7].ToString(); } catch { }
                        if (change_info_table == "1") { table_acc.Change_Info = "YES"; } else { table_acc.Change_Info = "NO"; }

                        table_acc.Result = "Trùng với Acc cũ!";
                        list_table_acc.Add(table_acc);
                    }
                    else //Không trùng acc cũ;
                    {
                        bool no_repeatation_02 = true;
                        for (int j = 0; j < new_accounts_list.Count; j++)
                        {
                            if (j == i)
                            {
                                continue;
                            }
                            else
                            {
                                if (new_accounts_list[i][0] == new_accounts_list[j][0])
                                {
                                    if (new_email_repeatation_list.Count > 0)
                                    {
                                        bool add_email_list = true;
                                        foreach (string email in new_email_repeatation_list)
                                        {
                                            if (new_accounts_list[i][0].ToString() == email)
                                            {
                                                no_repeatation_02 = false; //Không phải là giá trị đầu tiên
                                                add_email_list = false;
                                                break;
                                            }
                                        }
                                        if (add_email_list == true)
                                        {
                                            new_email_repeatation_list.Add(new_accounts_list[j][0].ToString());
                                        }
                                    }
                                    else
                                    {
                                        new_email_repeatation_list.Add(new_accounts_list[j][0].ToString());
                                    }
                                    break;
                                }
                            }
                        }

                        if (no_repeatation_02 == false)
                        {
                            table_acc.STT = i - 3;
                            table_acc.Email = new_accounts_list[i][0].ToString();
                            try { table_acc.AccPassword = new_accounts_list[i][1].ToString(); } catch { }
                            try { table_acc.EmailPassword = new_accounts_list[i][2].ToString(); } catch { }
                            try { table_acc.Email_2FA = new_accounts_list[i][3].ToString(); } catch { }
                            try { table_acc.RecoveryEmail = new_accounts_list[i][4].ToString(); } catch { }
                            try { table_acc.Forward_Email = new_accounts_list[i][5].ToString(); } catch { }
                            try { table_acc.Proxy = new_accounts_list[i][6].ToString(); } catch { }

                            string change_info_table = "";
                            try { change_info_table = new_accounts_list[i][7].ToString(); } catch { }
                            if (change_info_table == "1") { table_acc.Change_Info = "YES"; } else { table_acc.Change_Info = "NO"; }

                            table_acc.Result = "Trùng với Acc mới!";
                            list_table_acc.Add(table_acc);
                        }
                        else //Trả kết quả về giao diện
                        {
                            table_acc.STT = i - 3;
                            table_acc.Email = new_accounts_list[i][0].ToString();
                            try { table_acc.AccPassword = new_accounts_list[i][1].ToString(); } catch { }
                            try { table_acc.EmailPassword = new_accounts_list[i][2].ToString(); } catch { }
                            try { table_acc.Email_2FA = new_accounts_list[i][3].ToString(); } catch { }
                            try { table_acc.RecoveryEmail = new_accounts_list[i][4].ToString(); } catch { }
                            try { table_acc.Forward_Email = new_accounts_list[i][5].ToString(); } catch { }
                            try { table_acc.Proxy = new_accounts_list[i][6].ToString(); } catch { }

                            string change_info_table = "";
                            try { change_info_table = new_accounts_list[i][7].ToString(); } catch { }
                            if (change_info_table == "1") { table_acc.Change_Info = "YES"; } else { table_acc.Change_Info = "NO"; }

                            table_acc.Result = "Thêm Acc thành công!";
                            list_table_acc.Add(table_acc);

                            //Add vào database
                            add_database_acc.Email = new_accounts_list[i][0].ToString();
                            try { add_database_acc.AccPassword = new_accounts_list[i][1].ToString(); } catch { }
                            try { add_database_acc.EmailPassword = new_accounts_list[i][2].ToString(); } catch { }
                            try { add_database_acc.Email_2FA = new_accounts_list[i][3].ToString(); } catch { }
                            try { add_database_acc.RecoveryEmail = new_accounts_list[i][4].ToString(); } catch { }
                            try { add_database_acc.Forward_Email = new_accounts_list[i][5].ToString(); } catch { }
                            try { add_database_acc.Proxy = new_accounts_list[i][6].ToString(); } catch { }

                            var new_acc_email = new_accounts_list[i][0].ToString();
                            string email_type = null;
                            try { email_type = new_acc_email.Split('@')[1]; } catch { }
                            add_database_acc.EmailType = email_type;

                            string change_info = "";
                            try { change_info = new_accounts_list[i][7].ToString(); } catch { }
                            if (change_info == "1")
                            {
                                add_database_acc.AccType = "Chờ Set";
                            }
                            else
                            {
                                add_database_acc.AccType = "Đã đổi thông tin + uplink";

                                add_database_acc.Change_Acc_Info_All = true;
                                add_database_acc.Remove_OldRecoveryEmail = true;
                                add_database_acc.Add_NewRecoveryEmail = true;
                                add_database_acc.Remove_OldForwardEmail = true;
                                add_database_acc.Add_NewForwardEmail = true;
                                add_database_acc.Change_EmailPassword = true;
                                add_database_acc.Change_AccPassword = true;
                                add_database_acc.Up_Link_Status = true;
                            }

                            db.Accounts.Add(add_database_acc);
                            db.SaveChanges();
                        }
                    }
                }
                grvAddNewAccounts.DataSource = list_table_acc;

                try
                {
                    new Update_Database_To_Google_Sheet().Update_Database();
                }
                catch
                {
                    MessageBox.Show("Cập nhật Google Sheet lỗi!", "Thông báo");
                }
            }
            else 
            {
                MessageBox.Show("Goolge Sheet không có dữ liệu", "Thông báo");
            }
        }
    }
}
