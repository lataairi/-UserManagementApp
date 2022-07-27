using Microsoft.EntityFrameworkCore;
using UserManagementApp.Models;

namespace UserManagementApp.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
    }
}
