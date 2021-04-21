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
    public partial class AssociatedInfo : Form
    {
        
        public List<Item> itemsForSupplier = new List<Item>();
        public List<ItemOrdersEntity> ordersToItems = new List<ItemOrdersEntity>();

        public AssociatedInfo()
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
                dataGridView1.DataSource = MainForm.ordersItemsAssociatedData;
                button1.Text = "Редактировать заказ";
                button2.Text = "Новый заказ";
                
            }
            else if (this.Text == "Товары в заказе")
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = MainForm.ordersItemsAssociatedData;
                button1.Text = "Редактировать товар";
                button2.Text = "Новый товар";
            }
            else
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = itemBindingSource;
                dataGridView1.DataSource = MainForm.itemsConnectedData;
                button1.Text = "Редактировать товар";
                button2.Text = "Новый товар";

            }


        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
            {

            }

    }
}
