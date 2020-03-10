using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel
{
    class Read
    {
        private string _filename;
        public Read(string filename)
        {
            _filename = filename;
        }
        public IEnumerable<User> ReadAll()
        {
            IList<User> users = new List<User>();

            if(users.Count()==0 && !File.Exists(_filename))
                return users;

            IEnumerable<String> lines = File.ReadAllLines(_filename);

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

            int id = int.Parse(parameters[0]);
            string username = parameters[1];
            string password = parameters[2];
            string email = parameters[3];

            User user = new User(id, username, password, email);

            return user;
        }
    }
}
