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
        //static public string LoadAllStuff(int ID)
        //{

        //    using (var context = new ComputerShopEntities())
        //    {
        //!!!поиск товаров по OrderID через Where и Any (many-to-many)
        //var itemWithSellers = context.Items.Where(x => x.Orders.Any(y => y.OrderID == ID)).ToList();
        // string dataStr = "";
        // foreach (Item it in itemWithSellers)
        // {
        //     dataStr += it.Name + "; ";
        //     dataStr += "\n";
        // }
        // MessageBox.Show(itemWithSellers.Count().ToString());
        // MessageBox.Show(dataStr);
        // return dataStr;

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
        //    }
        //}

        //_______________________________________________________________________________

        // заказы товара в виде строки
        //static public string OrdersToString(int itemID)
        //{

        //    using (var context = new ComputerShopEntities())
        //    {

        //        //MessageBox.Show(dataStr1);

        //        // !!!поиск только одного товара
        //        var itemWithOrders = context.Items.Find(itemID);
        //        //!!! поиск множества товаров
        //        //var original = context.Items.Where(x => x.Name == item.Name);
        //        //MessageBox.Show(original.Sellers.Count().ToString());
        //        List<Order> ords = itemWithOrders.Orders.ToList();
        //        string sellersListStr = "";

        //        foreach (Order sel in ords)
        //        {
        //            sellersListStr += sel + "; ";
        //        }
        //        return sellersListStr;
        //    }
        //}

        //// список заказов на товар для ConnectedInfo
        //static public List<Order> OrdersToList(int itemID)
        //{
        //    using (var context = new ComputerShopEntities())
        //    {
        //        List<Order> original = context.Orders.Where(x => x.ItemID == itemID).ToList();
        //        return original;
        //    }
        //}

        // товары поставщика в виде строки
        static public string ItemsToString(int supplierID)
        {

            using (var context = new ComputerShopEntities())
            {

                //MessageBox.Show(dataStr1);

                // !!!поиск только одного товара
                var itemWithOrders = context.Suppliers.Find(supplierID);
                //!!! поиск множества товаров
                //var original = context.Items.Where(x => x.Name == item.Name);
                //MessageBox.Show(original.Sellers.Count().ToString());
                List<Item> ords = itemWithOrders.Items.ToList();
                string sellersListStr = "";

                foreach (Item sel in ords)
                {
                    sellersListStr += sel + "; ";
                }
                return sellersListStr;
            }
        }


        // список товаров поставщика для ConnectedInfo
        static public List<Item> ItemsToList(int SupplierID)
        {
            using (var context = new ComputerShopEntities())
            {
                List<Item> original = context.Items.Where(x => x.SupplierID == SupplierID).Include("Category").Include("Supplier").ToList();
                return original;
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

        // добавление товара в БД
        static public void addItem(Article insertItem)
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
                    Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == insertItem.Category);
                    Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == insertItem.Supplier);
                    var itemEntry = new Item()
                    {
                        Name = insertItem.Name,
                        Price = insertItem.Price,
                        Category = categoryEntry,
                        Supplier = supplierEntry,
                    };
                    context.Items.Add(itemEntry);

                    context.SaveChanges();
                    MessageBox.Show("Добавлен товрар: " + itemEntry.ToString());
                }

                //using (var context = new ComputerShopEntities())
                //{
                //    context.Items.Add(insertItem);

                //    context.SaveChanges();
                //    MessageBox.Show("Добавлен товар: " + insertItem.ToString());
                //}
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

        // редактирование товара
        static public void editItem(Article itemToEdit)
        {
            //using (var context = new ComputerShopEntities())
            //{
            //    Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == itemToEdit.Category.ToString());
            //    Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == itemToEdit.Supplier.ToString());
            //    var original = context.Items.Find(itemToEdit.ItemID);
            //    if (original != null)
            //    {
            //        original.Name = itemToEdit.Name.ToString();
            //        original.Price = Convert.ToDecimal(itemToEdit.Price);
            //        original.Category = categoryEntry;
            //        original.Supplier = supplierEntry;
            //    };
            //    context.SaveChanges();
            //    MessageBox.Show(itemToEdit.Name + " updated!");
            //}


            using (var context = new ComputerShopEntities())
            {
                var original = context.Items.Single(x => x.Name == itemToEdit.Name);

                Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == itemToEdit.Category);
                Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == itemToEdit.Supplier);

                if (original != null)
                {
                    original.Name = itemToEdit.Name;
                    original.Price = itemToEdit.Price;
                    original.Category = categoryEntry;
                    original.Supplier = supplierEntry;
                };
                context.SaveChanges();
                MessageBox.Show(itemToEdit.Name + " updated!");
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




        // SEARCHING

        static public List<Item> SearchItemByNameOrID(string itemName = null, int ID = 0)
        {
            // обработка поля поиск в главной форме
            if (ID == 0)
            {
                using (var context = new ComputerShopEntities())
                {
                    var data = context.Items.Where(x => x.Name == itemName).Include("Category").Include("Supplier").Include("Orders").ToList<Item>();

                    return data;
                }
            }
            // загрузка выбранной сущности в статусную строку
            else
            {
                using (var context = new ComputerShopEntities())
                {
                    var data = context.Items.Where(x => x.ItemID == ID).Include("Category").Include("Supplier").Include("OrderItems").ToList<Item>();

                    return data;
                }
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

        static public List<Order> SearchBySellerAndTime(string itemSeller, DateTime dateFrom, DateTime dateTo )
        {

            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.Where(x => dateTo >= x.OrderDate)
                                        .Where(x => x.OrderDate >= dateFrom)
                                        .Where(x => x.SellerName == itemSeller)
                                        .Include("Item").ToList<Order>();

                return data;
            }
        }

        static public List<Order> SearchByTime(DateTime dateFrom, DateTime dateTo)
        {

            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.Where(x => dateTo >= x.OrderDate)
                                        .Where(x => x.OrderDate >= dateFrom)
                                        .Include("Item").ToList<Order>();

                return data;
            }
        }

        static public List<Supplier> SearchBySupplier(string itemSupplier)
        {
            using (var context = new ComputerShopEntities())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(c => c.Name == itemSupplier);
                var data = context.Suppliers.Where(x => x.Name == itemSupplier).ToList<Supplier>();

                return data;
            }
        }

        static public Category SearchCategory(string categoryToFind)
        {
            using (var context = new ComputerShopEntities())
            {
                Category category = context.Categories.FirstOrDefault(c => c.Name == categoryToFind);
                return category;
            }
        }

        static public Supplier SearchSupplier(string supplierToFind)
        {
            using (var context = new ComputerShopEntities())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(c => c.Name == supplierToFind);
                return supplier;
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
        //static public string ItemsToString(int orderID)
        //{

            //using (var context = new ComputerShopEntities())
            //{
            //    ////!!! другой способ загрузки Sellers
            //    //var itemSearch = context.Items.Find(itemID);

            //    //// Load the blog related to a given post.
            //    //context.Entry(itemSearch).Collection(p => p.Sellers).Load();
            //    //List<Seller> sels1 = itemSearch.Sellers.ToList();
            //    //string dataStr1 = "";
            //    //foreach (Seller sel in sels1)
            //    //{
            //    //    dataStr1 += sel.SellerName + "; ";
            //    //    dataStr1 += "\n";
            //    //}


            //    //MessageBox.Show(dataStr1);

            //    // !!!поиск только одного товара
            //    var orderWithItems = context.Orders.Find(orderID);
            //    //!!! поиск множества товаров
            //    //var original = context.Items.Where(x => x.Name == item.Name);
            //    //MessageBox.Show(original.Sellers.Count().ToString());
            //    //var sellerResults = context.Items.Where(x => x.)

            //    List<Item> items = orderWithItems.Items.ToList();
            //    string itemsListStr = "";

            //    foreach (Item it in items)
            //    {
            //        itemsListStr += it.Name + "; "; ;
            //    }
            //    return itemsListStr;
            //}
        //}

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
