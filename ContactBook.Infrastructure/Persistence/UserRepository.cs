using ContactBook.Domain.Entities;
using ContactBook.Application.Common.Interfaces.Persistence;
using ContactBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ContactBookDbContext _context;

    public UserRepository(ContactBookDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}