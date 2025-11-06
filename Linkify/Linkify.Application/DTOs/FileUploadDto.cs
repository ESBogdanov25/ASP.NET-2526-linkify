using Microsoft.AspNetCore.Http;

namespace Linkify.Application.DTOs;

public class UploadProfilePictureDto
{
    public IFormFile File { get; set; } = null!;
    public int UserId { get; set; }
}

public class UploadPostImageDto
{
    public IFormFile File { get; set; } = null!;
    public int PostId { get; set; }
    public int UserId { get; set; }
}

public class FileUploadResponseDto
{
    public string FileUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}