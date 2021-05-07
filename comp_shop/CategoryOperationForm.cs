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
    public partial class CategoryOperationForm : Form
    {
        public string newCategory;
        Category workingCategory = new Category();
        List<Category> categoriesForDataGridView = DB.AllCategories();

        public CategoryOperationForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;            

            // удаление категории no category из списка всех категорий
            var noCategoryRemove = categoriesForDataGridView.Single(r => r.CategoryID == 1);
            categoriesForDataGridView.Remove(noCategoryRemove);
            dataGridView1.DataSource = categoriesForDataGridView;
        }

        // загрузка окна
        private void CategoryOperationForm_Load(object sender, EventArgs e)
        {
            // обновление и загрузка данных в таблицу категорий
            DataGridViewUpdate();
        }

        // нажатие кнопки добавить / удалить
        private void button1_Click(object sender, EventArgs e)
        {
            // определение нобходимого действия в зависимости от нажатой радиокнопки
            // добавление
            if (radioButton1.Checked)
            {
                // проверка заполненности поля названия категории
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Категория пуста!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DB.AddCategory(textBox1.Text);
            }
            //изменение
            else if (radioButton2.Checked)
            {

                // проверка заполненности поля названия категории
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Категория пуста!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                workingCategory.Name = textBox1.Text;
                DB.EditCategory(workingCategory);
                textBox1.Clear();
            }
            //удаление
            else
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                DB.RemoveCategory(rows[0].Cells[1].Value.ToString());
            }
            DataGridViewUpdate();
            button2.Text = "Готово";
        }

        // обновление и загрузка данных в таблицу категорий
       private void DataGridViewUpdate()
        {
            dataGridView1.AutoGenerateColumns = true;
            categoriesForDataGridView = DB.AllCategories();
            // удаление категории no category из списка всех категорий
            var noCategoryRemove = categoriesForDataGridView.Single(r => r.CategoryID == 1);
            categoriesForDataGridView.Remove(noCategoryRemove);

            dataGridView1.DataSource = categoriesForDataGridView;
            dataGridView1.Columns["CategoryID"].HeaderText = "ID категории";
            dataGridView1.Columns["Name"].HeaderText = "Название";
            dataGridView1.Columns["Items"].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        // нажатие кнопки отмена
        private void button2_Click(object sender, EventArgs e)
        {
            newCategory = null;
            this.Close();
        }

        // радиокнопка добавления категории
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            textBox1.Enabled = true;
            button1.Text = "Добавить";
        }

        // радиокнопка изменения категории
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            workingCategoryUpdate();
            textBox1.Enabled = true;
            textBox1.Text = workingCategory.Name;
            button1.Text = "Изменить";
        }

        // радиокнопка удаления категории
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            button1.Text = "Удалить";
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)
                try
                {
                    workingCategoryUpdate();
                }
                catch
                {

                }
        }

        //обновление сущности текущей категории и обновление содержимого поля с данными сущности
        private Category workingCategoryUpdate()
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            workingCategory = DB.SearchCategory(rows[0].Cells[1].Value.ToString());
            textBox1.Text = workingCategory.Name;
            return workingCategory;
        }
    }
}
