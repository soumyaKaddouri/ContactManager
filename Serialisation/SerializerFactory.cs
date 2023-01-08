using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Serialisation
{
    internal class SerializerFactory
    {
        public ISerializer CreateSerializer(string type)
        {
            if (type.ToLower() == "xml")
            {
                return new XmlSerializer();
            }
            else if (type.ToLower() == "binary")
            {
                return new BinarySerializer();
            }
            else
            {
                throw new ArgumentException("Invalid serialization type.");
            }
            
        }
    }
}
