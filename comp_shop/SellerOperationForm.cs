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
    public partial class SellerOperationForm : Form
    {
        public string newSeller;

        public SellerOperationForm()
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
                // проверка заполненности поля имени и контактов продавца
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Поля пусты!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DB.AddSeller(textBox1.Text, textBox2.Text);
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                DB.RemoveSeller(rows[0].Cells[1].Value.ToString());
            }
            DataGridViewUpdate();
            button2.Text = "Готово";
        }

        // нажатие кнопки отмена
        private void button2_Click(object sender, EventArgs e)
        {
            newSeller = null;
            this.Close();
        }

        // радиокнопка добавления продавца
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button1.Text = "Добавить";
        }

        // радиокнопка удаления продавца
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Text = "Удалить";
        }


        // обновление и загрузка данных в таблицу категорий
        private void DataGridViewUpdate()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = DB.ShowAllSellers();
            dataGridView1.Columns["SellerID"].HeaderText = "ID продавца";
            dataGridView1.Columns["Name"].HeaderText = "Имя";
            dataGridView1.Columns["Contacts"].HeaderText = "Контакты";
            dataGridView1.Columns["Orders"].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
