using PAYONEER.ChromeDrivers;
using PAYONEER.Dao;
using PAYONEER.DataConnection;
using PAYONEER.DataTransfer;
using PAYONEER.Forms;
using PAYONEER.GPM_API;
using PAYONEER.Models;
using PAYONEER.RandomData;
using PAYONEER.Goolge_Sheets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PAYONEER
{
    public partial class fManager : Form
    {
        List<AccountsModel> data = null;
        Column_Set_Status_Model column_status = null;
        public fManager()
        {
            new Check_API().Start();
            InitializeComponent();
            tboxSearch.Text = null;
            Load_Data_Table(combAccountCategory.Text, column_status);
            Load_RightMouse_Click_Menu();
            Load_Acc_Counting_TextBox();
            try 
            { 
                new Update_Database_To_Google_Sheet().Update_Database();
                new Save_Deleted_Accounts().Update_Databe_Del_Accounts();
            } 
            catch 
            {
                MessageBox.Show("Cập nhật Google Sheet lỗi!", "Thông báo");
            }
        }
        
        private void btnAddNewAccounts_Click(object sender, EventArgs e)
        {
            fAddNewAccounts form = new fAddNewAccounts();
            this.Hide();
            form.ShowDialog();
            this.Show();
            Load_Data_Table(combAccountCategory.Text, column_status);
        }
        private void btnDeleteAccounts_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn muốn xóa tài khoản?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    List<Account> list_accounts = new List<Account>();
                    for (int i = (selectedRowCount - 1); i >= 0; i--)
                    {
                        var accID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
                        var account = new AccountDao().Get_Account_ById(accID);
                        list_accounts.Add(account);
                        new AccountDao().Delete_Account(accID);                      
                    }
                    new Save_Del_Accounts().Save(list_accounts);
                    try { new Save_Deleted_Accounts().Save_Info(list_accounts); } catch { }
                    try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { }
                    Load_Data_Table(combAccountCategory.Text, column_status);

                    MessageBox.Show("Đã xóa!", "Thông báo");
                }               
            }
            else { MessageBox.Show("Cần chọn full dòng!"); }
        }
        private void btnCheckAPI_Click(object sender, EventArgs e)
        {
            new Check_API().Check();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Sau khi copy bạn cần bỏ 'Tick' nếu muốn trở về chế độ làm việc bình thường!", "Thông báo!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    grvAccountsTable.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    grvAccountsTable.MultiSelect = true;
                    grvAccountsTable.RowHeadersWidth = 65;
                }
                else { checkBox1.Checked = false; }
            }
            else
            {
                grvAccountsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grvAccountsTable.MultiSelect = true;
                grvAccountsTable.RowHeadersWidth = 15;
            }
        }
        private void btnFindEmail_Click(object sender, EventArgs e)
        {         
            string search_email = "";
            try { search_email = tboxSearch.Text.ToString(); } catch { }
            if (!string.IsNullOrEmpty(search_email)) 
            {
                combAccountCategory.Text = "Chọn danh mục";
                tbox_Account_Counting.Text = null;
                Load_Data_Table(combAccountCategory.Text, column_status); 
            }        
        }
        private void combAccountCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            tboxSearch.Text = null;
            Load_Acc_Counting_TextBox();
            Load_Data_Table(combAccountCategory.Text, column_status);
        }
        private void Load_Acc_Counting_TextBox () 
        {
            int get_total = new Load_Count_Accounts_By_Category_Dao().Count_Accounts(combAccountCategory.Text.ToString());
            tbox_Account_Counting.Text = get_total.ToString();
        }
        private void Reset_Table_Column(Column_Set_Status_Model stt)
        {
            column_status = stt;
            Load_Data_Table(combAccountCategory.Text, column_status);
        }
        private void btnColumnSet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tboxSearch.Text))
            {
                var account_check = new AccountDao().Get_Account_By_Email(tboxSearch.Text);
                if (account_check == null)
                {
                    tboxSearch.Text = null;
                }
                else
                {
                    combAccountCategory.Text = "Chọn danh mục";
                    tbox_Account_Counting.Text = null;
                }
            }
            fSetColumn form = new fSetColumn(column_status);
            form.sendStatus = new fSetColumn.Send_Back(Reset_Table_Column);
            form.ShowDialog();
        }
        private void Load_Data_Table(string combobox_text, Column_Set_Status_Model status)
        {
            string search_email = "";
            try { search_email = tboxSearch.Text.ToString(); } catch { }
            if (!string.IsNullOrEmpty(search_email))
            {
                combobox_text = "Chọn danh mục";
                tbox_Account_Counting.Text = null;
                bool check = new AccountDao().Check_Email(search_email);
                if(check == true) 
                {
                    Account account = new AccountDao().Get_Account_By_Email(search_email);
                    List<Account> listAcc = new List<Account>();
                    listAcc.Add(account);
                    data = new Accounts_Table().Get_Accounts_List(listAcc);
                }
                else 
                {
                    data = new List<AccountsModel>();
                }
            }
            else
            {
                List<Account> listAcc = new Load_Count_Accounts_By_Category_Dao().Get_Accounts(combobox_text.ToString());
                data = new Accounts_Table().Get_Accounts_List(listAcc);
            }     
            
            grvAccountsTable.DataSource = data;
            if(status == null) 
            {
                grvAccountsTable.Columns[1].Visible = false;

                grvAccountsTable.Columns[4].Visible = false;
                grvAccountsTable.Columns[5].Visible = false;
                grvAccountsTable.Columns[6].Visible = false;
                grvAccountsTable.Columns[7].Visible = false;
                grvAccountsTable.Columns[8].Visible = false;
                grvAccountsTable.Columns[9].Visible = false;
                grvAccountsTable.Columns[10].Visible = false;
                grvAccountsTable.Columns[11].Visible = false;
                grvAccountsTable.Columns[12].Visible = false;

                grvAccountsTable.Columns[14].Visible = false;
                grvAccountsTable.Columns[15].Visible = false;
                grvAccountsTable.Columns[16].Visible = false;
                grvAccountsTable.Columns[17].Visible = false;

                grvAccountsTable.Columns[24].Visible = false;

                grvAccountsTable.Columns[28].Visible = false;
                grvAccountsTable.Columns[29].Visible = false;
                grvAccountsTable.Columns[30].Visible = false;
                grvAccountsTable.Columns[31].Visible = false;
                grvAccountsTable.Columns[32].Visible = false;
                grvAccountsTable.Columns[33].Visible = false;
                grvAccountsTable.Columns[34].Visible = false;
                grvAccountsTable.Columns[35].Visible = false;
                grvAccountsTable.Columns[36].Visible = false;
            }
            else
            {
                if (status.STT == true) { grvAccountsTable.Columns[0].Visible = true; } else { grvAccountsTable.Columns[0].Visible = false; }
                grvAccountsTable.Columns[1].Visible = false;
                if (status.Email == true) { grvAccountsTable.Columns[2].Visible = true; } else { grvAccountsTable.Columns[2].Visible = false; }
                if (status.SessionResult == true) { grvAccountsTable.Columns[3].Visible = true; } else { grvAccountsTable.Columns[3].Visible = false; }
                if (status.AccPassword == true) { grvAccountsTable.Columns[4].Visible = true; } else { grvAccountsTable.Columns[4].Visible = false; }
                if (status.AccQuestion1 == true) { grvAccountsTable.Columns[5].Visible = true; } else { grvAccountsTable.Columns[5].Visible = false; }
                if (status.AccQuestion2 == true) { grvAccountsTable.Columns[6].Visible = true; } else { grvAccountsTable.Columns[6].Visible = false; }
                if (status.AccQuestion3 == true) { grvAccountsTable.Columns[7].Visible = true; } else { grvAccountsTable.Columns[7].Visible = false; }
                if (status.EmailPassword == true) { grvAccountsTable.Columns[8].Visible = true; } else { grvAccountsTable.Columns[8].Visible = false; }
                if (status.RecoveryEmail == true) { grvAccountsTable.Columns[9].Visible = true; } else { grvAccountsTable.Columns[9].Visible = false; }
                if (status.Forward_Email == true) { grvAccountsTable.Columns[10].Visible = true; } else { grvAccountsTable.Columns[10].Visible = false; }
                if (status.Email_2FA == true) { grvAccountsTable.Columns[11].Visible = true; } else { grvAccountsTable.Columns[11].Visible = false; }
                if (status.Proxy == true) { grvAccountsTable.Columns[12].Visible = true; } else { grvAccountsTable.Columns[12].Visible = false; }
                if (status.Profile == true) { grvAccountsTable.Columns[13].Visible = true; } else { grvAccountsTable.Columns[13].Visible = false; }
                if (status.ProfileID == true) { grvAccountsTable.Columns[14].Visible = true; } else { grvAccountsTable.Columns[14].Visible = false; }
                grvAccountsTable.Columns[15].Visible = false;
                grvAccountsTable.Columns[16].Visible = false;
                grvAccountsTable.Columns[17].Visible = false;
                if (status.Change_Acc_Info_All == true) { grvAccountsTable.Columns[18].Visible = true; } else { grvAccountsTable.Columns[18].Visible = false; }
                if (status.Remove_OldForwardEmail == true) { grvAccountsTable.Columns[19].Visible = true; } else { grvAccountsTable.Columns[19].Visible = false; }
                if (status.Add_NewForwardEmail == true) { grvAccountsTable.Columns[20].Visible = true; } else { grvAccountsTable.Columns[20].Visible = false; }
                if (status.Remove_OldRecoveryEmail == true) { grvAccountsTable.Columns[21].Visible = true; } else { grvAccountsTable.Columns[21].Visible = false; }
                if (status.Add_NewRecoveryEmail == true) { grvAccountsTable.Columns[22].Visible = true; } else { grvAccountsTable.Columns[22].Visible = false; }
                if (status.Change_EmailPassword == true) { grvAccountsTable.Columns[23].Visible = true; } else { grvAccountsTable.Columns[23].Visible = false; }
                grvAccountsTable.Columns[24].Visible = false;
                if (status.Change_AccPassword == true) { grvAccountsTable.Columns[25].Visible = true; } else { grvAccountsTable.Columns[25].Visible = false; }
                if (status.Add_AccQuestion == true) { grvAccountsTable.Columns[26].Visible = true; } else { grvAccountsTable.Columns[26].Visible = false; }
                if (status.Up_Link_Status == true) { grvAccountsTable.Columns[27].Visible = true; } else { grvAccountsTable.Columns[27].Visible = false; }
                if (status.Old_AccPassword == true) { grvAccountsTable.Columns[28].Visible = true; } else { grvAccountsTable.Columns[28].Visible = false; }
                if (status.Old_EmailPassword == true) { grvAccountsTable.Columns[29].Visible = true; } else { grvAccountsTable.Columns[29].Visible = false; }
                if (status.Old_RecoveryEmail == true) { grvAccountsTable.Columns[30].Visible = true; } else { grvAccountsTable.Columns[30].Visible = false; }
                if (status.Old_AccQuestion == true) { grvAccountsTable.Columns[31].Visible = true; } else { grvAccountsTable.Columns[31].Visible = false; }
                grvAccountsTable.Columns[32].Visible = false;
                grvAccountsTable.Columns[33].Visible = false;           
                if (status.Random_AccPassword == true) { grvAccountsTable.Columns[34].Visible = true; } else { grvAccountsTable.Columns[34].Visible = false; }
                if (status.Random_EmailPassword == true) { grvAccountsTable.Columns[35].Visible = true; } else { grvAccountsTable.Columns[35].Visible = false; }
                if (status.Random_Question == true) { grvAccountsTable.Columns[36].Visible = true; } else { grvAccountsTable.Columns[36].Visible = false; }
            }
        }

        private void Load_RightMouse_Click_Menu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem set_new_acc_all = (ToolStripMenuItem)menu.Items.Add("Set tài khoản mới");
            ToolStripItem set_gmail_all = set_new_acc_all.DropDownItems.Add("Set Gmail - All");
            set_gmail_all.Click += new EventHandler(Set_Gmail_All);
            ToolStripItem set_po_all = set_new_acc_all.DropDownItems.Add("Set PO - All");
            set_po_all.Click += new EventHandler(Set_PO_All);
            ToolStripItem login_gmail = set_new_acc_all.DropDownItems.Add("Đăng nhập Gmail");
            login_gmail.Click += new EventHandler(Gmail_Login);

            ToolStripMenuItem change_acc_info = (ToolStripMenuItem)menu.Items.Add("Đổi thông tin PO");         
            ToolStripItem change_po_password = change_acc_info.DropDownItems.Add("Đổi mật khẩu PO");
            change_po_password.Click += new EventHandler(Change_PO_Password_Gmail);
            ToolStripItem add_po_question = change_acc_info.DropDownItems.Add("Đổi/thêm câu hỏi BM");
            add_po_question.Click += new EventHandler(Add_PO_Questions_Gmail);

            ToolStripMenuItem change_gmail_info = (ToolStripMenuItem)menu.Items.Add("Đổi thông tin Gmail");

            ToolStripItem change_gmail_password = change_gmail_info.DropDownItems.Add("Đổi mật khẩu Gmail");
            change_gmail_password.Click += new EventHandler(Change_Gmail_Password);

            ToolStripItem change_add_new_recovery_email = change_gmail_info.DropDownItems.Add("Đổi/thêm Email khôi phục");
            change_add_new_recovery_email.Click += new EventHandler(Change_Add_New_RecoverEmail_Gmail_Beeliant);

            ToolStripItem remove_forward_email = change_gmail_info.DropDownItems.Add("Xóa Email forward");
            remove_forward_email.Click += new EventHandler(Remove_Forward_Email_Gmail);
            ToolStripItem add_forward_email = change_gmail_info.DropDownItems.Add("Thêm Email forward");
            add_forward_email.Click += new EventHandler(Add_Forward_Email_Gmail_Gmail);

            //Profile
            ToolStripMenuItem profile = (ToolStripMenuItem)menu.Items.Add("Quản lý Profile");
            

            ToolStripItem check_canvas = profile.DropDownItems.Add("Check Canvas");
            check_canvas.Click += new EventHandler(Check_Canvas);
            ToolStripItem canvas_on = profile.DropDownItems.Add("Set Canvas - ON");
            canvas_on.Click += new EventHandler(Set_Canvas_ON);
            ToolStripItem canvas_off = profile.DropDownItems.Add("Set Canvas - OFF");
            canvas_off.Click += new EventHandler(Set_Canvas_OFF);

            ToolStripItem past_profileId = profile.DropDownItems.Add("Dán ProfileId");
            past_profileId.Click += new EventHandler(Past_ProfileID);
            ToolStripItem delete_profile = profile.DropDownItems.Add("Xóa Profile");
            delete_profile.Click += new EventHandler(Delete_Profile);

            ToolStripItem past_proxy = profile.DropDownItems.Add("Dán 1 Proxy - Copy");
            past_proxy.Click += new EventHandler(Update_One_Proxy);
            ToolStripItem past_proxy_list = profile.DropDownItems.Add("Dán Proxy - Google Sheet");
            past_proxy_list.Click += new EventHandler(Update_List_Proxy);


            ToolStripMenuItem past_account_info = (ToolStripMenuItem)menu.Items.Add("Dán thông tin Acc");           
            ToolStripItem past_po_password = past_account_info.DropDownItems.Add("Dán mật khẩu PO");
            past_po_password.Click += new EventHandler(Past_PO_Password);
            ToolStripItem past_email_password = past_account_info.DropDownItems.Add("Dán mật khẩu Email");
            past_email_password.Click += new EventHandler(Past_Email_Password);
            ToolStripItem past_recovery_email = past_account_info.DropDownItems.Add("Dán Email khôi phục");
            past_recovery_email.Click += new EventHandler(Past_Revovery_Email);
            ToolStripItem past_forward_email = past_account_info.DropDownItems.Add("Dán Email forward");
            past_forward_email.Click += new EventHandler(Past_Forward_Email);
            // GMP-Login
            ToolStripMenuItem GMP_Login_API = (ToolStripMenuItem)menu.Items.Add("Set GMP-Login API");
            ToolStripItem past_api_url = GMP_Login_API.DropDownItems.Add("Dán API URL");
            past_api_url.Click += new EventHandler(Past_API_URL_CLick);
            ToolStripItem past_beeliant_profileId = GMP_Login_API.DropDownItems.Add("Dán Beeliant ProfileId");
            past_beeliant_profileId.Click +=  new EventHandler(Past_Beeliant_ProfileID_Click);
            ToolStripItem past_forward_email_gmail_profileId = GMP_Login_API.DropDownItems.Add("Dán ProfileId-Email-Forward-Gmail");
            past_forward_email_gmail_profileId.Click += new EventHandler(Past_ForwardEmail_Gmail_ProfileId);

            //
            //Google Sheet
            ToolStripMenuItem google_sheet = (ToolStripMenuItem)menu.Items.Add("Set Goolge Sheet");
            ToolStripItem past_google_sheet_id = google_sheet.DropDownItems.Add("Dán Google Sheet ID");
            past_google_sheet_id.Click += new EventHandler(Past_Google_Sheet_ID);
            ToolStripItem past_database_sheet_name = google_sheet.DropDownItems.Add("Dán tên sheet database");
            past_database_sheet_name.Click += new EventHandler(Past_Database_Sheet_Name);
            ToolStripItem past_backup_info_sheet_name = google_sheet.DropDownItems.Add("Dán tên sheet backup data");
            past_backup_info_sheet_name.Click += new EventHandler(Past_Backup_Info_Sheet_Link);

            grvAccountsTable.ContextMenuStrip = menu;
        }

        private void Update_One_Proxy(object sender, EventArgs e)
        {
            Int32 selected_row_count = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selected_row_count > 0)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var proxy = (String)iData.GetData(DataFormats.Text);
                int AccID = int.Parse(grvAccountsTable.SelectedRows[selected_row_count - 1].Cells[1].Value.ToString());
                PayoneerDbContext db = new PayoneerDbContext();
                var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                if (db_account.Profile == true)
                {
                    var check_api = new Check_API().Start();
                    if (check_api == true)
                    {
                        var result = new GPMLoginAPI().UpdateProxy(db_account.ProfileID, proxy);
                        if (result == true)
                        {
                            db_account.Proxy = proxy;
                            db.SaveChanges();
                            grvAccountsTable.SelectedRows[selected_row_count - 1].Cells[12].Value = proxy;
                        }
                    }
                }
                else
                {
                    db_account.Proxy = proxy;
                    db.SaveChanges();
                    grvAccountsTable.SelectedRows[selected_row_count - 1].Cells[12].Value = proxy;
                }

                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { MessageBox.Show("Cập nhật google sheet lỗi", "Thông báo"); }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo"); }
        }
        private void Update_List_Proxy(object sender, EventArgs e)
        {
            Int32 selected_row_count = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selected_row_count > 0)
            {
                var list_proxy = new Update_Database_To_Google_Sheet().Get_Proxy_List();
                int proxy_total = list_proxy.Count;

                if (proxy_total > 0)
                {
                    int index = 0;
                    if (proxy_total > selected_row_count) { index = selected_row_count; } else { index = proxy_total; }
                    int j = 0;
                    for (int i = selected_row_count - 1; i >= selected_row_count - index; i--)
                    {
                        string proxy = list_proxy[j][0].ToString();
                        int AccID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
                        PayoneerDbContext db = new PayoneerDbContext();
                        var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                        if (db_account.Profile == true)
                        {
                            var check_api = new Check_API().Start();
                            if (check_api == true)
                            {
                                var result = new GPMLoginAPI().UpdateProxy(db_account.ProfileID, proxy);
                                if (result == true)
                                {
                                    db_account.Proxy = proxy;
                                    db.SaveChanges();
                                    grvAccountsTable.SelectedRows[i].Cells[12].Value = proxy;
                                }
                            }
                        }
                        else
                        {
                            db_account.Proxy = proxy;
                            db.SaveChanges();
                            grvAccountsTable.SelectedRows[i].Cells[12].Value = proxy;
                        }
                        j++;
                    }
                }
                else
                {
                    MessageBox.Show("Google sheet không có dữ liệu", "Thông báo");
                }
                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { MessageBox.Show("Cập nhật google sheet lỗi", "Thông báo"); }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo"); }
        }

        private void Check_Canvas(object sender, EventArgs e)
        {
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    for (int i = (selectedRowCount - 1); i >= 0; i--)
                    {
                        int AccID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
                        PayoneerDbContext db = new PayoneerDbContext();
                        var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                        if (db_account.Canvas_Profiles == false)
                        {
                            grvAccountsTable.SelectedRows[i].Cells[3].Value = "Canvas = OFF --";
                        }
                        else
                        {
                            grvAccountsTable.SelectedRows[i].Cells[3].Value = "Canvas = ON --";
                        }
                    }
                }
                catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo"); }
        }
        private void Set_Canvas_ON(object sender, EventArgs e)
        {
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    for (int i = (selectedRowCount - 1); i >= 0; i--)
                    {
                        int AccID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
                        PayoneerDbContext db = new PayoneerDbContext();
                        var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                        db_account.Canvas_Profiles = true;
                        db.SaveChanges();
                        grvAccountsTable.SelectedRows[i].Cells[3].Value = "Canvas = ON --";
                    }
                }
                catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo"); }
        }
        private void Set_Canvas_OFF(object sender, EventArgs e)
        {
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    for (int i = (selectedRowCount - 1); i >= 0; i--)
                    {
                        int AccID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
                        PayoneerDbContext db = new PayoneerDbContext();
                        var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                        db_account.Canvas_Profiles = false;
                        db.SaveChanges();
                        grvAccountsTable.SelectedRows[i].Cells[3].Value = "Canvas = OFF --";
                    }

                }
                catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo"); }
        }
        private void Past_ProfileID(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            var copy_value = (String)iData.GetData(DataFormats.Text);
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var AccID = int.Parse(grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[1].Value.ToString());
                new AccountDao().Update_Account_ProfileId(AccID, copy_value);
                grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[14].Value = copy_value;
                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
        }
        private void Delete_Profile(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn muốn xóa tài khoản?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            for (int i = (selectedRowCount - 1); i >= 0; i--)
                            {
                                var Acc01 = Get_Acc_Info(i);
                                if (Acc01.Profile == true)
                                {
                                    PayoneerDbContext db = new PayoneerDbContext();
                                    string api_url = db.Admins.Where(x => x.Name == "API_URL").FirstOrDefault().Password;
                                    GPMLoginAPI api = new GPMLoginAPI();
                                    api.Delete(Acc01.ProfileID);
                                    var db_account = db.Accounts.Where(x => x.ID == Acc01.ID).FirstOrDefault();

                                    db_account.Profile = false;
                                    db_account.ProfileID = null;
                                    db_account.ProfileName = null;
                                    db_account.ProfilePath = null;
                                    db_account.ProfileCreatedTime = null;
                                    db.SaveChanges();

                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Profile";
                                    grvAccountsTable.SelectedRows[i].Cells[13].Value = "NO";
                                    grvAccountsTable.SelectedRows[i].Cells[14].Value = "";
                                    grvAccountsTable.SelectedRows[i].Cells[15].Value = "";
                                    grvAccountsTable.SelectedRows[i].Cells[16].Value = "";
                                    grvAccountsTable.SelectedRows[i].Cells[17].Value = "";
                                }
                                else
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa tạo Profile";
                                }
                            }

                            try
                            {
                                new Update_Database_To_Google_Sheet().Update_Database();
                            }
                            catch
                            {
                                MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo");
                            }
                        }
                        catch
                        {
                            try
                            {
                                new Update_Database_To_Google_Sheet().Update_Database();
                            }
                            catch
                            {
                                MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo");
                            }
                        }
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo"); }
            }
        }

        private void Past_PO_Password(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            var copy_value = (String)iData.GetData(DataFormats.Text);
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var AccID = int.Parse(grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[1].Value.ToString());
                new AccountDao().Update_PO_Password(AccID, copy_value);              
                grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[4].Value = copy_value;
                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
        }
        private void Past_Email_Password(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            var copy_value = (String)iData.GetData(DataFormats.Text);
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var AccID = int.Parse(grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[1].Value.ToString());
                new AccountDao().Update_Email_Password(AccID, copy_value);               
                grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[8].Value = copy_value;
                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
        }
        private void Past_Revovery_Email(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            var copy_value = (String)iData.GetData(DataFormats.Text);
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                var AccID = int.Parse(grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[1].Value.ToString());
                PayoneerDbContext db = new PayoneerDbContext();
                var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                if (!string.IsNullOrEmpty(db_account.RecoveryEmail)) { db_account.Old_RecoveryEmail += db_account.RecoveryEmail + "--"; }
                db_account.RecoveryEmail = copy_value;
                db.SaveChanges();
                grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[9].Value = copy_value;
                grvAccountsTable.SelectedRows[selectedRowCount - 1].Cells[30].Value = db_account.Old_RecoveryEmail;
                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
        }
        private void Past_Forward_Email(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            var copy_value = (String)iData.GetData(DataFormats.Text);
            Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for(int i = selectedRowCount - 1; i >=0; i--) 
                {
                    var AccID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
                    PayoneerDbContext db = new PayoneerDbContext();
                    var db_account = db.Accounts.Where(x => x.ID == AccID).FirstOrDefault();
                    db_account.Forward_Email = copy_value;
                    db.SaveChanges();
                    grvAccountsTable.SelectedRows[i].Cells[10].Value = copy_value;
                }
                try { new Update_Database_To_Google_Sheet().Update_Database(); } catch { }
            }
            else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
        }

        private void Past_API_URL_CLick(object sender, EventArgs e) 
        {
            DialogResult dialogResult = MessageBox.Show("Bạn muốn dán API URL?", "Xác nhận!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var api_url = (String)iData.GetData(DataFormats.Text);
                if (iData.GetDataPresent(DataFormats.Text) == true)
                {
                    bool result = new AdminDao().Update_GMP_API_URL(api_url);
                    if (result == true)
                    {
                        StringWriter stringWriter = new StringWriter();
                        stringWriter.WriteLine("- Dán thành công!");
                        stringWriter.WriteLine("- API URL hiện tại là: " + api_url);
                        MessageBox.Show(stringWriter.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
            }  
        }
        private void Past_Beeliant_ProfileID_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn muốn dán Beeliant ProfileId?", "Xác nhận!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var profileId = (String)iData.GetData(DataFormats.Text);
                if (iData.GetDataPresent(DataFormats.Text) == true)
                {
                    bool result = new AdminDao().Update_Beeliant_ProfileId(profileId);
                    if (result == true)
                    {
                        StringWriter stringWriter = new StringWriter();
                        stringWriter.WriteLine("- Dán thành công!");
                        stringWriter.WriteLine("- Beeliant ProfileId hiện tại là: ");
                        stringWriter.WriteLine(profileId);
                        MessageBox.Show(stringWriter.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
            }       
        }
        private void Past_ForwardEmail_Gmail_ProfileId(object sender, EventArgs e) 
        {
            DialogResult dialogResult = MessageBox.Show("Bạn muốn dán ProfileId email forward (gmail)?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            if (dialogResult == DialogResult.Yes)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var profileId = (String)iData.GetData(DataFormats.Text);
                if (iData.GetDataPresent(DataFormats.Text) == true)
                {
                    PayoneerDbContext db = new PayoneerDbContext();
                    db.Admins.Where(x => x.Name == "Forward_Email_Gmail").FirstOrDefault().Password = profileId;
                    db.SaveChanges();
                    StringWriter stringWriter = new StringWriter();
                    stringWriter.WriteLine("- Dán thành công!");
                    stringWriter.WriteLine("- ProfileId email forward (gmail) hiện tại là: ");
                    stringWriter.WriteLine(profileId);
                    MessageBox.Show(stringWriter.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, 
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
        }
        private void Past_Google_Sheet_ID (object sender, EventArgs e) 
        {
            DialogResult dialogResult = MessageBox.Show("Bạn muốn dán Google Sheet ID?", "Xác nhận!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var google_sheet_id = (String)iData.GetData(DataFormats.Text);
                if (iData.GetDataPresent(DataFormats.Text) == true)
                {
                    bool result = new AdminDao().Update_Google_Sheet_ID(google_sheet_id);
                    if (result == true)
                    {
                        StringWriter stringWriter = new StringWriter();
                        stringWriter.WriteLine("- Dán thành công!");
                        stringWriter.WriteLine("- Goolge Sheet ID là:");
                        stringWriter.WriteLine(google_sheet_id);
                        MessageBox.Show(stringWriter.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
            }
        }
        private void Past_Database_Sheet_Name(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn muốn dán tên sheet database?", "Xác nhận!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var sheet_name = (String)iData.GetData(DataFormats.Text);
                if (iData.GetDataPresent(DataFormats.Text) == true)
                {
                    bool result = new AdminDao().Update_Database_Sheet_Name(sheet_name);
                    if (result == true)
                    {
                        StringWriter stringWriter = new StringWriter();
                        stringWriter.WriteLine("- Dán thành công!");
                        stringWriter.WriteLine("- Tên sheet database là: " + sheet_name);
                        MessageBox.Show(stringWriter.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
            }
        }
        private void Past_Backup_Info_Sheet_Link(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn muốn dán tên sheet backup info?", "Xác nhận!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var sheet_name = (String)iData.GetData(DataFormats.Text);
                if (iData.GetDataPresent(DataFormats.Text) == true)
                {
                    bool result = new AdminDao().Update_Backup_Info_Sheet_Name(sheet_name);
                    if (result == true)
                    {
                        StringWriter stringWriter = new StringWriter();
                        stringWriter.WriteLine("- Dán thành công!");
                        stringWriter.WriteLine("- Tên sheet backup info là: " + sheet_name);
                        MessageBox.Show(stringWriter.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
            }
        }
        //Get Acc Info
        private Account Get_Acc_Info (int i)
        {
            int AccID = int.Parse(grvAccountsTable.SelectedRows[i].Cells[1].Value.ToString());
            var account = new AccountDao().Get_Account_ById(AccID);
            return account;
        }
        private void Set_Gmail_All (object sender, EventArgs e) 
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            if (Acc01.Change_Email_Info_All != true)
                            {
                                if (Acc01.EmailType == "gmail.com")
                                {
                                    try
                                    {
                                        if (Acc01.Profile != true)
                                        {
                                            if (string.IsNullOrEmpty(Acc01.Proxy))
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                            }
                                            else
                                            {
                                                var result_01 = new Create_Profiles().Create(Acc01);
                                                if (result_01.Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                    grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                    grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                                }
                                            }
                                        }

                                        //Mở Profile
                                        var Acc02 = Get_Acc_Info(i);
                                        if (driver == null && Acc02.Profile == true)
                                        {
                                            var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                            if (result_02.Status == true)
                                            {
                                                driver = result_02.Driver;
                                            }
                                        }

                                        //Đăng nhập Gmail
                                        var Acc03 = Get_Acc_Info(i);
                                        bool next_step = false;
                                        if (Acc03.Profile == true && driver != null)
                                        {
                                            var result_03 = new Gmail_Login().Login(Acc03, driver, cbox_setAcc_hold.Checked);
                                            if (result_03.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đăng nhập Email--";
                                                next_step = true;
                                            }
                                            driver = result_03.Driver;
                                        }

                                        //Xóa Email fw
                                        var Acc04 = Get_Acc_Info(i);
                                        if (Acc04.Remove_OldForwardEmail != true && next_step == true)
                                        {
                                            var result_04 = new Remove_Forward_Email_Gmail().Remove(Acc04, driver);
                                            if (result_04.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[19].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Email fw cũ--";
                                            }
                                            driver = result_04.Driver;
                                        }

                                        //Xóa email KP cũ + thêm Email KP mới
                                        var Acc05 = Get_Acc_Info(i);
                                        if (Acc05.Add_NewRecoveryEmail != true && next_step == true)
                                        {
                                            if (!string.IsNullOrEmpty(Acc05.RecoveryEmail))
                                            {
                                                if (Acc05.RecoveryEmail.Split('@')[1] == "beeliant.com")
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email KP đã là đuôi 'beeliant.com'--";
                                                }
                                                else
                                                {
                                                    var result_05 = new Add_New_Recovery_Email_Gmail_Beeliant().Add(Acc05, driver, cbox_setAcc_hold.Checked);
                                                    if (result_05.Status == true)
                                                    {
                                                        grvAccountsTable.SelectedRows[i].Cells[21].Value = "YES";
                                                        grvAccountsTable.SelectedRows[i].Cells[22].Value = "YES";
                                                        grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Email KP cũ, đã thêm Email KP mới--";
                                                        var db_account = new AccountDao().Get_Account_ById(Acc05.ID);
                                                        grvAccountsTable.SelectedRows[i].Cells[9].Value = db_account.RecoveryEmail;
                                                        grvAccountsTable.SelectedRows[i].Cells[30].Value = db_account.Old_RecoveryEmail;
                                                    }
                                                    driver = result_05.Driver;
                                                }
                                            }
                                            else
                                            {
                                                var result_05 = new Add_New_Recovery_Email_Gmail_Beeliant().Add(Acc05, driver, cbox_setAcc_hold.Checked);
                                                if (result_05.Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[21].Value = "YES";
                                                    grvAccountsTable.SelectedRows[i].Cells[22].Value = "YES";
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Email KP cũ, đã thêm Email KP mới--";
                                                    var db_account = new AccountDao().Get_Account_ById(Acc05.ID);
                                                    grvAccountsTable.SelectedRows[i].Cells[9].Value = db_account.RecoveryEmail;
                                                    grvAccountsTable.SelectedRows[i].Cells[30].Value = db_account.Old_RecoveryEmail;
                                                }
                                                driver = result_05.Driver;
                                            }
                                        }

                                        //Đổi mk email
                                        var Acc06 = Get_Acc_Info(i);
                                        if (Acc06.Change_EmailPassword != true && driver != null)
                                        {
                                            var result_06 = new Change_Gmail_Password().Change_Password(Acc06, driver);
                                            if (result_06.Status == true)
                                            {
                                                PayoneerDbContext db = new PayoneerDbContext();
                                                var db_account = db.Accounts.Where(x => x.ID == Acc06.ID).FirstOrDefault();

                                                grvAccountsTable.SelectedRows[i].Cells[8].Value = db_account.EmailPassword;
                                                grvAccountsTable.SelectedRows[i].Cells[29].Value = db_account.Old_EmailPassword;
                                                grvAccountsTable.SelectedRows[i].Cells[23].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đổi mk Email--";
                                            }
                                            driver = result_06.Driver;
                                        }

                                        if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                        try { driver.Quit(); } catch { }
                                        try { driver.Dispose(); } catch { }
                                    }
                                    catch
                                    {
                                        grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                        if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                        try { driver.Quit(); } catch { }
                                        try { driver.Dispose(); } catch { }
                                    }
                                }
                                else
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                                }
                            }
                            else
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đổi thông tin Email rồi--";
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        this.WindowState = FormWindowState.Minimized;
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Set_PO_All (object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            try
                            {
                                if (Acc01.Add_AccQuestion == true && Acc01.Change_AccPassword == true)
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đổi thông tin PO rồi--";
                                }
                                else
                                {
                                    if (Acc01.EmailType == "gmail.com")
                                    {
                                        if (Acc01.Change_Email_Info_All == true)
                                        {
                                            if (Acc01.Profile != true)
                                            {
                                                if (string.IsNullOrEmpty(Acc01.Proxy))
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                                }
                                                else
                                                {
                                                    var result_01 = new Create_Profiles().Create(Acc01);
                                                    if (result_01.Status == true)
                                                    {
                                                        grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                        grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                        grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                        grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                                    }
                                                }
                                            }

                                            //Mở Profile
                                            var Acc02 = Get_Acc_Info(i);
                                            bool next_step_02 = false;
                                            if (driver == null && Acc02.Profile == true)
                                            {
                                                var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                                if (result_02.Status == true)
                                                {
                                                    next_step_02 = true;
                                                }
                                                driver = result_02.Driver;
                                            }

                                            bool next_step_03 = false;
                                            if (next_step_02 == true)
                                            {
                                                var result_03 = new Login_Payoneer().Login(Acc02, driver);
                                                if (result_03.Status == true)
                                                {
                                                    next_step_03 = true;
                                                }
                                                driver = result_03.Driver;
                                            }

                                            if (next_step_03 = true && Acc02.Add_AccQuestion != true)
                                            {
                                                var result_04 = new Add_Payoneer_Security_Questions().Add_Questions(Acc02, driver);
                                                if (result_04.Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã thêm câu hỏi BM--";
                                                    grvAccountsTable.SelectedRows[i].Cells[26].Value = "Yes";
                                                    var db_account = new AccountDao().Get_Account_ById(Acc02.ID);
                                                    grvAccountsTable.SelectedRows[i].Cells[5].Value = db_account.AccQuestion1;
                                                    grvAccountsTable.SelectedRows[i].Cells[6].Value = db_account.AccQuestion2;
                                                    grvAccountsTable.SelectedRows[i].Cells[7].Value = db_account.AccQuestion3;

                                                    grvAccountsTable.SelectedRows[i].Cells[31].Value = db_account.Old_AccQuestion;
                                                    grvAccountsTable.SelectedRows[i].Cells[36].Value = db_account.Random_Question;
                                                }
                                                if (result_04.SetAcc_All_Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[18].Value = "Yes";
                                                }
                                                driver = result_04.Driver;
                                            }

                                            Thread.Sleep(RdTimes.T_2000());

                                            var Acc05 = Get_Acc_Info(i);
                                            if (next_step_03 = true && Acc05.Change_AccPassword != true)
                                            {
                                                var result_05 = new Change_Password_PO().Change_Password(Acc05, driver);
                                                if (result_05.Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đổi mật khẩu PO--";
                                                    grvAccountsTable.SelectedRows[i].Cells[25].Value = "Yes";
                                                    var db_account_01 = new AccountDao().Get_Account_ById(Acc05.ID);
                                                    grvAccountsTable.SelectedRows[i].Cells[4].Value = db_account_01.AccPassword;
                                                    grvAccountsTable.SelectedRows[i].Cells[28].Value = db_account_01.Old_AccPassword;
                                                    grvAccountsTable.SelectedRows[i].Cells[34].Value = db_account_01.Random_AccPassword;
                                                }
                                                driver = result_05.Driver;
                                            }

                                            if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                            try { driver.Quit(); } catch { }
                                            try { driver.Dispose(); } catch { }
                                        }
                                        else
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa đổi thông tin email, cần đổi thông tin email trước--";
                                        }
                                    }
                                    else
                                    {
                                        grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                                    }
                                }
                            }
                            catch
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                try { driver.Quit(); } catch { }
                                try { driver.Dispose(); } catch { }
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Gmail_Login(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            if (Acc01.EmailType == "gmail.com")
                            {
                                try
                                {
                                    if (Acc01.Profile != true)
                                    {
                                        if (string.IsNullOrEmpty(Acc01.Proxy))
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                        }
                                        else
                                        {
                                            var result_01 = new Create_Profiles().Create(Acc01);
                                            if (result_01.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                            }
                                        }
                                    }

                                    //Mở Profile
                                    var Acc02 = Get_Acc_Info(i);
                                    if (driver == null && Acc02.Profile == true)
                                    {
                                        var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                        if (result_02.Status == true)
                                        {
                                            driver = result_02.Driver;
                                        }
                                    }

                                    //Đăng nhập Gmail
                                    var Acc03 = Get_Acc_Info(i);
                                    if (Acc03.Profile == true && driver != null)
                                    {
                                        var result_03 = new Gmail_Login().Login(Acc03, driver, cbox_setAcc_hold.Checked);
                                        if (result_03.Status == true)
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đăng nhập Email--";
                                        }
                                        driver = result_03.Driver;
                                    }

                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                                catch
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                            }
                            else
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Không phải Gmail--";
                            }
                        }     
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Change_PO_Password_Gmail(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            try
                            {
                                if (Acc01.EmailType == "gmail.com")
                                {

                                    if (Acc01.Profile != true)
                                    {
                                        if (string.IsNullOrEmpty(Acc01.Proxy))
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                        }
                                        else
                                        {
                                            var result_01 = new Create_Profiles().Create(Acc01);
                                            if (result_01.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                            }
                                        }
                                    }

                                    //Mở Profile
                                    var Acc02 = Get_Acc_Info(i);
                                    bool next_step_02 = false;
                                    if (driver == null && Acc02.Profile == true)
                                    {
                                        var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                        if (result_02.Status == true)
                                        {
                                            next_step_02 = true;
                                        }
                                        driver = result_02.Driver;
                                    }

                                    bool next_step_03 = false;
                                    if (next_step_02 == true)
                                    {
                                        var result_03 = new Login_Payoneer().Login(Acc02, driver);
                                        if (result_03.Status == true)
                                        {
                                            next_step_03 = true;
                                        }
                                        driver = result_03.Driver;
                                    }

                                    var Acc05 = Get_Acc_Info(i);
                                    if (next_step_03 = true && Acc05.Change_AccPassword != true)
                                    {
                                        var result_05 = new Change_Password_PO().Change_Password(Acc05, driver);
                                        if (result_05.Status == true)
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đổi mật khẩu PO--";
                                            grvAccountsTable.SelectedRows[i].Cells[25].Value = "Yes";
                                            var db_account_01 = new AccountDao().Get_Account_ById(Acc05.ID);
                                            grvAccountsTable.SelectedRows[i].Cells[4].Value = db_account_01.AccPassword;
                                            grvAccountsTable.SelectedRows[i].Cells[28].Value = db_account_01.Old_AccPassword;
                                            grvAccountsTable.SelectedRows[i].Cells[34].Value = db_account_01.Random_AccPassword;
                                        }
                                        driver = result_05.Driver;
                                    }

                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                                else
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                                }
                            }
                            catch
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                try { driver.Quit(); } catch { }
                                try { driver.Dispose(); } catch { }
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            } 
        private void Add_PO_Questions_Gmail(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            try
                            {
                                if (Acc01.EmailType == "gmail.com")
                                {
                                    if (Acc01.Profile != true)
                                    {
                                        if (string.IsNullOrEmpty(Acc01.Proxy))
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                        }
                                        else
                                        {
                                            var result_01 = new Create_Profiles().Create(Acc01);
                                            if (result_01.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                            }
                                        }
                                    }

                                    //Mở Profile
                                    var Acc02 = Get_Acc_Info(i);
                                    bool next_step_02 = false;
                                    if (driver == null && Acc02.Profile == true)
                                    {
                                        var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                        if (result_02.Status == true)
                                        {
                                            next_step_02 = true;
                                        }
                                        driver = result_02.Driver;
                                    }

                                    bool next_step_03 = false;
                                    if (next_step_02 == true)
                                    {
                                        var result_03 = new Login_Payoneer().Login(Acc02, driver);
                                        if (result_03.Status == true)
                                        {
                                            next_step_03 = true;
                                        }
                                        driver = result_03.Driver;
                                    }

                                    if (next_step_03 = true && Acc02.Add_AccQuestion != true)
                                    {
                                        var result_04 = new Add_Payoneer_Security_Questions().Add_Questions(Acc02, driver);
                                        if (result_04.Status == true)
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã thêm câu hỏi BM--";
                                            grvAccountsTable.SelectedRows[i].Cells[26].Value = "Yes";
                                            var db_account = new AccountDao().Get_Account_ById(Acc02.ID);
                                            grvAccountsTable.SelectedRows[i].Cells[5].Value = db_account.AccQuestion1;
                                            grvAccountsTable.SelectedRows[i].Cells[6].Value = db_account.AccQuestion2;
                                            grvAccountsTable.SelectedRows[i].Cells[7].Value = db_account.AccQuestion3;

                                            grvAccountsTable.SelectedRows[i].Cells[31].Value = db_account.Old_AccQuestion;
                                            grvAccountsTable.SelectedRows[i].Cells[36].Value = db_account.Random_Question;
                                        }
                                        if (result_04.SetAcc_All_Status == true)
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[18].Value = "Yes";
                                        }
                                        driver = result_04.Driver;
                                    }

                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                                else
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                                }
                            }
                            catch
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                try { driver.Quit(); } catch { }
                                try { driver.Dispose(); } catch { }
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Change_Gmail_Password(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            if (Acc01.EmailType == "gmail.com")
                            {
                                try
                                {
                                    if (Acc01.Profile != true)
                                    {
                                        if (string.IsNullOrEmpty(Acc01.Proxy))
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                        }
                                        else
                                        {
                                            var result_01 = new Create_Profiles().Create(Acc01);
                                            if (result_01.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                            }
                                        }
                                    }

                                    //Mở Profile
                                    var Acc02 = Get_Acc_Info(i);
                                    if (driver == null && Acc02.Profile == true)
                                    {
                                        var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                        if (result_02.Status == true)
                                        {
                                            driver = result_02.Driver;
                                        }
                                    }

                                    //Đổi mk email
                                    var Acc06 = Get_Acc_Info(i);
                                    if (Acc06.Profile == true && driver != null)
                                    {
                                        var result_06 = new Change_Gmail_Password().Change_Password(Acc06, driver);
                                        if (result_06.Status == true)
                                        {
                                            PayoneerDbContext db = new PayoneerDbContext();
                                            var db_account = db.Accounts.Where(x => x.ID == Acc06.ID).FirstOrDefault();

                                            grvAccountsTable.SelectedRows[i].Cells[8].Value = db_account.EmailPassword;
                                            grvAccountsTable.SelectedRows[i].Cells[29].Value = db_account.Old_EmailPassword;
                                            grvAccountsTable.SelectedRows[i].Cells[23].Value = "YES";
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã đổi mk Email--";
                                        }
                                        driver = result_06.Driver;
                                    }

                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                                catch
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                            }
                            else
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Change_Add_New_RecoverEmail_Gmail_Beeliant(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            if (Acc01.EmailType == "gmail.com")
                            {
                                try
                                {
                                    if (Acc01.Profile != true)
                                    {
                                        if (string.IsNullOrEmpty(Acc01.Proxy))
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                        }
                                        else
                                        {
                                            var result_01 = new Create_Profiles().Create(Acc01);
                                            if (result_01.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                            }
                                        }
                                    }

                                    //Mở Profile
                                    bool next_step = false;
                                    var Acc02 = Get_Acc_Info(i);
                                    if (driver == null && Acc02.Profile == true)
                                    {
                                        var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                        if (result_02.Status == true)
                                        {
                                            driver = result_02.Driver;
                                            next_step = true;
                                        }
                                    }

                                    //Xóa email KP cũ + thêm Email KP mới
                                    var Acc05 = Get_Acc_Info(i);
                                    if (next_step == true)
                                    {
                                        if (!string.IsNullOrEmpty(Acc05.RecoveryEmail))
                                        {
                                            if (Acc05.RecoveryEmail.Split('@')[1] == "beeliant.com")
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email KP đã là đuôi 'beeliant.com'--";
                                            }
                                            else
                                            {
                                                var result_05 = new Add_New_Recovery_Email_Gmail_Beeliant().Add(Acc05, driver, cbox_setAcc_hold.Checked);
                                                if (result_05.Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[21].Value = "YES";
                                                    grvAccountsTable.SelectedRows[i].Cells[22].Value = "YES";
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Email KP cũ, đã thêm Email KP mới--";
                                                    var db_account = new AccountDao().Get_Account_ById(Acc05.ID);
                                                    grvAccountsTable.SelectedRows[i].Cells[9].Value = db_account.RecoveryEmail;
                                                    grvAccountsTable.SelectedRows[i].Cells[30].Value = db_account.Old_RecoveryEmail;
                                                }
                                                driver = result_05.Driver;
                                            }
                                        }
                                        else
                                        {
                                            var result_05 = new Add_New_Recovery_Email_Gmail_Beeliant().Add(Acc05, driver, cbox_setAcc_hold.Checked);
                                            if (result_05.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[21].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[22].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Email KP cũ, đã thêm Email KP mới--";
                                                var db_account = new AccountDao().Get_Account_ById(Acc05.ID);
                                                grvAccountsTable.SelectedRows[i].Cells[9].Value = db_account.RecoveryEmail;
                                                grvAccountsTable.SelectedRows[i].Cells[30].Value = db_account.Old_RecoveryEmail;
                                            }
                                            driver = result_05.Driver;
                                        }
                                    }
                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                                catch
                                {
                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                            }
                            else
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Remove_Forward_Email_Gmail(object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            if (Acc01.EmailType == "gmail.com")
                            {
                                try
                                {
                                    if (Acc01.Profile != true)
                                    {
                                        if (string.IsNullOrEmpty(Acc01.Proxy))
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                        }
                                        else
                                        {
                                            var result_01 = new Create_Profiles().Create(Acc01);
                                            if (result_01.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã tạo Profile--";
                                            }
                                        }
                                    }

                                    //Mở Profile
                                    var Acc02 = Get_Acc_Info(i);
                                    if (driver == null && Acc02.Profile == true)
                                    {
                                        var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                        if (result_02.Status == true)
                                        {
                                            driver = result_02.Driver;
                                        }
                                    }

                                    //
                                    var Acc03 = Get_Acc_Info(i);
                                    if (Acc03.Profile == true && driver != null)
                                    {
                                        var result_03 = new Remove_Forward_Email_Gmail().Remove(Acc03, driver);
                                        if (result_03.Status == true)
                                        {
                                            grvAccountsTable.SelectedRows[i].Cells[3].Value += "Đã xóa Email forward--";
                                            if (!string.IsNullOrEmpty(Acc03.Forward_Email))
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[10].Value = "";
                                            }
                                        }
                                        driver = result_03.Driver;
                                    }

                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                                catch
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                    if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                    try { driver.Quit(); } catch { }
                                    try { driver.Dispose(); } catch { }
                                }
                            }
                            else
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
        private void Add_Forward_Email_Gmail_Gmail (object sender, EventArgs e) //DONE
        {
            var check_api = new Check_API().Start();
            if (check_api == true)
            {
                Int32 selectedRowCount = grvAccountsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    try
                    {
                        this.WindowState = FormWindowState.Minimized;
                        for (int i = (selectedRowCount - 1); i >= 0; i--)
                        {
                            UndectedChromeDriver driver = null;
                            var Acc01 = Get_Acc_Info(i);
                            if (Acc01.EmailType == "gmail.com")
                            {
                                if (!string.IsNullOrEmpty(Acc01.Forward_Email))
                                {
                                    try
                                    {
                                        if (Acc01.Profile != true)
                                        {
                                            if (string.IsNullOrEmpty(Acc01.Proxy))
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có proxy, cần nhập proxy để tạo profile--";
                                            }
                                            else
                                            {
                                                var result_01 = new Create_Profiles().Create(Acc01);
                                                if (result_01.Status == true)
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[13].Value = "YES";
                                                    grvAccountsTable.SelectedRows[i].Cells[14].Value = result_01.Value_01;
                                                    grvAccountsTable.SelectedRows[i].Cells[15].Value = Acc01.Email;
                                                }
                                                else
                                                {
                                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Mở chrome faile--";
                                                }

                                            }
                                        }

                                        //Mở Profile
                                        var Acc02 = Get_Acc_Info(i);
                                        if (driver == null && Acc02.Profile == true)
                                        {
                                            var result_02 = new Open_Profiles().Open(Acc02.ProfileID);
                                            if (result_02.Status == true)
                                            {
                                                driver = result_02.Driver;
                                            }
                                        }

                                        //
                                        var Acc03 = Get_Acc_Info(i);
                                        if (Acc03.Profile == true && driver != null)
                                        {
                                            var result_03 = new Add_New_Forward_Email_Gmail().Add(Acc03, driver);
                                            if (result_03.Status == true)
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value = "Đã thêm email forwawrd--";
                                                grvAccountsTable.SelectedRows[i].Cells[20].Value = "YES";
                                            }
                                            else
                                            {
                                                grvAccountsTable.SelectedRows[i].Cells[3].Value = "Thêm email forwawrd fail--";
                                            }
                                            driver = result_03.Driver;
                                        }

                                        if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                        try { driver.Quit(); } catch { }
                                        try { driver.Dispose(); } catch { }
                                    }
                                    catch
                                    {
                                        grvAccountsTable.SelectedRows[i].Cells[3].Value += "Lỗi web hoặc bị gián đoạn--";
                                        if (cbox_chromeOFF.Checked == true && driver != null) { driver.Close(); }
                                        try { driver.Quit(); } catch { }
                                        try { driver.Dispose(); } catch { }
                                    }
                                }
                                else
                                {
                                    grvAccountsTable.SelectedRows[i].Cells[3].Value += "Chưa có Email forward--";
                                }
                            }
                            else
                            {
                                grvAccountsTable.SelectedRows[i].Cells[3].Value += "Email này không phải gmail -> không thể đổi thông tin--";
                            }
                        }
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    catch
                    {
                        try
                        {
                            new Update_Database_To_Google_Sheet().Update_Database();
                        }
                        catch
                        {
                            MessageBox.Show("Cập nhật Google Sheet fail!", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
                else { MessageBox.Show("Cần chọn full dòng, click vào cell đầu tiên của dòng để chọn full dòng!", "Thông báo", MessageBoxButtons.OK); }
            }
        }
    }
}
