using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Foundation.Security
{
    /// <summary>
    /// Assists with application operations for symmetric (shared key) encryption
    /// </summary>
    public interface ISymmetricEncryptionService
    {
        byte[] Encrypt(string plaintext);
        string EncryptToBase64(string plaintext);
        string Decrypt(byte[] cypher);
        string DecryptFromBase64(string cypher);

        void SetKey(string usersalt);
    }
}
