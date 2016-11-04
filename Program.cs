using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

/*
Develop a simple console application that allowes to list all files in directory and read contents of chosen file.
*/

namespace EighteenthTask
{
    class Program
    {
        static void GettingFiles(string _dirName)
        {
            Console.WriteLine("Files:");
            string[] files = Directory.GetFiles(_dirName);

            Console.WriteLine("Choose file:");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, files[i]);
            }
            Console.WriteLine("Enter the file number.");
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException) { }
            if (choice < files.Length + 1)
            {
                string file = files[choice - 1];
                ReadingFromFile(file);
            }
            else Console.WriteLine("This file number doesn't exist.");
        }

        static void ReadingFromFile(string _file)
        {
            using (FileStream fileStream = File.OpenRead(_file))
            {
                byte[] data = new byte[fileStream.Length];
                for (int index = 0; index < fileStream.Length; index++)
                {
                    data[index] = (byte)fileStream.ReadByte();
                }
                Console.WriteLine(Encoding.UTF8.GetString(data));
            }
        }

        static void DirectoryPath()
        {
            Console.WriteLine("Enter file path: ");
            string filePath = Convert.ToString(Console.ReadLine());
            if (Regex.IsMatch(filePath, @"^(([a-zA-Z]\:)|\\)(\\|(\\[^/:*?<>""|]*)+)$", RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Validated directory path.");
                string dirName = filePath;
                GettingFiles(dirName);
            }
            else
            {
                Console.WriteLine("Invalidated directory path.");
            }
        }

        static void Main(string[] args)
        {
            int choice = 0;
            while (choice != 2)
            {
                Console.WriteLine("Menu: \n1. Input directory path. \n2. Exit.");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException) { }
                switch (choice)
                {
                    case 1:
                        DirectoryPath();
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("The menu doesn't have this item");
                        break;
                }
            }
        }
    }
}
