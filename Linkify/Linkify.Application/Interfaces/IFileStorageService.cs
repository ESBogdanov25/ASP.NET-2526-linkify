namespace Linkify.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName, string containerName);
    Task DeleteImageAsync(string fileUrl, string containerName);
}