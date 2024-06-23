using ContactBook.Application.Common.Interfaces.Authentication;
using ContactBook.Application.Common.Interfaces.Persistence;
using ContactBook.Domain.Entities;
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
        // Check if user exists
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
        {
            throw new Exception("User with this email does not exist");
        }

        // Check if password is correct
        if (user.Password != request.Password)
        {
            throw new Exception("Password is incorrect");
        }

        // Create token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, request.Username, request.Email);

        return new UserResponseDto
        {
            Id = user.Id,
            Username = request.Username,
            Email = request.Email,
            Token = token
        };
    }

    public async Task<UserResponseDto?> RegisterAsync(UserRegisterDto request)
    {
        // Check if user exists
        if (await _userRepository.GetUserByEmailAsync(request.Email) is not null)
        {
            throw new Exception("User with this email already exists");
        }

        // Create user
        var user = new Domain.Entities.User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        }; 

        // Check if password is correct
        if (request.Password != request.ConfirmPassword)
        {
            throw new Exception("Passwords do not match");
        }

        // Add user to database
        await _userRepository.AddUserAsync(user);

        // Create token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);

        return new UserResponseDto
        {
            Id = user.Id,
            Username = request.Username,
            Email = request.Email,
            Token = token
        };
    }
}