using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.UserLogic
{
    class Write
    {
        IList<User> _users = new List<User>();
        public void AddUser(string username, string password, string email)
        {
            int id = GetNextId();

            User user = new User(id, username, password, email);

            _users.Add(user);

            if (File.Exists(FileName._filename))
                File.AppendAllText(FileName._filename, ItemToTxt(user));
            else
                File.WriteAllText(FileName._filename, ItemToTxt(user));
        }
        private int GetNextId()
        {
            Read readFile = new Read();
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