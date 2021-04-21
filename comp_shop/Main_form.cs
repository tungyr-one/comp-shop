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
        //Article current_item = new Article();
        // сущности для обмена данными с формами добавления / редактирования сущносстей
        Item current_item = new Item();

        // списки для обмена данными с формами связанных данных
        List<Item> itemsConnectedData = new List<Item>();
        List<ItemOrdersEntity> ordersConnectedData = new List<ItemOrdersEntity>();

        ComputerShopEntities context = new ComputerShopEntities();

        public Main_form()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }


        // загрузка формы
        private void Main_form_Load(object sender, EventArgs e)
        {

            radioButton5.Enabled = false;
            radioButton6.Enabled = false;

            dateTimePicker1.Enabled = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker2.Visible = false;


            // отключение кнопок поиска по продавцу и поставщику

            dataGridView1.DataSource = DB.ShowAllItems();
            dataGridView2.DataSource = DB.ShowAllOrders();
            dataGridView3.DataSource = DB.ShowAllSuppliers();
            // загрузка списка заказов в DataGridView
            DataGridViewOrdersForItems();
            DataGridViewItemsForSuppliers();
            DataGridViewItemsForOrders();
            //DB.LoadAllStuff(1);

            //DataGridViewComboBoxColumn orders =
            //dataGridView1.Columns[6] as DataGridViewComboBoxColumn;
            //orders.DataSource = comboboxOrders;
            //orders.ValueType = typeof(Order);

            //orderBindingSource1.DataSource = context.Orders.ToList();
        }


        //COMBOBOX загрузка дополнительной информации o заказах в DataGridView1 
        //private void DataGridViewOrdersForItems()
        //{
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        List<Order> ordersData = DB.OrdersForDataGridView1(Convert.ToInt32(row.Cells[0].Value));
        //        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)(row.Cells["Orders"]);
        //        cell.DataSource = ordersData;
        //        // демонстрация первого заказа в combobox если он есть
        //        if (ordersData.Count > 0)
        //        cell.Value = ordersData[0];
        //    }
        //}

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


        //добавление кнопок для вызова дополнительной информации о продажах
        private void DataGridViewOrdersForItems()
        {
            
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Orders";
            buttonColumn.Name = "Show Orders";
            buttonColumn.Text = "Show Orders";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(buttonColumn);
        }

        //добавление кнопок для вызова дополнительной информации о заказанных товарах и их количестве
        private void DataGridViewItemsForOrders()
        {
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Items";
            buttonColumn.Name = "Show items";
            buttonColumn.Text = "Show items";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView2.Columns.Add(buttonColumn);
        }

        // //добавление кнопок для вызова дополнительной информации о товарах
        private void DataGridViewItemsForSuppliers()
        {
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Items";
            buttonColumn.Name = "Show items";
            buttonColumn.Text = "Show items";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView3.Columns.Add(buttonColumn);
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

                dataGridView1.DataSource = DB.SearchItemByNameOrID(searchBox1.Text);

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

            // поиск по цене
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

            // поиск по категории и цене
            if (radioButton4.Checked)
                try
                {
                    dataGridView1.DataSource = DB.SearchByCategoryAndPrice(searchBox1.Text, decimal.Parse(searchBox2.Text), decimal.Parse(searchBox3.Text));
                }
                catch
                {
                    MessageBox.Show("Неправильная категория или цена!");
                }

            // поиск по продажам, продавцам и времени
            if (radioButton5.Checked)
            {
                // поиск только по времени если поле имени продавца пустое
                if (searchBox1.Text == "")
                {
                    dataGridView2.DataSource = DB.SearchByTime(dateTimePicker1.Value, dateTimePicker2.Value);
                }
                else
                dataGridView2.DataSource = DB.SearchBySellerAndTime(searchBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            }

            // поиск по поставщику
            if (radioButton6.Checked)
            {
                dataGridView3.DataSource = DB.SearchBySupplier(searchBox1.Text);
            }
        }


        // обработка выбора чекбоксов
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
                searchParam2.Text = "Период от:";
                searchParam3.Text = "Период до:";

            }

            if (radioButton6.Checked)
            {
                searchParam1.Text = "Поставщик:";
            }
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
            //DB.RemoveItem(current_item);
        }

        //изменение выбора строки в товарах
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // присваивание текущей обрабатываемой сущности имени из выбранного элемента в DataGridView
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                current_item = DB.SearchItemByNameOrID(ID: Convert.ToInt32(row.Cells[0].Value))[0];
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                string value4 = row.Cells[3].Value.ToString();
                string value5 = row.Cells[4].Value.ToString();
                //string value6 = row.Cells[5].Value.ToString();
                toolStripStatusLabel1.Text = "Выбрано: " + value1 + " - " + value2 + " - " + value3 + " - " + value4 + " - " + value5;
            }


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

        // нажатие кнопки show orders в ячейках закладки товары
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            // проверка нажатия кнопки в ячейке
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                AssociatedInfo connInfoForm = new AssociatedInfo();
                ordersConnectedData = DB.OrdersForDataGridView1(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                connInfoForm.ordersToItems = ordersConnectedData;
                connInfoForm.ShowDialog();
            }
        }

        // нажатие кнопки show items в закладке продажи
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                AssociatedInfo connInfoForm = new AssociatedInfo();
                ordersConnectedData = DB.ItemsForDataGridView2(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value));
                connInfoForm.ordersToItems = ordersConnectedData;
                connInfoForm.ShowDialog();
            }
        }

        // нажатие кнопки show items в закладке поставщики
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                AssociatedInfo connInfoForm = new AssociatedInfo();
                itemsConnectedData = DB.ItemsToList(Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value));
                connInfoForm.itemsToSupplier = itemsConnectedData;
                connInfoForm.ShowDialog();
            }

        }

        // обработка переключения вкладок
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                // включение первой радиокнопки
                radioButton1.Checked = true;
// отключение первых пяти радиокнопок
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;

                searchParam2.Visible = false;
                searchParam3.Visible = false;

                dateTimePicker1.Enabled = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Visible = false;
            }
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = true;
                radioButton5.Checked = true;
                radioButton6.Enabled = false;

                searchParam2.Visible = true;
                searchParam3.Visible = true;

                dateTimePicker1.Enabled = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.Visible = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = true;
                radioButton6.Checked = true;

                searchParam2.Visible = false;
                searchParam3.Visible = false;

                dateTimePicker1.Enabled = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Visible = false;
            }
        }


    }
}
