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

        public int ArticleId
        { get; set; }

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


        public Article()
        {
        }

        public Article(string name, decimal price, string category, string seller, string supplier, int id = 0)
        {
            ArticleId = id;
            ArticleName = name;
            ArticlePrice = price;
            ArticleCategory = category;
            ArticleSeller = seller;
            ArticleSupplier = supplier;            
            //DB.addToDB(this.DBFormat());
            //items.Add(this);

        }

        public string DBFormat()
        {
            string str = ArticleId + '-' + ArticleName + '-' + ArticlePrice.ToString() + '-' + ArticleCategory + '-' + ArticleSupplier + '-' + ArticleSeller + '\n';
            return str;
        }

        public override string ToString()
        {          
            string item_string = ($"Id: {ArticleId} " + 
                $"\nНазвание: {ArticleName} " +
             $"\nЦена, руб: {ArticlePrice}" +
             $"\nКатегория: {ArticleCategory}" +
             $"\nПродавец: {ArticleSeller}" +
             $"\nПоставщик: {ArticleSupplier}" +
             "\n---------------------------------\n");

            return item_string;
        }




    }
}
