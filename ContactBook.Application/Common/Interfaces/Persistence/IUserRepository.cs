using System.Runtime.CompilerServices;
using ContactBook.Domain.Entities;

namespace ContactBook.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);

}
