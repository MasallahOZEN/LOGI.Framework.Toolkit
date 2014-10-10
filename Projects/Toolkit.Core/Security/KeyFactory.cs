using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Security.Cryptography;
namespace LOGI.Framework.Toolkit.Core.Security
{
    public class KeyFactory
    {
        Boolean _includeLetters = false;
        Boolean _includeNumbers = false;
        Boolean _includePunctuation = false;
        int _keySize = 0;

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string GetUniqueKey()
        {
            if (_keySize > 0 && (_includeLetters == true || _includeNumbers == true || _includePunctuation == true))
            {
                char[] chars = new char[62];
                string a = "";
                if (_includeLetters)
                    a += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (_includeNumbers)
                    a += "1234567890";

                if (_includePunctuation)
                    a += "!.()&+=-_^+%$#@";

                chars = a.ToCharArray();
                int size = _keySize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = _keySize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                { result.Append(chars[b % (chars.Length - 1)]); }
                return result.ToString();
            }
            else
            {
                return "";
            }
        }
        
        ///<summary>
        ///</summary>
        ///<param name="keySize"></param>
        ///<param name="includeLetters"></param>
        ///<param name="includeNumbers"></param>
        ///<param name="includePunctuation"></param>
        ///<returns></returns>
        public string GetUniqueKey(int keySize, Boolean includeLetters, Boolean includeNumbers, Boolean includePunctuation)
        {
            this._keySize = keySize;
            this._includeLetters = includeLetters;
            this._includeNumbers = includeNumbers;
            this._includePunctuation = includePunctuation;

            return GetUniqueKey();
        }

        ///<summary>
        ///</summary>
        public Boolean IncludeLetters
        {
            get { return _includeLetters; }
            set { _includeLetters = value; }
        }

        ///<summary>
        ///</summary>
        public Boolean IncludeNumbers
        {
            get { return _includeNumbers; }
            set { _includeNumbers = value; }
        }

        ///<summary>
        ///</summary>
        public Boolean IncludePunctuation
        {
            get { return _includePunctuation; }
            set { _includePunctuation = value; }
        }
        
        ///<summary>
        ///</summary>
        public int KeySize
        {
            get { return _keySize; }
            set { _keySize = value; }
        }
    }
}
