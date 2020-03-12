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
        Rd rd = new Rd();
        Read read = new Read();
        ObservableCollection<User> currentUser = new ObservableCollection<User>();
        private ItemType ItemType;
        private string DirToUser;
        public UserWindow(string username)
        {
            InitializeComponent();
            IEnumerable<User> users = read.ReadAll();
            
            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    username = user.Username + ".txt";
                    currentUser.Add(new User(user.Id, user.Username, user.Password, user.Email));
                }
            }
            DirToUser = Directory.GetCurrentDirectory() + @"\Users\" + username;
            rd.ReadItems(DirToUser);
            myDataGrid.ItemsSource = currentUser;
        }
        private void AddIncome(object sender, RoutedEventArgs e)
        {
            ItemType = ItemType.Income;
            rd.IfUserExists(incomeAmount.Text, incomeDate.Text, DirToUser, ItemType);
            incomeAmount.Clear();
            incomeDate.Clear();
        }
        private void AddOutcome(object sender, RoutedEventArgs e)
        {
            ItemType = ItemType.Outcome;
            rd.IfUserExists(outcomeAmount.Text, outcomeDate.Text, DirToUser, ItemType);
            outcomeAmount.Clear();
            outcomeDate.Clear();
        }
    }
}