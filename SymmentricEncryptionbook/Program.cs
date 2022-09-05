using System;
using static System.Console;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SymmentricEncryptionbook
{
    internal class Program
    {
        
        public static string NameofContainer { get; private set; }
        
        const string KEY_STORE_NAME = "MyKeyStore";
        public static CspParameters csp = new CspParameters();
        public static RSACryptoServiceProvider rsa;

        static void Main(string[] args)
        {
            bool blnUseMachineKeyStore = false;
            CreateAsmKeys(out string containerName, blnUseMachineKeyStore);
            string superSecretText = "Super Secret Message";
            WriteLine($"Message to encrypt: {superSecretText}");
            var (encrBytes, encrString) = AsymmEncrypt(superSecretText,
            containerName);
            WriteLine($"Encrypted bytes: {Convert.ToBase64String(encrBytes)}");
            WriteLine($"Encrypted string: {encrString}");
            var (decrBytes, decrString) = DecryptWithCsp(encrBytes, containerName);
            WriteLine($"Decrypted bytes: {Convert.ToBase64String(decrBytes)}");
            WriteLine($"Decrypted message: {decrString}");

            

        }

        
        private static void CreateAsmKeys(out string containerName, bool useMachineKeyStore)
        {
            csp.KeyContainerName = KEY_STORE_NAME;
            if (useMachineKeyStore)
                csp.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(csp);
            rsa.PersistKeyInCsp = true;
            CspKeyContainerInfo info = new CspKeyContainerInfo(csp);
            WriteLine($"The key container name: {info.KeyContainerName}");
            containerName = info.KeyContainerName;
            WriteLine($"Windows key container name:{ info.UniqueKeyContainerName}");
        }

        public static (byte[] encrBytes, string encrString) AsymmEncrypt(string message, string keyContainerName)
        {
            CspParameters cspParams = new CspParameters()
            {
                KeyContainerName = keyContainerName
            };
            RSACryptoServiceProvider rsa = new
           RSACryptoServiceProvider(cspParams);
            byte[] encryptedAsBytes =
           rsa.Encrypt(Encoding.UTF8.GetBytes(message), true);
            string encryptedAsBase64 = Convert.ToBase64String(encryptedAsBytes);
            return (encryptedAsBytes, encryptedAsBase64);
        }


        public static (byte[] decrBytes, string decrString) DecryptWithCsp(byte[]encrBytes, string containerName)
        {
            CspParameters cspParams = new CspParameters()
            {
                KeyContainerName = containerName
            };
            RSACryptoServiceProvider rsa = new
           RSACryptoServiceProvider(cspParams);
            byte[] decryptBytes = rsa.Decrypt(encrBytes, true);
            string secretMessage = Encoding.Default.GetString(decryptBytes);
            return (decryptBytes, secretMessage);
        }



        

        


       


    }
}
