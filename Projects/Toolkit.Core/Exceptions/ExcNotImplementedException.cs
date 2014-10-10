using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Exceptions
{
    ///<summary>
    /// NotImplementedException
    ///</summary>
    public class ExcNotImplementedException : Base.BaseException
    {
        ///<summary>
        /// Default Constructor
        ///</summary>
        public ExcNotImplementedException():base()
        {
            
        }

        ///<summary>
        /// String parametreli Constructor 
        ///</summary>
        public ExcNotImplementedException(String message)
            : base(message)
        {

        }

        ///<summary>
        /// String ve Exception parametreli Constructor 
        ///</summary>
        public ExcNotImplementedException(String message,Exception inner)
            : base(message, inner)
        {

        }
    }
}
