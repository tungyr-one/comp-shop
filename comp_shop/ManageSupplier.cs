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
    // TODO: заменить на SupplierOperationForm?
    public partial class ManageSupplier : Form
    {
        public string newSupplier = null;

        public ManageSupplier()
        {
            InitializeComponent();
        }

        private void ManageSupplier_Load(object sender, EventArgs e)
        {
            // вызов из закладки поставщиков для создания поставщика
            if (this.Text == "Новый поставщик")
            {
                radioButton1.Enabled = true;
                radioButton1.Checked = true;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
            }
            // вызов из закладки поставщиков изменения поставщика
            else if (this.Text == "Изменить поставщика")
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = true;
                radioButton2.Checked = true;
                radioButton3.Enabled = false;

                // назначение текстовым полям значений текущего поставщика
                textBox1.Text = MainForm.currentSupplier.Name;
                textBox2.Text = MainForm.currentSupplier.Contacts;

            }
            // вызов из окна операций с товаром
            else
            {
                radioButton1.Enabled = true;
                radioButton1.Checked = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;

            }
        }

        // завершение процесса
        private void button2_Click(object sender, EventArgs e)
        {
            newSupplier = null;
            this.Close();
        }

        // радиокнопка Добавление
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // изменение название кнопки в зависимости от выбранного радиокнопки
            button1.Text = "Добавить";
            textBox2.Enabled = true;
            // очистка поля имени и контактов
            textBox1.Text = "";
            textBox2.Text = "";

        }

        // радиокнопка Изменение
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // если окно было запущено из диалога изменения/создания товара
            if (this.Text == "Управление поставщиками")
            {
                button1.Text = "Выбрать";
                button2.Text = "Изменить";
            }
            else
            {
            button1.Text = "Изменить";
            button2.Text = "Готово";
            }

            textBox2.Enabled = true;
            // назначение текстовым полям значений текущего поставщика
            textBox1.Text = MainForm.currentSupplier.Name;
            textBox2.Text = MainForm.currentSupplier.Contacts;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Выбрать";
            button2.Text = "Удалить";
            textBox2.Enabled = false;
        }

        // нажатие кнопки добавить / выбрать
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
                Supplier newSupplier = new Supplier();
                newSupplier.Name = textBox1.Text;
                newSupplier.Contacts = textBox2.Text;
                DB.AddSupplier(newSupplier);
            }
            else if (radioButton2.Checked)
            {
                // если окно было запущено из диалога изменения/создания товара
                if (this.Text == "Управление поставщиками")
                {
                    ShowInfoForm suppliersShow = new ShowInfoForm();
                    suppliersShow.Text = "Выбор поставщика";
                    suppliersShow.ShowDialog();
                }
                // если окно было запущено из закладки поставщиков
                else
                {
                    Supplier editSupplier = new Supplier();
                    editSupplier.SupplierID = MainForm.currentSupplier.SupplierID;
                    editSupplier.Name = textBox1.Text;
                    editSupplier.Contacts = textBox2.Text;
                    DB.editSupplier(editSupplier);
                }
            }
            else
            {
                if (this.Text == "Управление поставщиками")
                {
                    ShowInfoForm suppliersShow = new ShowInfoForm();
                    suppliersShow.Text = "Выбор поставщика";
                    suppliersShow.ShowDialog();
                }
                //DB.RemoveSupplier(textBox1.Text);
            }
            button2.Text = "Готово";
        }


    }
}

