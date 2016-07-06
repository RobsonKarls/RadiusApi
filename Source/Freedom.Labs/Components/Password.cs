using System;
using System.Security.Cryptography;
using System.Text;

namespace Freedom.Labs.Components
{
    public static class Password
    {
        /// <summary>
        ///     Gera hash (SHA256) da string passada
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>Hash (SHA256)</returns>
        public static string CreateHashFrom(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            
            string hash = String.Empty;
            
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));

            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }

            return hash;
        }

        /// <summary>
        ///     Compare hashs to validate user
        /// </summary>
        /// <param name="password">data from user</param>
        /// <param name="storedHash">stored data for this user</param>
        /// <returns></returns>
        public static bool CompareHashFrom(string password, string storedHash)
        {
            return (CreateHashFrom(password) == storedHash);
        }
    }
}