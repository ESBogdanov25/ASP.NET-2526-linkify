using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Application.Interfaces;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Users.Commands;

public class LoginUserCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

        if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new Exception("Invalid email or password.");
        }

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