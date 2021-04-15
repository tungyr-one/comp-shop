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

    // how to bind collection to DataGridView: http://www.developer-corner.com/blog/2007/07/19/datagridview-how-to-bind-nested-objects/
    class DB    
    {

        //_______________________________________________________________________________


        //static public List<Item> ShowAllItems()
        //{

        //    ComputerShopEntities dataEntities = new ComputerShopEntities();

        //    using (var context = new ComputerShopEntities())
        //    {
        //        var data =  context.Items.Include(c => c.Sellers.Select(b => b.SellerName.ToString())).Load().ToList<Item>();
        //        //var data = context.Items.Include("Category").Include("Supplier").Include("Sellers").;
        //        return data;
        //    }

        //}

        // список продавцов по ID товара
        static public string LoadAllStuff(int ID)
        {

            using (var context = new ComputerShopEntities())
            {
                //!!!поиск товаров по OrderID через Where и Any
               var itemWithSellers = context.Items.Where(x => x.Orders.Any(y => y.OrderID == ID)).ToList();
                string dataStr = "";
                foreach (Item it in itemWithSellers)
                {
                    dataStr += it.Name + "; ";
                    dataStr += "\n";
                }
                MessageBox.Show(itemWithSellers.Count().ToString());
                MessageBox.Show(dataStr);
                return dataStr;

                //!!! поиск продавцов по ItemID через Where и Any
                //var SellersWithItems = context.Sellers.Where(x => x.Items.Any(y => y.ItemID == ID)).ToList();
                //string dataStr1 = "";
                //foreach (Seller sel in SellersWithItems)
                //{
                //    dataStr1 += sel.SellerName + "; ";
                //    dataStr1 += "\n";
                //}
                //MessageBox.Show(SellersWithItems.Count().ToString());
                //MessageBox.Show(dataStr1);
                //return dataStr1;


                ////!!! другой способ загрузки Sellers
                //var itemSearch = context.Items.Find(itemID);
                //context.Entry(itemSearch).Collection(p => p.Sellers).Load();
                //List<Seller> sels1 = itemSearch.Sellers.ToList();
                //string dataStr1 = "";
                //foreach (Seller sel in sels1)
                //{
                //    dataStr1 += sel.SellerName + "; ";
                //    dataStr1 += "\n";
                //}


                //MessageBox.Show(dataStr1);

                // !!!поиск только одного товара
                //var itemWithSellers = context.Items.Find(itemID);
                //!!! поиск множества товаров
                //var original = context.Items.Where(x => x.Name == item.Name);
                //MessageBox.Show(original.Sellers.Count().ToString());
                //List<Seller> sels = itemWithSellers.Sellers.ToList();
                //string sellersListStr = "";

                //foreach (Seller sel in sels)
                //{
                //    sellersListStr += sel.SellerName + "; ";
                //}
                //return sellersListStr;
            }
        }

        //_______________________________________________________________________________


        static public string SellersToString(int itemID)
        {

            using (var context = new ComputerShopEntities())
            {

               //MessageBox.Show(dataStr1);

                // !!!поиск только одного товара
                var itemWithSellers = context.Items.Find(itemID);
                //!!! поиск множества товаров
                //var original = context.Items.Where(x => x.Name == item.Name);
                //MessageBox.Show(original.Sellers.Count().ToString());
                List<Order> sels = itemWithSellers.Orders.ToList();
                string sellersListStr = "";

                foreach (Order sel in sels)
                {
                    sellersListStr += sel.SellerName + "; ";
                }
                return sellersListStr;
            }
        }

        static public List<Item> ShowAllItems()
        {

            ComputerShopEntities dataEntities = new ComputerShopEntities();


            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Include("Category").Include("Supplier").ToList<Item>();
                return data;
            }

        }



        // добавление категории
        static public void AddCategory(string categoryName)
        {
            using (var context = new ComputerShopEntities())
            {
                context.Categories.Add(new Category { Name = categoryName });
                context.SaveChanges();
                MessageBox.Show("Добавлена категория: " + categoryName);
            }
        }


        // TODO: автоматически назначать привязанным к категории товарам категорию "без категории" (и поставщика тоже)
        // удаление категории
        static public void RemoveCategory(string categoryName)
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    context.Categories.Remove(context.Categories.Single(a => a.Name == categoryName));
                    context.SaveChanges();
                    MessageBox.Show(categoryName + " удален!");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                MessageBox.Show("Невозможно удалить, в категории есть товары!");
            }
        }


        // добавление поставщика
        static public void AddSupplier(string supplierName, string supplierAddres = null)
        {
            using (var context = new ComputerShopEntities())
            {
                context.Suppliers.Add(new Supplier { Name = supplierName, Contacts = supplierAddres });
                context.SaveChanges();
                MessageBox.Show("Добавлен поставщик: " + supplierName);
            }
        }

        // удаление поставщика
        static public void RemoveSupplier(string supplierName)
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    context.Suppliers.Remove(context.Suppliers.Single(a => a.Name == supplierName));
                    context.SaveChanges();
                    MessageBox.Show(supplierName + " удален!");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                MessageBox.Show("Невозможно удалить, к поставщику привязаны товары!");
            }
        }


        static public void addToDB(Article insertEntry)
        {
            // проверка содержимого полей Article
            //MessageBox.Show("outside using: " + insertEntry.Name + "-" +
            //    insertEntry.Price + "-" +
            //    insertEntry.Category + "-" +
            //    insertEntry.Sellers + "-" +
            //    insertEntry.Supplier);
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == insertEntry.Category);
                    Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == insertEntry.Supplier);
                    var itemEntry = new Item()
                    {
                        Name = insertEntry.Name,
                        Price = insertEntry.Price,
                        Category = categoryEntry,
                        Supplier = supplierEntry,
                    };                    
                    context.Items.Add(itemEntry);

                    context.SaveChanges();
                    MessageBox.Show("Добавлен товрар: " + itemEntry.ToString());
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

        // удаление товара
        static public void RemoveItem(Article removeEntry)
        {
            using (var context = new ComputerShopEntities())
            {
                context.Items.Remove(context.Items.Single(a => a.ItemID == removeEntry.Id));
                context.SaveChanges();
                MessageBox.Show(removeEntry.Name + " removed from database!");
            }
        }


        // редактирование товара
        static public void editEntry(Article entryToEdit)
        {
            using (var context = new ComputerShopEntities())
            {
                var original = context.Items.Find(entryToEdit.Id);

                Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == entryToEdit.Category);
                Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == entryToEdit.Supplier);

                if (original != null)
                {
                    original.Name = entryToEdit.Name;
                    original.Price = entryToEdit.Price;
                    original.Category = categoryEntry;
                    original.Supplier = supplierEntry;
                };
                context.SaveChanges();
                MessageBox.Show(entryToEdit.Name + " updated!");
            }
            
        }

        static public List<Item> SearchItemByName(string itemName)
        {
            // простой варант но показывает System.Data.Entity.DynamicProxies при включенной lazy loading и ничего не показывает при выключенной
            //var context = new ComputerShopEntities();
            //return (from item in context.Items where item.Name == itemName select item).ToList();
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(x => x.Name == itemName).Include("Category").Include("Supplier").ToList<Item>();

                return data;
            }
        }

        static public List<Item> SearchByPrice(decimal priceFrom, decimal priceTo)
        {

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

        //static public List<Item> SearchBySeller(string itemSeller)
        //{
        //    using (var context = new ComputerShopEntities())
        //    {
        //        var data = context.Items.Where(x => x.Seller == itemSeller).Include("Category").Include("Supplier").ToList<Item>();

        //        return data;
        //    }
        //}

        static public List<Item> SearchBySupplier(string itemSupplier)
        {
            using (var context = new ComputerShopEntities())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(c => c.Name == itemSupplier);
                var data = context.Items.Where(x => x.Supplier.Name == supplier.Name).Include("Category").Include("Supplier").ToList<Item>();

                return data;
            }
        }


        // ORDERS


        static public List<Order> ShowAllOrders()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();

            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.ToList<Order>();
                return data;
            }
        }

        // список товаров по ID order
        static public string ItemsToString(int orderID)
        {

            using (var context = new ComputerShopEntities())
            {
                ////!!! другой способ загрузки Sellers
                //var itemSearch = context.Items.Find(itemID);

                //// Load the blog related to a given post.
                //context.Entry(itemSearch).Collection(p => p.Sellers).Load();
                //List<Seller> sels1 = itemSearch.Sellers.ToList();
                //string dataStr1 = "";
                //foreach (Seller sel in sels1)
                //{
                //    dataStr1 += sel.SellerName + "; ";
                //    dataStr1 += "\n";
                //}


                //MessageBox.Show(dataStr1);

                // !!!поиск только одного товара
                var orderWithItems = context.Orders.Find(orderID);
                //!!! поиск множества товаров
                //var original = context.Items.Where(x => x.Name == item.Name);
                //MessageBox.Show(original.Sellers.Count().ToString());
                //var sellerResults = context.Items.Where(x => x.)

                List<Item> items = orderWithItems.Items.ToList();
                string itemsListStr = "";

                foreach (Item it in items)
                {
                    itemsListStr += it.Name + "; "; ;
                }
                return itemsListStr;
            }
        }

            // SUPPLIERS


            static public List<Supplier> ShowAllSuppliers()
            {
            ComputerShopEntities dataEntities = new ComputerShopEntities();

            using (var context = new ComputerShopEntities())
            {
                var data = context.Suppliers.ToList<Supplier>();
                return data;
            }
            }
    }
}
