using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Core.Common.Collections;
using LOGI.Framework.Toolkit.Foundation.Serialization;

namespace LOGI.Framework.Toolkit.Core.Serialization
{
    public class BinaryFormatterSerializer : SerializerBase
    {
        #region Overrides of SerializerBase

        protected override string SerializeObject<T>(T serializableValue, ISerializerOptions serializerOptions)
        {            
            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();
            StreamReader sr = null;

            string result = "";

            try
            {
                formatter.Serialize(ms, serializableValue);
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
                try
                {
                    if (sr != null)
                    { sr.Close(); }
                    ms.Close();
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        protected override object DeSerializeObject(Type type, string deserializableValue, ISerializerOptions serializerOptions)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            try
            {
                var repo =  new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(deserializableValue));
                var result = formatter.Deserialize(repo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    ms.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion
    }
}
