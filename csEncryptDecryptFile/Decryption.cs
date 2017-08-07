using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace csEncryptDecryptFile
{
    class Decryption
    {

        //key and iv used for encryption and decryption
        //dont really need to be created like this actually. could just randomgen it and 
        //i think it could be created in the encrypt/decrypt methods themselves 
        //but this simpler to test, so we'll go with this
        //key is 32 bytes, iv is 16
        public static string Key = "d8c83j2jskdkl4hd2jndkjh454fdfw09";
        public static string IV = "4jdjwhsicnekal43";

        public static string DecryptText(string encryptedFile)
        {
            //converting text file to bytes using frombase64string (base64 is regular text...)
            try
            {
                byte[] byteEncrypted = Convert.FromBase64String(encryptedFile);


                //create AES required info for decryption
                AesCryptoServiceProvider AES = new AesCryptoServiceProvider();

                // these are standards for decryption...
                //set bit sizes
                AES.BlockSize = 128;
                AES.KeySize = 256;
                //set the key to key provided above. same with iv
                //iv and key used for decryption. something abou proper security practices...
                // iv = initialization vector. kinda like a header of the decrypted file.. i think
                AES.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
                AES.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                //cipher mode used for the decryption
                //CBC = Cipher Block Chaining. there's other modes, but thisone is apparently better
                AES.Mode = CipherMode.CBC;
                //byte padding used to make sure the block size stays the same
                //kinda like added white spaces to the rest of the file..
                //also needed since we're using CBC
                //PKCS7 is a style of padding
                AES.Padding = PaddingMode.PKCS7;

                //using the icryptotransform interface to create an decryptor object for...decryption
                //empty values will create it's own key and iv using randomgen - could be worth doing if we want custom key/iv for every file
                ICryptoTransform decryptor = AES.CreateDecryptor(AES.Key, AES.IV);

                //finally actually decryption using the cryptotransform interface transformfinalblock method
                //takes in a byte array((our file)), the start and end of the array
                byte[] decryptedFile = decryptor.TransformFinalBlock(byteEncrypted, 0, byteEncrypted.Length);
                //using dispose method to... dispose of the decryptor object
                //frees the object
                decryptor.Dispose();
                //returns the decrypted file to string
                return System.Text.ASCIIEncoding.ASCII.GetString(decryptedFile);
            }
            catch (Exception e)
            {
                return "Could not decrypt. File was corruptedor already decrypted.";
            }
        }
    }
}
