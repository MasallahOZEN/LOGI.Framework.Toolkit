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
    public class DataContractSerializerClass : SerializerBase
    {
        protected override string SerializeObject<T>(T serializableValue, ISerializerOptions serializerOptions = null)
        {
            using (var writer = new StringWriter())
            {
                using (XmlWriter xmlWriter = new XmlTextWriter(writer))
                {
                    DataContractSerializer serializer = null;
                    if (serializerOptions != null)
                    {
                        var options = serializerOptions as DataContractSerializerOptions;
                        if (options != null && options.KnownTypes != null)
                        {
                            serializer = new DataContractSerializer(typeof(T), options.RootName, options.RootNamespace, options.KnownTypes);
                        }
                    }

                    if (serializer == null)
                    {
                        serializer = new DataContractSerializer(typeof(T));
                    }


                    serializer.WriteObject(xmlWriter, serializableValue);
                    return writer.ToString();
                }
            }

        }

        protected override object DeSerializeObject(Type type, string deserializableValue, ISerializerOptions serializerOptions = null)
        {
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(deserializableValue)))
            {
                using (var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer serializer = null;
                    if (serializerOptions != null)
                    {
                        var options = serializerOptions as DataContractSerializerOptions;
                        if (options != null && options.KnownTypes != null)
                        {
                            serializer = new DataContractSerializer(type, options.RootName, options.RootNamespace, options.KnownTypes);
                        }
                    }

                    if (serializer == null)
                    {
                        serializer = new DataContractSerializer(type);
                    }
                    // Deserialize the data and read it from the instance.
                    return serializer.ReadObject(reader);
                }
            }

        }
    }
}
