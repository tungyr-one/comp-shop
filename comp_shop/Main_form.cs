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

// LINKS
//https://www.entityframeworktutorial.net/
//http://csharp.net-informations.com/datagridview/csharp-datagridview-add-column.htm
//https://www.entityframeworktutorial.net/entity-relationships.aspx
//https://www.entityframeworktutorial.net/basics/entity-in-entityframework.aspx#reference-navigation-property
//https://entityframework.net/include-with-where-clause
// https://www.tutorialsteacher.com/linq/linq-query-syntax

// disable lazy loading EF:

//public partial class ComputerShopEntities : DbContext
//{
//    public ComputerShopEntities()
//        : base("name=ComputerShopEntities")
//    {
//        this.Configuration.LazyLoadingEnabled = false;
//    }

namespace comp_shop
{
    public partial class Main_form : Form
    {
        Article current_item = new Article();
        List<Item> search_result;

        public Main_form()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        //// нажатие кнопки добавить
        private void addItem_Click(object sender, EventArgs e)
        {
            NewItemForm new_item_form = new NewItemForm();
            new_item_form.ShowDialog();
            DB.ShowAllItems();
        }


        

        // нажатие кнопки редактировать
        private void editItem_Click(object sender, EventArgs e)
        {
            // открыте формы изменения товара
            NewItemForm new_item_form = new NewItemForm(false);
            // передача в форму редактирования выбранной сущности
            new_item_form.workingItem = current_item;
            new_item_form.ShowDialog();            
            DB.ShowAllItems();

        }

        // нажатие кнопки поиск
        private void find_Click(object sender, EventArgs e)
        {
            
            // TODO: text fields validation first
            if (radioButton1.Checked)
            {

                dataGridView1.DataSource = DB.SearchItemByName(searchBox1.Text);

                #region
                //// ЗАПИСЬ С ПОДТЯГИВАНИЕМ ИНФЫ ИЗ ДРУГИХ ТАБЛИЦ
                //using (var context = new ComputerShopEntities())
                //{
                //    var data = (from item in context.Items
                //        join category in context.Categories on item.CategoryID equals category.CategoryID
                //        join supplier in context.Suppliers on item.SupplierID equals supplier.SupplierID
                //             where item.Name == searchBox1.Text
                //             select new
                //             {
                //                 ItemId = item.ItemID,
                //                 ItemName = item.Name,
                //                 ItemPrice = item.Price,
                //                 ItemSeller = item.Seller,
                //                 CategoryName = category.Name,
                //                 SupplierName = supplier.Name
                //             }).ToList();

                //    dataGridView1.DataSource = data;
                //}

                //// найти одну запись без подтягивания записей из других таблиц
                //using (var context = new ComputerShopEntities())
                //{
                //    var data = (from d in context.Items
                //                where d.Name == searchBox1.Text
                //                select d).ToList();
                //    dataGridView1.DataSource = data;

                //}



                //using (var db = new ComputerShopEntities())
                //{
                //    var data = (from item in db.Items.Where(s => s.Name == searchBox1.Text).ToList()                            
                //                select new
                //                {
                //                    item.ItemID,
                //                    ItemName = item.Name,
                //                    ItemPrice = item.Price,
                //                    ItemSeller = item.Seller,
                //                }).ToList();
                //    dataGridView1.DataSource = data;
                //    return;
                //}
                #endregion

            }

            if (radioButton2.Checked)
                try
                {
                    dataGridView1.DataSource = DB.SearchByPrice(decimal.Parse(searchBox1.Text), decimal.Parse(searchBox2.Text));

                }
                catch
                {
                    MessageBox.Show("Неправильная цена!");
                }

            // поиск по категории
            if (radioButton3.Checked)
                dataGridView1.DataSource = DB.SearchByCategory(searchBox1.Text);
            //try
            //    {
            //        //List<Category> search_result;
            //        dataGridView1.DataSource = DB.SearchByCategory(searchBox1.Text);

            //    }
            //    catch
            //    {
            //        MessageBox.Show("Неправильная категория!");
            //    }

            if (radioButton4.Checked)
                try
                {
                    dataGridView1.DataSource = DB.SearchByCategoryAndPrice(searchBox1.Text, decimal.Parse(searchBox2.Text), decimal.Parse(searchBox3.Text));
                }
                catch
                {
                    MessageBox.Show("Неправильная категория или цена!");
                }

            if (radioButton5.Checked)
            {
                dataGridView1.DataSource = DB.SearchBySeller(searchBox1.Text);
            }

            if (radioButton6.Checked)
            {
                dataGridView1.DataSource = DB.SearchBySupplier(searchBox1.Text);
            }

            //dataGridView1.DataSource = search_result;
        }


        // выбор чекбоксов
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            searchParam3.Visible = false;
            searchBox3.Visible = false;

            searchParam2.Visible = false;
            searchBox2.Visible = false;

            if (radioButton1.Checked)
            {
                searchParam1.Text = "Название товара:";
            }

            if (radioButton2.Checked)
            {
                // TODO: поля увеличивать и смещать левее, координаты как установить?
                searchParam1.Text = "Цена от: ";
                searchParam2.Text = "Цена до: ";
                searchParam2.Visible = true;
                searchBox2.Visible = true;
            }

            if (radioButton3.Checked)
            {
                // TODO: Сделать комбо категории товара
                searchParam1.Text = "Категория товара:";
            }

            if (radioButton4.Checked)
            {
                // TODO: Сделать комбо категории товара
                searchParam1.Text = "Категория: ";
                searchParam2.Text = "Цена от:";
                searchParam2.Visible = true;
                searchBox2.Visible = true;
                searchParam3.Visible = true;
                searchBox3.Visible = true;
            }

            if (radioButton5.Checked)
            {
                searchParam1.Text = "Продавец:";
            }

            if (radioButton6.Checked)
            {
                searchParam1.Text = "Поставщик:";
            }
        }

        // проперти текущего товара
        public Article ItemGetter
        {
            get { return current_item; }
        }


        // https://www.entityframeworktutorial.net/basics/how-entity-framework-works.aspx


        private void Main_form_Load(object sender, EventArgs e)
        {
            //dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = DB.ShowAllItems();

        }

        // отобразить все записи
        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00##";
            dataGridView1.DataSource = DB.ShowAllItems();
        }

        private void Main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // обработка нажатия Enter
        private void Main_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findButton.PerformClick();
            }
        }

        private void searchBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findButton.PerformClick();
            }
        }

        private void searchBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findButton.PerformClick();
            }
        }

        private void searchBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findButton.PerformClick();
            }
        }


        private void searchBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findButton.PerformClick();
            }
        }

        //нажатие кнопки удалить
        private void button2_Click(object sender, EventArgs e)
        {
            DB.RemoveItem(current_item);
        }

        //изменение выбора строки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // присваивание текущему обрабатываемому товару имена из выбранного элемента в DataGridView
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                current_item.ArticleId = Convert.ToInt32(row.Cells[0].Value);
                current_item.ArticleName = row.Cells[1].Value.ToString();
                current_item.ArticlePrice = Convert.ToDecimal(row.Cells[2].Value);
                current_item.ArticleSeller = row.Cells[3].Value.ToString();
                current_item.ArticleCategory = row.Cells[4].Value.ToString();
                current_item.ArticleSupplier = row.Cells[5].Value.ToString();
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                string value4 = row.Cells[3].Value.ToString();
                string value5 = row.Cells[4].Value.ToString();
                string value6 = row.Cells[5].Value.ToString();
                toolStripStatusLabel1.Text = "Выбрано: " + value1 + " - " + value2 + " - " + value3 + " - " + value4 + " - " + value5 + " - " + value6;
            }
        }
    }
}
