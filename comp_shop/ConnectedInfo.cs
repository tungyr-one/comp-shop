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
    public partial class ConnectedInfo : Form
    {
        
        public List<Item> itemsToSupplier = new List<Item>();
        public List<Order> ordersToItems = new List<Order>();

        public ConnectedInfo()
        {
            InitializeComponent();
            
        }

        private void ConnectedInfo_Load(object sender, EventArgs e)
        {
            // проверка заполненности списка товаров и домонстрация его в DataGridView
            if (itemsToSupplier.Count > 0)
            {//dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = itemBindingSource;
                dataGridView1.DataSource = itemsToSupplier;                
                itemsToSupplier.Clear();
            }
            //else
            //{
            //    dataGridView1.DataSource = orderBindingSource;
            //    dataGridView1.DataSource = ordersToItems;
            //}

  
        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
