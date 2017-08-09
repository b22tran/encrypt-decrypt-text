using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//for aescryptoserviceprovider class
using System.Security.Cryptography;

namespace csEncryptDecryptFile
{
    public partial class Form1 : Form
    {
        //key and iv used for encryption and decryption
        //dont really need to be created like this actually. could just randomgen it and 
        //i think it could be created in the encrypt/decrypt methods themselves 
        //but this simpler to test, so we'll go with this
        //key is 32 bytes, iv is 16
        public static string Key = "d8c83j2jskdkl4hd2jndkjh454fdfw09";
        public static string IV = "4jdjwhsicnekal43";
        private int userid;

        public Form1(int user)
        {
            InitializeComponent();
            this.userid = user;
            rtbDisplayFile.Text = "Open a .txt file to either ENCRYPT or DECRYPT it's contents.\n\n"
                                + "A password may be entered as a 'salt' allowing the text to be encrypted "
                                + "with added security and the same password (that was used to encrypt) "
                                +"must be entered when decrypting that text."
                                ;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
        }

        //button click method for choosing a file
        //filters by txt files for now
        //pretty much the example we did in class before
        private async void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    //shows file name
                    //also prints out to rich text box
                    tbFileName.Text = ofd.FileName;
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        rtbDisplayFile.Text = await sr.ReadToEndAsync();
                    }
                    // record checksum
                    //byte[] checksum = FileIO.getCheckSum(ofd.FileName);

                    btnEncrypt.Enabled = true;
                    btnDecrypt.Enabled = true;
                    tbPassword.Text = "";
                }
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (tbFileName.Text != String.Empty &&
                File.Exists(tbFileName.Text))
            {
                string input = rtbDisplayFile.Text;
                //save file called filename.encrypt
 //               string output = tbFileName.Text + ".encrypt";
                //save password to db to check if it's correct so you can decrypt later??
                string password = tbPassword.Text;

                //calls the encrypttext method
                //probably wont work since this is just the filename to text
                //need to actually get the text from the file and put it in here
                rtbDisplayFile.Text = Encrypt.EncryptText(input, password);

                btnEncrypt.Enabled = false;
                btnDecrypt.Enabled = true;
                tbPassword.Text = "";

                
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (tbFileName.Text != String.Empty &&
                File.Exists(tbFileName.Text))
            {
                string input = rtbDisplayFile.Text;
                //save file called filename.encrypt
                //               string output = tbFileName.Text + ".encrypt";
                //save password to db to check if it's correct so you can decrypt later??
                string password = tbPassword.Text;

                //calls the decrypt method
                //probably wont work since this is just the filename to text
                //need to actually get the text from the file and put it in here
                rtbDisplayFile.Text = Decrypt.DecryptText(input, password);

                btnEncrypt.Enabled = true;
                btnDecrypt.Enabled = false;
                tbPassword.Text = "";

            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (rtbDisplayFile.Text != String.Empty)
            {
                using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Text Documents|*.txt", ValidateNames = true})
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(rtbDisplayFile.Text);
                            MessageBox.Show("Encrypted File Saved ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.ShowDialog();
        }
    }
}

