using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    // TODO: создать просто поле с Order и список OrderItems вместо этого колхоза
    public class ItemOrdersEntity
    {
        public string Item
        { get; set; }

        public int OrderID
        { get; set; }

        public int Quantity
        { get; set; }

        public string OrderDate
        { get; set; }

        public string SellerName
        { get; set; }
        
        public string Customer
        { get; set; }

        public string CustomerContact
        { get; set; }

        public string Category
        { get; set; }

        public ItemOrdersEntity()
        {
        }

        // TODO: также копировать список связанных товаров-заказов при создании

        public ItemOrdersEntity(int orderID, string orderDate, string sellerName, string customer, string customerContact, string item = null, int quantity = 0, string category = null)
        {
            Item = item;
            OrderID = orderID;
            Quantity = quantity;
            OrderDate = orderDate;
            SellerName = sellerName;
            Customer = customer;
            CustomerContact = customerContact;
            Category = category;
           
        }

        // конструктор копирования
        public ItemOrdersEntity(ItemOrdersEntity obj)
        {
            this.Item = obj.Item;
            this.OrderID = obj.OrderID;
            this.Quantity = obj.Quantity;
            this.OrderDate = obj.OrderDate;
            this.SellerName = obj.SellerName;
            this.Customer = obj.Customer;
            this.CustomerContact = obj.CustomerContact;
            this.Category = obj.Category;
        }
    }
}
