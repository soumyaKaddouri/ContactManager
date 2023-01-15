using ContactManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Serialisation
{
    public class BinarySerializer : ISerializer
    {
        public void Serialize(Folder data,string fileName)
        {
            var currentUserMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileLocation = Path.Combine(currentUserMyDocuments, fileName);

            Console.WriteLine($"Saving {fileLocation} file...");

            try
            {
                // Prompt user for encryption key 
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("> Please enter the encryption key: ");
                string key = Console.ReadLine();
                Console.ResetColor();

                // If no key is specified, We use the internal identifier (SID) of the current Windows identity
                if (string.IsNullOrEmpty(key))
                {
                    var identity = WindowsIdentity.GetCurrent();
                    key = identity.User.Value;
                }
                
                using (FileStream input = new FileStream(fileLocation, FileMode.Create))
                {
                    //Encryption
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    byte[] iv = new byte[16];
                    Array.Copy(keyBytes, iv, Math.Min(keyBytes.Length, iv.Length));

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = keyBytes;
                        aes.IV = iv;

                        using (CryptoStream cryptoStream = new CryptoStream(input, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(cryptoStream, data);
                        }
                    }
                }

                Helper.PrintSuccess($"{fileLocation} savec successfully.");
            }
            catch (FileNotFoundException )
            {
                Helper.PrintError("File not found, please check the file path.");
            }
            catch (CryptographicException )
            {
                Helper.PrintError("Decryption failed. Please check the key and try again.");
            }
            catch (IOException)
            {
                Helper.PrintError("The File is in use by other program, please try later.");
            }
            catch (SerializationException)
            {
                Helper.PrintError("There is a problem with the file, Please check the file format and try again.");
            }
        }

        public Folder Deserialize(string fileName, Folder root, out bool exceptionHandled)
        {
            var currentUserMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileLocation = Path.Combine(currentUserMyDocuments, fileName);
            
            try
            {
                // Prompt user for encryption key : We must give the same key used during the encryption
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("> Please enter the encryption key: ");
                string key = Console.ReadLine();
                Console.ResetColor();

                // If no key is specified, We use the internal identifier (SID) of the current Windows identity
                if (string.IsNullOrEmpty(key))
                {
                    var identity = WindowsIdentity.GetCurrent();
                    key = identity.User.Value;
                }

                using (FileStream input = new FileStream(fileLocation, FileMode.Open))
                {
                    // Decryption
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    byte[] iv = new byte[16];
                    Array.Copy(keyBytes, iv, Math.Min(keyBytes.Length, iv.Length));

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = keyBytes;
                        aes.IV = iv;

                        using (CryptoStream cryptoStream = new CryptoStream(input, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            exceptionHandled = false;
                            return (Folder)formatter.Deserialize(cryptoStream);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Helper.PrintError("File not found, please check the file path.");
                exceptionHandled = true;
                return root;
            }
            catch (CryptographicException)
            {
                Helper.PrintError("Decryption failed. Please check the key and try again.");
                exceptionHandled = true;
                return root;
            }
            catch (IOException)
            {
                Helper.PrintError("The File is in use by other program, please try later.");
                exceptionHandled = true;
                return root;
            }
            catch (SerializationException)
            {
                Helper.PrintError("There is a problem with the Serialization process, Please check the file format or the encryption key and try again.");
                exceptionHandled = true;
                return root;
            }
        }

    }
}
