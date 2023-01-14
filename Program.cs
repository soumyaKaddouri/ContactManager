using ContactManager.Models;
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
                Console.WriteLine(@"|*-----------------Menu-------------------*|");
                Console.WriteLine(@"|* 1. View the entire structure           *|");
                Console.WriteLine(@"|* 2. Create a new folder                 *|");
                Console.WriteLine(@"|* 3. Add a contact                       *|");
                Console.WriteLine(@"|* 4. Quit                                *|");
                Console.WriteLine(@"|*----------------------------------------*|");

                Console.WriteLine();
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
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please mention the folder name");
                            Console.ResetColor();
                        }
                        break;
                    case "3":
                        if (fields.Length > 5)
                        {
                            currentFolder = Helper.AddContact(currentFolder, fields);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please add all the contact information");
                            Console.ResetColor();
                        }
                        break;
                    case "4":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unknown instruction");
                        Console.ResetColor();
                        break;
                        
                }
            }
        }

    }
}
