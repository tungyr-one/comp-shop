using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    public class ItemOrdersEntity
    {
        public string Item
        { get; set; }

        public string OrderID
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

        public ItemOrdersEntity(string item, string orderID, int quantity, string orderDate, string sellerName, string customer, string customerContact, string category)
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
    }
}
