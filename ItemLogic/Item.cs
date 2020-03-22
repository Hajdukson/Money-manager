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
        public ItemType Type { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Notes { get; private set; }

        public Item(decimal amount, DateTime date, ItemType itemType, string notes)
        {
            Amount = amount;
            Date = date;
            Type = itemType;
            Notes = notes;
        }
    }
}
