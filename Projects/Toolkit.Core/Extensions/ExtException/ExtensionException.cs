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

            var excMessage = ex.Message;
            if (ex.InnerException != null)
            {
                return string.Format("{0} > {1} ", excMessage, GetInnerException(ex.InnerException));
            }
            return excMessage;
        }

    }
}
