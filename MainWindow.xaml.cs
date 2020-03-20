using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UsersPanel.UserLogic;

namespace UsersPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserReader read = new UserReader();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LogInButton(object sender, RoutedEventArgs e)
        {
            if (inUsernameBox.Text == "" || inPasswordBox.Password == "")
                MessageBox.Show("Enter all data.", "Warning", MessageBoxButton.OK);
            else if (read.UserLogIn(inUsernameBox.Text, inPasswordBox.Password))
            {
                UserWindow userWindow = new UserWindow(inUsernameBox.Text);
                userWindow.ShowDialog();
                inUsernameBox.Clear();
                inPasswordBox.Clear();
            }
            else
                MessageBox.Show("Check your username and password.", "Warning", MessageBoxButton.OK);
        }
    }
}
