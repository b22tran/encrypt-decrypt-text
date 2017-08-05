using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csEncryptDecryptFile
{
    class User
    {
        /// <summary>
        /// The ID of the user which will be their unique identifier
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// Username that is used to authenticate the user, must be unique
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// for logging in
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// user email, can be empty if user doesn't want to give it
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Takes in a username and password string and compares it to the object's own
        /// username and password fields to see if they match, used for login authentication
        /// </summary>
        /// <param name="u">the username to compare with</param>
        /// <param name="p">the password to compare with</param>
        /// <returns>true if there is a match, false otherwise</returns>
        public bool validate(string u, string p)
        {
            if (u.Equals(this.UserName) && p.Equals(this.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
