using MoneyManager.Domain.Models;
using MoneyManager.Wpf.Commands;
using System;
using System.Windows.Input;

namespace MoneyManager.Wpf.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        public ICommand AddItemCommand => new AddItemCommand(new Item());
    }
}
