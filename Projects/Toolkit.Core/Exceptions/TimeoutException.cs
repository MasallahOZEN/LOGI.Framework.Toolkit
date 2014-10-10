using LOGI.Framework.Toolkit.Core.Exceptions.Base;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace LOGI.Framework.Toolkit.Core.Exceptions
{
    [Serializable]
    public class TimeoutException : Base.BaseException
    {
        public TimeoutException(string prefix="", string suffix="")
            : base(string.Format("{0} TimeOut ! {1}", prefix,suffix))
        {
        }

        
    }
}

