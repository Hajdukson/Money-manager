using MoneyManager.Database;
using MoneyManager.Database.Services;
using MoneyManager.Domain.Models;
using MoneyManager.Domain.Services;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> dataService = new GenericDataService<User>(new MoneyManagerDbContextFactory());
            var user = dataService.Create(new User { Username="michal"}).Result;

            Console.WriteLine(user.Username);
        }
    }
}
