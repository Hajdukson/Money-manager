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
        private ItemType _ItemType;
        private string _DirToUser;
        public UserWindow()
        { }
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
            _userItemsMonth = ShowMothReport();
            monthReportTable.ItemsSource = _userItemsMonth;   
        }
        private void HideMonthReport(object sender, RoutedEventArgs e)
        {
            _userItemsMonth = null;
            monthReportTable.ItemsSource = null;
        }

        private void ShowLifetimeReport(object sender, RoutedEventArgs e)
        {
            _userItemWhole = ShowLifetiemReport();
            lifetimeReportTable.ItemsSource = _userItemWhole;
            
        }
        private void HideLifetimeReport(object sender, RoutedEventArgs e)
        {
            _userItemWhole = null;
            lifetimeReportTable.ItemsSource = null;
        }

        private ObservableCollection<Item> ShowLifetiemReport()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            IEnumerable<Item> lines = _rd.ReadItems(_DirToUser);

            foreach (Item item in lines)
                items.Add(item);

            return items;
        }
        private ObservableCollection<Item> ShowMothReport()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            IEnumerable<Item> lines = _rd.ReadItems(_DirToUser);

            foreach (Item item in lines)
                if (item.Date.Year == DateTime.Now.Year && item.Date.Month == DateTime.Now.Month)
                    items.Add(item);

            return items;
        }
    }
}