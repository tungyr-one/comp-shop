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
        Seller workingSeller = new Seller();

        public SellerOperationForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        // загрузка окна
        private void CategoryOperationForm_Load(object sender, EventArgs e)
        {
            // обновление и загрузка данных в таблицу продавцов
            DataGridViewUpdate();
            // по умолчанию нажата первая радиокнопка
            radioButton1.Checked = true;
            // заполнение комбобокса
            List<string> dataSource =  new List<string>() {"admin", "seller"};
            comboBox1.DataSource = dataSource;
        }


        // радиокнопка Добавление
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // изменение название кнопки в зависимости от выбранного радиокнопки
            button1.Text = "Добавить";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;


            // очистка полей имени, контактов и пароля после переключения с других радиокнопок
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear(); 
        }

        // радиокнопка Изменение
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            workingSellerUpdate();
            button1.Text = "Изменить";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
        }
        // радиокнопка Удаление
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            workingSellerUpdate();
            button1.Text = "Удалить";
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
        }

        // нажатие кнопки добавить / изменить / удалить
        private void button1_Click(object sender, EventArgs e)
        {
            // определение нобходимого действия в зависимости от нажатой радиокнопки
            // добавление продавца
            if (radioButton1.Checked)
            {
                // проверка заполненности поля имени и контактов продавца
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Поля пусты!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DB.AddSeller(textBox1.Text, textBox2.Text, comboBox1.SelectedValue.ToString(), textBox3.Text);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            // изменение продавца
            else if (radioButton2.Checked)
            {
                // проверка заполненности поля имени и контактов продавца
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Поля пусты!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                workingSeller.Name = textBox1.Text;
                workingSeller.Contacts = textBox2.Text;
                workingSeller.AccountType = comboBox1.SelectedValue.ToString();
                workingSeller.Password = textBox3.Text;

                DB.EditSeller(workingSeller);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            // удаление 
            else
            {
                //var sellerRemove = workingSellerUpdate();
                // проверка на попытку удаления админа
                if (workingSeller.Name == "admin")
                {
                    MessageBox.Show("Администратора удалить нельзя!", "Внимаение!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    DB.RemoveSeller(workingSeller.Name);
                }
                
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

        // обработка нажатия Enter
        private void sellerFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        // обновление и загрузка данных в таблицу категорий
        private void DataGridViewUpdate()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = DB.AllSellers();
            dataGridView1.Columns["SellerID"].HeaderText = "ID продавца";
            dataGridView1.Columns["Name"].HeaderText = "Имя";
            dataGridView1.Columns["Contacts"].HeaderText = "Контакты";
            dataGridView1.Columns["AccountType"].HeaderText = "Тип аккаунта";
            dataGridView1.Columns["Password"].HeaderText = "Пароль";
            dataGridView1.Columns["Orders"].Visible = false;          
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(!radioButton1.Checked) 
            try
            {
                workingSellerUpdate();
            }
            catch
            {
                    
            }
        }

        //обновление сущности текущего продавца и обновление содержимого полей с данными сущности
        private Seller workingSellerUpdate()
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            workingSeller = DB.SearchSeller(rows[0].Cells[1].Value.ToString());
            textBox1.Text = workingSeller.Name;
            textBox2.Text = workingSeller.Contacts;
            comboBox1.SelectedItem = workingSeller.AccountType.ToString();
            textBox3.Text = workingSeller.Password;
            return workingSeller;
        }
    }
}
