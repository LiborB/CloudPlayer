using System;
using CloudPlayerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudPlayerAPI.Data
{
    public partial class CloudPlayerContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Song> Song { get; set; }

        public CloudPlayerContext(DbContextOptions<CloudPlayerContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
