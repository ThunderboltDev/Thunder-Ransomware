using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace WindowsFormsApp2
{
    internal static class AESFileDecryption
    {
        internal static void DecryptFilesInUsersFolder(string password)
        {
            string baseDirectory = @"C:\Users";
            string[] encryptedExtensions = { ".thunder" };

            string[] files = Directory.GetFiles(baseDirectory, "*.*", SearchOption.AllDirectories)
                .Where(file => encryptedExtensions.Contains(Path.GetExtension(file)))
                .ToArray();

            foreach (string filePath in files)
            {
                string outputFile = Path.ChangeExtension(filePath, null);
                DecryptFile(filePath, outputFile, password);

                // Check if decryption was successful
                if (File.Exists(outputFile))
                {
                    // Rename the decrypted file back to its original name
                    string originalFileName = Path.GetFileNameWithoutExtension(filePath);
                    string originalFilePath = Path.Combine(Path.GetDirectoryName(outputFile), originalFileName);
                    File.Move(outputFile, originalFilePath);
                }
            }
        }

        internal static void DecryptFile(string inputFile, string outputFile, string password)
        {
            // Decryption logic remains the same
        }
    }
}

