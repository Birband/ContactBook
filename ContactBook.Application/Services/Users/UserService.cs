using ContactBook.Application.Common.Interfaces.Authentication;
using ContactBook.Application.Common.Interfaces.Persistence;
using ContactBook.Domain.Entities;
using ContactBook.Application.DTOs;
using ContactBook.Application.Common.Security;
using ContactBook.Application.Common.Validators;
using ContactBook.Application.Common.Exceptions;
using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Services.Users;

/// <summary>
/// User service
/// </summary>
public class UserService : IUserService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public UserService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<UserResponseDto?> LoginAsync(UserLoginDto request)
    {
        // Validate user input
        var emailValidCheck = EmailValidator.ValidateEmail(request.Email);
        if (!emailValidCheck.IsValid)
        {
            throw new ValidationException(emailValidCheck);
        }

        // Check if password is empty
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new Exception("Password cannot be empty");
        }

        // Check if user exists
        User user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
        {
            throw new Exception("User with this email does not exist");
        }

        // Check if password is correct
        if (!PasswordHash.VerifyPassword(user.Password, request.Password))
        {
            throw new Exception("Password or email is incorrect");
        }

        // Create token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);

        return new UserResponseDto
        {
            Email = user.Email,
            Token = token
        };
    }

    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<UserResponseDto?> RegisterAsync(UserRegisterDto request)
    {
        // Validate user input
        var emailValidCheck = EmailValidator.ValidateEmail(request.Email);
        if (!emailValidCheck.IsValid)
        {
            throw new ValidationException(emailValidCheck);
        }

        var passwordValidCheck = PasswordValidator.ValidatePassword(request.Password);

        if (!passwordValidCheck.IsValid)
        {
            throw new ValidationException(passwordValidCheck);
        }

        // Check if user exists
        if (await _userRepository.GetUserByEmailAsync(request.Email) is not null)
        {
            throw new Exception("User with this email already exists");
        }

        // Create user
        var user = new User
        {
            Email = request.Email,
            Password = request.Password
        };

        // Check if password is correct
        if (request.Password != request.ConfirmPassword)
        {
            throw new Exception("Passwords do not match");
        }

        // Hash password
        user.Password = PasswordHash.HashPassword(user.Password);

        // Add user to database
        await _userRepository.AddUserAsync(user);

        // Get user from database
        user = await _userRepository.GetUserByEmailAsync(request.Email);

        // Create token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);

        return new UserResponseDto
        {
            Email = request.Email,
            Token = token
        };
    }
}