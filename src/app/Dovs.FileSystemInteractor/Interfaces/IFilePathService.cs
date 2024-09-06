namespace Dovs.FileSystemInteractor.Interfaces
{
    /// <summary>
    /// Interface for file path service.
    /// </summary>
    public interface IFilePathService
    {
        /// <summary>
        /// Gets the base path by traversing a specified number of directory levels.
        /// </summary>
        /// <param name="levelsToTraverse">The number of directory levels to traverse.</param>
        /// <returns>The base path as a string.</returns>
        string GetBasePath(int levelsToTraverse);

        /// <summary>
        /// Gets the file path, using a default file path if necessary.
        /// </summary>
        /// <param name="defaultFilePath">The default file path to use if no other path is found.</param>
        /// <returns>The file path as a string.</returns>
        string GetFilePath(string defaultFilePath);

        /// <summary>
        /// Gets an array of Excel file paths from the specified base path.
        /// </summary>
        /// <param name="basePath">The base path to search for Excel files.</param>
        /// <returns>An array of Excel file paths.</returns>
        string[] GetExcelFiles(string basePath);
    }
}
