using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace csEncryptDecryptFile
{
    class FileIO
    {
        //TODO
        // file validation

        //open file


        //TODO
        public bool verifyCheckSum(byte[] checksum, User user){
            return false;
        }

        // method that gets the checksum of the input file
        public byte[] getCheckSum(String filename)
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
