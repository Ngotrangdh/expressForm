using expressForm.Core.Forms;
using Microsoft.EntityFrameworkCore;

namespace expressForm.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=expressFormDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
