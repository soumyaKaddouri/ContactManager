using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    internal class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public ContactLink Link { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public Folder ParentFolder { get; set; }

        public Contact()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }

    }
}
