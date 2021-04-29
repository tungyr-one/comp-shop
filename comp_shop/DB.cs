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
        #region
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


        //// товары поставщика в виде строки
        //static public string ItemsToString(int supplierID)
        //{

        //    using (var context = new ComputerShopEntities())
        //    {

        //        //MessageBox.Show(dataStr1);

        //        // !!!поиск только одного товара
        //        var itemWithOrders = context.Suppliers.Find(supplierID);
        //        //!!! поиск множества товаров
        //        //var original = context.Items.Where(x => x.Name == item.Name);
        //        //MessageBox.Show(original.Sellers.Count().ToString());
        //        List<Item> ords = itemWithOrders.Items.ToList();
        //        string sellersListStr = "";

        //        foreach (Item sel in ords)
        //        {
        //            sellersListStr += sel + "; ";
        //        }
        //        return sellersListStr;
        //    }
        //}
        #endregion

        // ASSOCIATED LISTS

        // создание списка заказов на товар для вывода в ShowInfoForm
        static public List<ItemOrdersEntity> OrdersOfItem(int itemID)
        {
            using (var context = new ComputerShopEntities())
            {
                // выборка всех промежуточных сущностей заказов с количеством товара, найденного по ID товара
                List<OrderItems> orderIts = context.OrderItems1.Where(x => x.ItemID == itemID).Include(x => x.Order).Include("Item").ToList();

                List<ItemOrdersEntity> data = new List<ItemOrdersEntity>();

                // заполнение форм сущности ItemOrdersEntity данными
                for (int i = 0; i < orderIts.Count(); i++)
                {
                    ItemOrdersEntity ord = new ItemOrdersEntity(
                    item: orderIts[i].Item.ToString(),
                    orderID: orderIts[i].OrderID,
                    quantity: orderIts[i].ItemsQuantity,
                    orderDate: orderIts[i].Order.OrderDate.ToString(),
                    sellerName: orderIts[i].Order.SellerName,
                    customer: orderIts[i].Order.Customer,
                    customerContact: orderIts[i].Order.CustomerContact,
                    category: orderIts[i].Item.Category.Name
                    );
                    data.Add(ord);
                }
                return data;
            }
        }

        // создание списка товаров на заказ для вывода в ShowInfoForm
        static public List<ItemOrdersEntity> LoadItemOrdersEntities(int orderID)
        {
            using (var context = new ComputerShopEntities())
            {
                // выборка всех промежуточных сущностей товаров с количеством товара, найденного по ID заказа
                List<OrderItems> orderIts = context.OrderItems1.Where(x => x.OrderID == orderID).Include(x => x.Item).Include("Order").ToList();
                List<ItemOrdersEntity> data = new List<ItemOrdersEntity>();

                // заполнение форм сущности ItemOrdersEntity данными
                for (int i = 0; i < orderIts.Count(); i++)
                {
                    ItemOrdersEntity ord = new ItemOrdersEntity(
                    item: orderIts[i].Item.ToString(),
                    orderID: orderIts[i].OrderID,
                    quantity: orderIts[i].ItemsQuantity,
                    orderDate: orderIts[i].Order.OrderDate.ToString(),
                    sellerName: orderIts[i].Order.SellerName,
                    customer: orderIts[i].Order.Customer,
                    customerContact: orderIts[i].Order.CustomerContact,
                    category: orderIts[i].Item.Category.Name
                    );
                    data.Add(ord);
                }
                return data;
            }
        }

        // создание списка товаров поставщика для вывода в ShowInfoForm
        static public List<Item> ItemsToList(int SupplierID)
        {
            using (var context = new ComputerShopEntities())
            {
                List<Item> original = context.Items.Where(x => x.SupplierID == SupplierID).Include("Category").Include("Supplier").ToList();
                return original;
            }
        }

        // ITEMS

        // загрузка всех товаров
        static public List<Item> ShowAllItems()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();

            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Include("Category").Include("Supplier").ToList<Item>();
                return data;
            }
        }

        // добавление товара в БД
        static public void addItem()
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    context.Items.Add(MainForm.currentItem);
                    context.SaveChanges();
                    MessageBox.Show("Добавлен товрар: " + MainForm.currentItem.ToString());
                }

            }
            // отлавливание ошибок
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
        static public void editItem()
        {
            using (var context = new ComputerShopEntities())
            {
                var original = context.Items.Single(x => x.ItemID == MainForm.currentItem.ItemID);

                Category categoryEntry = context.Categories.FirstOrDefault(c => c.Name == MainForm.currentItem.Category.Name);
                Supplier supplierEntry = context.Suppliers.FirstOrDefault(c => c.Name == MainForm.currentItem.Supplier.Name);

                if (original != null)
                {
                    original.Name = MainForm.currentItem.Name;
                    original.Price = MainForm.currentItem.Price;
                    original.Category = categoryEntry;
                    original.Supplier = supplierEntry;
                };
                context.SaveChanges();
                MessageBox.Show(MainForm.currentItem.Name + " обновлен!");
            }
        }

        // удаление товара
        static public void RemoveItem(Item removeEntry)
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    var original = context.Items.Remove(context.Items.Single(a => a.ItemID == removeEntry.ItemID));
                    context.Entry(original).Collection(r => r.OrderItems).CurrentValue = null;
                    context.Entry(original).State = EntityState.Deleted;
                    context.SaveChanges();
                    MessageBox.Show(removeEntry.Name + " удален из базы!");
                }
            }
            catch
            {
                 MessageBox.Show("На товар оформлен заказ!");
            }
        }

        // ITEM SEARCHING

        // поиск товара по имени или ID
        static public List<Item> SearchItemByNameOrID(string itemName = null, int ID = 0)
        {
            // обработка поля поиск в главной форме
            if (ID == 0)
            {
                using (var context = new ComputerShopEntities())
                {
                    var data = context.Items.Where(x => x.Name == itemName).Include("Category").Include("Supplier").ToList<Item>();

                    return data;
                }
            }
            else
            {
                using (var context = new ComputerShopEntities())
                {
                    var data = context.Items.Where(x => x.ItemID == ID).Include("Category").Include("Supplier").Include("OrderItems").ToList<Item>();

                    return data;
                }
            }
        }

        // поиск товара по цене
        static public List<Item> SearchByPrice(decimal priceFrom, decimal priceTo)
        {
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(x => priceTo >= x.Price && x.Price >= priceFrom).Include("Category").Include("Supplier").ToList<Item>();

                return data;
            }
        }

        // поиск товара по категории
        static public List<Item> SearchByCategory(string itemCategory)
        {
            using (var context = new ComputerShopEntities())
            {
                Category category = context.Categories.FirstOrDefault(c => c.Name == itemCategory);
                var data = context.Items.Where(x => x.Category.Name == category.Name).Include("Category").Include("Supplier").ToList<Item>();
                return data;
            }
        }

        // поиск товара по категории и цене
        static public List<Item> SearchByCategoryAndPrice(string itemCategory, decimal priceFrom, decimal priceTo)
        {
            using (var context = new ComputerShopEntities())
            {
                var data = context.Items.Where(s => priceTo >= s.Price && s.Price >= priceFrom && s.Category.Name == itemCategory).Include("Category").Include("Supplier").ToList();
                return data;
            }
        }

        // CATEGORY

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

        // поиск категории
        static public Category SearchCategory(string categoryToFind)
        {
            using (var context = new ComputerShopEntities())
            {
                Category category = context.Categories.FirstOrDefault(c => c.Name == categoryToFind);
                return category;
            }
        }

        // ORDERS

        // создание заказа с товарами и привязанным количеством
        static public void AddOrder()
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    // создание нового Order-a
                    var orderEntry = new Order()
                    {
                        SellerName = MainForm.currentItemOrdersEntities[0].SellerName,
                        OrderDate = Convert.ToDateTime(MainForm.currentItemOrdersEntities[0].OrderDate),
                        Customer = MainForm.currentItemOrdersEntities[0].Customer,
                        CustomerContact = MainForm.currentItemOrdersEntities[0].CustomerContact
                    };
                    context.Orders.Add(orderEntry);
                    context.SaveChanges();
                    int orderId = orderEntry.OrderID;

                    // создание привязанных к созданному Order OrderItems из списка
                    foreach (ItemOrdersEntity ordItem in MainForm.currentItemOrdersEntities)
                    {
                        var orderItemsEntry = new OrderItems()
                        {
                            // нахождение ItemID по имени Item
                            ItemID = DB.SearchItemByNameOrID(itemName:ordItem.Item)[0].ItemID,
                            OrderID = orderId,
                            ItemsQuantity = ordItem.Quantity,
                        };
                        context.OrderItems1.Add(orderItemsEntry);
                        context.SaveChanges();
                    }
                    MessageBox.Show($"Добавлен заказ: ID{orderId} на {MainForm.currentItemOrdersEntities.Count} товара", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        static public void editOrder(int orderID)
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    // поиск Order-a для редактирования
                    var original = context.Orders.Single(x => x.OrderID == orderID);
                    // изменение полей Order-a
                    if (original != null)
                    {
                        original.SellerName = MainForm.currentItemOrderEntity.SellerName;
                        original.OrderDate = Convert.ToDateTime(MainForm.currentItemOrderEntity.OrderDate);
                        original.Customer = MainForm.currentItemOrderEntity.Customer;
                        original.CustomerContact = MainForm.currentItemOrderEntity.CustomerContact;
                    }
                    context.SaveChanges();

                    // редактирование привязанных к созданному Order OrderItems из списка
                    // удаление всех старых сущностей товаров заказа
                    original.OrderItems.Clear();
                    context.SaveChanges();

                    // добавление новых сущностей товаров в список товара заказа
                    foreach (ItemOrdersEntity itOrd in MainForm.currentItemOrdersEntities)
                    {
                        OrderItems temp = new OrderItems();
                        temp.ItemID = SearchItemByNameOrID(itemName: itOrd.Item)[0].ItemID;
                        temp.OrderID = itOrd.OrderID;
                        temp.ItemsQuantity = itOrd.Quantity;
                        original.OrderItems.Add(temp);
                    }
                    context.SaveChanges();
                    MessageBox.Show($"Обновлен заказ: ID{orderID} на {MainForm.currentItemOrdersEntities.Count} товара", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // удаление заказа
        static public void RemoveOrder(ItemOrdersEntity toRemove)
        {
            using (var context = new ComputerShopEntities())
            {
                var original = context.Orders.Find(toRemove.OrderID);
                context.OrderItems1.RemoveRange(original.OrderItems);
                context.Entry(original).State = EntityState.Deleted;
                context.SaveChanges();
                MessageBox.Show("Заказ с ID " + toRemove.OrderID + " удален из базы данных!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // список всех заказов
        static public List<Order> ShowAllOrders()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();

            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.ToList<Order>();
                return data;
            }
        }

        // поиск заказа по ID
        static public Order SearchOrderByID(int orderId)
        {
            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.Find(orderId);
                return data;
            }
        }

        // поиск заказа по продавцу и времени
        static public List<Order> SearchBySellerAndTime(string itemSeller, DateTime dateFrom, DateTime dateTo)
        {

            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.Where(x => dateTo >= x.OrderDate)
                                        .Where(x => x.OrderDate >= dateFrom)
                                        .Where(x => x.SellerName == itemSeller)
                                        .ToList<Order>();

                return data;
            }
        }

        // поиск заказа по времени
        static public List<Order> SearchByTime(DateTime dateFrom, DateTime dateTo)
        {

            using (var context = new ComputerShopEntities())
            {
                var data = context.Orders.Where(x => dateTo >= x.OrderDate)
                                        .Where(x => x.OrderDate >= dateFrom)
                                       .ToList<Order>();

                return data;
            }
        }

        // SUPPLIERS

        // список всех поставщиков
        static public List<Supplier> ShowAllSuppliers()
        {
            ComputerShopEntities dataEntities = new ComputerShopEntities();

            using (var context = new ComputerShopEntities())
            {
                var data = context.Suppliers.ToList<Supplier>();
                return data;
            }
        }

        // добавление поставщика
        static public void AddSupplier(Supplier newSupplier)
        {
            using (var context = new ComputerShopEntities())
            {
                context.Suppliers.Add(newSupplier);
                context.SaveChanges();

                MessageBox.Show($"Добавлен поставщик:  {newSupplier.Name}", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                context.SaveChanges();
            }
        }

        // удаление поставщика
        static public void RemoveSupplier(Supplier toRemove)
        {
            try
            {
                using (var context = new ComputerShopEntities())
                {
                    var original = context.Suppliers.Find(toRemove.SupplierID);
                    context.Items.RemoveRange(original.Items);
                    context.Entry(original).State = EntityState.Deleted;
                    context.SaveChanges();
                    MessageBox.Show(toRemove.Name + " удален!");
                }
        }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                MessageBox.Show("Невозможно удалить, к поставщику привязаны товары!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        // редактирование поставщика
        static public void editSupplier(Supplier toEdit)
        {
            using (var context = new ComputerShopEntities())
            {
                var original = context.Suppliers.Find(toEdit.SupplierID);
                context.Entry(original).CurrentValues.SetValues(toEdit);
                context.SaveChanges();
                MessageBox.Show($"Поставщик {toEdit.Name} обновлен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // поиск поставщика по имени
        static public List<Supplier> SearchBySupplier(string itemSupplier)
        {
            using (var context = new ComputerShopEntities())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(c => c.Name == itemSupplier);
                var data = context.Suppliers.Where(x => x.Name == itemSupplier).ToList<Supplier>();

                return data;
            }
        }

        // поиск поставщика 
        static public Supplier SearchSupplier(int supplierID=0, string supplierName=null)
        {
            if (supplierName == null)
            {
                using (var context = new ComputerShopEntities())
                {
                    Supplier supplier = context.Suppliers.Find(supplierID);
                    return supplier;
                }
            }

            using (var context = new ComputerShopEntities())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(c => c.Name == supplierName);
                return supplier;
            }
        }

        // SELLER

        // создание списка продавцов
        static public List<string> AllSellers()
        {
            List<string> sellers = new List<string>();
            foreach (Order ord in ShowAllOrders())
            {
                if(!sellers.Contains(ord.SellerName))
                {
                    sellers.Add(ord.SellerName);
                }
            }
            return sellers;
        }
    }
}
