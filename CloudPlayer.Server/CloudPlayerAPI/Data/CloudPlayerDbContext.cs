using CloudPlayerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudPlayerAPI.Data
{
    public class CloudPlayerDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=CloudPlayer;user=user;password=password");
        }
    }
}