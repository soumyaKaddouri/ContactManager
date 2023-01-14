using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public static class Helper
    {
        static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public static Folder AddContact(Folder currentFolder, string[] fields)
        {
            // Vérification de la validité de l'adresse email
            try
            {
                var addr = new System.Net.Mail.MailAddress(fields[3]);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid email address.");
                Console.ResetColor();
                return currentFolder;
            }

            string[] links = new string[4] { "friend", "colleague", "relation", "network" };
            ContactLink _link;

            //Link Verification
            if (links.Contains(fields[5]))
            {
                //Convertion de string vers le type enum ContactLink
                
                _link = (ContactLink)Enum.Parse(typeof(ContactLink), textInfo.ToTitleCase(fields[5]));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Link format (must be: Friend, Colleague, Relation, or Network)");
                Console.ResetColor();

                return currentFolder;
            }


            //Creation de nouveau contact

            /*var newContact = new Contact(fields[1].Substring(0,1).ToUpper() + fields[1].Substring(1),
                fields[2].Substring(0, 1).ToUpper() + fields[2].Substring(1), fields[3],
                fields[4].Substring(0,1).ToUpper() + fields[4].Substring(1), _link);*/

            var newContact = new Contact(textInfo.ToTitleCase(fields[1]),textInfo.ToTitleCase(fields[2]), fields[3],
               textInfo.ToTitleCase(fields[4]), _link);

            // Ajout de nouveau contact au dossier courant
            currentFolder.Contacts.Add(newContact);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contact added successfully.");
            Console.ResetColor ();
           
            return currentFolder;
        }

        public static Folder CreateFolder(Folder currentFolder, string nameFolder)
        {
            var newFolder = new Folder(textInfo.ToTitleCase(nameFolder));

            currentFolder.ChildFolders.Add(newFolder);
            currentFolder = newFolder;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Folder created successfully.");
            Console.ResetColor () ;
           

            return currentFolder;

        }


        public static void DisplayStructure(Folder folder, int depth)
        {
            Console.WriteLine(new string('-', depth) + folder.ToString());
            foreach (var subFolder in folder.ChildFolders)
            {
                DisplayStructure(subFolder, depth + 1);
            }
            foreach (var contact in folder.Contacts)
            {
                Console.WriteLine(new string('-', depth + 1) + contact.ToString());
            }
        }
    }
}
