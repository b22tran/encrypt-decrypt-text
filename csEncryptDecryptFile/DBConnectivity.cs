using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csEncryptDecryptFile
{
    class DBConnectivity
    {
        private SqlConnection conn = new SqlConnection();
        private string conString = "Server=(local); Database=edproj; User=test; Password=test";
        SqlCommand cmd;


        public DataTable queryDB(String query)
        {
            DataTable dt = new DataTable();

            //the following line provides information to connect to the database
            conn.ConnectionString = conString;

            //we need a command object to execute a query
            cmd = conn.CreateCommand();

            try
            {
                cmd.CommandText = query;
                conn.Open();


                SqlDataReader reader = cmd.ExecuteReader();
                //There is data in the reader object.
                //We need to convert the data to an object the gridview can read!


                reader.Close();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return dt;
        }

        private bool executeDB(String query)
        {
            conn.ConnectionString = conString;
            cmd = conn.CreateCommand();
            object check;
            bool success = false;
            try
            {
                cmd.CommandText = query;
                conn.Open();
                check = cmd.ExecuteScalar();
                if (check.Equals(null))
                {
                    success =  false;
                }
                else
                {
                    success= true;
                }
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return success;
        }

        private void handleException(Exception ex)
        {

            string msg = ex.Message.ToString();
            string caption = "Error";
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}