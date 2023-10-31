using Microsoft.EntityFrameworkCore;
using UsersApiTest.DataBases;
using UsersApiTest.Models;
using UsersApiTest.Repository.Interfaces;

namespace UsersApiTest.Repository;

public class UserRepository : IUserRepository
{
    private readonly UsersContext _context;

    public UserRepository(UsersContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersAsync(List<int> ids = null, List<string> names = null,
        List<string> phones = null,
        List<string> emails = null)
    {
        var usersQueryable = _context.Users.AsQueryable();

        if (ids != null && ids.Any())
        {
            usersQueryable = usersQueryable.Where(x => ids.Contains(x.Id));
        }

        if (names != null && names.Any())
        {
            usersQueryable = usersQueryable.Where(x => names.Contains(x.Name));
        }

        if (phones != null && phones.Any())
        {
            usersQueryable = usersQueryable.Where(x => phones.Contains(x.Phone));
        }

        if (emails != null && emails.Any())
        {
            usersQueryable = usersQueryable.Where(x => emails.Contains(x.Email));
        }

        return await usersQueryable.ToListAsync();
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        if (user == null) return new User();
        
        user.Id = 0;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User update)
    {
        if (update == null) return new User();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == update.Id);
        if (user == null) return new User();

        user.Name = string.IsNullOrWhiteSpace(update.Name) ? user.Name : update.Name;
        user.Phone = string.IsNullOrWhiteSpace(update.Phone) ? user.Phone : update.Phone;
        user.Email = string.IsNullOrWhiteSpace(update.Email) ? user.Email : update.Email;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var deletedCount = await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();

        return deletedCount == 1;
    }
}