using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using LOGI.Framework.Toolkit.Core.Exceptions;
using LOGI.Framework.Toolkit.Core.Properties;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtDateTime
{
    ///<summary>
    /// Object Extensions
    ///</summary>
    public static class Extentions
    {
        #region Public Functions
        /// <summary>
        /// DateTime değerinden int olarak yaşı döndürür
        /// </summary>
        /// <param name="birthdate"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime birthdate)
        {
            var now = DateTime.Today;
            var years = now.Year - birthdate.Year;
            if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
                --years;

            return years;
        }

        #endregion

    }
}
