using Linkify.Application.DTOs;
using Linkify.Application.Interfaces;
using Linkify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace Linkify.Application.Features.Users.Commands;

public class UploadProfilePictureCommand : IRequest<FileUploadResponseDto>
{
    public IFormFile File { get; set; } = null!;
    public int UserId { get; set; }
}

public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand, FileUploadResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public UploadProfilePictureCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<FileUploadResponseDto> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Delete old profile picture if exists
        if (!string.IsNullOrEmpty(user.ProfilePicture))
        {
            await _fileStorageService.DeleteImageAsync(user.ProfilePicture, "profile-pictures");
        }

        // Upload new profile picture
        using var stream = request.File.OpenReadStream();
        var fileUrl = await _fileStorageService.UploadImageAsync(
            stream,
            request.File.FileName,
            "profile-pictures"
        );

        // Update user profile
        user.ProfilePicture = fileUrl;
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new FileUploadResponseDto
        {
            FileUrl = fileUrl,
            Message = "Profile picture uploaded successfully."
        };
    }
}