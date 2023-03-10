using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    [Serializable]
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public ContactLink Link { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        // It's important to have a parameterless constructor during when we declare a class as Serializable
        public Contact() { }

        public Contact(string firstName, string lastName, string email, string company, ContactLink link)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Company = company;
            Link = link;
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }

        public override string ToString()
        {
            return "| [C] " + LastName + ", " + FirstName + " (" + Company + "), Email:" + Email + ", Link:" + Link;
        }
    }
}
