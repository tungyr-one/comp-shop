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

        public ConnectedInfo()
        {
            InitializeComponent();
            
        }

        private void ConnectedInfo_Load(object sender, EventArgs e)
        {
            //dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = itemsToSupplier;
            for (int i=0;  i < dataGridView1.RowCount;i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = itemsToSupplier[i].Category;
            }
        }

            private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
