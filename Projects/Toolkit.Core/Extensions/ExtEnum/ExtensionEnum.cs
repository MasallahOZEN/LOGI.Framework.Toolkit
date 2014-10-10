using System;
using System.Reflection;
using LOGI.Framework.Toolkit.Core.Enums;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtEnum
{
    ///<summary>
    /// Enum Extensions
    ///</summary>
    public static class Extensions
    {
        ///<summary>
        /// GetEnumValue
        ///</summary>
        ///<param name="source">Kaynak Enum type</param>
        ///<param name="value">Enum değerini vericek değer</param>
        ///<typeparam name="T">Generic Enum Tipi</typeparam>
        ///<returns>Generic Enum Tipi</returns>
        ///<exception cref="Exception"></exception>
        public static T GetEnumValue<T>(this Enum source,string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                throw new Exception(string.Format("Can not convert {0} to enum type {1}", value, typeof(T)));
            }
        }
        
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum source)
        {
            // Get the type
            var type = source.GetType();

            // Get fieldinfo for this type
            var fieldInfo = type.GetField(source.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs!=null && attribs.Length > 0 ? attribs[0].StringValue : null;
        }

    }
}
