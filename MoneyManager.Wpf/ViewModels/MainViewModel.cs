using MoneyManager.Wpf.State.Navigators;

namespace MoneyManager.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
