using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UsersPanel.UserLogic;

namespace UsersPanel
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Write write = new Write();
        Read read = new Read();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Create_account_button(object sender, RoutedEventArgs e)
        {
            if (onUsernameBox.Text == "" || onPasswordBox.Password == "" ||
                onRePasswordBox.Password == "" || emailBox.Text == "")
                MessageBox.Show("Enter all data.", "Warning", MessageBoxButton.OK);
            else if (onPasswordBox.Password != onRePasswordBox.Password)
                MessageBox.Show("Passwords should be the same.", "Warning", MessageBoxButton.OK);
            else
            {
                if (!read.ThatUserExist(onUsernameBox.Text, emailBox.Text))
                {
                    write.AddUser(onUsernameBox.Text, onPasswordBox.Password, emailBox.Text);
                    MessageBox.Show("Account was created successfully.", "Nice work", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("That eamil or username has already been used.", "Warning");

                onUsernameBox.Clear();
                onPasswordBox.Clear();
                onRePasswordBox.Clear();
                emailBox.Clear();
            }       
        }

    }
}
