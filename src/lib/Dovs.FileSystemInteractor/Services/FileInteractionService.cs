using Dovs.FileSystemInteractor.Interfaces;
using System;
using System.IO;

namespace Dovs.FileSystemInteractor.Services
{
    public class FileInteractionService : IFileInteractionService
    {
        private const int DefaultLevelsToTraverse = 2;
        private const string DefaultFileExtension = ".xlsx";

        /// <summary>
        /// Selects a file path based on the provided file extension and levels to traverse.
        /// </summary>
        /// <param name="filePathService">The file path service to use for retrieving file paths.</param>
        /// <param name="levelsToTraverse">The number of directory levels to traverse.</param>
        /// <param name="fileExtension">The file extension to filter files.</param>
        /// <returns>The selected file path.</returns>
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
                DisplayFileOptions(files, fileExtension);
            }

            Console.WriteLine($"{files.Length + 1}. Enter your own file path");

            int fileOption = GetOptionFromUser();
            string filePath = GetFilePathFromOption(files, fileOption);

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("File not found. Please provide a valid file path.");
                return SelectFilePath(filePathService, levelsToTraverse, fileExtension);
            }

            return filePath;
        }

        /// <summary>
        /// Gets a file path with the specified extension.
        /// </summary>
        /// <param name="filePathService">The file path service to use for retrieving file paths.</param>
        /// <returns>The selected file path with the specified extension.</returns>
        public string GetFilePathWithExtension(IFilePathService filePathService)
        {
            string fileExtension = GetFileExtension();
            if (string.IsNullOrEmpty(fileExtension))
            {
                Console.WriteLine("Invalid file extension. Exiting the program.");
                return string.Empty;
            }

            string filePath = SelectFilePath(filePathService, DefaultLevelsToTraverse, fileExtension);
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Invalid file path. Exiting the program.");
                return string.Empty;
            }

            return filePath;
        }

        /// <summary>
        /// Prompts the user to select a file extension or specify one directly.
        /// </summary>
        /// <returns>The selected or specified file extension.</returns>
        public string GetFileExtension()
        {
            Console.WriteLine("Please select the file type or specify the extension:\n1. Excel (.xlsx)\n2. Markdown (.md)\n3. CSV (.csv)\nPress Enter to select Excel (.xlsx) by default.");
            string input = Console.ReadLine() ?? string.Empty;

            return input switch
            {
                "1" => ".xlsx",
                "2" => ".md",
                "3" => ".csv",
                "" => DefaultFileExtension, // Default to Excel if Enter is pressed
                _ => input // Assume the user specified the extension directly
            };
        }

        /// <summary>
        /// Displays the available file options to the user.
        /// </summary>
        /// <param name="files">The array of file paths to display.</param>
        /// <param name="fileExtension">The file extension to filter files.</param>
        private void DisplayFileOptions(string[] files, string fileExtension)
        {
            Console.WriteLine($"Please select a file with extension {fileExtension}:");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }
        }

        /// <summary>
        /// Gets the file path based on the user's selected option.
        /// </summary>
        /// <param name="files">The array of file paths to choose from.</param>
        /// <param name="fileOption">The user's selected option.</param>
        /// <returns>The selected file path.</returns>
        private string GetFilePathFromOption(string[] files, int fileOption)
        {
            if (fileOption == files.Length + 1)
            {
                Console.Write("Enter the file path: ");
                return Console.ReadLine() ?? string.Empty;
            }
            else if (fileOption < 1 || fileOption > files.Length)
            {
                Console.WriteLine("Invalid option. Please try again.");
                return string.Empty;
            }
            else
            {
                return files[fileOption - 1];
            }
        }

        /// <summary>
        /// Prompts the user to enter an option number and validates the input.
        /// </summary>
        /// <returns>The validated option number.</returns>
        private int GetOptionFromUser()
        {
            Console.Write("Enter the option number: ");
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int option))
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