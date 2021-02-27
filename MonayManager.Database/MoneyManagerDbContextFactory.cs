using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoneyManager.Database
{
    public class MoneyManagerDbContextFactory : IDesignTimeDbContextFactory<MoneyManagerDbContext>
    {
        public MoneyManagerDbContext CreateDbContext(string[] args = null)
        {
            var optionBuilder = new DbContextOptionsBuilder<MoneyManagerDbContext>();
            optionBuilder.UseSqlServer("Server=.;Database=MoneyManagerDb;Trusted_Connection=True;");

            return new MoneyManagerDbContext(optionBuilder.Options);
        }

    }
}
