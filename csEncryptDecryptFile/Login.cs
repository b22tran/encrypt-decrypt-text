using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csEncryptDecryptFile
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// authenticates the user and allows access to the rest of the application
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.UserName = txtUsername.Text;
            user.Password = txtPassword.Text;
            bool result = false;
            
            if (user.UserName == "" || user.Password == "")
            {
                MessageBox.Show("No empty boxes","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
            //create dataset and table adapter objects
            edproj1 ed = new edproj1();
            edproj1TableAdapters.UserTableAdapter uAdapter =
                new edproj1TableAdapters.UserTableAdapter();

            //get info from table adaptors
            uAdapter.Fill(ed.User);

            DataRow[] dr = ed.User.Select();
            int userid = 0;
            //compare given username and password to every entry in the table
            for (int i = 0; i < dr.Length; i++)
            {
                string dbUser = (string)dr[i]["Name"];
               string dbPass = (string)dr[i]["Password"];

                //compare the given username and password with the records in the table
                if (user.validate(dbUser, dbPass))
                {
                    result = true;
                    userid = i;
                    break;
                }
                else
                {
                    result = false;
                }
            }

            //if there is a match the validate method will return true and login is successful
            //otherwise there is no match and the user will be asked to enter again
            if (result)
            {
                MessageBox.Show("Login SuccessFul","Success!"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                //transfer to next web form
                Form1 f = new Form1(userid);
                this.Hide();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Login failed, check your username and password and try again", "Access Denied"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Opens the registration form
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            this.Hide();
            r.ShowDialog();
        }


    }
}
