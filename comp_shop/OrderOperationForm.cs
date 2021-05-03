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
            }
        }

        // загрузка формы
        private void OrderOperationForm_Load(object sender, EventArgs e)
        {
            // если окно вызывается для редактирования заказа
            if (this.Text == "Редактирование заказа")
            {                
                // очистка текущего товара
                MainForm.currentItem = null;
                // установка значений для полей редактирования
                comboBox1.SelectedItem = MainForm.currentItemOrderEntity.SellerName;
                dateTimePicker1.Value = Convert.ToDateTime(MainForm.currentItemOrderEntity.OrderDate);
                textBox2.Text = MainForm.currentItemOrderEntity.Customer;
                textBox3.Text = MainForm.currentItemOrderEntity.CustomerContact;

                // заполнение таблицы данными о товарах в заказе
                MainForm.currentItemOrdersEntities = DB.LoadItemOrdersEntities(MainForm.currentItemOrderEntity.OrderID);
                dataGridView1.ColumnCount = 2;
                // проверка на отсутствие товаров в заказе
                if (MainForm.currentItemOrdersEntities.Count == 0)
                { dataGridView1.RowCount = 1; return; }
                dataGridView1.RowCount = MainForm.currentItemOrdersEntities.Count;

                dataGridView1.ReadOnly = true;
                // заполенение таблицы данными
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItemOrdersEntities[i].Item;
                    dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItemOrdersEntities[i].Quantity;
                }
            }
        }      

        // обработка нажатия кнопки показа всех товаров для добавления в заказ
        private void button1_Click(object sender, EventArgs e)
        {
            ShowInfoForm allItems = new ShowInfoForm();
            allItems.Text = "Выбор товара";
            allItems.ShowDialog();

            // отображение выбранного товара
            if (MainForm.currentItem != null)
            { label8.Text = MainForm.currentItem.Name; }
        }

        // добавление нового товара в заказ
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {                
                // создание отдельной сущности заказа с товаром и его количеством
                MainForm.currentItemOrderEntity.SellerName = comboBox1.SelectedItem.ToString();
                MainForm.currentItemOrderEntity.OrderDate = dateTimePicker1.Value.ToString();
                MainForm.currentItemOrderEntity.Item = MainForm.currentItem.Name;
                MainForm.currentItemOrderEntity.Quantity = Convert.ToInt32(numericUpDown1.Text);
                MainForm.currentItemOrderEntity.Customer = textBox2.Text;
                MainForm.currentItemOrderEntity.CustomerContact = textBox3.Text;

                // добавление в список товаров нового товара с помощью конструктора копирования
                ItemOrdersEntity newItem = new ItemOrdersEntity(MainForm.currentItemOrderEntity);
                // проверка дублирования добавляемого товара
                foreach (ItemOrdersEntity it in MainForm.currentItemOrdersEntities)
                {
                    if (newItem.Item == it.Item)
                    {
                        MessageBox.Show("Товар уже в заказе!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }    
                MainForm.currentItemOrdersEntities.Add(newItem);

                // отображение всех товаров в заказе в DataGridView1
                dataGridView1.RowCount = MainForm.currentItemOrdersEntities.Count;
                dataGridView1.ColumnCount = 2;

                dataGridView1.ReadOnly = true;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItemOrdersEntities[i].Item;
                    dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItemOrdersEntities[i].Quantity;
                }

            }
            catch
            {
                MessageBox.Show("Ошибка! Проверьте заполненность полей!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // занесение нового или отредактированного списка заказов с товарами и их количеством в БД 
        private void button3_Click(object sender, EventArgs e)
        {
            // проверка заполненности полей имени и контактов покупателя
            if (textBox2.Text == "" || textBox3.Text == "" )
            {
                MessageBox.Show("Не заполнены поля покупателя!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // создание нового заказа
            if (this.Text == "Создание нового заказа")
            {
                MainForm.currentItemOrderEntity.SellerName = comboBox1.SelectedItem.ToString();
                MainForm.currentItemOrderEntity.OrderDate = dateTimePicker1.Value.ToString();
                MainForm.currentItemOrderEntity.Customer = textBox2.Text;
                MainForm.currentItemOrderEntity.CustomerContact = textBox3.Text;
                DB.AddOrder();
                this.Close();
            }
            // если редактирование заказа
            else
            {
                // если товаров в заказе нет - заказ удаляется
                if (MainForm.currentItemOrdersEntities.Count == 0)
                {
                    // очищаем все товары в заказе в БД
                    try
                    {
                        DB.RemoveOrder(MainForm.currentItemOrderEntity);
                        this.Close();
                    }
                    catch
                    {

                    }
                    return;
                }

                // если пользователь не добавлял новые товары через ShowInfoForm
                if (MainForm.currentItem == null)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        MainForm.currentItem = DB.SearchItemByNameOrID(itemName: row.Cells[0].Value.ToString())[0];
                    }
                }

                // обновление данных сущности
                MainForm.currentItemOrderEntity.SellerName = comboBox1.SelectedItem.ToString();
                MainForm.currentItemOrderEntity.OrderDate = dateTimePicker1.Value.ToString();
                MainForm.currentItemOrderEntity.Item = MainForm.currentItem.Name;
                MainForm.currentItemOrderEntity.Quantity = Convert.ToInt32(numericUpDown1.Text);
                MainForm.currentItemOrderEntity.Customer = textBox2.Text;
                MainForm.currentItemOrderEntity.CustomerContact = textBox3.Text;

                DB.editOrder(MainForm.currentItemOrderEntity.OrderID);
                this.Close();
            }
        }

        // нажатие кнопки удаления товара из заказа
        private void button4_Click(object sender, EventArgs e)
        {   
            // если товаров в заказе меньше либо равно одному
            if (MainForm.currentItemOrdersEntities.Count <= 1)
            {
                // очищение списка заказанных товаров
                MainForm.currentItemOrdersEntities.Clear();
                // очищение таблицы
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                MessageBox.Show("Заказ будет удален если в нем не будет товаров", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // удаление из списка заказов по индексу
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            MainForm.currentItemOrdersEntities.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.ReadOnly = true;

            // заполнение таблицы оставшимися товарами
            dataGridView1.RowCount = MainForm.currentItemOrdersEntities.Count;
            dataGridView1.ColumnCount = 2;
            dataGridView1.ReadOnly = true;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItemOrdersEntities[i].Item;
                dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItemOrdersEntities[i].Quantity;
            }
        }
    }
}
