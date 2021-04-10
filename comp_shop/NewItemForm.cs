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
        public Article workingItem = new Article();
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
                
                this.textBox1.Text = workingItem.ArticleName;
                this.textBox2.Text = workingItem.ArticlePrice.ToString();
                // TODO: show article catagory and supplier
                this.comboBox1.SelectedItem = workingItem.ArticleCategory;
                this.textBox3.Text = workingItem.ArticleSeller;
                this.comboBox2.SelectedItem = workingItem.ArticleCategory;
            }
        }


        //private void editItem(Article itemToEdit)
        //{
        //    this.name = textBox1.Text;
        //    this.price = decimal.Parse(textBox2.Text);
        //    this.category = comboBox1.SelectedItem.ToString();
        //    this.seller = textBox3.Text;
        //    this.supplier = comboBox2.SelectedItem.ToString();
        //    this.button2.Text = "Готово";
        //}

        private void CreateEditItem()
        {
            workingItem.ArticleName = textBox1.Text;
            workingItem.ArticlePrice = decimal.Parse(textBox2.Text);
            workingItem.ArticleCategory = comboBox1.SelectedItem.ToString();
            workingItem.ArticleSeller = textBox3.Text;
            workingItem.ArticleSupplier = comboBox2.SelectedItem.ToString();
            this.button2.Text = "Готово";

            if(createOperation)
            {
                DB.addToDB(workingItem);
            }
            else
            {
                DB.editEntry(workingItem);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.name = textBox1.Text;
            //this.price = double.Parse(textBox2.Text);
            //this.category = comboBox1.Text;
            //this.supplier = textBox3.Text;
            //if (createOperation == true)
            //{
            //    this.CreateItem();
            //}
            //else
            //{
            //    this.editItem(my_item);
            //}
            CreateEditItem();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        //добавление категории
        private void button3_Click(object sender, EventArgs e)
        {
            NewCategory addCategoryForm = new NewCategory();
            addCategoryForm.Show();
            //if (addCategoryForm.newCategory != null)
            //{ DB.AddCategory(addCategoryForm.newCategory); 
            this.Update();

        }
    }
}
