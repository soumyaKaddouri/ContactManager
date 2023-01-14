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
        // Utiliser pour convertir la 1ere lettre d'une chaine de caractere au Majuscule
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
                PrintError("Invalid email address.");

                return currentFolder;
            }

            string[] links = new string[4] { "friend", "colleague", "relation", "network" };
            ContactLink _link;

            // Link Verification
            if (links.Contains(fields[5]))
            {
                // Convertion de string vers le type enum ContactLink
                _link = (ContactLink)Enum.Parse(typeof(ContactLink), textInfo.ToTitleCase(fields[5]));

                // Creation de nouveau contact
                var newContact = new Contact(textInfo.ToTitleCase(fields[1]), textInfo.ToTitleCase(fields[2]), fields[3],
                   textInfo.ToTitleCase(fields[4]), _link);

                // Ajout de nouveau contact au dossier courant
                currentFolder.Contacts.Add(newContact);

                PrintSuccess("Contact added successfully.");
            }
            else
            {
                PrintError("Invalid Link format (must be: Friend, Colleague, Relation, or Network)");
            }
                   
            return currentFolder;
        }

        public static Folder CreateFolder(Folder currentFolder, string nameFolder)
        {
            var newFolder = new Folder(textInfo.ToTitleCase(nameFolder));

            currentFolder.ChildFolders.Add(newFolder);
            currentFolder = newFolder;

            PrintSuccess("Folder created successfully.");
           

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

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
        }

        public static void PrinWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + message);
        }

        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success: " + message);
        }
    }
}
