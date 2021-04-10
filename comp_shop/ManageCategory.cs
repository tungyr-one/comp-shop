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
    public partial class ManageCategory : Form
    {
        public string newCategory;

        public ManageCategory()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

         // нажатие кнопки добавить / удалить
        private void button1_Click(object sender, EventArgs e)
        {
            // проверка заполненности поля названия категории
            if (textBox1.Text == "")
            {
                MessageBox.Show("Категория пуста!");
            }

            // определение нобходимого действия в зависимости от нажатой радиокнопки
            if (radioButton1.Checked)
            {
                DB.AddCategory(textBox1.Text);
            }
            else
            {
                DB.RemoveCategory(textBox1.Text);
            }
            button2.Text = "Готово";
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            newCategory = null;
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Добавить";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Удалить";
        }
    }
}
