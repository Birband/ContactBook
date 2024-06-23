using ContactBook.Application.Common.Interfaces.Authentication;
using ContactBook.Application.Common.Interfaces.Persistence;
using ContactBook.Application.DTOs;

namespace ContactBook.Application.Services.User;

public class UserService : IUserService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public UserService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto?> LoginAsync(UserLoginDto request)
    {   

        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, request.Username, request.Email);

        return new UserResponseDto
        {
            Id = userId,
            Username = request.Username,
            Email = request.Email,
            Token = token
        };
    }

    public async Task<UserResponseDto?> RegisterAsync(UserRegisterDto request)
    {
        return new UserResponseDto
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            Token = "token"
        };
    }
}