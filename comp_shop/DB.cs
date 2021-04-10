using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Windows.Forms;

namespace comp_shop
{
    class DB     

    {

        // TODO: сделать один список на всех, а не в каждом методе свой?


        //static string name = "temp_name";
        //static double price = 36.9;
        //static string category = "temp_category";
        //static string seller = "temp_seller";
        //static string supplier = "temp_supplier";


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


        static public void AddCategory(string categoryName)
        {
            using (var context = new ComputerShopEntities())
            {
                context.Categories.Add(new Category { Name = categoryName });
            }
        }

        static public void addToDB(Article insertEntry)
        {
            // проверка содержимого полей Article
            //MessageBox.Show("outside using: " + insertEntry.ArticleName + "-" + 
            //    insertEntry.ArticlePrice + "-" +
            //    insertEntry.ArticleCategory + "-" +
            //    insertEntry.ArticleSeller + "-" +                
            //    insertEntry.ArticleSupplier);
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == insertEntry.ArticleCategory);
                    Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == insertEntry.ArticleSupplier);
                    // проверка содержимого полей Article
                //    MessageBox.Show("inside using: " + insertEntry.ArticleName + "-" +
                //insertEntry.ArticlePrice + "-" +
                //insertEntry.ArticleCategory + "-" +
                //insertEntry.ArticleSeller + "-" +
                //insertEntry.ArticleSupplier);

                    var itemEntry = new Item()
                    {
                        Name = insertEntry.ArticleName,
                        Price = insertEntry.ArticlePrice,
                        Category = categoryEntry,
                        Seller = insertEntry.ArticleSeller,
                        Supplier = supplierEntry,
                    };

                    MessageBox.Show("itemEntry name: " + itemEntry.ToString()) ;
                    context.Items.Add(itemEntry);

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show(ve.ErrorMessage);
                    }
                }
                throw;

            }
        }

        static public void RemoveItem(Article removeEntry)
        {
            using (var context = new ComputerShopEntities())
            {
                context.Items.Remove(context.Items.Single(a => a.ItemID == removeEntry.ArticleId));
                context.SaveChanges();
                MessageBox.Show(removeEntry.ArticleName + " removed from database!");
            }
        }

        static public void editEntry(Article entryToEdit)
        {
            using (var context = new ComputerShopEntities())
            {
                var original = context.Items.Find(entryToEdit.ArticleId);

                Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == entryToEdit.ArticleCategory);
                Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == entryToEdit.ArticleSupplier);

                if (original != null)
                {
                    original.Name = entryToEdit.ArticleName;
                    original.Price = entryToEdit.ArticlePrice;
                    original.Category = categoryEntry;
                    original.Seller = entryToEdit.ArticleSeller;
                    original.Supplier = supplierEntry;
                };
                context.SaveChanges();
                MessageBox.Show(entryToEdit.ArticleName + " updated!");
            }
            
        }

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




        static public List<Item> SearchByPrice(decimal priceFrom, decimal priceTo)
        {
            //using (var tables = new ComputerShopEntities())
            //{
            //    //Item itemEntity = tables.Items.Find(1);
            //    var studentEntity = tables.Items.SqlQuery("select * from items where itemid = 1").FirstOrDefault<Item>();

            //    var computerList = tables.Items.Where(s => priceTo >= s.Price && s.Price >= priceFrom).ToList();
            //    return computerList;
            //    //return studentEntity;


            //}

            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(x => priceTo >= x.Price && x.Price >= priceFrom).Include("Category").Include("Supplier").ToList<Item>();

                return data;
            }
        }

        static public List<Item> SearchByCategory(string itemCategory)
        {
            using (var context = new ComputerShopEntities())
            {
                Category category = context.Categories.FirstOrDefault(c => c.Name == itemCategory);
                var data = context.Items.Where(x => x.Category.Name == category.Name).Include("Category").Include("Supplier").ToList<Item>();
                return data;
            }
        }

        static public List<Item> SearchByCategoryAndPrice(string itemCategory, decimal priceFrom, decimal priceTo)
        {
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(s => priceTo >= s.Price && s.Price >= priceFrom && s.Category.Name == itemCategory).Include("Category").Include("Supplier").ToList();
                return data;
            }
        }

        static public List<Item> SearchBySeller(string itemSeller)
        {
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(x => x.Seller == itemSeller).Include("Category").Include("Supplier").ToList<Item>();

                return data;
            }
        }

        static public List<Item> SearchBySupplier(string itemSupplier)
        {
            using (var context = new ComputerShopEntities())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(c => c.Name == itemSupplier);
                var data = context.Items.Where(x => x.Supplier.Name == supplier.Name).Include("Category").Include("Supplier").ToList<Item>();

                return data;
            }
        }



    }
}
