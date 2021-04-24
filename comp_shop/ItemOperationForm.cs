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
        public Article articleItem = new Article();
        //public Item workingItem = new Item();
        bool createOperation;

        public ItemOperationForm(bool createOperation = true)
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

            // TODO: просто выключить, а не скрывать
            label5.Visible = false;
            comboBox2.Visible = false;
        }

        // установка значений полей при загрузке
        private void NewItemForm_Load(object sender, EventArgs e)
        {
            // TODO: изменить способ определения назначения вызова формы
            // если создание товара
            if (this.Text == "Создание нового товара")
            {
                button1.Text = "Добавить";

                //TODO: delete
                // временные постоянные значения полей
                textBox1.Text = "Razor";
                textBox2.Text = "10255,45";

                // изменение видимости лэйбла и кнопки заказы
                label4.Visible = false;
                button5.Visible = false;
                comboBox2.Enabled = true;
            }
            else if (this.Text == "Добавление товара поставщика")
            {
                textBox1.Text = "Razor";
                textBox2.Text = "10255,45";

                label4.Visible = false;
                button5.Visible = false;

                comboBox2.Enabled = false;
            }
            // если редактирование товара
            else
            {
                this.Text = "Редактирование товара";
                button1.Text = "Изменить";

                comboBox2.Enabled = true;

                label5.Visible = true;
                comboBox2.Visible = true;

                // присваивание текстовым полям значений редактируемого товара
                this.textBox1.Text = MainForm.currentItem.Name;
                this.textBox2.Text = MainForm.currentItem.Price.ToString();
                // TODO: показывать в комбобоксе категорию текущего товара, а не первую из списка
                this.comboBox1.SelectedItem = MainForm.currentItem.Category;
                //this.textBox3.Text = workingItem.OrdersToString();
                this.comboBox2.SelectedItem = MainForm.currentItem.Supplier;

                // изменение видимости лэйбла и кнопни заказы
                label4.Visible = true;
                button5.Visible = true;


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

            // TODO: replace to currentItem!!!
            // формирования объекта класса Article для передачи в БД
            articleItem.Id = MainForm.currentItem.ItemID;
            articleItem.Name = textBox1.Text;
            articleItem.Price = decimal.Parse(textBox2.Text);
            articleItem.Category = comboBox1.SelectedItem.ToString();
            //workingItem.Orders = textBox3.Text;
            articleItem.Supplier = comboBox2.SelectedItem.ToString();            

            this.button2.Text = "Готово";
            // определение необходимого метода в классе работы с БД
            if (this.Text == "Создание нового товара")
            {
                DB.addItem(articleItem);
            }
            else if (this.Text == "Добавление товара поставщика")
            {
                MainForm.currentItem = new Item(MainForm.currentItem);
                MainForm.currentItem.Name = textBox1.Text;
                MainForm.currentItem.Price = decimal.Parse(textBox2.Text);
                MainForm.currentItem.Category = DB.SearchCategory(comboBox1.SelectedItem.ToString());
                MainForm.currentItem.Supplier = null;
                //workingItem.Orders = textBox3.Text;

                this.Close();
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

        // кнопка показать заказы
        private void button5_Click(object sender, EventArgs e)
        {
            // загрузка окна заказов на товар
            ShowInfoForm associatedInfoForm = new ShowInfoForm();
            List<ItemOrdersEntity> ordersConnectedData = DB.OrdersForDataGridView1(MainForm.currentItem.ItemID);
            associatedInfoForm.ordersToItems = ordersConnectedData;
            associatedInfoForm.Text = "Заказы товара";
            associatedInfoForm.ShowDialog();
        }
    }
}
