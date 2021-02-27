using MoneyManager.Wpf.ViewModels;
using System.Windows.Input;

namespace MoneyManager.Wpf.State.Navigators
{
    public enum ViewType
    {
        AddingItem,
        Summary
    }
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; } 
    }
}
