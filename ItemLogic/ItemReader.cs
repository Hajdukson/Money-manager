using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.ItemLogic
{
    class ItemReader
    {
        private string _dirToUser;
        public ItemReader(string dirToUser)
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

            
            DateTime date = new DateTime();
            decimal amout = Convert.ToDecimal(item[2]);
            string notes = item[3];

            return new Item(amout, date, itemType, notes);
        }
        /*I implemented 'delete' button to datagird before, but I don't know how to use that to remove some item yet.
         *I had set an Id for every item and tryed to send the value (tryed to use SelectedIndex property) to Remove function, but it failed.
        */
        //public void Remove(int id)
        //{
        //    IEnumerable<string> lines = File.ReadAllLines(_dirToUser);
        //    IList<string> toSave = new List<string>();

        //    foreach(string line in lines)
        //    {
        //        if(!HasId(id, line))
        //            toSave.Add(line);
        //    }
        //}
        //private bool HasId(int id, string line)
        //{
        //    string[] clumns = line.Split(';');

        //    int lineId = Convert.ToInt32(clumns[0]);

        //    return lineId == id;
        //}
    }
}