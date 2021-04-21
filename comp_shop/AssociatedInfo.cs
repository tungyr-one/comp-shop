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
        
        public List<Item> itemsToSupplier = new List<Item>();
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


            // проверка заполненности списка товаров и домонстрация его в DataGridView
            if (itemsToSupplier.Count > 0)
            {
                this.Text = "Товары поставщика";
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = itemBindingSource;
                dataGridView1.DataSource = itemsToSupplier;
                
            }
            else
            {
                this.Text = "Продажи товара";
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = orderBindingSource;
                dataGridView1.DataSource = ordersToItems;
                
            }


        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
            {

            }

        private void ConnectedInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            itemsToSupplier.Clear();
            ordersToItems.Clear();
        }
    }
}
