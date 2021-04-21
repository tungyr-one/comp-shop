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
    public partial class OrderOperationForm : Form
    {
        public OrderOperationForm()
        {
            InitializeComponent();

            // заполнение данными комбобоксов
            using (ComputerShopEntities c = new ComputerShopEntities())
            {
                comboBox1.DataSource = DB.AllSellers();

                //comboBox1.ValueMember = "Name";
                //comboBox1.DisplayMember = "Name";

            }
        }

        private void OrderOperationForm_Load(object sender, EventArgs e)
        {
            if (this.Text == "Создание нового заказа")
            {               
                //button1.Text = "Редактировать заказ";
                //button2.Text = "Готово";

            }
            else if (this.Text == "Редактирование заказа")
            {
                //dataGridView1.AutoGenerateColumns = true;
                //dataGridView1.DataSource = orderBindingSource;
                //dataGridView1.DataSource = MainForm.ordersItemsAssociatedData;
                //button1.Text = "Редактировать товар";
                //button2.Text = "Новый товар";
            }
        }

        // обработка нажатия кнопки показа всех заказов
        private void button1_Click(object sender, EventArgs e)
        {
             ShowInfoForm allItems = new ShowInfoForm();
            MainForm.itemsAssociatedData = DB.ShowAllItems();
            allItems.Text = "Товары в заказ";
            allItems.ShowDialog();
        }

        // добавление нового товара в заказ
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm.currentOrderItems.SellerName = comboBox1.SelectedItem.ToString();
            MainForm.currentOrderItems.OrderDate = dateTimePicker1.Value.ToString();
            MainForm.currentOrderItems.Item = MainForm.currentItem.Name;
            MainForm.currentOrderItems.Quantity = Convert.ToInt32(textBox1.Text);
            MainForm.currentOrderItems.Customer = textBox2.Text;
            MainForm.currentOrderItems.CustomerContact = textBox3.Text;
        }
    }
}
