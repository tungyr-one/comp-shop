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
    public partial class ManageSupplier : Form
    {
        public string newSupplier = null;

        public ManageSupplier()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        // завершение процесса
        private void button2_Click(object sender, EventArgs e)
        {
            newSupplier = null;
            this.Close();
        }

        // изменение название кнопки в зависимости от выбранного радиокнопки
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Добавить";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Удалить";
            textBox2.Enabled = false;
        }

        // нажатие кнопки добавить / удалить
        private void button1_Click(object sender, EventArgs e)
        {
            // проверка заполненности поля названия категории
            if (textBox1.Text == "")
            {
                MessageBox.Show("Имя поставщика пусто!");
            }

            // определение нобходимого действия в зависимости от нажатой радиокнопки
            if (radioButton1.Checked)
            {
                DB.AddSupplier(textBox1.Text, textBox2.Text);
            }
            else
            {
                DB.RemoveSupplier(textBox1.Text);
            }
            button2.Text = "Готово";
        }
    }
}

