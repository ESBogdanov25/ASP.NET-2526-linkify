using Linkify.Application.DTOs;
using Linkify.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Linkify.Domain.Interfaces;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands;

public class UploadPostImageCommand : IRequest<FileUploadResponseDto>
{
    public IFormFile File { get; set; } = null!;
    public int PostId { get; set; }
    public int UserId { get; set; }
}

public class UploadPostImageCommandHandler : IRequestHandler<UploadPostImageCommand, FileUploadResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public UploadPostImageCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<FileUploadResponseDto> Handle(UploadPostImageCommand request, CancellationToken cancellationToken)
    {
        var post = await _unitOfWork.Posts.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new Exception("Post not found.");
        }

        // Verify post ownership
        if (post.UserId != request.UserId)
        {
            throw new Exception("You can only upload images to your own posts.");
        }

        // Delete old post image if exists
        if (!string.IsNullOrEmpty(post.ImageUrl))
        {
            await _fileStorageService.DeleteImageAsync(post.ImageUrl, "post-images");
        }

        // Upload new post image
        using var stream = request.File.OpenReadStream();
        var fileUrl = await _fileStorageService.UploadImageAsync(
            stream,
            request.File.FileName,
            "post-images"
        );

        // Update post image
        post.ImageUrl = fileUrl;
        _unitOfWork.Posts.Update(post);
        await _unitOfWork.SaveChangesAsync();

        return new FileUploadResponseDto
        {
            FileUrl = fileUrl,
            Message = "Post image uploaded successfully."
        };
    }
}