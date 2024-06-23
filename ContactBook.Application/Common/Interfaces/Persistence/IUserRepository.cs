using ContactBook.Domain.Entities;

namespace ContactBook.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    void AddUser(User user);

}
