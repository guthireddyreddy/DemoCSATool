using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1
{
    public class program
    {
        public static string NameofContainer { get; private set; }

        const string KEY_STORE_NAME = "MyKeyStore";
        public static CspParameters csp = new CspParameters();
        public static RSACryptoServiceProvider rsa;

        bool blnUseMachineKeyStore = false;
       


        private static void CreateAsmKeys(out string containerName, bool useMachineKeyStore)
        {
            csp.KeyContainerName = KEY_STORE_NAME;
            if (useMachineKeyStore)
                csp.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(csp);
            rsa.PersistKeyInCsp = true;
            CspKeyContainerInfo info = new CspKeyContainerInfo(csp);
            //The key container name: {info.KeyContainerName} is stored.
            containerName = info.KeyContainerName;
            //Windows key container name:{info.UniqueKeyContainerName}.

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


        public static (byte[] decrBytes, string decrString) DecryptWithCsp(byte[] encrBytes, string containerName)
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