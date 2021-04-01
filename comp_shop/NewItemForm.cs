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
        decimal price;
        string category;
        string seller;
        string supplier;
        public Article my_item = null;

        public NewItemForm()
        {
            InitializeComponent();

            textBox1.Text = "Razor";
            textBox2.Text = "10255,45";

            // Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал?
            textBox3.Text = "Pupkin";

            using (ComputerShopEntities c = new ComputerShopEntities())
            {
                comboBox1.DataSource = c.Categories.ToList();
                comboBox2.DataSource = c.Suppliers.ToList();

                comboBox1.ValueMember = "Name";
                comboBox1.DisplayMember = "Name";

                comboBox2.ValueMember = "Name";
                comboBox2.DisplayMember = "Name";
            }
        }

        private void editItem(Article itemToEdit)
        {
            this.textBox1.Text = my_item.ArticleName;
            this.textBox2.Text = my_item.ArticlePrice.ToString();
            //this.comboBox1.DataSource = categories;
            // Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал? 
            this.textBox3.Text = my_item.ArticleSeller;
            //this.comboBox2.DataSource = suppliers;
        }

        private void CreateItem()
        {
            this.name = textBox1.Text;
            this.price = decimal.Parse(textBox2.Text);
            this.category = comboBox1.SelectedItem.ToString();
            this.seller = textBox3.Text;
            this.supplier = comboBox2.SelectedItem.ToString();
            this.button2.Text = "Готово";

            Article new_item = new Article(name, price, category, seller, supplier);
            my_item = new_item;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.name = textBox1.Text;
            //this.price = double.Parse(textBox2.Text);
            //this.category = comboBox1.Text;
            //this.supplier = textBox3.Text;
            if (this.my_item == null)
            {
                button1.Text = "Довить";
                this.CreateItem();
            }
            else
            {
                button1.Text = "Изменить";
                this.editItem(my_item);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        // установка значений полей при загрузке, если редактирование товара
        private void NewItemForm_Load(object sender, EventArgs e)
        {
            if (this.my_item != null)
            {
                this.textBox1.Text = my_item.ArticleName;
                this.textBox2.Text = my_item.ArticlePrice.ToString();
                //this.comboBox1.DataSource = categories;
                // Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал?
                this.textBox3.Text = my_item.ArticleSeller;
                //this.comboBox2.DataSource = suppliers;
            }
        }
    }
}
