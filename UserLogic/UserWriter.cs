using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.UserLogic
{
    class UserWriter
    {   
        public void AddUser(string username, string password)
        {
            int id = GetNextId();

            User user = new User(id, username, password);

            if (File.Exists(DbName._filename))
                File.AppendAllText(DbName._filename, UserToTxt(user));
            else
            { 
                File.WriteAllText(DbName._filename, UserToTxt(user));
                Directory.CreateDirectory(Directory.GetCurrentDirectory()+@"\Users");
            }
        }
        private int GetNextId()
        {
            UserReader readFile = new UserReader();
            IEnumerable<User> users = readFile.ReadAll();

            if (users.Count() == 0)
                return 1;

            int lastIndex = users.Count() - 1;

            return users.ElementAt(lastIndex).Id + 1;
        }
        private string UserToTxt(User user)
        {
            string line = string.Format("{0};{1};{2}",
                user.Id,
                user.Username,
                user.Password);

            return line + Environment.NewLine;
        }
    }
}