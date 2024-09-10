using Dovs.FileSystemInteractor.Interfaces;
using System;
using System.IO;

namespace Dovs.FileSystemInteractor.Services
{
    public class FileInteractionService : IFileInteractionService
    {
        public string SelectFilePath(IFilePathService filePathService, int levelsToTraverse, string fileExtension)
        {
            string basePath = filePathService.GetBasePath(levelsToTraverse);
            string[] files = filePathService.GetFiles(basePath, fileExtension);

            if (files.Length == 0)
            {
                Console.WriteLine($"No files with extension {fileExtension} found in the directory.");
            }
            else
            {
                Console.WriteLine($"Please select a file with extension {fileExtension}:");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }
            }

            Console.WriteLine($"{files.Length + 1}. Enter your own file path");

            int fileOption = GetOptionFromUser();
            string filePath;

            if (fileOption == files.Length + 1)
            {
                Console.Write("Enter the file path: ");
                filePath = Console.ReadLine() ?? string.Empty;
            }
            else if (fileOption < 1 || fileOption > files.Length)
            {
                Console.WriteLine("Invalid option. Please try again.");
                return SelectFilePath(filePathService, levelsToTraverse, fileExtension);
            }
            else
            {
                filePath = files[fileOption - 1];
            }

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("File not found. Please provide a valid file path.");
                return SelectFilePath(filePathService, levelsToTraverse, fileExtension);
            }

            return filePath;
        }

        private int GetOptionFromUser()
        {
            Console.Write("Enter the option number: ");
            string input = Console.ReadLine() ?? string.Empty;
            int option;
            if (int.TryParse(input, out option))
            {
                return option;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
                return GetOptionFromUser();
            }
        }
    }
}