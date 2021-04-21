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

        public decimal Quantity
        { get; set; }

        public string OrderDate
        { get; set; }

        public string SellerName
        { get; set; }
        
        public string Customer
        { get; set; }

        public string CustomerContact
        { get; set; }

        public ItemOrdersEntity(string item, string orderID, int quantity, string orderDate, string sellerName, string customer, string customerContact)
        {
            Item = item;
            OrderID = orderID;
            Quantity = quantity;
            OrderDate = orderDate;
            SellerName = sellerName;
            Customer = customer;
            CustomerContact = customerContact;

           
        }
    }
}
