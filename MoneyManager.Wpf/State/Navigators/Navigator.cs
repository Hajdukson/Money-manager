using MoneyManager.Wpf.Commands;
using MoneyManager.Wpf.Models;
using MoneyManager.Wpf.ViewModels;
using System.Windows.Input;

namespace MoneyManager.Wpf.State.Navigators
{
    public class Navigator : ObservableObject ,INavigator
    {
        private BaseViewModel mCurrentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return mCurrentViewModel; }
            set 
            {
                mCurrentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
