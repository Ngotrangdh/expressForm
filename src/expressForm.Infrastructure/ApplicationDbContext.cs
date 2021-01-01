using expressForm.Core.Models.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace expressForm.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .Property(f => f.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Form>()
                .Property(f => f.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Form>()
                .Property(f => f.Guid)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Response>()
                .Property(r => r.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
