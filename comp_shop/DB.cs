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

        // TODO: сделать один список на всех, а не в каждом методе свой?


        static string name = "temp_name";
        static double price = 36.9;
        static string category = "temp_category";
        static string seller = "temp_seller";
        static string supplier = "temp_supplier";

        static private string filename = "db_file.txt";

        static public List<Item> ShowAllItems()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();
            return dataEntities.Items.ToList();
        }

        static public List<Order> ShowAllOrders()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();
            return dataEntities.Orders.ToList();
        }

        static public List<Item> SearchItemByName(string itemName)
        {
            
            using (var tables = new ComputerShopEntities())
            {
                //var computer = tables.Items
                //                .Where(s => s.Name == "itemName")
                //                .FirstOrDefault<Item>();
                var computerList = tables.Items.Where(s => s.Name == itemName).ToList();
                return computerList;
            }

            

            // ComputerShopEntities dataEntities = new ComputerShopEntities();
            // var queryItem =
            //(from item in dataEntities.Items
            // where item.Name == itemName
            // //orderby item.ItemID
            // select new Item() { Name = item.Name, Price = item.Price, Category = item.Category, Seller = item.Seller, Supplier = item.Supplier });

            // return queryItem.ToList();


            //   // show all orders
            //   var queryAllOrders =
            //  (from order in dataEntities.Orders
            //       //where item.Item1 == "Computer Dell"
            //orderby order.OrderID
            //   select new { order.OrderID, order.ItemID, order.Customer, order.OrderDate, order.OrderQuantity }).ToList();



            //   BindingSource bindingSource1 = new BindingSource();
            //   bindingSource1.DataSource = (from item in dataEntities.Items
            //                                where item.Item1 == "Computer Dell"
            //                                orderby item.ItemID
            //                                select new { item.Item1, item.Price, item.Category, item.Seller, item.Supplier }).ToList();


            //   dataGridView1.AutoGenerateColumns = true;
            //   dataGridView1.DataSource = bindingSource1;
        }

        static public void addToDB(string item_entry)
        {
            //StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding(1251));
            StreamWriter sw = File.AppendText(filename);
            sw.WriteLine(item_entry);
            sw.Close();
        }


        static public List<Item> SearchByPrice(decimal priceFrom, decimal priceTo)
        {
            using (var tables = new ComputerShopEntities())
            {
                //var computer = tables.Items
                //                .Where(s => s.Name == "itemName")
                //                .FirstOrDefault<Item>();
                var computerList = tables.Items.Where(s => priceTo >= s.Price && s.Price >= priceFrom).ToList();
                return computerList;
            }
        }

        static public List<Item> SearchByCategory(string itemCategory)
        {
            using (var tables = new ComputerShopEntities())
            {
                var computerList = tables.Items.Where(s => s.Category == itemCategory).ToList();
                return computerList;
            }
        }

        static public List<Article> SearchByCategoryAndPrice(string category, double Price)
        {
            List<Article> items_by_category_and_price = new List<Article>();
            Article item_fm_db = new Article(name, price, category, seller, supplier);
            //TODO:  searching logic
            items_by_category_and_price.Add(item_fm_db);
            return items_by_category_and_price;
        }

        static public List<Item> SearchBySeller(string itemSeller)
        {
            using (var tables = new ComputerShopEntities())
            {
                var computerList = tables.Items.Where(s => s.Seller == itemSeller).ToList();
                return computerList;
            }
        }

        static public List<Item> SearchBySupplier(string itemSupplier)
        {
            using (var tables = new ComputerShopEntities())
            {
                var computerList = tables.Items.Where(s => s.Supplier == itemSupplier).ToList();
                return computerList;
            }
        }

    }
}
