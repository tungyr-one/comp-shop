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

        static public List<Item> ShowAllItems()
        {

            ComputerShopEntities dataEntities = new ComputerShopEntities();

            // TO-DO не показывает заказы
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Include("Category").Include("Supplier").ToList<Item>();
                return data;
        }   }

        //static public List<Order> ShowAllOrders()
        //{
        //    ComputerShopEntities dataEntities = new ComputerShopEntities();
        //    return dataEntities.Orders.ToList();
        //}

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
                    var itemEntry = new Item()
                    {
                        Name = insertEntry.ArticleName,
                        Price = insertEntry.ArticlePrice,
                        Category = categoryEntry,
                        Seller = insertEntry.ArticleSeller,
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
                context.Items.Remove(context.Items.Single(a => a.ItemID == removeEntry.ArticleId));
                context.SaveChanges();
                MessageBox.Show(removeEntry.ArticleName + " removed from database!");
            }
        }


        // редактирование товара
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


        //// ORDERS

        //static public List<Order> ShowAllOrders(string itemSupplier)
        //{ 
        //    using (var context = new ComputerShopEntities())
        //    {
        //        //var data = context.Orders.Include("Category").Include("Supplier").ToList<Item>();
        //        var data = context.Orders.ToList<Order>();
        //        return data;
        //    }            
        //}

        //static public ICollection<Order> SearchOrderByItem(Item item)
        //{
        //    using (var context = new ComputerShopEntities())
        //    {
        //        var data = context.Items.Where(x => x.Name == item.Name).ToList<Item>();
        //        foreach (Item itemForShow in data)


        //        return data;
        //    }
        //}
    }
}
