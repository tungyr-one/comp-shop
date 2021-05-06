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
    public partial class MainForm : Form
    {
        // сущности для обмена данными с формами добавления / редактирования сущносстей
        public static Item currentItem = new Item();
        public static ItemOrdersEntity currentItemOrderEntity = new ItemOrdersEntity();
        public static Supplier currentSupplier = new Supplier();

        // списки для обмена данными с формами связанных данных
        public static List<Item> currentItems = new List<Item>();
        public static List<ItemOrdersEntity> currentItemOrdersEntities = new List<ItemOrdersEntity>();

        public MainForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        public void AppEnter()
        {
            entryForm newEntryForm = new entryForm();
            newEntryForm.ShowDialog();
            if (newEntryForm.login == "exitApp")
            {
                this.Close();
            }
        }

        // загрузка формы
        private void Main_form_Load(object sender, EventArgs e)
        {
            AppEnter();
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;

            dateTimePicker1.Enabled = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker2.Visible = false;

            UpdateMainForm();

            // добавление кнопок для вызова дополнительной информации
            DataGridViewOrdersForItems();
            DataGridViewItemsForSuppliers();
            DataGridViewItemsForOrders();
        }

        // обновление данных всех таблиц
        public void UpdateMainForm()
        {
            dataGridView1.DataSource = DB.ShowAllItems();
            dataGridView2.DataSource = DB.ShowAllOrders();
            dataGridView3.DataSource = DB.ShowAllSuppliers();

            // настройка ширины колонок
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();            
            
            // настройка ширины колонок
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoResizeColumns();            
            
            // настройка ширины колонок
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoResizeColumns();
        }

        //добавление кнопок в DataGridView1 для вызова дополнительной информации о продажах
        private void DataGridViewOrdersForItems()
        {
            
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Заказы";
            buttonColumn.Name = "Показать заказы";
            buttonColumn.Text = "Показать заказы";
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns.Add(buttonColumn);
        }

        //добавление кнопок в DataGridView2 для вызова дополнительной информации о заказанных товарах и их количестве
        private void DataGridViewItemsForOrders()
        {
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Товары";
            buttonColumn.Name = "Показать товары";
            buttonColumn.Text = "Показать товары";
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView2.Columns.Add(buttonColumn);
        }

        // //добавление кнопок в DataGridView3 для вызова дополнительной информации о товарах поставщика
        private void DataGridViewItemsForSuppliers()
        {
            DataGridViewButtonColumn buttonColumn =
                        new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Товары";
            buttonColumn.Name = "Показать товары";
            buttonColumn.Text = "Показать товары";
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView3.Columns.Add(buttonColumn);
        }

        // загрузка всех записей во всех вкладках
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateMainForm();
        }

        // нажатие кнопки добавить 
        private void Add_Click(object sender, EventArgs e)
        {
            // вкладка товары
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                // новый экземпляр окна операций с товаром
                ItemOperationForm newItemForm = new ItemOperationForm()
                {
                    Text = "Создание нового товара"
                };
                newItemForm.ShowDialog();
            }
            // вкладка заказы
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                // удаление всех предыдущих значений в списке для нового заказа
                currentItemOrdersEntities.Clear();
                // новый экземпляр окна операций с заказом
                OrderOperationForm orderForm = new OrderOperationForm
                {
                    Text = "Создание нового заказа"
                };
                orderForm.ShowDialog();
            }
            // вкладка поставщики
            else
            {
                // новый экземпляр окна операций с поставщиком
                SupplierOperationForm supplierForm = new SupplierOperationForm
                {
                    Text = "Новый поставщик"
                };
                supplierForm.ShowDialog();
            }
            UpdateMainForm();
        }

        // нажатие кнопки редактировать
        private void editItem_Click(object sender, EventArgs e)
        {
            // товар
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                // открыте формы изменения товара
                ItemOperationForm editItemForm = new ItemOperationForm(false);
                editItemForm.Text = "Редактирование товара";
                editItemForm.ShowDialog();
            }
            //заказ
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                // удаление всех предыдущих значений в списке для редактируемого заказа
                currentItemOrdersEntities.Clear();
                OrderOperationForm orderForm = new OrderOperationForm();
                orderForm.Text = "Редактирование заказа";
                orderForm.ShowDialog();
            }
            // вкладка поставщики
            else
            {
                // открыте формы изменения поставщика
                SupplierOperationForm supplierForm = new SupplierOperationForm
                {
                    Text = "Изменить поставщика"
                };
                supplierForm.ShowDialog();
            }
            UpdateMainForm();
        }

        //нажатие кнопки удалить
        private void button2_Click(object sender, EventArgs e)
        {
            // вкладка товары
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                DB.RemoveItem(currentItem);
            }
            // вкладка заказы
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                DB.RemoveOrder(currentItemOrderEntity);
            }
            // вкладка поставщики
            else
            {
                DB.RemoveSupplier(currentSupplier);
            }
            UpdateMainForm();
        }

        // нажатие кнопки show orders в ячейках закладки товары
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            // проверка нажатия кнопки в ячейке
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                // загрузка формы демонстрции заказов товара
                ShowInfoForm ordersForItemForm = new ShowInfoForm();
                currentItemOrdersEntities = DB.OrdersOfItem(MainForm.currentItem.ItemID);
                ordersForItemForm.Text = "Заказы товара";
                ordersForItemForm.ShowDialog();
            }
        }

        // нажатие кнопки show items в закладке продажи
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            // проверка нажатия кнопки в ячейке
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                // загрузка формы демонстрции товаров заказа
                ShowInfoForm connInfoForm = new ShowInfoForm();
                currentItemOrdersEntities = DB.LoadItemOrdersEntities(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value));
                connInfoForm.Text = "Товары в заказе";
                connInfoForm.ShowDialog();
            }
        }

        // нажатие кнопки show items в закладке поставщики
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            // проверка нажатия кнопки в ячейке
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                // загрузка формы демонстрции товаров поставщика
                ShowInfoForm connInfoForm = new ShowInfoForm();
                currentItems = DB.ItemsToList(Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value));
                connInfoForm.Text = "Товары поставщика";
                connInfoForm.ShowDialog();
            }
        }


        // нажатие кнопки поиск
        private void find_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (searchBox1.Text == "")
                {
                    MessageBox.Show("Неправильное название!","Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dataGridView1.DataSource = DB.SearchItemByNameOrID(searchBox1.Text);
                }
            }

            // поиск по цене
            if (radioButton2.Checked)
            {
                try
                {
                    dataGridView1.DataSource = DB.SearchByPrice(decimal.Parse(searchBox1.Text), decimal.Parse(searchBox2.Text));
                }
                catch
                {
                    MessageBox.Show("Неправильная цена!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // поиск по категории
            if (radioButton3.Checked)
            {
                try
                {
                    dataGridView1.DataSource = DB.SearchByCategory(searchBox1.Text);

                }
                catch
                {
                    MessageBox.Show("Неправильная категория!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // поиск по категории и цене
            if (radioButton4.Checked)
            {
                try
                {
                    dataGridView1.DataSource = DB.SearchByCategoryAndPrice(searchBox1.Text, decimal.Parse(searchBox2.Text), decimal.Parse(searchBox3.Text));
                }
                catch
                {
                    MessageBox.Show("Неправильная категория или цена!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                {
                    dataGridView2.DataSource = DB.SearchBySellerAndTime(searchBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
                }
            }

            // поиск по поставщику
            if (radioButton6.Checked)
            {
                if (searchBox1.Text == "")
                {
                    MessageBox.Show("Неправильное имя поставщика!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dataGridView3.DataSource = DB.SearchBySupplier(searchBox1.Text);
                }
            }
        }


        // обработка выбора чекбоксов
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            searchParam3.Visible = false;
            searchBox3.Visible = false;

            searchParam2.Visible = false;
            searchBox2.Visible = false;

            // если нажата кнопка поиска по названию товара
            if (radioButton1.Checked)
            {
                searchParam1.Text = "Название товара:";
            }

            // если нажата кнопка поиска по цене товара
            if (radioButton2.Checked)
            {
                searchParam1.Text = "Цена от: ";
                searchParam2.Text = "Цена до: ";
                searchParam2.Visible = true;
                searchBox2.Visible = true;
            }

            // если нажата кнопка поиска по категории товара
            if (radioButton3.Checked)
            {
                searchParam1.Text = "Категория товара:";
            }

            // если нажата кнопка поиска по категории и цене товара
            if (radioButton4.Checked)
            {
                searchParam1.Text = "Категория: ";
                searchParam2.Text = "Цена от:";
                searchParam3.Text = "Цена до:";
                searchParam2.Visible = true;
                searchBox2.Visible = true;
                searchParam3.Visible = true;
                searchBox3.Visible = true;
            }

            // если нажата кнопка поиска по продавцу товара
            if (radioButton5.Checked)
            {
                searchParam1.Text = "Продавец:";
                searchParam2.Text = "Период от:";
                searchParam3.Text = "Период до:";

            }

            // если нажата кнопка поиска по поставщику товара
            if (radioButton6.Checked)
            {
                searchParam1.Text = "Поставщик:";
            }
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


        //изменение выбора строки в товарах
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // присваивание текущей обрабатываемой сущности товара имени из выбранного элемента в DataGridView1
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                currentItem = DB.SearchItemByNameOrID(ID: Convert.ToInt32(row.Cells[0].Value))[0];
            }

            // вывод текущего товара в статусную строку
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                string value4 = row.Cells[3].Value.ToString();
                string value5 = row.Cells[4].Value.ToString();
                toolStripStatusLabel1.Text = "Выбрано: " + value1 + " - " + value2 + " - " + value3 + " - " + value4 + " - " + value5;
            }
        }

        //изменение выбора строки в заказах
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            // присваивание текущей обрабатываемой сущности заказа имени из выбранного элемента в DataGridView2
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                // поиск выделенного заказа в БД
                var selectedOrder = DB.SearchOrderByID(Convert.ToInt32(row.Cells[0].Value));
                currentItemOrderEntity.OrderID = selectedOrder.OrderID;
                currentItemOrderEntity.SellerName = selectedOrder.Seller.ToString();
                currentItemOrderEntity.OrderDate = selectedOrder.OrderDate.ToString();
                currentItemOrderEntity.Customer = selectedOrder.Customer;
                currentItemOrderEntity.CustomerContact = selectedOrder.CustomerContact;
            }

            // вывод текущего заказа в статусную строку
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                string value4 = row.Cells[3].Value.ToString();
                string value5 = row.Cells[4].Value.ToString();
                toolStripStatusLabel1.Text = "Выбрано: " + value1 + " - " + value2 + " - " + value3 + " - " + value4 + " - " + value5;
            }
        }

        //изменение выбора строки в поставщиках
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            // присваивание текущей обрабатываемой сущности поставщика имени из выбранного элемента в DataGridView3
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                // поиск выделенного поставщика в БД
                var selectedSupplier = DB.SearchSupplier(supplierID: Convert.ToInt32(row.Cells[0].Value));
                currentSupplier.SupplierID = selectedSupplier.SupplierID;
                currentSupplier.Name = selectedSupplier.Name;
                currentSupplier.Contacts = selectedSupplier.Contacts;
                currentSupplier.Items = DB.ItemsToList(SupplierID: Convert.ToInt32(row.Cells[0].Value));
            }

            // вывод текущего поставщика в статусную строку
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                toolStripStatusLabel1.Text = "Выбрано: " + value1 + " - " + value2 + " - " + value3;
            }
        }

        // сокрытие предупреждений таблиц
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        // обработка переключения вкладок
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // вкладка товары
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                // включение / выключение радиокнопок
                radioButton1.Checked = true;

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

                // изменение названия кнопок
                edit.Text = "Редактировать товар";
                add.Text = "Добавить новый товар";
                remove.Text = "Удалить товар";
            }

            //вкладка продажи
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                // включение / выключение радиокнопок
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

                // изменение названия кнопок
                edit.Text = "Редактировать заказ";
                add.Text = "Добавить новый заказ";
                remove.Text = "Удалить заказ";
            }
            
            // вкладка поставщики
            else
            {
                // включение / выключение радиокнопок
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

                // изменение названия кнопок
                edit.Text = "Изменить поставщика";
                add.Text = "Добавить нового поставщика";
                remove.Text = "Удалить поставщика";
            }
            UpdateMainForm();
        }

        // нажатие кнопки управления категориями
        private void button1_Click_1(object sender, EventArgs e)
        {
            // создание экземпляра окна управления категориями
            CategoryOperationForm CategoryForm = new CategoryOperationForm();
            CategoryForm.ShowDialog();
        }

// нажатие кнопки управления продавцами
        private void button2_Click_1(object sender, EventArgs e)
        {
            // создание экземпляра окна управления продавцами
            SellerOperationForm SellerForm = new SellerOperationForm();
            SellerForm.ShowDialog();
        }
    }
}
