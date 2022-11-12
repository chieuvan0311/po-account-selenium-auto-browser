using PAYONEER.DataConnection;
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
    public partial class fLogin : Form
    {
        PayoneerDbContext db = null;
        

        public fLogin()
        {
            InitializeComponent();
            db = new PayoneerDbContext();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(tbPassword.Text)) 
            {
                var check_account = db.Admins.Where(x => x.Name == tbName.Text && x.Password == tbPassword.Text);
                if (check_account != null)
                {
                    fManager form = new fManager();
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên hoặc mật khẩu không đúng!");
                }
            }
            else
            {
                MessageBox.Show("Cần nhập đủ tên và mật khẩu!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}