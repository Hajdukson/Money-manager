using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.ItemLogic
{
    class ItemWriter
    {
        private string _dirToUser;
        public ItemWriter(string dirToUser)
        {
            _dirToUser = dirToUser;
        }
        public void AddItem(decimal amount, DateTime? date, ItemType itemType, string notes)
        {
            Item item = new Item(amount, Convert.ToDateTime(date), itemType, notes);

            if ((File.Exists(_dirToUser) && date == null) || (!File.Exists(_dirToUser) && date == null))
                MessageBox.Show("Enter date and amount.", "Warning", MessageBoxButton.OK);
            else if (!File.Exists(_dirToUser))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Users");
                File.WriteAllText(_dirToUser, ItemToTxt(item, itemType));
            }
            else
                File.AppendAllText(_dirToUser, ItemToTxt(item, itemType));
        }
        private string ItemToTxt(Item item, ItemType itemType)
        {
            string type = "I";

            if (itemType == ItemType.Outcome)
                type = "O";

            string line = string.Format("{0};{1};{2};{3}",
                type,
                item.Date.ToString("dd-MM-yyyy"),
                item.Amount,
                item.Notes
                );

            return line + Environment.NewLine;
        }
    }
}
