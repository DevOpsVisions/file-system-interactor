
using Dovs.FileSystemInteractor.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace Dovs.FileSystemInteractor.Services
{
    /// <summary>  
    /// Service for reading user data from an Excel file.  
    /// </summary>  
    public class ExcelReaderService : IExcelReaderService
    {
        /// <summary>  
        /// Reads user data from the specified Excel file.  
        /// </summary>  
        /// <param name="filePath">The path to the Excel file.</param>  
        /// <returns>A list of user data read from the Excel file.</returns>  
        /// <exception cref="Exception">Thrown when required columns are not found or no user data is found.</exception>  
        public List<Dictionary<string, string>> ReadData(string filePath, List<string> columnNames)
        {
            var dataList = new List<Dictionary<string, string>>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var header = new Dictionary<string, int>();
                bool isHeaderProcessed = false;

                while (reader.Read())
                {
                    if (!isHeaderProcessed)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetValue(i)?.ToString();
                            if (!string.IsNullOrEmpty(columnName))
                            {
                                header[columnName] = i;
                            }
                        }

                        foreach (var columnName in columnNames)
                        {
                            if (!header.ContainsKey(columnName))
                            {
                                throw new Exception($"Required column '{columnName}' not found in Excel file.");
                            }
                        }

                        isHeaderProcessed = true;
                        continue;
                    }

                    var dataRow = new Dictionary<string, string>();
                    foreach (var columnName in columnNames)
                    {
                        dataRow[columnName] = reader.GetValue(header[columnName])?.ToString();
                    }

                    if (dataRow.Values.All(value => !string.IsNullOrEmpty(value)))
                    {
                        dataList.Add(dataRow);
                    }
                }
            }

            if (dataList.Count == 0)
            {
                throw new Exception("No data found in Excel.");
            }

            return dataList;
        }

    }
}