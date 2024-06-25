using ContactBook.Domain.Entities;
using ContactBook.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence.Repositories;

/// <summary>
/// User repository
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ContactBookDbContext _context;

    public UserRepository(ContactBookDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Add a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}