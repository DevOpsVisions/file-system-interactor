using System.Collections.Generic;

namespace Dovs.FileSystemInteractor.Interfaces
{
    /// <summary>
    /// Interface for Excel reader service.
    /// </summary>
    public interface IExcelReaderService
    {
        /// <summary>
        /// Reads data from the specified Excel file.
        /// </summary>
        /// <param name="filePath">The path to the Excel file.</param>
        /// <param name="columnNames">The list of column names to read.</param>
        /// <returns>A list of data read from the Excel file.</returns>
        List<Dictionary<string, string>> ReadData(string filePath, List<string> columnNames);
    }
}
