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
    public partial class NewCategory : Form
    {
        public string newCategory;

        public NewCategory()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB.AddCategory(textBox1.Text);
            button2.Text = "Готово";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newCategory = null;
            this.Close();
        }
    }
}
