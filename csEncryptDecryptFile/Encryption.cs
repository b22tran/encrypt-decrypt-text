using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace csEncryptDecryptFile
{
    class Encrypt
    {

        //key and iv used for encryption and decryption
        //dont really need to be created like this actually. could just randomgen it and 
        //i think it could be created in the encrypt/decrypt methods themselves 
        //but this simpler to test, so we'll go with this
        //key is 32 bytes, iv is 16
        public static string Key = "d8c83j2jskdkl4hd2jndkjh454fdfw09";
        public static string IV = "4jdjwhsicnekal43";

        public static string EncryptText(string text, string password)
        {
            string tempKey = Key.Substring(0, (32 - password.Length));
            Key = tempKey + password;
            //array for text
            byte[] normalText = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            //create AES required info for encryption
            //using AesCryptoServiceProvider class for cryptography
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider();
            // these are standards for encryption...
            //set bit sizes
            AES.BlockSize = 128;
            AES.KeySize = 256;
            //set the key to key provided above. same with iv
            //iv and key used for encryption. something abou proper security practices...
            // iv = initialization vector. kinda like a header of the encrypted file.. i think
            AES.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
            AES.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            //cipher mode used for the encryption
            //CBC = Cipher Block Chaining. there's other modes, but thisone is apparently better
            AES.Mode = CipherMode.CBC;
            //byte padding used to make sure the block size stays the same
            //kinda like added white spaces to the rest of the file..
            //also needed since we're using CBC
            //PKCS7 is a style of padding
            AES.Padding = PaddingMode.PKCS7;

            //using the icryptotransform interface to create an encryptor object for...encryption
            //empty values will create it's own key and iv using randomgen - could be worth doing if we want custom key/iv for every file
            ICryptoTransform encryptor = AES.CreateEncryptor(AES.Key, AES.IV);

            //finally actually encrypting using the cryptotransform interface transformfinalblock method
            //takes in a byte array((our file)), the start and end of the array
            byte[] encryptedFile = encryptor.TransformFinalBlock(normalText, 0, normalText.Length);
            //using dispose method to... dispose of the encryptor object
            //frees the object
            encryptor.Dispose();
            //needs a return so..
            return Convert.ToBase64String(encryptedFile);
        }
    }
}
