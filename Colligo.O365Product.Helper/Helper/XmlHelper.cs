using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Colligo.O365Product.Helper.Helper
{
    public static class XmlHelper
    {
        /// <summary>
        /// method will generate xml for a particular object
        /// </summary>
        /// <param name="type"></param>
        /// <returns>String</returns>
        /// <remarks></remarks>
        public static string ObjectToXml<T>(T type)
        {
            TextWriter writer = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(type.GetType(), string.Empty);
                writer = new StringWriter();
                serializer.Serialize(writer, type);
                return writer.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Generic Method will generate object from a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns>ObjectType</returns>
        /// <remarks></remarks>
        public static T XmlToObject<T>(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                stream = new StringReader(xml);
                reader = new XmlTextReader(stream);
                return (T)serializer.Deserialize(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
