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
        //Item current_item = new Item();
        List<Item> itemsConnectedData = new List<Item>();

        ComputerShopEntities context = new ComputerShopEntities();

        public Main_form()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }


        // загрузка формы
        private void Main_form_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DB.ShowAllItems();
            dataGridView2.DataSource = DB.ShowAllOrders();
            dataGridView3.DataSource = DB.ShowAllSuppliers();
            // загрузка списка заказов в DataGridView
            DataGridViewOrdersForItems();
            DataGridViewItemsForSuppliers();
            //DB.LoadAllStuff(1);

            //DataGridViewComboBoxColumn orders =
            //dataGridView1.Columns[6] as DataGridViewComboBoxColumn;
            //orders.DataSource = comboboxOrders;
            //orders.ValueType = typeof(Order);

            //orderBindingSource1.DataSource = context.Orders.ToList();
        }


        //COMBOBOX загрузка дополнительной информации o заказах в DataGridView1 
        private void DataGridViewOrdersForItems()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                List<Order> ordersData = DB.OrdersToList(Convert.ToInt32(row.Cells[0].Value));
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)(row.Cells["Orders"]);
                cell.DataSource = ordersData;
                // демонстрация первого заказа в combobox если он есть
                if (ordersData.Count > 0)
                cell.Value = ordersData[0];
            }
        }

        //STRING загрузка дополнительной информации o связанных заказах в DataGridView1 
        //private void DataGridViewOrdersForItems()
        //{
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        row.Cells[dataGridView1.Columns["Orders"].Index].Value = DB.OrdersToString(Convert.ToInt32(row.Cells[0].Value));
        //    }
        //}


        //STRING загрузка дополнительной информации o связанных заказах в DataGridView3
        //private void DataGridViewItemsForSuppliers()
        //{
        //    foreach (DataGridViewRow row in dataGridView3.Rows)
        //    {
        //        row.Cells[dataGridView3.Columns["Items"].Index].Value = DB.ItemsToString(Convert.ToInt32(row.Cells[0].Value));
        //        MessageBox.Show(DB.ItemsToString(Convert.ToInt32(row.Cells[0].Value)));
        //    }
        //}


        //загрузка информации о связанных товарах DataGridView3
        private void DataGridViewItemsForSuppliers()
        {
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Items";
            buttonColumn.Name = "Status Request";
            buttonColumn.Text = "Show items";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView3.Columns.Add(buttonColumn);
            // получаем данные о товарах поставщика                    

            //dataGridView3.CellClick +=
            //new DataGridViewCellEventHandler(dataGridView3_CellClick);
        }

        // отобразить все записи
        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00##";
            dataGridView1.DataSource = DB.ShowAllItems();
            dataGridView2.DataSource = DB.ShowAllOrders();
            dataGridView3.DataSource = DB.ShowAllSuppliers();

            // загрузка списка продавцов в DataGridView
            //DataGridViewSellersForItems();

            // TODO: временная проверка удалить
            List<Item> check = DB.ShowAllItems();
            foreach (Item ord in check)
            {
                label2.Text += ord + " - ";
            }

        }




        // нажатие кнопки добавить
        private void addItem_Click(object sender, EventArgs e)
        {
            NewItemForm new_item_form = new NewItemForm();
            new_item_form.ShowDialog();
            //обновление списка товаров
            dataGridView1.DataSource = DB.ShowAllItems();
        }


        // нажатие кнопки редактировать
        private void editItem_Click(object sender, EventArgs e)
        {
            // открыте формы изменения товара
            NewItemForm new_item_form = new NewItemForm(false);
            // передача в форму редактирования выбранной сущности
            new_item_form.workingItem = current_item;
            new_item_form.ShowDialog();
            //обновление списка товаров
            dataGridView1.DataSource = DB.ShowAllItems();

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
                //dataGridView1.DataSource = DB.SearchBySeller(searchBox1.Text);
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
        //public Article ItemGetter
        //{
        //    get { return current_item; }
        //}




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

            // присваивание текущему обрабатываемому товару имен из выбранного элемента в DataGridView
            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    //current_item.Id = Convert.ToInt32(row.Cells[0].Value);
            //    //current_item.Name = row.Cells[1].Value.ToString();
            //    //current_item.Price = Convert.ToDecimal(row.Cells[2].Value);                
            //    //current_item.Category = row.Cells[3].Value.ToString();
            //    //current_item.Supplier = row.Cells[4].Value.ToString();
            //    //current_item.Sellers = row.Cells[5].Value.ToString();
            //    // TODO: заменить Article на Item повсюду
            //    //current_item.Category = context.Categories.FirstOrDefault(c => c.Name == row.Cells[4].Value);

            //    //DB.ShowByName(current_item);
            //}

            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    string value1 = row.Cells[0].Value.ToString();
            //    string value2 = row.Cells[1].Value.ToString();
            //    string value3 = row.Cells[2].Value.ToString();
            //    string value4 = row.Cells[3].Value.ToString();
            //    string value5 = row.Cells[4].Value.ToString();
            //    //string value6 = row.Cells[5].Value.ToString();
            //    toolStripStatusLabel1.Text = "Выбрано: " + value1 + " - " + value2 + " - " + value3 + " - " + value4 + " - " + value5;
            //}


        }


        // не показывает ошибку, хотя она присутствует
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ////This event is used to avoid the error of DataGridviewCombobox Cell
            //if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            //{

            //    object value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //    if (!((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Contains(value))
            //    {
            //        ((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Add(value); e.ThrowException = false;
            //    }
            //}
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                ConnectedInfo connInfoForm = new ConnectedInfo();
                itemsConnectedData = DB.ItemsToList(Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value));
                connInfoForm.itemsToSupplier = itemsConnectedData;
                connInfoForm.ShowDialog();
            }

        }
    }
}
