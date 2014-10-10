using System;
using System.Collections.Generic;

namespace LOGI.Framework.Toolkit.Foundation.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T serializableValue);
        string Serialize<T>(T serializableValue, ISerializerOptions serializerOptions);
        T DeSerialize<T>(string deSerializableValue);
        T DeSerialize<T>(string deSerializableValue, ISerializerOptions serializerOptions);

        object DeSerialize(Type type, string deSerializableValue);
        object DeSerialize(Type type, string deSerializableValue, ISerializerOptions serializerOptions);
    }
}
