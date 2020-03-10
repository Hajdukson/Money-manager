using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel
{
    class Write
    {
        IList<User> _users = new List<User>();
        private string _filename;
        public Write(string databaseName)
        {
            _filename = databaseName;
        }
        public void AddUser(string username, string password, string email)
        {
            int id = GetNextId();

            User user = new User(id, username, password, email);

            _users.Add(user);

            if (File.Exists(_filename))
                File.AppendAllText(_filename, ItemToTxt(user));
            else if (!File.Exists(_filename))
                File.WriteAllText(_filename, ItemToTxt(user));
            else
                MessageBox.Show("File does not exist.", "Warning");
        }
        private int GetNextId()
        {
            Read readFile = new Read(_filename);
            IEnumerable<User> users = readFile.ReadAll();

            if (users.Count() == 0)
                return 1;

            int lastIndex = users.Count() - 1;

            return users.ElementAt(lastIndex).Id + 1;
        }
        private string ItemToTxt(User user)
        {
            string line = string.Format("{0};{1};{2};{3}",
                user.Id,
                user.Username,
                user.Password,
                user.Email);

            return line + Environment.NewLine;
        }
    }
}
