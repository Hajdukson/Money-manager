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

namespace UsersPanel
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        ObservableCollection<User> currentUser = new ObservableCollection<User>();
        Read read = new Read();
        private ItemType ItemType;
        private string Username;

        public UserWindow(string username)
        {
            InitializeComponent();
            IEnumerable<User> users = read.ReadAll();

            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    Username = user.Username + ".txt";
                    currentUser.Add(new User(user.Id, user.Username, user.Password, user.Email));
                }
            }

            myDataGrid.ItemsSource = currentUser;
        }
        private void AddIncome(object sender, RoutedEventArgs e)
        {
            ItemType = ItemType.Income;
            
            if (!File.Exists(Username))
                File.WriteAllText(Username, ItemToTxt(new Item(Convert.ToDecimal(incomeAmount.Text), Convert.ToDateTime(incomeDate.Text))));
            else
                File.AppendAllText(Username, ItemToTxt(new Item(Convert.ToDecimal(incomeAmount.Text), Convert.ToDateTime(incomeDate.Text))));
        }

        private void AddOutcome(object sender, RoutedEventArgs e)
        {
            ItemType = ItemType.Outcome;

            if (!File.Exists(Username))
                File.WriteAllText(Username, ItemToTxt(new Item(Convert.ToDecimal(outcomeAmount.Text), Convert.ToDateTime(outcomeDate.Text))));
            else
                File.AppendAllText(Username, ItemToTxt(new Item(Convert.ToDecimal(incomeAmount.Text), Convert.ToDateTime(outcomeDate.Text))));
        }
        private string ItemToTxt(Item item)
        {
            string type = "I";
            
            if (ItemType == ItemType.Outcome)
                type = "O";

            string line = string.Format("{0};{1};{2}",
                type,
                item.Date.ToString("dd-MM-yyyy"),
                item.Amount
                );

            return line + Environment.NewLine;
        }
    }
}