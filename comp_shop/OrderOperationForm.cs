﻿using System;
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

            dataGridView1.Columns[0].Name = "Item name";
            dataGridView1.Columns[1].Name = "Item quantity";

            if (this.Text == "Создание нового заказа")
            {               
                //button1.Text = "Редактировать заказ";
                //button2.Text = "Готово";

            }
            else if (this.Text == "Редактирование заказа")
            {
                // установка значений для полей редактирования
                comboBox1.SelectedItem = MainForm.currentItemOrderEntity.SellerName;
                dateTimePicker1.Value = Convert.ToDateTime(MainForm.currentItemOrderEntity.OrderDate);
                textBox2.Text = MainForm.currentItemOrderEntity.Customer;
                textBox3.Text = MainForm.currentItemOrderEntity.CustomerContact;

                // заполнение таблицы данными о товарах в заказе
                MainForm.currentItemOrdersEntities = DB.LoadItemOrdersEntities(MainForm.currentItemOrderEntity.OrderID);
                dataGridView1.RowCount = MainForm.currentItemOrdersEntities.Count;
                dataGridView1.ColumnCount = 2;

                // TODO: изменение количества товара прямо в таблице
                dataGridView1.ReadOnly = true;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItemOrdersEntities[i].Item;
                    dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItemOrdersEntities[i].Quantity;
                }
            }
        }

        // обработка нажатия кнопки показа всех заказов
        private void button1_Click(object sender, EventArgs e)
        {
             ShowInfoForm allItems = new ShowInfoForm();
            MainForm.currentItems = DB.ShowAllItems();
            allItems.Text = "Товары в заказ";
            allItems.ShowDialog();
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
                MainForm.currentItemOrdersEntities.Add(newItem);

                // отображение всех товаров в заказе в DataGridView1
                dataGridView1.RowCount = MainForm.currentItemOrdersEntities.Count;
                dataGridView1.ColumnCount = 2;

                // TODO: изменение количества товара прямо в таблице
                dataGridView1.ReadOnly = true;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItemOrdersEntities[i].Item;
                    dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItemOrdersEntities[i].Quantity;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        // нажатие кнопки удаления товара из заказа
        private void button4_Click(object sender, EventArgs e)
        {            
            if (MainForm.currentItemOrdersEntities.Count <= 1)
            {
                // очищение списка заказанных товаро
                MainForm.currentItemOrdersEntities.Clear();
                // очищение таблицы
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                return;
            }

            // удаление из списка заказов по индексу
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            MainForm.currentItemOrdersEntities.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            // TODO: изменение количества товара прямо в таблице
            dataGridView1.ReadOnly = true;

            dataGridView1.RowCount = MainForm.currentItemOrdersEntities.Count;
            dataGridView1.ColumnCount = 2;
            dataGridView1.ReadOnly = true;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItemOrdersEntities[i].Item;
                dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItemOrdersEntities[i].Quantity;
            }
        }

        // занесение в БД списка заказов с товарами и их количеством
        private void button3_Click(object sender, EventArgs e)
        {
            DB.AddOrder();
            this.Close();
        }
    }
}
