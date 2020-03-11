using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersPanel
{
    class Item
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public ItemType Type { get; set; }

        public Item(decimal amount, DateTime date)
        {
            Amount = amount;
            Date = date; 
        }
    }
}
