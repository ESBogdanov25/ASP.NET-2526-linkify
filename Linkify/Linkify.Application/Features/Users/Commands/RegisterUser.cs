using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Application.Interfaces;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Users.Commands;

public class RegisterUserCommand : IRequest<AuthResponseDto>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (await _unitOfWork.Users.ExistsAsync(request.Email, request.Username))
        {
            throw new Exception("User with this email or username already exists.");
        }

        // Create new user with hashed password
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password)
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Generate token
        var token = _jwtService.GenerateToken(user);
        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            Token = token,
            User = userDto
        };
    }
}