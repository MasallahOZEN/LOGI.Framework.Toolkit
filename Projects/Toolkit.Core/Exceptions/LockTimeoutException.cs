using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Exceptions
{
    public class LockTimeoutException : Base.BaseException
    {
        ///<summary>
        /// Default Constructor
        ///</summary>
        public LockTimeoutException():base("Timeout waiting for lock")
        {
            
        }

        ///<summary>
        /// String parametreli Constructor 
        ///</summary>
        public LockTimeoutException(String message)
            : base(message)
        {

        }

        ///<summary>
        /// String ve Exception parametreli Constructor 
        ///</summary>
        public LockTimeoutException(String message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
