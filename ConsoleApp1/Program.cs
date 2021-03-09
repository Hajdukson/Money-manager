using MoneyManager.Database;
using MoneyManager.Database.Services;
using MoneyManager.Domain.Models;
using MoneyManager.Domain.Services;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> dataService = new GenericDataService<User>(new MoneyManagerDbContextFactory());
            var user = dataService.Delete(1).Result;
        }
    }
}
