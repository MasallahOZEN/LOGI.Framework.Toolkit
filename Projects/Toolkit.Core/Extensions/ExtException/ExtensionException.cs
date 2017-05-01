using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using LOGI.Framework.Toolkit.Core.Exceptions;
using LOGI.Framework.Toolkit.Core.Properties;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtException
{
    ///<summary>
    /// Object Extensions
    ///</summary>
    public static class Extentions
    {
        public static string GetInnerException(this Exception ex)
        {
            if (ex==null)
            {
                return string.Empty;
            }

            var excMessage = string.Format("Message:{0} \r\n StackTrace:{1} ", ex.Message,ex.StackTrace);
            if (ex.InnerException != null)
            {
                excMessage+=GetInnerException(ex.InnerException);
            }
            return excMessage;
        }

    }
}
