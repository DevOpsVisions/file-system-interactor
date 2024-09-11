# File System Interactor

## Purpose
The File System Interactor is a library designed to facilitate various file system operations, such as reading data from Excel files, interacting with file paths, and handling configuration settings. It provides a set of services and interfaces to streamline these tasks, making it easier for developers to work with file systems in their applications.

## Features
- **Configuration Service**: Retrieve configuration values and column names from configuration settings.
- **Excel Reader Service**: Read data from Excel files based on specified column names.
- **File Interaction Service**: Select file paths and handle user input for file selection.
- **File Path Service**: Get base paths, file paths, and files with specific extensions.

## Usage
### Example 1: Reading Data from an Excel File
```csharp
using Dovs.FileSystemInteractor.Interfaces;
using Dovs.FileSystemInteractor.Services;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        IExcelReaderService excelReaderService = new ExcelReaderService();
        List<string> columnNames = new List<string> { "Name", "Age", "Email" };
        string filePath = "path/to/excel/file.xlsx";

        try
        {
            List<Dictionary<string, string>> data = excelReaderService.ReadData(filePath, columnNames);
            foreach (var row in data)
            {
                Console.WriteLine($"Name: {row["Name"]}, Age: {row["Age"]}, Email: {row["Email"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### Example 2: Getting Configuration Values
```csharp
using Dovs.FileSystemInteractor.Interfaces;
using Dovs.FileSystemInteractor.Services;
using System;

class Program
{
    static void Main()
    {
        IConfigurationService configService = new ConfigurationService();
        string configValue = configService.GetConfigValue("SomeKey");
        Console.WriteLine($"Config Value: {configValue}");
    }
}
```

## Classes, Interfaces, and Services
### Interfaces
- **IConfigurationService**: Interface for configuration service.
  - `string GetConfigValue(string key)`: Gets the configuration value for the specified key.
  - `List<string> GetColumnNames(string key)`: Gets the column names from the configuration.

- **IExcelReaderService**: Interface for Excel reader service.
  - `List<Dictionary<string, string>> ReadData(string filePath, List<string> columnNames)`: Reads data from the specified Excel file.

- **IFileInteractionService**: Interface for file interaction service.
  - `string SelectFilePath(IFilePathService filePathService, int levelsToTraverse, string fileExtension)`: Selects a file path based on user input.

- **IFilePathService**: Interface for file path service.
  - `string GetBasePath(int levelsToTraverse)`: Gets the base path by traversing the specified number of levels.
  - `string GetFilePath(string defaultFilePath)`: Gets the file path based on user input.
  - `string[] GetFiles(string basePath, string fileExtension)`: Gets an array of file paths with the specified extension from the base path.

### Services
- **ConfigurationService**: Service for handling configuration operations.
  - Implements `IConfigurationService`.

- **ExcelReaderService**: Service for reading data from Excel files.
  - Implements `IExcelReaderService`.

- **FileInteractionService**: Service for handling file interactions.
  - Implements `IFileInteractionService`.

- **FilePathService**: Service for handling file path operations.
  - Implements `IFilePathService`.
