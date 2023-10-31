using UsersApiTest.Models;

namespace UsersApiTest.Repository.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync(List<int> ids = null, List<string> names = null, List<string> phones = null, List<string> emails = null);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User update);
    Task<bool> DeleteUserAsync(int id);
}