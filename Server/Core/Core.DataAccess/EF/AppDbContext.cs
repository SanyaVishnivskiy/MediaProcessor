using Core.DataAccess.Auth;
using Core.DataAccess.Auth.Roles;
using Core.DataAccess.Records.DB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Record> Records { get; set; }
        public DbSet<RecordFile> Files { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
