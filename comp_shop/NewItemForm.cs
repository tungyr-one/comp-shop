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
    public partial class NewItemForm : Form
    {
        public Article articleItem = new Article();
        public Item workingItem = new Item();
        bool createOperation;

        public NewItemForm(bool createOperation = true)
        {
            InitializeComponent();
            
           this.createOperation = createOperation;

            // Todo: сделать поле продавец изначально пустым, затем списком кто и сколько продавал?
// заполнение комбобоксов значениями
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

        // установка значений полей при загрузке
        private void NewItemForm_Load(object sender, EventArgs e)
        {
            // если создание товара
            if (createOperation == true)
            {
                this.Text = "Добавление товара";
                button1.Text = "Добавить";

                // временные постоянные значения полей
                textBox1.Text = "Razor";
                textBox2.Text = "10255,45";
                textBox3.Text = "Pupkin";
            }
            // если редактирование товара
            else
            {
                this.Text = "Изменение товара";
                button1.Text = "Изменить";                
                
                // присваивание текстовым полям значений редактируемого товара
                this.textBox1.Text = workingItem.Name;
                this.textBox2.Text = workingItem.Price.ToString();
                this.comboBox1.SelectedItem = workingItem.Category;
                this.textBox3.Text = workingItem.OrdersToString();
                this.comboBox2.SelectedItem = workingItem.Category;
            }
        }

        // метод создания/редактирования товара
        private void CreateEditItem()
        {
            //// формирования объекта класса Item для передачи в БД
            //workingItem.Name = textBox1.Text;
            //workingItem.Price = decimal.Parse(textBox2.Text);
            //workingItem.Category = DB.SearchCategory(comboBox1.SelectedItem.ToString());
            ////workingItem.Orders = textBox3.Text;
            //workingItem.Supplier = DB.SearchSupplier(comboBox2.SelectedItem.ToString());
            //this.button2.Text = "Готово";

            // формирования объекта класса Article для передачи в БД
            articleItem.Name = textBox1.Text;
            articleItem.Price = decimal.Parse(textBox2.Text);
            articleItem.Category = comboBox1.SelectedItem.ToString();
            //workingItem.Orders = textBox3.Text;
            articleItem.Supplier = comboBox2.SelectedItem.ToString();
            this.button2.Text = "Готово";

            // определение необходимого метода в классе работы с БД
            if (createOperation)
            {
                DB.addItem(articleItem);
            }
            else
            {
                DB.editItem(articleItem);
            }

        }

        // обработка нажатия кнопки добавления категории
        private void button3_Click(object sender, EventArgs e)
        {
            ManageCategory CategoryForm = new ManageCategory();
            CategoryForm.ShowDialog();
            ComputerShopEntities c = new ComputerShopEntities();
            comboBox1.DataSource = c.Categories.ToList();
        }

        // обработка нажатия кнопки добавления поставщика
        private void button4_Click(object sender, EventArgs e)
        {
            ManageSupplier SupplierForm = new ManageSupplier();
            SupplierForm.ShowDialog();
            ComputerShopEntities c = new ComputerShopEntities();
            comboBox2.DataSource = c.Suppliers.ToList();
        }

        // обработка нажатия кнопки создания/редактирования товара
        private void button1_Click(object sender, EventArgs e)
        {
            CreateEditItem();
        }

        // обработка нажатия кнопки завершающей процесс создания/редактирования товара
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
    }
}
