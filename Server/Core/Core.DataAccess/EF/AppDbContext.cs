using Core.DataAccess.Records.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; }
        public DbSet<RecordFile> Files { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
