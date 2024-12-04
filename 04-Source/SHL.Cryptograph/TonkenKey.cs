using SHL.Criptografia;
using System;
using System.Globalization;

namespace SHL.TokenSecurity
{
    public static class TokenKey
    {
        public static String GetCryptMessage(String Credencial, String URL)
        {
            CultureInfo MyCultureInfo = new CultureInfo("en-US");
            String FullMessage = String.Empty;
            String ParcialCryptedMessage = String.Empty;
            String FullCrypedMesssage = String.Empty;            
            String hash = String.Empty;
            String message = String.Empty;
            
            using (var rijndael = System.Security.Cryptography.Rijndael.Create())
            {
                rijndael.GenerateKey();
                hash = Convert.ToBase64String(rijndael.Key);
            }

            message = String.Format("{0}|{1}", Credencial, URL);

            ParcialCryptedMessage = Cryptography.Encryption(message, hash);

            FullMessage = String.Format("{0}|{1}|{2}", ParcialCryptedMessage, DateTime.Now.ToString(MyCultureInfo), hash);

            FullCrypedMesssage = Cryptography.Encryption(FullMessage);

            return FullCrypedMesssage;
        }

        public static String GetDecryptMessage(String message)
        {
            CultureInfo MyCultureInfo = new CultureInfo("en-US");
            String messageret = String.Empty;
            String parcialdecrptedmessage = String.Empty;
            String[] splittedmessage = null;
            DateTime timemessage;            
            TimeSpan interval;

            parcialdecrptedmessage = Cryptography.Decryption(message);
            splittedmessage = parcialdecrptedmessage.Split('|');

            timemessage = DateTime.Parse(splittedmessage[1], MyCultureInfo);
            interval = DateTime.Now - timemessage;

            #if DEBUG
            if (interval.Minutes > 100)
                return "";
            #else
            if (interval.Seconds > 10)
                return "";
            #endif

            messageret = Cryptography.Decryption(splittedmessage[0], splittedmessage[2]);

            return messageret;
        }
    }
}
