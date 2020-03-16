using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UsersPanel.ItemLogic
{
    class Item
    {
        public ItemType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Item(decimal amount, DateTime date, ItemType itemType)
        {
            Amount = amount;
            Date = date;
            Type = itemType;
        }
    }
}
