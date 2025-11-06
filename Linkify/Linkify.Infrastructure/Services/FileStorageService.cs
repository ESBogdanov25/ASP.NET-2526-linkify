using Linkify.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Linkify.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IConfiguration _configuration;
    private readonly string _uploadsPath;

    public FileStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        // Ensure uploads directories exist
        Directory.CreateDirectory(Path.Combine(_uploadsPath, "profile-pictures"));
        Directory.CreateDirectory(Path.Combine(_uploadsPath, "post-images"));
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string containerName)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var uploadsFolder = Path.Combine(_uploadsPath, containerName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using var file = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(file);

        return $"/uploads/{containerName}/{uniqueFileName}";
    }

    public Task DeleteImageAsync(string fileUrl, string containerName)
    {
        if (string.IsNullOrEmpty(fileUrl)) return Task.CompletedTask;

        var fileName = Path.GetFileName(fileUrl);
        var filePath = Path.Combine(_uploadsPath, containerName, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}