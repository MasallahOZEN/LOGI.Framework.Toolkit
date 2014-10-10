using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using LOGI.Framework.Toolkit.Core.Exceptions;
using LOGI.Framework.Toolkit.Core.Properties;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtObject
{
    ///<summary>
    /// Object Extensions
    ///</summary>
    public static class Extentions
    {
        #region Public Functions

        /////<summary>
        ///// Convert to T
        /////</summary>
        /////<param name="convertableSource">Çevrilecek obje</param>
        /////<param name="defaultValue">Hata durumunda dönecek default değer</param>
        /////<typeparam name="T"></typeparam>
        /////<exception cref="ExcConvertionException"></exception>
        /////<returns>T</returns>
        //public static T To<T>(this object convertableSource, T defaultValue)
        //{
        //    try
        //    {
        //        if (convertableSource == null || convertableSource.ToString() == string.Empty)
        //            return defaultValue;
        //        return To<T>(convertableSource);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("ConvertableSource:", convertableSource);
        //        ex.Data.Add("defaultValue:", defaultValue);

        //        throw new ExcConvertionException("Tür dönüşümünde hata oluştu",ex);
        //    }
        //}

        /////<summary>
        ///// Convert to T
        /////</summary>
        /////<param name="convertableSource">Çevrilecek obje</param>
        /////<param name="returnDefaultValue">Hata durumunda default değer dönsün mü?</param>
        /////<typeparam name="T"></typeparam>
        /////<returns></returns>
        //public static T To<T>(this object convertableSource, bool returnDefaultValue=false)
        //{
        //    try
        //    {
        //        if (convertableSource == null || convertableSource.ToString() == string.Empty)
        //            return default(T);
        //        return To<T>(convertableSource);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("ConvertableSource:", convertableSource);
        //        ex.Data.Add("returnDefaultValue:", returnDefaultValue);

        //        throw new ExcConvertionException("Tür dönüşümünde hata oluştu", ex);
        //    }
        //}

        ///<summary>
        /// Convert to T
        ///</summary>
        ///<param name="convertableSource">Çevrilecek obje</param>
        ///<param name="defaultValue"></param>
        ///<param name="returnDefaultValue"></param>
        ///<param name="provider"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public static T To<T>(this object convertableSource, T defaultValue = default(T), bool returnDefaultValue = false, IFormatProvider provider = null)
        {
            try
            {
                if (convertableSource==null)
                {
                    if (returnDefaultValue)
                    {
                        return default(T);
                    }
                    else
                    {
                        return defaultValue;
                    }

                }
                if (typeof(T).IsGenericType)
                {
                    if (typeof(T).GetGenericArguments()[0].ToString() == "System.Guid")
                    {
                        return (T)((object)new Guid(convertableSource.ToString()));
                    }

                    if (convertableSource == DBNull.Value)
                    {
                        throw new ArgumentNullException("convertableSource", Resources.Extentions_To_ConvertableSource_must_be_not_null);
                    }

                    return GetConvertedCultureValue<T>(convertableSource,provider);
                }

                if ((typeof(T).ToString() == "System.Guid"))
                {
                    return (T)((object)new Guid(convertableSource.ToString()));
                }
                
                if (typeof(T).BaseType == typeof(Enum))
                {
                    return (T)Enum.Parse(typeof(T), convertableSource.ToString());                    
                }

                if (typeof(T) == typeof(bool))
                {
                    var convertableSourceVal = convertableSource;

                    if (convertableSource.ToString()=="0")
                    {
                        convertableSourceVal = "false";
                    }

                    if (convertableSource.ToString() == "1")
                    {
                        convertableSourceVal = "true";
                    }

                    return (T)Convert.ChangeType(convertableSourceVal, typeof(T));
                }

                return GetConvertedCultureValue<T>(convertableSource,provider);
            }
            catch (Exception ex)
            {
                //TODO: Log
                if (returnDefaultValue)
                {
                    return default(T);
                }

                if (!object.Equals(defaultValue, default(T)))
                {
                    return defaultValue;
                }

                var errorStr = string.Format("Tür dönüşümünde hata oluştu ! {0}Çevrilecek Değer:{1}{0}Çevrilmek İstenen Tip:{2}{0}DefaultValue:{3}{0}Provider:{4}{0}ReturnDefaultValue:{5}{0}", Environment.NewLine, convertableSource, typeof(T).FullName, defaultValue, provider, returnDefaultValue);//TODO: resourse'a al

                throw new ExcConvertionException(errorStr, ex);
            }
        }

        #region Chained null checks and the Maybe monad
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o,
  Func<TInput, TResult> evaluator, TResult failureValue) where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator)
  where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator)
          where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? null : o;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
  where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static TInput DoWhen<TInput>(this TInput o, Func<TInput, bool> evaluator, Func<TInput, TInput> wheTrueAction, Func<TInput, TInput> wheFalseAction)
  where TInput : class
        {
            if (o == null)
            {
                return null;
            }

            return evaluator(o) ? wheTrueAction(o) : wheFalseAction(o);
        }

        public static TInput DoWhenNull<TInput>(this TInput o, Action<TInput> nullAction)
  where TInput : class
        {
            if (o == null)
            {
                nullAction(o);
                return null;
            }
            return o;
        }

        public static TInput DoWhenNull<TInput>(this TInput o, Action nullAction)
  where TInput : class
        {
            if (o == null)
            {
                nullAction();
                return null;
            }
            return o;
        } 
        #endregion

        #region In
        /// <summary>
        /// Determines whether this System.Object is contained in the specified IEnumerable
        /// </summary>
        /// <param name="o">The System.Object</param>
        /// <param name="enumerable">The IEnumerable to check</param>
        /// <returns>true if enumerable contains this System.Object, otherwise false.</returns>
        public static bool In(this object o, IEnumerable enumerable)
        {
            foreach (object item in enumerable)
            {
                if (item.Equals(o))
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// Determines whether this T is contained in the specified 'IEnumerable of T'
        /// </summary>
        /// <typeparam name="T">This System.Object's type</typeparam>
        /// <param name="t">This item</param>
        /// <param name="enumerable">The 'IEnumerable of T' to check</param>
        /// <returns>true if enumerable contains this item, otherwise false.</returns>
        public static bool In<T>(this T t, IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                if (item.Equals(t))
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// Determines whether this System.Object is contained in the specified values
        /// </summary>
        /// <param name="o">The System.Object</param>
        /// <param name="items">The values to compare</param>
        /// <returns>true if values contains this System.Object, otherwise false.</returns>
        public static bool In(this object o, params object[] items)
        {
            foreach (object item in items)
            {
                if (item.Equals(o))
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// Determines whether this T is contained in the specified values
        /// </summary>
        /// <typeparam name="T">This System.Object's type</typeparam>
        /// <param name="t">This item</param>
        /// <param name="items">The values to compare</param>
        /// <returns>true if values contains this item, otherwise false.</returns>
        public static bool In<T>(this T t, params T[] items)
        {
            foreach (T item in items)
            {
                if (item.Equals(t))
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// Determines whether this collections contains any of the specified values
        /// </summary>
        /// <typeparam name="T">The type of the values to compare</typeparam>
        /// <param name="t">This collection</param>
        /// <param name="items">The values to compare</param>
        /// <returns>true if the collection contains any of the specified values, otherwise false</returns>
        public static bool ContainsAny<T>(this T t, params T[] items) where T : ICollection<T>
        {
            foreach (T item in items)
            {
                if (t.Contains(item))
                { return true; }
            }
            return false;
        }
        #endregion

        ///<summary>
        /// Döviz kurları arasında yapılacak dönüşümlerde string değerin kura dönüştürülebilmesi için o anki culture 'a göre decimal separator değiştirilir
        ///</summary>
        ///<param name="currencyValue"></param>
        ///<param name="toStringFormat"></param>
        ///<returns></returns>
        public static string ToStringCultureSeparatorFormat(this object currencyValue, string toStringFormat = "", CultureInfo cultureInfo = null)
        {
            if (string.IsNullOrEmpty(currencyValue.With(x => x).Return(x => x.ToString(), "")))
            {
                return "0";
            }

            var cultureInfoValue = cultureInfo ?? CultureInfo.CurrentCulture;

            var value = currencyValue.ToString();
            
            //var newCurrencyData = value.Replace(",", cultureInfoValue.NumberFormat.NumberDecimalSeparator).
            //    Replace(".", cultureInfoValue.NumberFormat.NumberDecimalSeparator);

            var p = Regex.Split(value, @"^(.*)([,.])([0-9]+)$");

            var newCurrencyData = p[0];

            if (p.Length>1)
            {
                var realNumber = Regex.Replace(p[1], @"[,.]", "");
                var fraction = p[3];

                newCurrencyData = string.Format("{0}{1}{2}", realNumber, cultureInfoValue.NumberFormat.NumberDecimalSeparator, fraction);
    
            }
            
            if (!string.IsNullOrEmpty(toStringFormat))
            {
                var decimalVal = newCurrencyData.To<decimal>(defaultValue:-1,provider:cultureInfo);

                if (decimalVal>-1)
                {
                    if (cultureInfo!=null)
                    {
                        newCurrencyData = decimalVal.ToString(toStringFormat, cultureInfo);
                    }
                    else
                    {
                        newCurrencyData = decimalVal.ToString(toStringFormat);
                    }
                }

            }

            return newCurrencyData;
        }

        #region AOP
        // Dictionary to hold type initialization methods' cache 
        static private Dictionary<Type, Action<Object>> _typesInitializers = new Dictionary<Type, Action<Object>>();

        /// <summary>
        /// Implements precompiled setters with embedded constant values from DefaultValueAttributes
        /// </summary>
        public static void ApplyDefaultValues(this Object _this)
        {
            Action<Object> setter = null;

            // Attempt to get it from cache
            if (!_typesInitializers.TryGetValue(_this.GetType(), out setter))
            {
                // If no initializers are added do nothing
                setter = (o) => { };

                // Iterate through each property
                ParameterExpression objectTypeParam = Expression.Parameter(typeof(object), "this");
                foreach (PropertyInfo prop in _this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    Expression dva;

                    // Skip read only properties
                    if (!prop.CanWrite)
                        continue;

                    // There are no more then one attribute of this type
                    DefaultValueAttribute[] attr = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false) as DefaultValueAttribute[];

                    // Skip properties with no DefaultValueAttribute
                    if ((null == attr) || (null == attr[0]))
                        continue;

                    // Build the Lambda expression
#if DEBUG
                    // Make sure types do match
                    try
                    {
                        dva = Expression.Convert(Expression.Constant(attr[0].Value), prop.PropertyType);
                    }
                    catch (InvalidOperationException e)
                    {
                        string error = String.Format("Type of DefaultValueAttribute({3}{0}{3}) does not match type of property {1}.{2}",
                            attr[0].Value.ToString(), _this.GetType().Name, prop.Name, ((typeof(string) == attr[0].Value.GetType()) ? "\"" : ""));

                        throw (new InvalidOperationException(error, e));
                    }
#else
                    dva = Expression.Convert(Expression.Constant(attr[0].Value), prop.PropertyType);
#endif
                    Expression setExpression = Expression.Call(Expression.TypeAs(objectTypeParam, _this.GetType()), prop.GetSetMethod(), dva);
                    Expression<Action<Object>> setLambda = Expression.Lambda<Action<Object>>(setExpression, objectTypeParam);

                    // Add this action to multicast delegate
                    setter += setLambda.Compile();
                }

                // Save in the type cache
                _typesInitializers.Add(_this.GetType(), setter);
            }

            // Initialize member properties
            setter(_this);
        }

        /// <summary>
        /// Implements cache of ResetValue delegates
        /// </summary>
        public static void ResetDefaultValues(this Object _this)
        {
            Action<Object> setter = null;

            // Attempt to get it from cache
            if (!_typesInitializers.TryGetValue(_this.GetType(), out setter))
            {
                // Init delegate with empty body,
                // If no initializers are added do nothing
                setter = (o) => { };

                // Go throu each property and compile Reset delegates
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(_this))
                {
                    // Add only these which values can be reset
                    if (prop.CanResetValue(_this))
                        setter += prop.ResetValue;
                }

                // Save in the type cache
                _typesInitializers.Add(_this.GetType(), setter);
            }

            // Initialize member properties
            setter(_this);
        }
        #endregion
        #endregion

        #region Private Functions
        private static T GetConvertedCultureValue<T>(object value, IFormatProvider cultureInfo)
        {
            var tempValue = value;
            
            Type destinationType = typeof(T);
            if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (tempValue == null)
                    return default(T);

                var nullableConverter = new NullableConverter(destinationType);
                destinationType = nullableConverter.UnderlyingType;
            }
            if (tempValue is IConvertible)
            {
                var culture = cultureInfo;
                tempValue = (T)Convert.ChangeType(tempValue, destinationType, culture);
            }
            return (T)tempValue;
        }
        #endregion
    }
}
