using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Reflection
{
    public sealed class DeepCopyCloner
    {
        public static T DeepCopy<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Object cannot be null");
            return (T)Process(obj, new Dictionary<object, object>() { });
        }

        static object Process(object obj, Dictionary<object, object> circular)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            if (type.IsArray)
            {
                if (circular.ContainsKey(obj))
                    return circular[obj];
                string typeNoArray = type.FullName.Replace("[]", string.Empty);
                Type elementType = Type.GetType(typeNoArray + ", " + type.Assembly.FullName);
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                circular[obj] = copied;
                for (int i = 0; i < array.Length; i++)
                {
                    object element = array.GetValue(i);
                    object copy = null;
                    if (element != null && circular.ContainsKey(element))
                        copy = circular[element];
                    else
                        copy = Process(element, circular);
                    copied.SetValue(copy, i);
                }
                return Convert.ChangeType(copied, obj.GetType());
            }
            if (type.IsClass)
            {
                if (circular.ContainsKey(obj))
                    return circular[obj];
                object toret = Activator.CreateInstance(obj.GetType());
                circular[obj] = toret;
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj);
                    if (fieldValue == null)
                        continue;
                    object copy = circular.ContainsKey(fieldValue) ? circular[fieldValue] : Process(fieldValue, circular);
                    field.SetValue(toret, copy);
                }
                return toret;
            }
            else
                throw new ArgumentException("Unknown type");
        }

    }
}
