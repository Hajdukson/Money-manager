using MoneyManager.Database;
using MoneyManager.Database.Services;
using MoneyManager.Domain.Models;
using MoneyManager.Domain.Services;
using System;
using System.Windows.Input;

namespace MoneyManager.Wpf.Commands
{
    public class AddItemCommand : ICommand
    {
        private readonly Item mItem;

        public AddItemCommand(Item item)
        {
            mItem = item;
        }

        public event EventHandler CanExecuteChanged;

        public object IDataService { get; private set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            IDataService<Item> dataService = new GenericDataService<Item>(new MoneyManagerDbContextFactory());

            var item = dataService.Create(mItem).Result;
        }
    }
}
