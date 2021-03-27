using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
            //простой варант но показывает System.Data.Entity.DynamicProxies при включенной lazy loading и ничего не показывает при выключенной
            ComputerShopEntities dataEntities = new ComputerShopEntities();
            //return dataEntities.Items.ToList();

            // TO-DO не показывает заказы
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Include("Category").Include("Supplier").ToList<Item>();

                //// raw sql
                //var data = context.Items.SqlQuery("select Items.Name, Categories.Name" +
                //    " from items " +
                //    "left join Categories ON Items.CategoryID = Categories.CategoryID").ToList<Item>();

                return data;

                //List<Item> res = new List<Item>();

                //using (var db = new ComputerShopEntities())
                //{
                //    var data = (from item in db.Items.ToList()
                //                join category in db.Categories
                //                on item.CategoryID equals category.CategoryID
                //                select new 
                //                {
                //                    item.ItemID,
                //                    ItemName = item.Name,
                //                    CategoryName = category.Name,
                //                }).ToList();
                //    return data;
                //}
                //using (ComputerShopEntities db = new ComputerShopEntities())
                //{
                //    var items = db.Items;
                //    foreach (Item i in items)
                //        res = db.Items.ToList();
                //}
                //return res;
            }    }

        static public List<Order> ShowAllOrders()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();
            return dataEntities.Orders.ToList();
        }


        //static public IEnumerable<Item> SearchItemByName(string itemName)
        static public List<Item> SearchItemByName(string itemName)
        {
            // простой варант но показывает System.Data.Entity.DynamicProxies при включенной lazy loading и ничего не показывает при выключенной
            //var context = new ComputerShopEntities();
            //return (from item in context.Items where item.Name == itemName select item).ToList();
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(x => x.Name == itemName).Include("Category").Include("Supplier").Include("ItemsOrder").ToList<Item>();

                return data;
            }



            //using (var tables = new ComputerShopEntities())
            //{
            //    //var computer = tables.Items
            //    //                .Where(s => s.Name == "itemName")
            //    //                .FirstOrDefault<Items>();
            //    var computerList = tables.Items.Where(s => s.Name == itemName).ToList();
            //    return computerList;
            //}

            // пример с anonymous error требует решения
            //using (var context = new ComputerShopEntities())
            //{
            //       var data = (from item in context.Items
            //                join category in context.Categories on item.CategoryID equals category.CategoryID
            //                join supplier in context.Suppliers on item.SupplierID equals supplier.SupplierID
            //                where item.Name == itemName
            //                select new
            //                {
            //                    ItemId = item.ItemID,
            //                    ItemName = item.Name,
            //                    ItemPrice = item.Price,
            //                    ItemSeller = item.Seller,
            //                    CategoryName = category.Name,
            //                    SupplierName = supplier.Name
            //                }).ToList();

            //    return data;
            //}



            // ComputerShopEntities dataEntities = new ComputerShopEntities();
            // var queryItem =
            //(from item in dataEntities.Items
            // where item.Name == itemName
            // //orderby item.ItemID
            // select new Items() { Name = item.Name, Price = item.Price, Category = item.Category, Seller = item.Seller, Supplier = item.Supplier });

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

        static public void addToDB(Article insertEntry)
        {
            //using (var tables = new ComputerShopEntities())
            //{
            //    var std = new Item()
            //    {
            //        Name = insertEntry.ArticleName,
            //        Price = insertEntry.ArticlePrice,
            //        Category = 1
            //        Seller = insertEntry.ArticleSeller,
            //        Supplier = insertEntry.ArticleSupplier
            //    };
            //    tables.Items.Add(std);

            //    tables.SaveChanges();
            //}
        }


        static public List<Item> SearchByPrice(decimal priceFrom, decimal priceTo)
        {
            using (var tables = new ComputerShopEntities())
            {
                //Item itemEntity = tables.Items.Find(1);
                var studentEntity = tables.Items.SqlQuery("select * from items where itemid = 1").FirstOrDefault<Item>();

                var computerList = tables.Items.Where(s => priceTo >= s.Price && s.Price >= priceFrom).ToList();
                return computerList;
                //return studentEntity;
            }
        }

        static public List<Category> SearchByCategory(string itemCategory)
        {
            using (var tables = new ComputerShopEntities())
            {
                var computerList = tables.Categories.Where(s => s.Name == itemCategory).ToList();
                return computerList;
            }
        }

        //static public List<Items> SearchByCategoryAndPrice(string itemCategory, decimal priceFrom, decimal priceTo)
        //{
        //    using (var tables = new ComputerShopEntities())
        //    {
        //        var computerList = tables.Items.Where(s => itemCategory == s.Category && priceTo >= s.Price && s.Price >= priceFrom).ToList();
        //        return computerList;
        //    }
        //}

        static public List<Item> SearchBySeller(string itemSeller)
        {
            using (var tables = new ComputerShopEntities())
            {
                var computerList = tables.Items.Where(s => s.Seller == itemSeller).ToList();
                return computerList;
            }
        }

        //static public List<Items> SearchBySupplier(string itemSupplier)
        //{
        //    using (var tables = new ComputerShopEntities())
        //    {
        //        var computerList = tables.Items.Where(s => s.Supplier == itemSupplier).ToList();
        //        return computerList;
        //    }
        //}

    }
}
