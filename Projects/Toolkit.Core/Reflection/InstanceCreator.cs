using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Reflection
{
    public static class InstanceCreator
    {
        ///<summary>
        /// T tipindeki objeden bir instance oluşturu
        ///</summary>
        ///<param name="param">contructor değerleri</param>
        ///<typeparam name="T">Instance'ı oluşturulacak tip</typeparam>
        ///<returns>Instance'ı oluşturulacak tip</returns>
        public static T CreateInstance<T>(params object[] param)
        {
            //var response = (T)Activator.CreateInstance(typeof(T), param);
            var response = (T)Activator.CreateInstance(typeof(T), BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance | BindingFlags.OptionalParamBinding, null, param, null);


            return response;
        }
    }
}
