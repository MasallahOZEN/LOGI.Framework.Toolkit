using System;
using System.Security.Cryptography;
using System.Text;
using YKM.Toolkit.Core.Security;

namespace YKM.Toolkit.Core.Extensions.ExtString
{
    ///<summary>
    /// String Extensions
    ///</summary>
    public static class SecurityExtentions
    {
        /// <summary>
        /// Computes the MD5 hash for this string and returns it as an ASCII string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ComputeMD5Hash(this string value)
        {
            return ComputeMD5Hash(value, Encoding.ASCII);
        }

        /// <summary>
        /// Computes the MD5 hash for this string and returns it encoded with the specified text encoding.
        /// </summary>
        public static string ComputeMD5Hash(this string value, Encoding encoding)
        {
            if (value != null)
            {
                StringBuilder sb = new StringBuilder(32);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = encoding.GetBytes(value);
                data = md5.ComputeHash(data);
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }

                return sb.ToString();
            }
            return null;
        }

        /// <summary>
        /// Computes the SHA256 hash for this string with the specified salt. Useful for comparing passwords with an accompanying salt.
        /// </summary>
        public static string ComputeSHA256Hash(this string value, int salt)
        {
            if (value != null)
            {
                StringBuilder sb = new StringBuilder(64);
                SHA256CryptoServiceProvider sha2 = new SHA256CryptoServiceProvider();
                byte[] data = Encoding.Default.GetBytes(value);
                byte[] saltBytes = BitConverter.GetBytes(salt);
                byte[] both = new byte[data.Length + saltBytes.Length];

                Buffer.BlockCopy(data, 0, both, 0, data.Length);
                Buffer.BlockCopy(saltBytes, 0, both, data.Length, saltBytes.Length);

                data = sha2.ComputeHash(both);
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }

                return sb.ToString();
            }
            return null;
        }

        /// <summary>
        /// Computes the SHA256 hash for this string with a random salt. The salt used is returned in the result.
        /// </summary>
        public static HashResult ComputeSHA256Hash(this string value)
        {
            int randomSalt = BitConverter.ToInt32(CreateRandomSalt(sizeof(int)), 0);
            return new HashResult()
            {
                Hash = ComputeSHA256Hash(value, randomSalt),
                SaltUsed = randomSalt
            };
        }

        /// <summary>
        /// Creates a random key salt of the specified length.
        /// </summary>
        public static byte[] CreateRandomSalt(int length)
        {
            // Create a buffer
            byte[] randBytes = new byte[(length >= 1 ? length : 1)];
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            return randBytes;
        }
    }
}
