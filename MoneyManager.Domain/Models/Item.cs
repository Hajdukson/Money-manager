using System;

namespace MoneyManager.Domain.Models
{
    public class Item : EntityObject
    {
        public decimal Ammount { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public ItemType ItemType { get; set; }
        public Categories Categories { get; set; }
        public DateTime Date { get; set; }
    }
}
