using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Serialisation
{
    internal interface ISerializer
    {
        void Serialize(string fileName, object obj);
        T Deserialize<T>(string fileName);

    }
}
