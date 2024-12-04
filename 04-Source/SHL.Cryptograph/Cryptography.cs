using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace SHL.Criptografia
{
    public static class Cryptography
    {
        // Representa a transformação para base64 de um conjunto de 32 caracteres (8 * 32 = 256bits)
        // Os 32 caracteres utilizados foram: ocif@itavlas@innavoig@$redocnE$$
        private const string m_Chave = "b2NpZkBpdGF2bGFzQGlubmF2b2lnQCRyZWRvY25FJCQ=";

        // Vetor de bytes utilizados para a criptografia (Chave Externa)
        private static byte[] bIV =
            {0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC};

        /// <summary>
        /// Criptografa uma String
        /// </summary>
        /// <param name="Texto">Texto que deverá ser criptografado</param>
        /// <returns>Texto criptografado</returns>
        public static string Encryption(string Texto)
        {
            return Encryption(Texto, m_Chave);
        }

        /// <summary>
        /// Criptografa uma String
        /// </summary>
        /// <param name="Texto">Texto que deverá ser criptografado</param>
        /// <returns>Texto criptografado</returns>
        public static string Encryption(string Texto, String Chave)
        {
            if (string.IsNullOrEmpty(Texto))
                return null;

            // Cria instancias de vetores de bytes com as chaves
            byte[] bChave = Convert.FromBase64String(Chave);
            byte[] bTexto = new UTF8Encoding().GetBytes(Texto);

            // Instancia a classe de criptografia Rijndael
            Rijndael rijndael = new RijndaelManaged();

            // Define o tamanho da chave utilizada "256 = 8 * 32"
            // Chaves possíves: 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)
            rijndael.KeySize = 256;

            // Cria o espaço de memória para guardar o valor criptografado:
            MemoryStream mStream = new MemoryStream();

            // Instancia o encriptador 
            CryptoStream cripto =
                new CryptoStream(mStream, rijndael.CreateEncryptor(bChave, bIV), CryptoStreamMode.Write);

            // Faz a escrita dos dados criptografados no espaço de memória
            cripto.Write(bTexto, 0, bTexto.Length);
            // Despeja toda a memória.
            cripto.FlushFinalBlock();
            // Pega o vetor de bytes da memória e gera a string criptografada
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// Descriptografa uma string criptografada por essa mesma classe
        /// </summary>
        /// <param name="Texto">Texto para ser descriptografado</param>
        /// <returns>Texto original descriptografado</returns>
        public static string Decryption(string Texto)
        {
            return Decryption(Texto, m_Chave);
        }

        /// <summary>
        /// Descriptografa uma string criptografada por essa mesma classe
        /// </summary>
        /// <param name="Texto">Texto para ser descriptografado</param>
        /// <param name="Chave">Texto para ser descriptografado</param>
        /// <returns>Texto original descriptografado</returns>
        public static string Decryption(string Texto, String Chave)
        {
            if (string.IsNullOrEmpty(Texto))
                return null;

            try
            {
                // Cria instancias de vetores de bytes com as chaves
                byte[] bChave = Convert.FromBase64String(Chave);
                byte[] bTexto = Convert.FromBase64String(Texto);

                // Instancia a classe de criptografia Rijndael
                Rijndael rijndael = new RijndaelManaged();

                // Define o tamanho da chave utilizada "256 = 8 * 32"
                // Chaves possíves: 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)
                rijndael.KeySize = 256;

                // Cria o espaço de memória para guardar o valor DEScriptografado:
                MemoryStream mStream = new MemoryStream();

                // Instancia o Decriptador 
                CryptoStream decripto = new CryptoStream(mStream, rijndael.CreateDecryptor(bChave, bIV),
                    CryptoStreamMode.Write);

                // Faz a escrita dos dados criptografados no espaço de memória
                decripto.Write(bTexto, 0, bTexto.Length);
                // Despeja toda a memória.
                decripto.FlushFinalBlock();

                // Instancia a classe de codificação para que a string venha de forma correta
                UTF8Encoding utf8 = new UTF8Encoding();
                // Com o vetor de bytes da memória, gera a string descritografada em UTF8
                return utf8.GetString(mStream.ToArray());
            }
            catch (Exception)
            {
                return Texto;
            }
        }

        public static bool IsBase64(string Texto)
        {
            //try
            //{
            //    byte[] converted = Convert.FromBase64String(Texto);
            //    return Texto.EndsWith("=");
            //}
            //catch
            //{
            //    return false;
            //}

            string exp = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";
            return Regex.IsMatch(Texto, exp);



        }
    }
}