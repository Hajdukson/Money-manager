using System.Collections.Generic;

namespace MoneyManager.Domain.Models
{
    public class Account : EntityObject
    {
        public User AccountHolder { get; set; }
        public List<Item> Items { get; set; }
        public double Balance { get; set; }
    }
}