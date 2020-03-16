using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.ItemLogic
{
    class Rd
    {
        private string _dirToUser;
        public Rd(string dirToUser)
        {
            _dirToUser = dirToUser;
        }
        public IEnumerable<Item> ReadItems()
        {
            IList<Item> items = new List<Item>();

            if (!File.Exists(_dirToUser))
                return items;

            IEnumerable<string> lines = File.ReadAllLines(_dirToUser);

            foreach (string line in lines)
            {
                Item item = TextToItems(line);
                items.Add(item);
            }

            return items;
        }

        private Item TextToItems(string line)
        {
            string[] item = line.Split(';');

            ItemType itemType = ItemType.Income;

            if (item[0] == "O")
                itemType = ItemType.Outcome;

            DateTime date = Convert.ToDateTime(item[1]);
            decimal amout = Convert.ToDecimal(item[2]);

            return new Item(amout, date, itemType);
        }
        private string ItemToTxt(Item item, ItemType itemType)
        {
            string type = "I";

            if (itemType == ItemType.Outcome)
                type = "O";

            string line = string.Format("{0};{1};{2}",
                type,
                item.Date.ToString("dd-MM-yyyy"),
                item.Amount
                );

            return line + Environment.NewLine;
        }
        public void IfUserExists(string amount, DateTime? date, ItemType itemType)
        {
            if ((File.Exists(_dirToUser) && (amount == "" || date == null)) || !File.Exists(_dirToUser) && (amount == "" || date == null))
                MessageBox.Show("Enter data.", "Warning", MessageBoxButton.OK);
            else if (!File.Exists(_dirToUser))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Users");
                File.WriteAllText(_dirToUser, ItemToTxt(new Item(Convert.ToDecimal(amount), Convert.ToDateTime(date), itemType), itemType));
            }
            else
                File.AppendAllText(_dirToUser, ItemToTxt(new Item(Convert.ToDecimal(amount), Convert.ToDateTime(date), itemType), itemType));
        }
    }
}