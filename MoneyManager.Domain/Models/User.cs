using System;

namespace MoneyManager.Domain.Models
{
    public class User : EntityObject
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
