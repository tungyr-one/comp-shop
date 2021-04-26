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
                dataGridView1.DataSource = itemOrdersEntityBindingSource;
                dataGridView1.DataSource = MainForm.currentItemOrdersEntities;
                button4.Text = "Готово";


                dataGridView1.Columns["OrderDate"].HeaderText = "Дата заказа";
                dataGridView1.Columns["OrderID"].HeaderText = "ID заказа";
                dataGridView1.Columns["Quanitity"].HeaderText = "Количество";
                dataGridView1.Columns["SellerName"].HeaderText = "Имя продавца";
                dataGridView1.Columns["Customer"].HeaderText = "Покупатель";
                dataGridView1.Columns["CustomerContact"].HeaderText = "Контакты покупателя";
            }
            // загузка товаров текущего заказа 
            else if (this.Text == "Товары в заказе")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = MainForm.currentItemOrdersEntities;
                button4.Text = "Готово";
            }
            // загрузка товаров текушего поставщика
            else if (this.Text == "Товары поставщика")
            {
                //dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = itemBindingSource;
                dataGridView1.DataSource = MainForm.currentItems;
                button4.Text = "Готово";
            }
            // загрузка всех товаров для выбора
            else
            {
                //dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = DB.ShowAllItems();
                button4.Text = "Отмена";
            }

        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
            {

            }


        // обработка двойного клика
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // если выбор товара из всех имеющихся
            if (this.Text == "Выбор товара")
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                MainForm.currentItem = DB.SearchItemByNameOrID(ID: Convert.ToInt32(rows[0].Cells[0].Value))[0];
                this.Close();
            }
        }

        // нажатие кнопки отмена
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
