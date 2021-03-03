using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Требования к функциональным характеристикам:
//-	программа должна обеспечивать доступ к информации о товарах магазина компьютерной техники, 
//    содержащейся в БД;
//-	программа должна выдавать информацию о категориях товаров, о поставщиках, 
//    продавцах и других записях в БД;
//-	программа должна обеспечивать поиск по информации в БД в соответствии с 
//    различными критериями поиска:
//a)	нужного товара по его названию; 
//товаров по диапазону цен;
//товаров по категории;
//товаров по категории и по диапазону цен;
//b)	по какой цене и сколько товара продал каждый продавец за указанный интервал времени; 
//какие товары поставляет определенный поставщик.
//-  программа должна обладать формой пользовательского интерфейса для ввода 
//    информации об объектах и корректировки данных в случае необходимости;

namespace comp_shop
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            Form new_item = new NewItemForm();
            new_item.ShowDialog();
        }
    }
}
