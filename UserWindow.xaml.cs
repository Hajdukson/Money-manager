using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UsersPanel.ItemLogic;
using UsersPanel.UserLogic;

namespace UsersPanel
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        Rd _rd;
        Read _read = new Read();
        ObservableCollection<Item> _userItemsMonth = new ObservableCollection<Item>();
        ObservableCollection<Item> _userItemWhole= new ObservableCollection<Item>();
        Services _services = new Services();
        IEnumerable<Item> _items;
        private ItemType _itemType;
        private string _dirToUser;
        int _userId;
        public UserWindow(string username)
        {
            InitializeComponent();
            IEnumerable<User> users = _read.ReadAll();

            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    username = user.Username + ".txt";
                    _userId = user.Id;
                    name.Text = user.Username;
                    id.Text = Convert.ToString(user.Id);
                    email.Text = user.Email;
                }
            }
            _dirToUser = Directory.GetCurrentDirectory() + @"\Users\" + username;
            _rd = new Rd(_dirToUser);
        }
        private void AddIncome(object sender, RoutedEventArgs e)
        {
            _itemType = ItemType.Income;
            _rd.IfUserExists(incomeAmount.Text, incomeDate.SelectedDate, _itemType);
            incomeAmount.Clear();
        }
        private void AddOutcome(object sender, RoutedEventArgs e)
        {
            _itemType = ItemType.Outcome;
            _rd.IfUserExists(outcomeAmount.Text, outcomeDate.SelectedDate, _itemType);
            outcomeAmount.Clear();
        }
        private void ShowMothReport(object sender, RoutedEventArgs e)
        {
            _items = _rd.ReadItems();
            _userItemsMonth = _services.ShowMothReport(_items);
            balanceMonth.Content = "Month Balance = " + _services.DispalyBalanceMonth(_items) + Environment.NewLine;
            monthReportTable.ItemsSource = _userItemsMonth;   
        }
        private void HideMonthReport(object sender, RoutedEventArgs e)
        {
            _userItemsMonth = null;
            monthReportTable.ItemsSource = null;
        }

        private void ShowLifetimeReport(object sender, RoutedEventArgs e)
        {
            _items = _rd.ReadItems();
            _userItemWhole = _services.ShowLifetiemReport(_items);
            balanceLifetime.Content = "Lifetime Balance = " +  _services.DispalyBalanceLifetime(_items) + Environment.NewLine;
            lifetimeReportTable.ItemsSource = _userItemWhole;
            
        }
        private void HideLifetimeReport(object sender, RoutedEventArgs e)
        {
            _userItemWhole = null;
            lifetimeReportTable.ItemsSource = null;
        }

        private void DeleateAccount(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resoult = MessageBox.Show("Are you sure?", "Statment", MessageBoxButton.YesNo);
            
            if (resoult == MessageBoxResult.Yes)
            {
                _read.Remove(_userId);
                File.Delete(_dirToUser);
                Close();
                MessageBox.Show("Your account has been deleted successfully", "Statment");
            }  
        }

    }
}