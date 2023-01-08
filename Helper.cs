using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal class Helper
    {
        public static void CreateContact(string firstName, string lastName, string email, string company, ContactLink link, Folder parentFolder = null)
        {
            var newContact = new Contact(firstName, lastName, email, company, link, parentFolder);
            if (parentFolder == null)
            {
                // Ajoutez le nouveau contact à la liste des contacts racines
            }
            else
            {
                parentFolder.Contacts.Add(newContact);
            }
        }

    }
}
