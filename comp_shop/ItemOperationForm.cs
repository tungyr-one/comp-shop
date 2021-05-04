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
    public partial class ItemOperationForm : Form
    {
        //public Item workingItem = new Item();
        bool createOperation;

        public ItemOperationForm(bool createOperation = true)
        {
            InitializeComponent();
            
           this.createOperation = createOperation;

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
        private void itemOperationFormLoad(object sender, EventArgs e)
        {
            // если создание товара
            if (this.Text == "Создание нового товара")
            {
                button1.Text = "Добавить";

                //TODO: delete
                // временные постоянные значения полей
                textBox1.Text = "Razor";
                textBox2.Text = "10255,45";
            }
            else if (this.Text == "Добавление товара поставщика")
            {
                textBox1.Text = "Razor";
                textBox2.Text = "10255,45";
            }
            // если редактирование товара
            else
            {
                this.Text = "Редактирование товара";
                button1.Text = "Изменить";

                // присваивание текстовым полям значений редактируемого товара
                this.textBox1.Text = MainForm.currentItem.Name;
                this.textBox2.Text = MainForm.currentItem.Price.ToString();
                this.comboBox1.SelectedValue = MainForm.currentItem.Category.Name;
                this.comboBox2.SelectedValue = MainForm.currentItem.Supplier.Name;

                // изменение видимости лэйбла и кнопни заказы
                label4.Visible = true;
                button5.Visible = true;
            }
        }

        // метод создания/редактирования товара
        private void CreateEditItem()
        {

            this.button2.Text = "Готово";  

            // определение необходимого метода в классе работы с БД
            // если создание товара
            if (this.Text == "Создание нового товара")
            {
                // создание нового экземпляра currentItem
                //MainForm.currentItem = new Item();
                // формирования объекта класса Item для передачи в БД
                //MainForm.currentItem.Name = textBox1.Text;
                //MainForm.currentItem.Price = decimal.Parse(textBox2.Text);
                //MainForm.currentItem.Category = DB.SearchCategory(comboBox1.SelectedItem.ToString());
                //MainForm.currentItem.Supplier = DB.SearchSupplier(supplierName: comboBox2.SelectedItem.ToString());
                DB.addItem(name: textBox1.Text, price: decimal.Parse(textBox2.Text), category: comboBox1.SelectedItem.ToString(), supplier: comboBox2.SelectedItem.ToString());
            }
            // если добавление товара поставщика
            else if (this.Text == "Добавление товара поставщика")
            {
                // копирование текущего товара
                MainForm.currentItem = new Item(MainForm.currentItem);
                MainForm.currentItem.Name = textBox1.Text;
                MainForm.currentItem.Price = decimal.Parse(textBox2.Text);
                MainForm.currentItem.Category = DB.SearchCategory(comboBox1.SelectedItem.ToString());
                MainForm.currentItem.Supplier = null;

                this.Close();
            }
            // редактирование товара
            else
            {
                MainForm.currentItem.Name = textBox1.Text;
                MainForm.currentItem.Price = decimal.Parse(textBox2.Text);
                MainForm.currentItem.Category = DB.SearchCategory(comboBox1.SelectedItem.ToString());
                MainForm.currentItem.Supplier = DB.SearchSupplier(supplierName: comboBox2.SelectedItem.ToString());
                DB.editItem();
            }
        }

        // обработка нажатия кнопки управления категориями
        private void button3_Click(object sender, EventArgs e)
        {
            // создание экземпляра окна управления категориями
            CategoryOperationForm CategoryForm = new CategoryOperationForm();
            CategoryForm.ShowDialog();

            // отображение обновленного списка категорий в combobox после закрытия формы редактирования категории
            ComputerShopEntities c = new ComputerShopEntities();
            comboBox1.DataSource = c.Categories.ToList();
        }

        // обработка нажатия кнопки управления поставщиками
        private void button4_Click(object sender, EventArgs e)
        {
            // поиск поставщика из комбо и назначение текущей сущности поставщика
            MainForm.currentSupplier = DB.SearchSupplier(supplierName: comboBox2.SelectedItem.ToString());
            // создание экземпляра окна управления поставщиками
            SupplierOperationForm SupplierForm = new SupplierOperationForm();
            SupplierForm.Text = "Управление поставщиками";
            SupplierForm.ShowDialog();

            // отображение обновленного списка поставщиков в combobox после закрытия формы редактирования поставщика
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

        // кнопка показать заказы
        private void button5_Click(object sender, EventArgs e)
        {
            // загрузка окна заказов на товар
            ShowInfoForm associatedInfoForm = new ShowInfoForm();
            List<ItemOrdersEntity> ordersConnectedData = DB.OrdersOfItem(MainForm.currentItem.ItemID);
            associatedInfoForm.ordersToItems = ordersConnectedData;
            associatedInfoForm.Text = "Заказы товара";
            associatedInfoForm.ShowDialog();
        }
    }
}
