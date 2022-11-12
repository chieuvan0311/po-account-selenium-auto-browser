using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using PAYONEER.Dao;
using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PAYONEER.Goolge_Sheets
{
    public class Save_Deleted_Accounts
    {
        string driver_link_id = null;
        string save_accounts_sheet = null;
        PayoneerDbContext db = null;
        public Save_Deleted_Accounts()
        {
            db = new PayoneerDbContext();
        }

        public void Save_Info (List<Account> list_data_accounts)
        {
            driver_link_id = db.Admins.Where(x => x.Name == "Google_Sheet_Link").FirstOrDefault().Password;
            save_accounts_sheet = db.Admins.Where(x => x.Name == "Del_Accounts_Sheet_Name").FirstOrDefault().Password;
            string google_sheet_name_range = save_accounts_sheet + "!A2:AB";
            var service = Authorize_GoogleApp_For_SheetsService();

            int google_sheet_row_total = Get_Total_GoogleSheet_Row(driver_link_id, google_sheet_name_range);
            string row_index_start = (google_sheet_row_total + 2).ToString();
            string row_index_end = (google_sheet_row_total + 2 + list_data_accounts.Count).ToString();


            string sheet_name_past_range = save_accounts_sheet + "!A" + row_index_start + ":AB" + row_index_end;


            IList<IList<Object>> objNeRecords = Get_GoogleSheet_Data_Model(list_data_accounts, google_sheet_row_total + 1);
            Update_GoogleSheet_InBatch(objNeRecords, driver_link_id, sheet_name_past_range, service);
        }
        public void Update_Databe_Del_Accounts()
        {
            driver_link_id = db.Admins.Where(x => x.Name == "Google_Sheet_Link").FirstOrDefault().Password;
            save_accounts_sheet = db.Admins.Where(x => x.Name == "Del_Accounts_Sheet_Name").FirstOrDefault().Password;
            var service = Authorize_GoogleApp_For_SheetsService();

            string google_sheet_name_column_range = save_accounts_sheet + "!A2:AB";
            int google_sheet_row_total = Get_Total_GoogleSheet_Row(driver_link_id, google_sheet_name_column_range);

            var accounts_all_list = db.Del_Account_Save.ToList();
            int database_list_row_count = accounts_all_list.Count;

            if (google_sheet_row_total > database_list_row_count) //Clear phần data dư thừa
            {
                string row_index = (database_list_row_count + 2).ToString();

                string sheetName_ClearRange = save_accounts_sheet + "!A" + row_index + ":AB";
                int total_clear_row = google_sheet_row_total - database_list_row_count;
                IList<IList<Object>> empty_model = Get_GoogleSheet_Empty_Model(total_clear_row);

                Clear_Sheet_Range_InBatch(empty_model, driver_link_id, sheetName_ClearRange, service);
            }

            string sheetName_UpdateRange = save_accounts_sheet + "!A2:AB";
            IList<IList<Object>> objNeRecords = Get_Del_Accounts_Data_Model();
            Update_GoogleSheet_InBatch(objNeRecords, driver_link_id, sheetName_UpdateRange, service);
        }

        private static SheetsService Authorize_GoogleApp_For_SheetsService()
        {
            // If modifying these scopes, delete your previously saved credentials  
            // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json  
            string[] Scopes = { SheetsService.Scope.Spreadsheets };
            string ApplicationName = "Google Sheets API .NET Quickstart";
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("MyAppsToken")).Result;

            }
            // Create Google Sheets API service.  
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        private static int Get_Total_GoogleSheet_Row(string spreadsheetId, string sheet_name_range)
        {
            int totalRow = 0;
            SheetsService service = Authorize_GoogleApp_For_SheetsService();
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, sheet_name_range);
            ValueRange response = request.Execute();
            IList<IList<Object>> getValues = response.Values;
            if (getValues != null)
            {
                totalRow = getValues.Count;
            }
            return totalRow;
        }
        private IList<IList<Object>> Get_GoogleSheet_Data_Model(List<Account> accounts_all_list, int stt)
        {
            List<IList<Object>> google_sheet_row_data_list = new List<IList<Object>>();

            for (int i = 0; i < accounts_all_list.Count; i++)
            {
                IList<Object> google_sheet_row_data = new List<Object>();
                // 1-4
                google_sheet_row_data.Add(i + stt);   
                google_sheet_row_data.Add(accounts_all_list[i].ID);  
                google_sheet_row_data.Add(accounts_all_list[i].Email);
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccPassword)) { google_sheet_row_data.Add(accounts_all_list[i].AccPassword); } else { google_sheet_row_data.Add(""); }

                // 5-7
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccQuestion1)) { google_sheet_row_data.Add(accounts_all_list[i].AccQuestion1); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccQuestion2)) { google_sheet_row_data.Add(accounts_all_list[i].AccQuestion2); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccQuestion3)) { google_sheet_row_data.Add(accounts_all_list[i].AccQuestion3); } else { google_sheet_row_data.Add(""); }

                // 8-12
                if (!string.IsNullOrEmpty(accounts_all_list[i].EmailPassword)) { google_sheet_row_data.Add(accounts_all_list[i].EmailPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].RecoveryEmail)) { google_sheet_row_data.Add(accounts_all_list[i].RecoveryEmail); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Forward_Email)) { google_sheet_row_data.Add(accounts_all_list[i].Forward_Email); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Email_2FA)) { google_sheet_row_data.Add(accounts_all_list[i].Email_2FA); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Proxy)) { google_sheet_row_data.Add(accounts_all_list[i].Proxy); } else { google_sheet_row_data.Add(""); }

                // 13-21
                if (accounts_all_list[i].Change_Acc_Info_All == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Remove_OldForwardEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Add_NewForwardEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Remove_OldRecoveryEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Add_NewRecoveryEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Change_EmailPassword == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Change_AccPassword == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Add_AccQuestion == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Up_Link_Status == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }

                // 22-25
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_AccPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Old_AccPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_EmailPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Old_EmailPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_RecoveryEmail)) { google_sheet_row_data.Add(accounts_all_list[i].Old_RecoveryEmail); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_AccQuestion)) { google_sheet_row_data.Add(accounts_all_list[i].Old_AccQuestion); } else { google_sheet_row_data.Add(""); }

                // 26-28
                if (!string.IsNullOrEmpty(accounts_all_list[i].Random_AccPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Random_AccPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Random_EmailPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Random_EmailPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Random_Question)) { google_sheet_row_data.Add(accounts_all_list[i].Random_Question); } else { google_sheet_row_data.Add(""); }

                google_sheet_row_data_list.Add(google_sheet_row_data);
            }
            return google_sheet_row_data_list;
        }
        private IList<IList<Object>> Get_Del_Accounts_Data_Model()
        {
            List<IList<Object>> google_sheet_row_data_list = new List<IList<Object>>();
            var accounts_all_list = db.Del_Account_Save.ToList();

            for (int i = 0; i < accounts_all_list.Count; i++)
            {
                IList<Object> google_sheet_row_data = new List<Object>();
                // 1-4
                google_sheet_row_data.Add(i + 1);
                google_sheet_row_data.Add(accounts_all_list[i].ID);
                google_sheet_row_data.Add(accounts_all_list[i].Email);
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccPassword)) { google_sheet_row_data.Add(accounts_all_list[i].AccPassword); } else { google_sheet_row_data.Add(""); }

                // 5-7
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccQuestion1)) { google_sheet_row_data.Add(accounts_all_list[i].AccQuestion1); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccQuestion2)) { google_sheet_row_data.Add(accounts_all_list[i].AccQuestion2); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].AccQuestion3)) { google_sheet_row_data.Add(accounts_all_list[i].AccQuestion3); } else { google_sheet_row_data.Add(""); }

                // 8-12
                if (!string.IsNullOrEmpty(accounts_all_list[i].EmailPassword)) { google_sheet_row_data.Add(accounts_all_list[i].EmailPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].RecoveryEmail)) { google_sheet_row_data.Add(accounts_all_list[i].RecoveryEmail); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Forward_Email)) { google_sheet_row_data.Add(accounts_all_list[i].Forward_Email); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Email_2FA)) { google_sheet_row_data.Add(accounts_all_list[i].Email_2FA); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Proxy)) { google_sheet_row_data.Add(accounts_all_list[i].Proxy); } else { google_sheet_row_data.Add(""); }

                // 13-21
                if (accounts_all_list[i].Change_Acc_Info_All == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Remove_OldForwardEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Add_NewForwardEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Remove_OldRecoveryEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Add_NewRecoveryEmail == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Change_EmailPassword == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Change_AccPassword == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Add_AccQuestion == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }
                if (accounts_all_list[i].Up_Link_Status == true) { google_sheet_row_data.Add("YES"); } else { google_sheet_row_data.Add("NO"); }

                // 22-25
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_AccPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Old_AccPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_EmailPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Old_EmailPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_RecoveryEmail)) { google_sheet_row_data.Add(accounts_all_list[i].Old_RecoveryEmail); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Old_AccQuestion)) { google_sheet_row_data.Add(accounts_all_list[i].Old_AccQuestion); } else { google_sheet_row_data.Add(""); }

                // 26-28
                if (!string.IsNullOrEmpty(accounts_all_list[i].Random_AccPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Random_AccPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Random_EmailPassword)) { google_sheet_row_data.Add(accounts_all_list[i].Random_EmailPassword); } else { google_sheet_row_data.Add(""); }
                if (!string.IsNullOrEmpty(accounts_all_list[i].Random_Question)) { google_sheet_row_data.Add(accounts_all_list[i].Random_Question); } else { google_sheet_row_data.Add(""); }

                google_sheet_row_data_list.Add(google_sheet_row_data);
            }
            return google_sheet_row_data_list;
        }
        private IList<IList<Object>> Get_GoogleSheet_Empty_Model(int row_total)
        {
            List<IList<Object>> empty_row_list = new List<IList<Object>>();
            string empty_cell = "";
            for (int i = 1; i <= row_total; i++)
            {
                IList<Object> empty_row = new List<Object>();
                for (int col = 1; col <= 28; col++)
                {
                    empty_row.Add(empty_cell);
                }
                empty_row_list.Add(empty_row);
            }
            return empty_row_list;
        }

        private static void Update_GoogleSheet_InBatch(IList<IList<Object>> values, string spreadsheetId, string sheet_name_range, SheetsService service)
        {
            SpreadsheetsResource.ValuesResource.UpdateRequest request =
                service.Spreadsheets.Values.Update(new ValueRange() { Values = values }, spreadsheetId, sheet_name_range);

            //request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOption.INSERTROWS;
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            var response = request.Execute();
        }
        private static void Clear_Sheet_Range_InBatch(IList<IList<Object>> values, string spreaddriver_link_id, string sheet_name_range, SheetsService service)
        {
            SpreadsheetsResource.ValuesResource.UpdateRequest request =
                service.Spreadsheets.Values.Update(new ValueRange() { Values = values }, spreaddriver_link_id, sheet_name_range);

            //request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOption.INSERTROWS;
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            var response = request.Execute();
        }
    }
}
