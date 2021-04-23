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
            if (this.Text == "Заказы товара")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                //dataGridView1.DataSource = ordersToItems;
                dataGridView1.DataSource = MainForm.currentItemOrdersEntities;
                button1.Text = "Редактировать заказ";
                button2.Text = "Новый заказ";
                button3.Text = "Поиск по ID";

            }
            else if (this.Text == "Товары в заказе")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = MainForm.currentItemOrdersEntities;
                button1.Text = "Редактировать товар";
                button2.Text = "Новый товар";
                button3.Text = "Поиск по названию";
            }
            // добавление товаров в новый заказ
            else if (this.Text == "Товары в заказ")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = DB.ShowAllItems();
                button1.Text = "Выбрать товар";
                button2.Text = "Отмена";
                button3.Text = "Поиск по названию";
            }
            // добавление товаров к поставщику
            else if (this.Text == "Товары к поставщику")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = DB.ShowAllItems();
                button1.Text = "Выбрать товар";
                button2.Text = "Отмена";
                button3.Text = "Поиск по названию";
            }
            else
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = itemBindingSource;
                dataGridView1.DataSource = MainForm.currentItems;
                button1.Text = "Редактировать товар";
                button2.Text = "Новый товар";
                button3.Text = "Поиск по названию";

            }
        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
            {

            }

        // TODO: упростить выбор товара и поиск его в БД (возвращать не список а один товар?)
        // TODO: добавление разных сущностей?
        // нажатие кнопки добавить товар в новый заказ
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
