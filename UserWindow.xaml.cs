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
        ItemType _itemType;
        Services _services = new Services();

        UserReader _readUser = new UserReader();

        string _dirToUser;
        int _userId;
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
                }
            }

            _dirToUser = Directory.GetCurrentDirectory() + @"\Users\" + username;
            _writeItem = new ItemWriter(_dirToUser);
            _itemReader = new ItemReader(_dirToUser);
        }

        private void AddIncome(object sender, RoutedEventArgs e)
        {
            try
            {
                _itemType = ItemType.Income;
                _writeItem.AddItem(decimal.Round(decimal.Parse(incomeAmount.Text), 2),
                    incomeDate.SelectedDate, _itemType, notesIncome.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Enter date and amount.", "Warning", MessageBoxButton.OK);
            }
            
            incomeAmount.Clear();
            notesIncome.Clear();
        }
        private void AddOutcome(object sender, RoutedEventArgs e)
        {
            try
            {
                _itemType = ItemType.Outcome;
                _writeItem.AddItem(decimal.Round(decimal.Parse(outcomeAmount.Text), 2),
                    outcomeDate.SelectedDate, _itemType, notesOutcome.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Enter date and amount.", "Warning", MessageBoxButton.OK);
            }
            outcomeAmount.Clear();
            notesOutcome.Clear();
        }
        private void ShowMothReport(object sender, RoutedEventArgs e)
        {
            _items = _itemReader.ReadItems();
            _userItemsMonth = _services.ShowMothReport(_items);
            balanceMonth.Content = "Month Balance = " + _services.DispalyBalanceMonth(_items) + Environment.NewLine;
            monthReportTable.ItemsSource = _userItemsMonth;
            comboBoxColumnMonth.ItemsSource = Enum.GetValues(typeof(ItemType));
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
            balanceLifetime.Content = "Lifetime Balance = " + _services.DispalyBalanceLifetime(_items) + Environment.NewLine;
            lifetimeReportTable.ItemsSource = _userItemWhole;
            comboBoxColumnYear.ItemsSource = Enum.GetValues(typeof(ItemType));

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
        private void monthReportTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ItemWriter itemWriter = new ItemWriter(_dirToUser);
            File.WriteAllLines(_dirToUser, itemWriter.ChangeItemsData(_userItemsMonth));
        }

        private void lifetimeReportTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void Shutdown_Window(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Minimized_Window(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Minimized;
            else
                WindowState = WindowState.Normal;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // // // // // // // // // // // // // // // // // // // // // // // // Income watermarker
        private void WmIncomeAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            wahtermarkedTextInAmount.Visibility = Visibility.Collapsed;
            incomeAmount.Visibility = Visibility.Visible;
            incomeAmount.Focus();
        }

        private void IncomeAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(incomeAmount.Text))
            {
                incomeAmount.Visibility = Visibility.Collapsed;
                wahtermarkedTextInAmount.Visibility = Visibility.Visible;
            }
        }

        private void WmIncomeNotes_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkedTextInNotes.Visibility = Visibility.Collapsed;
            notesIncome.Visibility = Visibility.Visible;
            notesIncome.Focus();
        }

        private void IncomeNotes_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(notesIncome.Text))
            {
                notesIncome.Visibility = Visibility.Collapsed;
                watermarkedTextInNotes.Visibility = Visibility.Visible;
            }
        }
        // // // // // // // // // // // // // // // // // // // // // // // // Outcome watermarker
        private void WmOutcomeAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkedTextOuAmount.Visibility = Visibility.Collapsed;
            outcomeAmount.Visibility = Visibility.Visible;
            outcomeAmount.Focus();
        }
        private void OutcomeAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(outcomeAmount.Text))
            {
                outcomeAmount.Visibility = Visibility.Collapsed;
                watermarkedTextOuAmount.Visibility = Visibility.Visible;
            }
        }

        private void WmOutcomeNotes_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkedTextOuNotes.Visibility = Visibility.Collapsed;
            notesOutcome.Visibility = Visibility.Visible;
            notesOutcome.Focus();
        }

        private void OutcomeNotes_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(notesOutcome.Text))
            {
                notesOutcome.Visibility = Visibility.Collapsed;
                watermarkedTextOuNotes.Visibility = Visibility.Visible;
            }
        }

        private void EditItems_Click(object sender, RoutedEventArgs e)
        {
            monthReportTable.IsReadOnly = false;
            lifetimeReportTable.IsReadOnly = false;
        }
    }
}