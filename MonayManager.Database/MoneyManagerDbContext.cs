using Microsoft.EntityFrameworkCore;
using MoneyManager.Domain.Models;

namespace MoneyManager.Database
{
    public class MoneyManagerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MoneyManagerDb;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
