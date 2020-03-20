using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        ItemWriter _writeItem;
        ItemReader _itemReader;
        ObservableCollection<Item> _userItemsMonth;
        ObservableCollection<Item> _userItemWhole;
        IEnumerable<Item> _items;
        private ItemType _itemType;
        Services _services = new Services();

        UserReader _readUser = new UserReader();

        private string _dirToUser;
        private int _userId;
        public UserWindow(string username)
        {
            InitializeComponent();
            IEnumerable<User> users = _readUser.ReadAll();

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
            _writeItem = new ItemWriter(_dirToUser);
            _itemReader = new ItemReader(_dirToUser);
        }
        
        private void AddIncome(object sender, RoutedEventArgs e)
        {
            _itemType = ItemType.Income;
            _writeItem.AddItem(incomeAmount.Text, incomeDate.SelectedDate, _itemType, notesIncome.Text);
            incomeAmount.Clear();
            notesIncome.Clear();
        }
        private void AddOutcome(object sender, RoutedEventArgs e)
        {
            _itemType = ItemType.Outcome;
            _writeItem.AddItem(outcomeAmount.Text, outcomeDate.SelectedDate, _itemType, notesOutcome.Text);
            outcomeAmount.Clear();
            notesOutcome.Clear();
        }
        private void ShowMothReport(object sender, RoutedEventArgs e)
        {
            _items = _itemReader.ReadItems();
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
            _items = _itemReader.ReadItems();
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
                _readUser.Remove(_userId);
                File.Delete(_dirToUser);
                Close();
                MessageBox.Show("Your account has been deleted successfully", "Statment");
            }  
        }
    }
}