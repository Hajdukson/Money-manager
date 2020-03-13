using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersPanel.ItemLogic
{
    class Services
    {
        Rd rd = new Rd();
        private string _dirToUser;
        public Services(string dirToUser)
        {
            _dirToUser = dirToUser;
        }

        public void ShowLifeTimeReport()
        {
            IEnumerable<Item> items = rd.ReadItems(_dirToUser);
            
            
        }

    }
}