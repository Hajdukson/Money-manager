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
        public IEnumerable<Item> ReadItems(string userFile)
        {
            IList<Item> items = new List<Item>();

            if (!File.Exists(userFile))
                return items;

            IEnumerable<string> lines = File.ReadAllLines(userFile);

            foreach (string line in lines)
                items.Add(TextToItems(line));

            return items;
        }
        private Item TextToItems(string line)
        {
            string[] item = line.Split(';');

            DateTime date = Convert.ToDateTime(item[1]);
            decimal amout = Convert.ToDecimal(item[2]);

            return new Item(amout, date);
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
        public void IfUserExists(string amount, DateTime? date, string dirToUser, ItemType itemType)
        {
            if (!File.Exists(dirToUser))
                File.WriteAllText(dirToUser, ItemToTxt(new Item(Convert.ToDecimal(amount), Convert.ToDateTime(date)), itemType));
            else if (File.Exists(dirToUser) && (amount == "" || date == null))
                MessageBox.Show("Enter data.", "Warning", MessageBoxButton.OK);
            else
                File.AppendAllText(dirToUser, ItemToTxt(new Item(Convert.ToDecimal(amount), Convert.ToDateTime(date)), itemType));
        }
    }
}