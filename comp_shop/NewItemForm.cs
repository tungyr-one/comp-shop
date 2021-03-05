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
        // TODO: Спросить: Используется одна форма для добавления и редактирования. 
        // Как заставить понимать форму что нужно делать - редактировать или создавать товар?
        string name;
        double price;
        string category;
        string seller;
        string supplier;
        public Item my_item = null;

        public NewItemForm()
        {
            InitializeComponent();

            //textBox1.Text = "Компьютер";
            //textBox2.Text = "10255,45";
            //comboBox1.Text = "Компьютеры";
            //// Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал?
            //textBox3.Text = "Петров В.А.";
            //textBox4.Text = "Иванов ИП";

        }

        private void editItem(Item itemToEdit)
        {
            this.textBox1.Text = my_item.ItemNameGetter;
            this.textBox2.Text = my_item.ItemPriceGetter.ToString();
            this.comboBox1.Text = my_item.ItemCategoryGetter;
            // Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал? 
            this.textBox3.Text = my_item.ItemSellerGetter;
            this.textBox4.Text = my_item.ItemSupplierGetter;
        }

        private void CreateItem()
        {
            this.name = textBox1.Text;
            this.price = double.Parse(textBox2.Text);
            this.category = comboBox1.Text;
            this.seller = textBox3.Text;
            this.supplier = textBox3.Text;
            this.button2.Text = "Готово";

            // TODO: Спросить: как это исправить??? Как сразу создать new_item и чтобы оно было видно на уровне класса?
            Item new_item = new Item(name, price, category, seller, supplier);
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
                this.textBox1.Text = my_item.ItemNameGetter;
                this.textBox2.Text = my_item.ItemPriceGetter.ToString();
                this.comboBox1.Text = my_item.ItemCategoryGetter;
                // Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал?
                this.textBox3.Text = my_item.ItemSellerGetter;
                this.textBox4.Text = my_item.ItemSupplierGetter;
            }
        }
    }
}
