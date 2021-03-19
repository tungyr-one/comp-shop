using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    public class Article
    {
        private string name;
        private decimal price;
        private string category;
        private string seller;
        private string supplier;
        //private List<Item> items = new List<Item>();

        public Article()
        {
        }

        public Article(string name, decimal price, string category, string seller, string supplier)
        {
            this.name = name;
            this.price = price;
            this.category = category;
            this.seller = seller;
            this.supplier = supplier;            
            //DB.addToDB(this.DBFormat());
            //items.Add(this);

        }

        public string DBFormat()
        {
            string str = this.name + '-' + this.price.ToString() + '-' + this.category + '-' + this.supplier + '-' + this.seller + '\n';
            return str;
        }

        public override string ToString()
        {          

            string item_string = ($"Название: {name} " +
             $"\nЦена, руб: {price}" +
             $"\nКатегория: {category}" +
             $"\nПродавец: {seller}" +
             $"\nПоставщик: {supplier}" +
             "\n---------------------------------\n");

            return item_string;
        }


        public string ArticleName
        { get; set; }

        public decimal ArticlePrice
        { get; set; }

        public string ArticleCategory
        { get; set; }

        public string ArticleSeller
        { get; set; }

        public string ArticleSupplier
        { get; set; }

    }
}
