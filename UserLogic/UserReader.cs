using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.UserLogic
{
    class UserReader
    {
        public IEnumerable<User> ReadAll()
        {
            IList<User> users = new List<User>();

            if (!File.Exists(DbName._filename))
                return users;

            IEnumerable<String> lines = File.ReadAllLines(DbName._filename);

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

            User user = new User(id, username, password);

            return user;
        }
        public bool ThatUserExist(string username)
        {
            IEnumerable<User> users = ReadAll();
            foreach (User user in users)
            {
                if (user.Username == username)
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
        public void Remove(int id)
        {
            IEnumerable<string> lines = File.ReadAllLines(DbName._filename);
            IList<string> toSave = new List<string>();

            foreach (string line in lines)
                if (!HasId(id, line))
                    toSave.Add(line);

            File.WriteAllLines(DbName._filename, toSave);
        }
        private bool HasId(int id, string line)
        {
            string[] columns = line.Split(';');

            int lineId = int.Parse(columns[0]);

            return id == lineId;
        }
    }
}