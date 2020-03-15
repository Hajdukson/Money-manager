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
        Rd _rd = new Rd();
        Read _read = new Read();
        ObservableCollection<User> _currentUser = new ObservableCollection<User>();
        ObservableCollection<Item> _userItemsMonth = new ObservableCollection<Item>();
        ObservableCollection<Item> _userItemWhole= new ObservableCollection<Item>();
        Services _services;
        IEnumerable<Item> _items;
        private ItemType _ItemType;
        private string _DirToUser;
        public UserWindow(string username)
        {
            InitializeComponent();
            IEnumerable<User> users = _read.ReadAll();

            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    username = user.Username + ".txt";
                    _currentUser.Add(new User(user.Id, user.Username, user.Password, user.Email));
                }
            }
            _DirToUser = Directory.GetCurrentDirectory() + @"\Users\" + username;
            _services = new Services();
            myDataGrid.ItemsSource = _currentUser;
        }
        private void AddIncome(object sender, RoutedEventArgs e)
        {
            
            _ItemType = ItemType.Income;
            _rd.IfUserExists(incomeAmount.Text, incomeDate.SelectedDate, _DirToUser, _ItemType);
            incomeAmount.Clear();
            //incomeDate.Clear();
        }
        private void AddOutcome(object sender, RoutedEventArgs e)
        {
            _ItemType = ItemType.Outcome;
            _rd.IfUserExists(outcomeAmount.Text, outcomeDate.SelectedDate, _DirToUser, _ItemType);
            outcomeAmount.Clear();
            //outcomeDate.Clear();
        }

        private void ShowMothReport(object sender, RoutedEventArgs e)
        {
            _items = _rd.ReadItems(_DirToUser);
            _userItemsMonth = _services.ShowMothReport(_items);
            balanceMonth.Text = "Balance = " + _services.DispalyBalanceMonth(_items) + Environment.NewLine;
            monthReportTable.ItemsSource = _userItemsMonth;   
        }
        private void HideMonthReport(object sender, RoutedEventArgs e)
        {
            _userItemsMonth = null;
            monthReportTable.ItemsSource = null;
        }

        private void ShowLifetimeReport(object sender, RoutedEventArgs e)
        {
            _items = _rd.ReadItems(_DirToUser);
            _userItemWhole = _services.ShowLifetiemReport(_items);
            balanceLifetime.Text = "Balance = " +  _services.DispalyBalanceLifetime(_items) + Environment.NewLine;
            lifetimeReportTable.ItemsSource = _userItemWhole;
            
        }
        private void HideLifetimeReport(object sender, RoutedEventArgs e)
        {
            _userItemWhole = null;
            lifetimeReportTable.ItemsSource = null;
        }
    }
}