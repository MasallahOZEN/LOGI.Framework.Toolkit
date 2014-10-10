using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Sessions
{
    /// <summary>
    /// Session data kaynağından veri çekmek için 
    /// <example> 
    /// <code> 
    /// SessionManager&lt;string&gt;.Session.SetValue(SessionKeys.UserName, "XXXXX");
    /// string s =  SessionManager&lt;string&gt;.Session.GetValue(SessionKeys.UserName);
    /// </code> 
    /// </example> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SessionManager<T>
    {
        private static bool IsWebApplication()
        {
            return (System.Web.HttpContext.Current != null);
        }
        public static Session<T> Session
        {
            get
            {
                if (IsWebApplication())
                {
                    return new WebSession<T>();
                }
                else
                {
                    return new WindowsSession<T>();
                }
            }
        }
    }
}
