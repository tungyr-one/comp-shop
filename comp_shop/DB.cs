using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    class DB     

    {
        // TODO: Спросить: как подключить БД и какую лучше выбрать?
        // TODO: сделать один список на всех, а не в каждом методе свой?


        static string name = "temp_name";
        static double price = 36.9;
        static string category = "temp_category";
        static string seller = "temp_seller";
        static string supplier = "temp_supplier";

        static private string filename = "db_file.txt";


        static public void addToDB(string item_entry)
        {
            //StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding(1251));
            StreamWriter sw = File.AppendText(filename);
            sw.WriteLine(item_entry);
            sw.Close();
        }



        static public List<Item> SearchByName(string name)
        {
            List<Item> items_by_name = new List<Item>();
            Item item_fm_db1 = new Item(name, price, category, seller, supplier);
            Item item_fm_db2 = new Item(name, price, category, seller, supplier);
            Item item_fm_db3 = new Item(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_name.Add(item_fm_db1);
            items_by_name.Add(item_fm_db2);
            items_by_name.Add(item_fm_db3);
            return items_by_name;
        }


        static public List<Item> SearchByPrice(double priceFrom, double priceTo)
        {
            List<Item> items_by_price = new List<Item>();
            Item item_fm_db = new Item(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_price.Add(item_fm_db);
            return items_by_price;
        }

        static public List<Item> SearchByCategory(string category)
        {
            List<Item> items_by_category = new List<Item>();
            Item item_fm_db = new Item(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_category.Add(item_fm_db);
            return items_by_category;
        }

        static public List<Item> SearchByCategoryAndPrice(string category, double Price)
        {
            List<Item> items_by_category_and_price = new List<Item>();
            Item item_fm_db = new Item(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_category_and_price.Add(item_fm_db);
            return items_by_category_and_price;
        }

        static public List<Item> SearchBySeller(string seller)
        {
            List<Item> items_by_seller = new List<Item>();
            Item item_fm_db = new Item(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_seller.Add(item_fm_db);
            return items_by_seller;
        }

        static public List<Item> SearchBySupplier(string supplier)
        {
            List<Item> items_by_supplier = new List<Item>();
            Item item_fm_db = new Item(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_supplier.Add(item_fm_db);
            return items_by_supplier;
        }

    }
}
