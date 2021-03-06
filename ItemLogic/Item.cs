﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UsersPanel.ItemLogic
{
    class Item
    {
        private int Id { get; set; }
        public ItemType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public Item(decimal amount, DateTime date, ItemType itemType, string notes)
        {
            Amount = amount;
            Date = date;
            Type = itemType;
            Notes = notes;
        }
    }
}
