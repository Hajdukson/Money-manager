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
            string notes = item[3];

            return new Item(amout, date, itemType, notes);
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
        public void IfUserExists(string amount, DateTime? date, ItemType itemType, string notes)
        {

            if ((File.Exists(_dirToUser) && (amount == "" || date == null)) || 
                (!File.Exists(_dirToUser) && (amount == "" || date == null)) ||
                !AmountOrNot(amount))
                MessageBox.Show("Enter date and amount.", "Warning", MessageBoxButton.OK);
            else if (!File.Exists(_dirToUser))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Users");
                File.WriteAllText(_dirToUser, ItemToTxt(new Item(decimal.Round(Convert.ToDecimal(amount), 2), Convert.ToDateTime(date), itemType, notes), itemType));
            }
            else
                File.AppendAllText(_dirToUser, ItemToTxt(new Item(decimal.Round(Convert.ToDecimal(amount), 2), Convert.ToDateTime(date), itemType, notes), itemType));
        }
        private bool AmountOrNot(string amount)
        {
            for(int i = 0; i < amount.Length; i++)
            {
                if(amount[i] < '0' || amount[i] > '9')
                {
                    if (amount[i] == ',')
                        continue;
                    return false;
                }
            }
            return true;
        }
    }
}