using ContactBook.Application.DTOs;

namespace ContactBook.Application.Services.User;

public interface IUserService
{
    Task<UserResponseDto?> LoginAsync(UserLoginDto request);
    Task<UserResponseDto?> RegisterAsync(UserRegisterDto request);
}