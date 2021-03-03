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
    public partial class NewItemForm : Form
    {
        string name;
        double price;
        string category;
        string supplier;

        public NewItemForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.name = textBox1.Text;
            this.price = double.Parse(textBox2.Text);
            this.category = comboBox1.Text;
            this.supplier = textBox3.Text;

            Item new_item = new Item(name, price, category, supplier);
        }
    }
}
