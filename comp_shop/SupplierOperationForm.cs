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
    public partial class SupplierOperationForm : Form
    {
        public SupplierOperationForm()
        {
            InitializeComponent();
        }

        // загрузка формы
        private void SupplierOperationForm_Load(object sender, EventArgs e)
        {
            //dataGridView1.Columns[0].Name = "Item ID";
            //dataGridView1.Columns[1].Name = "Item Name";
            //dataGridView1.Columns[2].Name = "Category";

            if (this.Text != "Новый поставщик")
            {
                textBox1.Text = MainForm.currentSupplier.Name;
                textBox2.Text = MainForm.currentSupplier.Contacts;

                // заполнение таблицы данными о товарах поставщика
                MainForm.currentItems = DB.ItemsToList(MainForm.currentSupplier.SupplierID);
                dataGridView1.ColumnCount = 3;
                // проверка на отсутствие товаров в заказе
                if (MainForm.currentItems.Count == 0)
                { dataGridView1.RowCount = 1; return; }
                dataGridView1.RowCount = MainForm.currentItems.Count;

                // заполенение таблицы данными
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItems[i].ItemID;
                    dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItems[i].Name;
                    dataGridView1.Rows[i].Cells[2].Value = MainForm.currentItems[i].Category;
                }

            }
        }

        // обработка нажатия кнопки показа всех товаров для привязки к поставщику
        private void button1_Click(object sender, EventArgs e)
        {
            ShowInfoForm allItems = new ShowInfoForm();
            allItems.Text = "Товары к поставщику";
            allItems.ShowDialog();
        }

        // добавление нового товара в заказ
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // создание отдельной сущности поставщика
                MainForm.currentSupplier.Name = textBox1.Text;
                MainForm.currentSupplier.Contacts = textBox2.Text;

                // добавление к поставщику товаров с помощью конструктора копирования
                Item newItem = new Item(MainForm.currentItem);
                MainForm.currentItems.Add(newItem);

                // отображение всех товаров поставщика в DataGridView1
                dataGridView1.RowCount = MainForm.currentItems.Count;
                dataGridView1.ColumnCount = 3;

                dataGridView1.ReadOnly = true;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItems[i].ItemID;
                    dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItems[i].Name;
                    dataGridView1.Rows[i].Cells[2].Value = MainForm.currentItems[i].Category;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        // нажатие кнопки исключения товара из поставщика
        private void button4_Click(object sender, EventArgs e)
        {
            // если количество товаров <= 1
            if (MainForm.currentItems.Count <= 1)
            {
                // очищение списка товаров
                MainForm.currentItems.Clear();
                // очищение таблицы
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                return;
            }

            // удаление из списка товаров по индексу
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            MainForm.currentItems.RemoveAt(dataGridView1.CurrentCell.RowIndex);

            // TODO: собрать повторящийся код заполнения таблицы в один метод
            dataGridView1.RowCount = MainForm.currentItems.Count;
            dataGridView1.ColumnCount = 3;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = MainForm.currentItems[i].ItemID;
                dataGridView1.Rows[i].Cells[1].Value = MainForm.currentItems[i].Name;
                dataGridView1.Rows[i].Cells[2].Value = MainForm.currentItems[i].Category;
            }
        }

        // TODO: проверка на дублированные значения товаров у поставщика
        // занесение нового или отредактированного поставщика и его товаров в БД
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Text == "Новый поставщик")
            {
                DB.AddSupplier();
                this.Close();
            }
            else
            {
                //// обновление данных сущности
                //MainForm.currentSupplier.Name = textBox1.Text;
                //MainForm.currentSupplier.Contacts = textBox2.Text;
                                                  
                //DB.edit(MainForm.currentItemOrderEntity.OrderID);
                //this.Close();
            }
        }

    }
}
