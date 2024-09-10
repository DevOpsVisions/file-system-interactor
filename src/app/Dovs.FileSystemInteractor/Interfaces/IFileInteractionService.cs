namespace Dovs.FileSystemInteractor.Interfaces
{
    public interface IFileInteractionService
    {
        string SelectFilePath(IFilePathService filePathService, int levelsToTraverse, string fileExtension);
    }
}