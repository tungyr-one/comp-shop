using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    public class Article
    {

        //private List<Items> items = new List<Items>();

        public int Id
        { get; set; }

        public string Name
        { get; set; }

        public decimal Price
        { get; set; }

        public string Category
        { get; set; }

        public string Sellers
        { get; set; }

        public string Supplier
        { get; set; }


        public Article()
        {
        }

        public Article(string name, decimal price, string category, string supplier, int id = 0, string seller=null)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            Sellers = seller;
            Supplier = supplier;            
            //DB.addToDB(this.DBFormat());
            //items.Add(this);

        }

        public string DBFormat()
        {
            string str = Id + '-' + Name + '-' + Price.ToString() + '-' + Category + '-' + Supplier + '-' + Sellers + '\n';
            return str;
        }

        public override string ToString()
        {          
            string item_string = ($"Id: {Id} " + 
                $"\nНазвание: {Name} " +
             $"\nЦена, руб: {Price}" +
             $"\nКатегория: {Category}" +
             $"\nПродавец: {Sellers}" +
             $"\nПоставщик: {Supplier}" +
             "\n---------------------------------\n");

            return item_string;
        }




    }
}
