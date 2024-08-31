using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Crypter
{



    static class Program
    {
        static byte[] AES_Encrypt(byte[] bytesToBeEncrypted,byte[] key, byte[] iv)
        {
            byte[] encryptedBytes = null;
          
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {

                    AES.Key = key;
                    AES.IV = iv;
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }


        static void Main()
        { /*
            cshrp formatted shellcode here
            */
            // GENERATE RANDOM KEY AND IV

            Random rnd = new Random();
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            rnd.NextBytes(key);
            rnd.NextBytes(iv);

            Console.WriteLine("Original Shellcode\n");
            Console.WriteLine(String.Join("", shellcode.Select(by => by.ToString("X2"))));
            byte[] bytesEncrypted = AES_Encrypt(shellcode, key, iv);
            Console.WriteLine("\nAES Encrypted Shellcode \n");
            Console.WriteLine(String.Join("", bytesEncrypted.Select(by => by.ToString("X2"))));
            Console.WriteLine("\nKey\n");
            Console.WriteLine(String.Join("", key.Select(by => by.ToString("X2"))));
            Console.WriteLine("\nIV\n");
            Console.WriteLine(String.Join("", iv.Select(by => by.ToString("X2"))));



        }
    }
}