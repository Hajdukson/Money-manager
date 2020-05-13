using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersPanel.UserLogic
{
    class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
