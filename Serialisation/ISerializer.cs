using ContactManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Serialisation
{
    internal interface ISerializer
    {
        void Serialize(Folder data, string fileName);
        Folder Deserialize(string fileName, Folder root, out bool exceptionHandled);

    }
}
