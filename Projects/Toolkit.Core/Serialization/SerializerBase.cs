using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Foundation.Serialization;

namespace LOGI.Framework.Toolkit.Core.Serialization
{
    public abstract class SerializerBase : ISerializer
    {
        public string Serialize<T>(T serializableValue)
        {
            var returnValue = SerializeObject<T>(serializableValue);

            return returnValue;
        }

        public virtual string Serialize<T>(T serializableValue, ISerializerOptions serializerOptions)
        {
            var returnValue = SerializeObject<T>(serializableValue, serializerOptions);

            return returnValue;
        }

        public T DeSerialize<T>(string deSerializableValue)
        {
            var returnValue = DeSerializeObject(typeof(T), deSerializableValue);

            if (returnValue != null)
            {
                return (T)returnValue;
            }

            return default(T);
        }

        public virtual T DeSerialize<T>(string deSerializableValue, ISerializerOptions serializerOptions)
        {
            var returnValue = DeSerializeObject(typeof(T),deSerializableValue, serializerOptions);

            if (returnValue != null)
            {
                return (T)returnValue;
            }

            return default(T);

        }

        public object DeSerialize(Type type, string deSerializableValue)
        {
            var returnValue = DeSerializeObject(type, deSerializableValue);

            return returnValue;
        }

        public object DeSerialize(Type type, string deSerializableValue, ISerializerOptions serializerOptions)
        {
            var returnValue = DeSerializeObject(type, deSerializableValue, serializerOptions);

            return returnValue;
        }

        protected abstract string SerializeObject<T>(T serializableValue, ISerializerOptions serializerOptions = null);

        protected abstract object DeSerializeObject(Type type, string deserializableValue, ISerializerOptions serializerOptions = null);
    }
}
