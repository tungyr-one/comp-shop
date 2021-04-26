using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp_shop
{
    public partial class ShowInfoForm : Form
    {
        
        public List<Item> itemsForSupplier = new List<Item>();
        public List<ItemOrdersEntity> ordersToItems = new List<ItemOrdersEntity>();

        public ShowInfoForm()
        {
            InitializeComponent();
            
        }

        private void ConnectedInfo_Load(object sender, EventArgs e)
        {

            //dataGridView1.AutoGenerateColumns = true;
            ////dataGridView1.DataSource = orderBindingSource;
            //dataGridView1.DataSource = ordersToItems;
            //ordersToItems.Clear();


            // проверка для чего было вызвано окно
            // загузка оформленных заказов на текущий товар
            if (this.Text == "Заказы товара")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                //dataGridView1.DataSource = ordersToItems;
                dataGridView1.DataSource = MainForm.currentItemOrdersEntities;
                button1.Text = "Добавить заказ";
                button2.Text = "Удалить заказ";
                button3.Text = "Поиск по ID";
                button4.Text = "Готово";
            }
            // загузка товаров текущего заказа 
            else if (this.Text == "Товары в заказе")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = MainForm.currentItemOrdersEntities;
                button1.Text = "Добавить товар";
                button2.Text = "Редактировать товар";
                button3.Text = "Поиск по названию";
                button4.Text = "Готово";
            }
            // загрузка товаров текушего поставщика
            else if (this.Text == "Товары поставщика")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = itemBindingSource;
                dataGridView1.DataSource = MainForm.currentItems;
                button1.Text = "Добавить товар";
                button2.Text = "Удалить товар";
                button3.Text = "Поиск по названию";
                button4.Text = "Готово";
            }
            // загрузка всех товаров для выбора
            else
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = DB.ShowAllItems();
                button1.Text = "Новый товар";
                button2.Text = "Выбрать товар";
                button3.Text = "Поиск по названию";
                button4.Text = "Отмена";
            }
        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
            {

            }

        // TODO: упростить выбор товара и поиск его в БД (возвращать не список а один товар?)
        // TODO: добавление разных сущностей?
        // нажатие кнопки добавить заказ / товар / выбрать товар
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            // если список заказов товара - добавление нового заказа
            if (this.Text == "Заказы товара")
            {
                // удаление всех предыдущих значений в списке для нового заказа
                MainForm.currentItemOrdersEntities.Clear();
                OrderOperationForm orderForm = new OrderOperationForm
                {
                    Text = "Добавление нового заказа на товар"
                };
                orderForm.ShowDialog();
                //обновление списка заказов
                dataGridView1.DataSource = DB.ShowAllOrders();
                //this.Close();
            }
            else
            {
                
            }               
            
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            // нажатие кнопки "Выбрать товар"
            MainForm.currentItem = DB.SearchItemByNameOrID(ID: Convert.ToInt32(rows[0].Cells[0].Value))[0];
            this.Close();
        }

        // TODO: допилить поиск в форме
        // поиск товара / заказа
        private void button3_Click(object sender, EventArgs e)
        {
            //поиск заказа по ID
            if (this.Text == "Заказы товара")
            {

            }
            // поиск товара по имени
            else
            {
                dataGridView1.DataSource = DB.SearchItemByNameOrID(textBox1.Text);
            }
        }


        // обработка двойного клика
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // если выбор товара из всех имеющихся
            if (this.Text == "Выбор товара")
            { button2_Click(sender, e); }
        }

        // нажатие кнопки отмена
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
