using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LOGI.Framework.Toolkit.Core.Reflection;

namespace LOGI.Framework.Toolkit.Core.Security
{
    public class SecurityOperations 
    {
        // Gets the single instance of SingleInstanceClass.
        public static SecurityOperations Instance
        {
            get { return SingletonBase<SecurityOperations>.Instance; }
        }

        public string GenerateUniqueKey(int minSize,int maxSize)
        {

            if (minSize>maxSize || minSize==maxSize)
            {
                throw new ArgumentException("MinSize, MaxSize dan küçük olmalı !");
            }

            char[] chars = new char[62];
            string a;
            a ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b%(chars.Length - 1)]);
            }
            return result.ToString();
        }
    }
}
