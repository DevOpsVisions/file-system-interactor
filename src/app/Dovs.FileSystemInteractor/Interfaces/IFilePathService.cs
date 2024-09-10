namespace Dovs.FileSystemInteractor.Interfaces
{
    /// <summary>
    /// Interface for file path service.
    /// </summary>
    public interface IFilePathService
    {
        string GetBasePath(int levelsToTraverse);
        string GetFilePath(string defaultFilePath);
        /// <summary>
        /// Gets an array of file paths with the specified extension from the base path.
        /// </summary>
        /// <param name="basePath">The base path to search for files.</param>
        /// <param name="fileExtension">The file extension to search for.</param>
        /// <returns>An array of file paths.</returns>
        string[] GetFiles(string basePath, string fileExtension);
    }
}
