using MoneyManager.Wpf.State.Navigators;
using MoneyManager.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MoneyManager.Wpf.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public INavigator mNavigator;
        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            mNavigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.AddingItem:
                        mNavigator.CurrentViewModel = new AddingItemViewModel();
                        break;
                    case ViewType.Summary:
                        mNavigator.CurrentViewModel = new SummaryViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}