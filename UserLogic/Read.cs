using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.UserLogic
{
    class Read
    {
        public IEnumerable<User> ReadAll()
        {
            IList<User> users = new List<User>();

            if (!File.Exists(FileName._filename))
                return users;

            IEnumerable<String> lines = File.ReadAllLines(FileName._filename);

            foreach(string line in lines)
            {
                User user = TextToUser(line);
                users.Add(user);
            }

            return users;
        }
        private User TextToUser(string line)
        {
            string[] parameters = line.Split(';');

            int id = Convert.ToInt32(parameters[0]);
            string username = parameters[1];
            string password = parameters[2];
            string email = parameters[3];

            User user = new User(id, username, password, email);

            return user;
        }
        public bool ThatUserExist(string username, string email)
        {
            IEnumerable<User> users = ReadAll();
            foreach (User user in users)
            {
                if (user.Username == username || user.Email == email)
                    return true;
            }
            return false;
        }
        public bool UserLogIn(string username, string password)
        {
            IEnumerable<User> users = ReadAll();
            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                    return true;
            }
            return false;
        }
    }
}