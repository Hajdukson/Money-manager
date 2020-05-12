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
        UserWriter write = new UserWriter(); 
        UserReader read = new UserReader();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login_Button(object sender, RoutedEventArgs e)
        {
            if (read.UserLogIn(UsernameBox.Text, PasswordBox.Password))
            {
                UserWindow userWindow = new UserWindow(UsernameBox.Text);
                userWindow.ShowDialog();
                UsernameBox.Text = "Username";
                PasswordBox.Password = "Account";
                accountCreated.Visibility = Visibility.Collapsed;
                checkPasswords.Visibility = Visibility.Collapsed;
                checkUsernameAndPasswordNotification.Visibility = Visibility.Collapsed;
            }
            else
            {
                accountCreated.Visibility = Visibility.Collapsed;
                checkPasswords.Visibility = Visibility.Collapsed;
                checkUsernameAndPasswordNotification.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Shutdown_Window(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Minimized_Window(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Minimized;
            else
                WindowState = WindowState.Normal;
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                    string.IsNullOrWhiteSpace(PasswordBox2.Password) || PasswordBox2.Password == "Account" ||
                    PasswordBox.Password == "Account" || UsernameBox.Text == "Username")
                MessageBox.Show("Enter all data.", "Warning", MessageBoxButton.OK);
            else if (PasswordBox.Password != PasswordBox2.Password)
                checkPasswords.Visibility = Visibility.Visible;
            else
            {
                if (!read.ThatUserExist(UsernameBox.Text))
                {
                    write.AddUser(UsernameBox.Text, PasswordBox.Password);
                    checkUsernameAndPasswordNotification.Visibility = Visibility.Collapsed;
                    checkPasswords.Visibility = Visibility.Collapsed;
                    accountCreated.Visibility = Visibility.Visible;
                }
                else
                    MessageBox.Show("That user exist.", "Warning", MessageBoxButton.OK);
                checkUsernameAndPasswordNotification.Visibility = Visibility.Collapsed;
                UsernameBox.Text = "Username";
                PasswordBox.Password = "Account";
                PasswordBox2.Password = PasswordBox.Password;

            }
        }
    }
}
