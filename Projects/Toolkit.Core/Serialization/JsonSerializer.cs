using System;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Foundation.Serialization;

namespace LOGI.Framework.Toolkit.Core.Serialization
{
    public class JsonSerializer : SerializerBase
    {
        protected override string SerializeObject<T>(T serializableValue, ISerializerOptions serializerOptions = null)
        {
            if (serializerOptions==null)
            {
                return JsonConvert.SerializeObject(serializableValue);
            }
            else
            {
                var jsonSerOp = serializerOptions as JsonSerializerOptions;
                return JsonConvert.SerializeObject(serializableValue, jsonSerOp.Formatting, jsonSerOp.JsonSerializerSettings);
            }
        }

        protected override object DeSerializeObject(Type type, string deserializableValue, ISerializerOptions serializerOptions = null)
        {
            var jsonSerOp = serializerOptions as JsonSerializerOptions;

            if (serializerOptions == null || jsonSerOp==null)
            {
                return JsonConvert.DeserializeObject(deserializableValue,type);
            }
            else
            {
                
                return JsonConvert.DeserializeObject(deserializableValue,type,jsonSerOp.JsonSerializerSettings);
            }
        }

    }
}
