using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Serialisation
{
    internal class XmlSerializer : ISerializer
    {

        public void Serialize(string fileName, object obj)
        {
            using (Stream stream = File.Open(fileName, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                serializer.Serialize(stream, obj);
            }
        }

        public T Deserialize<T>(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

    }
}
