using Microsoft.EntityFrameworkCore;
using MoneyManager.Domain.Models;
using System;

namespace MoneyManager.Database
{
    public class MoneyManagerDbContext : DbContext
    {
       
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public MoneyManagerDbContext(DbContextOptions options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=MoneyManagerDb;Trusted_Connection=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
