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

        static public List<ComputerShopEntities> ShowAllItems()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();
            // show all items
            var queryAllItems =
           (from item in dataEntities.Items
                //where item.Item1 == "Computer Dell"
            orderby item.ItemID
            select new { item.Name, item.Price, item.Category, item.Seller, item.Supplier });

            return queryAllItems.ToList();

           // // show all orders
           // var queryAllOrders =
           //(from order in dataEntities.Orders
           //     //where item.Item1 == "Computer Dell"
           // orderby order.OrderID
           // select new { order.OrderID, order.ItemID, order.Customer, order.OrderDate, order.OrderQuantity }).ToList();



            //BindingSource bindingSource1 = new BindingSource();
            //bindingSource1.DataSource = (from item in dataEntities.Items
            //                             where item.Item1 == "Computer Dell"
            //                             orderby item.ItemID
            //                             select new { item.Item1, item.Price, item.Category, item.Seller, item.Supplier }).ToList();


            //dataGridView1.AutoGenerateColumns = true;
            //dataGridView1.DataSource = bindingSource1;

            
        }

        static public void addToDB(string item_entry)
        {
            //StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding(1251));
            StreamWriter sw = File.AppendText(filename);
            sw.WriteLine(item_entry);
            sw.Close();
        }

 
        static public List<Article> SearchByName(string name)
        {
            List<Article> items_by_name = new List<Article>();
            Article item_fm_db1 = new Article(name, price, category, seller, supplier);
            Article item_fm_db2 = new Article(name, price, category, seller, supplier);
            Article item_fm_db3 = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_name.Add(item_fm_db1);
            items_by_name.Add(item_fm_db2);
            items_by_name.Add(item_fm_db3);
            return items_by_name;
        }


        static public List<Article> SearchByPrice(double priceFrom, double priceTo)
        {
            List<Article> items_by_price = new List<Article>();
            Article item_fm_db = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_price.Add(item_fm_db);
            return items_by_price;
        }

        static public List<Article> SearchByCategory(string category)
        {
            List<Article> items_by_category = new List<Article>();
            Article item_fm_db = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_category.Add(item_fm_db);
            return items_by_category;
        }

        static public List<Article> SearchByCategoryAndPrice(string category, double Price)
        {
            List<Article> items_by_category_and_price = new List<Article>();
            Article item_fm_db = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_category_and_price.Add(item_fm_db);
            return items_by_category_and_price;
        }

        static public List<Article> SearchBySeller(string seller)
        {
            List<Article> items_by_seller = new List<Article>();
            Article item_fm_db = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_seller.Add(item_fm_db);
            return items_by_seller;
        }

        static public List<Article> SearchBySupplier(string supplier)
        {
            List<Article> items_by_supplier = new List<Article>();
            Article item_fm_db = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_supplier.Add(item_fm_db);
            return items_by_supplier;
        }

    }
}
