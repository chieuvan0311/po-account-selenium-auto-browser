using PAYONEER.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAYONEER.RandomData
{
    public class Get_Answer_By_Question
    {
        PayoneerDbContext db = null;
        public Get_Answer_By_Question() 
        {
            db = new PayoneerDbContext();
        }

        public string Get_Answer(string question) 
        {
            string answer = "";
            var ques = db.Questions.Where(x => x.Se_Question == question).FirstOrDefault();
            if(ques != null) 
            {
                if(ques.Category == 1) { answer = new Answer_Question_Cate_01_Date().Get_Date(); }
                if(ques.Category == 2) { answer = new Answer_Question_Cate_02_Name().Get_Name(); }
                if(ques.Category == 3) { answer = new Answer_Question_Cate_03_PersonName().Get_Name(); }
                if(ques.Category == 4) { answer = new Answer_Question_Cate_04_PetName().Get_Name(); }
                if(ques.Category == 5) { answer = new Answer_Question_Cate_05_FirstName().Get_Name(); }
                if (ques.Category == 6) { answer = new Answer_Question_Cate_06_TimeOfDay().Get_Time_Of_Date(); }
                if (ques.Category == 7) { answer = new Answer_Question_Cate_07_Age().Get_Age_22_35(); }
            }
            else 
            {
                answer = new Answer_Question_Cate_08_ABC().Get_3_Letters();
            }
            return answer;
        }
    }
}
