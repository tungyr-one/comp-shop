using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    class Item
    {
        string item_string;

        public Item(string name, double price, string category, string supplier)
        {
            item_string = name + '-' + price.ToString() + '-' + category + '-' + supplier + '\n';
            DB.addToDB(item_string);
        }

    }
}
