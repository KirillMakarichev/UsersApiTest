using Microsoft.EntityFrameworkCore;
using UsersApiTest.Models;

namespace UsersApiTest.DataBases;

public class UsersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public UsersContext(DbContextOptions options) : base(options)
    {
        
    }
}