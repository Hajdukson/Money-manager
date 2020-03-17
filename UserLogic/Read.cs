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
        public void Remove(int id)
        {
            IEnumerable<string> lines = File.ReadAllLines(FileName._filename);
            IList<string> toSave = new List<string>();

            foreach (string line in lines)
                if (!HasId(id, line))
                    toSave.Add(line);

            File.WriteAllLines(FileName._filename, toSave);
        }
        private bool HasId(int id, string line)
        {
            string[] columns = line.Split(';');

            int lineId = int.Parse(columns[0]);

            return id == lineId;
        }
        public bool ValidateEmail(string email)
        {
            if (email.Contains('@') && email.Contains('.'))
            {
                if (email[0] != '@')
                {
                    for (int i = 0; i < email.Length; i++)
                    {
                        if (email[i] == '@')
                        {
                            if (email[i + 1] == '.')
                                return false;
                            else
                            {
                                for (int j = i + 2; j < email.Length; j++)
                                {
                                    if (email[j] == '.')
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}