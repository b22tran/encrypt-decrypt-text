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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the user presses this button, a copy of the User table is made and each entry is
        /// retrieved and placed in a datarow[]. The inputted username and password are compared
        /// to each entry in the table to check if the username is unique. If it is then a message
        /// is displayed to the user and the table is updated with the new account info.
        /// </summary>
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //new user object will store the inputted username and password
            //the values will be compared to the table records
            User user = new User();
            user.UserName = txtName.Text;
            user.Password = txtPass.Text;
            user.Email = txtEmail.Text;//can be null
            bool result = false;

            //there must be a username and password for you to register
            if (user.UserName == "" || user.Password == "")
            {
                MessageBox.Show("No empty boxes", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //dataset and table adapter for connecting, retrieving, and updating from database
                edproj ed = new edproj();
                edprojTableAdapters.UserTableAdapter uAdapter =
                    new edprojTableAdapters.UserTableAdapter();

                //get info from table adaptors
                uAdapter.Fill(ed.User);
                //dr contains all record from table (select * from user)
                DataRow[] dr = ed.User.Select();

                //loop through each record in the table and compare the usernames and passwords 
                //to the user object's attributes
                for (int i = 0; i < dr.Length; i++)
                {
                    string dbUser = (string)dr[i]["Name"];
                    string dbPass = (string)dr[i]["Password"];

                    //if true is returned, then the given username is in the table already
                    //and not available, else it is and registration proceeds
                    if (user.UserName==dbUser)
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        result = true;
                    }
                }

                //if there is a match with usernames then an error is returned and user prompted again
                //if no match ie. the username is unique then it gets added to the user table
                //user is then sent back to the login and can try to login with their new account
                if (result == false)
                {
                    MessageBox.Show("Username taken, please try a different one", "Error"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registration Successful", "Success"
                        , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    //uRow contains new user info and will be used to update the real database
                    edproj.UserRow uRow = ed.User.NewUserRow();
                    uRow.Name = user.UserName;
                    uRow.Password = user.Password;
                    uRow.Email = user.Email;

                    //add new row to the local cache
                    ed.User.Rows.Add(uRow);
                    //update the real database with the new row
                    uAdapter.Update(ed.User);
                }
            }
        }

        /// <summary>
        /// Sends user back to the login form
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.ShowDialog();
        }
    }
}
