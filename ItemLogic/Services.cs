using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsersPanel.ItemLogic
{
    class Services
    { 
        public ObservableCollection<Item> ShowLifetiemReport(IEnumerable<Item> items)
        {
            ObservableCollection<Item> itemsToCollection = new ObservableCollection<Item>();

            foreach (Item item in items)
                itemsToCollection.Add(item);

            return itemsToCollection;
        }
        public ObservableCollection<Item> ShowMothReport(IEnumerable<Item> items)
        {
            ObservableCollection<Item> itemsToCollection = new ObservableCollection<Item>();

            foreach (Item item in items)
                if (item.Date.Year == DateTime.Now.Year && item.Date.Month == DateTime.Now.Month)
                    itemsToCollection.Add(item);

            return itemsToCollection;
        }
        public string DispalyBalanceMonth(IEnumerable<Item> items)
        {
            decimal sum = 0.0M;

            sum = SumIncomesMonth(items) - SumOutcomesMonth(items);

            return "" + sum;
        }
        public string DispalyBalanceLifetime(IEnumerable<Item> items)
        {
            decimal sum = 0.0M;
            sum = SumIncomesLifetime(items) - SumOutcomesLifetime(items);

            return "" + sum;
        }
        private decimal SumIncomesLifetime(IEnumerable<Item> items)
        {
            decimal sum = 0.0M;
            foreach (Item item in items)
            {
                if (item.Type == ItemType.Income)
                {
                    sum += item.Amount;
                }
            }
            return sum;
        }
        private decimal SumOutcomesLifetime(IEnumerable<Item> items)
        {
            decimal sum = 0.0M;
            foreach (Item item in items)
            {
                if (item.Type == ItemType.Outcome)
                {
                    sum += item.Amount;
                }
            }
            return sum;
        }
        private decimal SumIncomesMonth(IEnumerable<Item> items)
        {
            decimal sum = 0.0M;
            foreach (Item item in items)
            {
                if (item.Type == ItemType.Income && (item.Date.Year == DateTime.Now.Year && item.Date.Month == DateTime.Now.Month))
                {
                    sum += item.Amount;
                }
            }
            return sum;
        }
        private decimal SumOutcomesMonth(IEnumerable<Item> items)
        {
            decimal sum = 0.0M;
            foreach (Item item in items)
            {
                if (item.Type == ItemType.Outcome && (item.Date.Year == DateTime.Now.Year && item.Date.Month == DateTime.Now.Month))
                {
                    sum += item.Amount;
                }
            }
            return sum;
        }
    }
}