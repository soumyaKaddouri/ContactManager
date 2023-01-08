using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    internal class Folder
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Folder ParentFolder { get; set; }
        public List<Folder> ChildFolders { get; set; }
        public List<Contact> Contacts { get; set; }

        public Folder(string name, Folder parentFolder = null)
        {
            Name = name;
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            ParentFolder = parentFolder;
            ChildFolders = new List<Folder>();
            Contacts = new List<Contact>();
        }
    }
}
