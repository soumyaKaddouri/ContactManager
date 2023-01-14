using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal static class Helper
    {
        static void AddContact(Folder currentFolder)
        {
            Console.WriteLine("Enter the first name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter the last name: ");
            var lastName = Console.ReadLine();
            Console.WriteLine("Entrez the email: ");
            var email = Console.ReadLine();

            // Vérification de la validité de l'adresse email
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid email address.");
                return;
            }

            Console.WriteLine("Enter the company name:");
            var company = Console.ReadLine();

            Console.WriteLine("Enter the link (Friend, Colleague, Relation, Network):");
            var link = Console.ReadLine();

            string[] links = new string[4] { "Friend", "Colleague", "Relation", "Network" };
            ContactLink _link;

            //Link Verification
            if (links.Contains(link))
            {
                //Convertion de string vers le type enum ContactLink
                _link = (ContactLink)Enum.Parse(typeof(ContactLink), link);
            }
            else
            {
                throw new Exception("Invalid Link format (must be: Friend, Colleague, Relation, or Network)");
            }

            
            //Creation de nouveau contact
            var newContact = new Contact(firstName,lastName,email,company,_link);
            
            // Ajout de nouveau contact au dossier courant
            currentFolder.Contacts.Add(newContact);
            Console.WriteLine("Contact added successfully.");
        }

        static void CreateFolder(Folder currentFolder)
        {
            Console.WriteLine("Entrez the folder name:");
            var name = Console.ReadLine();

            var newFolder = new Folder(name);
            

            currentFolder.ChildFolders.Add(newFolder);
            currentFolder = newFolder;
            Console.WriteLine("Folder created successfully.");

        }


            static void DisplayStructure(Folder folder, int depth)
        {
            Console.WriteLine(new string('-', depth) + folder.Name);
            foreach (var subFolder in folder.ChildFolders)
            {
                DisplayStructure(subFolder, depth + 1);
            }
            foreach (var contact in folder.Contacts)
            {
                Console.WriteLine(new string('-', depth + 1) + contact.FirstName + " " + contact.LastName);
            }
        }
    }
}
