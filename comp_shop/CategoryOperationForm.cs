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

        public CategoryOperationForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;
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
            dataGridView1.DataSource = DB.AllCategories();
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

        // радиокнопка удаления категории
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            textBox1.Enabled = false;
            button1.Text = "Удалить";
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
