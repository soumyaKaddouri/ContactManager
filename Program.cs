using ContactManager.Models;
using ContactManager.Serialisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class Program
    {
        static Folder root = new Folder ("Root");
        static Folder currentFolder = root;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine(@"|*-----------------Menu-------------------*|");
                Console.WriteLine(@"|* 1. View the entire structure           *|");
                Console.WriteLine(@"|* 2. Create a new folder                 *|");
                Console.WriteLine(@"|* 3. Add a contact                       *|");
                Console.WriteLine(@"|* 4. Load                                *|");
                Console.WriteLine(@"|* 5. Save                                *|");
                Console.WriteLine(@"|* 6. Quit                                *|");
                Console.WriteLine(@"|*----------------------------------------*|");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("> ");

                var input = Console.ReadLine();
                Console.ResetColor();

                input = (!string.IsNullOrEmpty(input) ?input.ToLower() : string.Empty);

                string[] fields = input.Split(' ', StringSplitOptions.None);

                switch (fields[0])
                {
                    case "1":
                        Helper.DisplayStructure(root, 0);
                        break;
                    case "2":
                        if (fields.Length > 1)
                        {
                            currentFolder = Helper.CreateFolder(currentFolder, fields[1]);
                        }
                        else
                        {
                            Helper.PrinWarning("Please mention the folder name.");
                        }
                        break;
                    case "3":
                        if (fields.Length > 5)
                        {
                            currentFolder = Helper.AddContact(currentFolder, fields);
                        }
                        else
                        {
                            Helper.PrinWarning("Please add all the contact information.");
                        }
                        break;
                    case "4":
                        var factory = new SerializerFactory();
                        ISerializer serializer;
                        string fileName;
                        Console.WriteLine("What type of load you want: ");
                        Console.WriteLine(" 1.Xml ");
                        Console.WriteLine(" 2.Binary");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("> ");
                        int? loadingType = int.Parse(Console.ReadLine());
                        Console.ResetColor();

                        if (loadingType == 1)
                        {
                            serializer = factory.CreateSerializer("xml");
                            fileName = "ContactManager1.db";

                        }
                        else if (loadingType == 2)
                        {
                            serializer = factory.CreateSerializer("binary");
                            fileName = "ContactManager2.db";
                        }
                        else
                        {
                            Helper.PrintError("Unknown instruction");
                            break;
                        }

                        string password = "mypassword";
                        int tryCount = 0;
                        string enteredPassword;
                        while (tryCount < 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("> Please enter the password: ");
                            enteredPassword = Console.ReadLine();
                            Console.ResetColor();

                            if (enteredPassword == password)
                            {
                                bool exceptionHandled;
                                root = serializer.Deserialize(fileName,root, out exceptionHandled);
                                
                                currentFolder = Helper.FindLastFolder(root);
                                if (!exceptionHandled)
                                {
                                    Helper.PrintSuccess("The object is loaded.");
                                }
                                break;
                            }
                            else
                            {
                                tryCount++;
                                Helper.PrintError("Wrong password, please try again.");
                            }
                        }
                        if (tryCount == 3)
                        {
                            var currentUserMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            var fileLocation = Path.Combine(currentUserMyDocuments, fileName);
                            Console.WriteLine(fileLocation);
                            File.Delete(fileLocation);
                            Helper.PrinWarning("You reached the maximum number of tries, the file has been deleted.");
                        }
                        
                        break;
                    case "5":
                        var fact = new SerializerFactory();
                        ISerializer serializer1;
                        Console.WriteLine("What type of load you want: ");
                        Console.WriteLine(" 1.Xml ");
                        Console.WriteLine(" 2.Binary");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("> ");
                        int? type = int.Parse(Console.ReadLine());
                        Console.ResetColor();

                        if (type == 1)
                        {
                            serializer1 = fact.CreateSerializer("xml");
                            serializer1.Serialize(root, "ContactManager1.db");
                        }
                        else if (type == 2)
                        {
                            serializer1 = fact.CreateSerializer("binary");
                            serializer1.Serialize(root, "ContactManager2.db");
                        }
                        else
                        {
                            Helper.PrintError("Unknown instruction");
                            break;
                        }
                        break;
                    case "6":
                        return;
                    default:
                        Helper.PrintError("Unknown instruction");
                        break;
                        
                }
            }
        }

    }
}
