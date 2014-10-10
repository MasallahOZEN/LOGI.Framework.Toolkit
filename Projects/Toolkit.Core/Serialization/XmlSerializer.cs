using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Core.Common.Collections;
using LOGI.Framework.Toolkit.Foundation.Serialization;

namespace LOGI.Framework.Toolkit.Core.Serialization
{
    public class CoreXmlSerializer : SerializerBase
    {
        #region Overrides of SerializerBase

        protected override string SerializeObject<T>(T serializableValue, ISerializerOptions serializerOptions)
        {
            XmlSerializer serializer = null;

            if (serializerOptions != null)
            {
                var options = serializerOptions as XMLSerializerOptions;
                if (options != null && options.ExtraTypes != null)
                {
                    serializer = new XmlSerializer(typeof(T), options.ExtraTypes);
                }
            }

            if (serializer == null)
            {
                serializer = new XmlSerializer(typeof(T));
            }

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
            StreamReader sr = null;

            string result = "";

            try
            {
                serializer.Serialize(sw, serializableValue);
                ms.Position = 0;
                sr = new StreamReader(ms, Encoding.UTF8);
                result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
                if (sr != null)
                { sr.Close(); }
                ms.Close();
            }

            return result;
        }

        protected override object DeSerializeObject(Type type, string deserializableValue, ISerializerOptions serializerOptions)
        {
            // Yeni bir Personel Nesnesi Yaratıyoruz Personel ReturnedEmployee; 
            // DeSerialize işleminde kullanılmak üzere yeni bir XmlSerializer 
            // nesnesi yaratıyor ve Serialize edilmiş verinin hangi nesne(Class) tipine çevirileceğini gösteriyoruz. 
            XmlSerializer serializer = null;

            if (serializerOptions != null)
            {
                var options = serializerOptions as XMLSerializerOptions;
                if (options != null && options.ExtraTypes != null)
                {
                    var list=options.ExtraTypes.ToList();

                    if (list.Count(x => x == typeof(SerializableDictionary<string, string>)) < 0)
                    {
                        list.Add(typeof(SerializableDictionary<string, string>));
                        options.ExtraTypes = list.ToArray();
                    }

                    serializer = new XmlSerializer(type, options.ExtraTypes);
                }
                else
                {
                    options=new XMLSerializerOptions
                                {
                                    ExtraTypes = new Type[] {typeof (SerializableDictionary<string, string>)}
                                };
                }
            }

            if (serializer == null)
            {
                serializer = new XmlSerializer(type);
            }


            // XML Verisini tutmak için bir StringReader yaratıyoruz.  
            StringReader SR = new StringReader(deserializableValue);
            XmlReader XR = new XmlTextReader(SR);

            // XML verisinin Deserialize edilip edilmeyeceğini kontrol ediyoruz.  
            if (serializer.CanDeserialize(XR))
            {
                // Ve XML verisini Deserialize ediyoruz. 
                return serializer.Deserialize(XR);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
