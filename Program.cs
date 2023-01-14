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
                Console.WriteLine("1. View the entire structure");
                Console.WriteLine("2. Create a new folder");
                Console.WriteLine("3. Add a contact");
                Console.WriteLine("4. Quit");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Helper.DisplayStructure(root, 0);
                        break;
                    case "2":
                        currentFolder =  Helper.CreateFolder(currentFolder);
                        break;
                    case "3":
                        currentFolder =  Helper.AddContact(currentFolder);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Unknown instruction");
                        break;
                        
                }
            }
        }

    }
}
