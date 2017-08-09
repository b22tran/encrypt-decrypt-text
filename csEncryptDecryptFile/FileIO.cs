using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace csEncryptDecryptFile
{
    class FileIO
    {
        // checks hte checksum of the current file to database file
        public bool verifyCheckSum(byte[] checksum, int userid){
            //create dataset and table adapter objects
            edproj1 ed = new edproj1();
            edproj1TableAdapters.EncryptedTableAdapter uAdapter =
                new edproj1TableAdapters.EncryptedTableAdapter();

            // get info from table adaptors
            uAdapter.Fill(ed.Encrypted);
            // datarow for encrypted
            DataRow[] dr = ed.Encrypted.Select();

            bool result = false;
            if (checksum== (byte[])dr[userid]["FileHash"])
            {
                result = true;
            }
            return result;
        }



        // method that gets the checksum of the input file
        public static byte[] getCheckSum(String filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}
