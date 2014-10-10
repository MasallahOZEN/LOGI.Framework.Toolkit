using System;

namespace LOGI.Framework.Toolkit.Core.Exceptions.Base
{
    ///<summary>
    /// Uçuş servisi item değer null olamaz
    ///</summary>
    public abstract class BaseArgumentException : BaseException
    {
        protected BaseArgumentException()
            : base("")
        {
            
        }

        protected BaseArgumentException(string msg)
            : base(string.Format("Parametre Hatası... -> {0}",msg))//TODO:Resource'a al
        {

        }
    }
}
