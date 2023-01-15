using ContactManager.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public static class Helper
    {
        // Used to convert the 1st letter of a string to Uppercase
        static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public static Folder AddContact(Folder currentFolder, string[] fields)
        {
            // Checking the validity of the email address
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
                // Converting string to enum type ContactLink
                _link = (ContactLink)Enum.Parse(typeof(ContactLink), textInfo.ToTitleCase(fields[5]));

                // Creating a new contact
                var newContact = new Contact(textInfo.ToTitleCase(fields[1]), textInfo.ToTitleCase(fields[2]), fields[3],
                   textInfo.ToTitleCase(fields[4]), _link);

                // Adding a new contact to current Folder
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
            // Current folder point on the last folder created
            currentFolder = newFolder;

            PrintSuccess("Folder created successfully.");
           

            return currentFolder;

        }


        public static void DisplayStructure(Folder folder, int depth)
        {
            // I used a recursive approach to display the entire structure
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

        // This method is used when I want to load the file and retrieve the last created folder 
        public static Folder FindLastFolder(Folder folder)
        {
            Folder lastFolder = folder;
            if (folder.ChildFolders.Count > 0)
            {
                lastFolder = folder.ChildFolders.OrderByDescending(f => f.CreationDate).First();
                lastFolder = FindLastFolder(lastFolder);
            }
            return lastFolder;
        }
        
        // For printing an Error message
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
        }

        // For printing a Warning message 
        public static void PrinWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + message);
        }

        // For printing a Success message
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success: " + message);
        }
    }
}
