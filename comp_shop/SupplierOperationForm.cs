﻿using System;
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
    public partial class SupplierOperationForm : Form
    {
        public SupplierOperationForm()
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
            // вызов из закладки поставщиков для изменения поставщика
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

        // радиокнопка Добавление
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // изменение название кнопки в зависимости от выбранного радиокнопки
            button1.Text = "Добавить";
            button2.Text = "Готово";
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
            // определение нобходимого действия в зависимости от нажатой радиокнопки
            // добавление нового поставщика
            if (radioButton1.Checked)
            {
                // проверка на пустое имя поставщика
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Имя поставщика пусто!");
                    return;
                }

                // сущность нового поставщика для добавления в БД
                Supplier newSupplier = new Supplier();
                newSupplier.Name = textBox1.Text;
                newSupplier.Contacts = textBox2.Text;
                DB.AddSupplier(newSupplier);
            }
            // вызов окна выбора постащика
            else if (radioButton2.Checked)
            {
                // если окно было запущено из диалога изменения/создания товара
                if (this.Text == "Управление поставщиками")
                {
                    // запуск окна выбора со всеми поставщиками 
                    ShowInfoForm suppliersShow = new ShowInfoForm();
                    suppliersShow.Text = "Выбор поставщика";
                    suppliersShow.ShowDialog();

                    // назначение текстовым полям формы изменения поставщика значений выбранного поставщика
                    textBox1.Text = MainForm.currentSupplier.Name;
                    textBox2.Text = MainForm.currentSupplier.Contacts;
                }
                // если окно было запущено из закладки поставщиков
                else
                {
                    // создание временной сущности поставщика для редактирования
                    Supplier editSupplier = new Supplier();
                    // назначение полей текущей сущности поставщика временной сущности
                    editSupplier.SupplierID = MainForm.currentSupplier.SupplierID;
                    editSupplier.Name = textBox1.Text;
                    editSupplier.Contacts = textBox2.Text;
                }
            }
            // если 3-я радиокнопка - удаление - нажата
            else
            {
                // если окно было запущено из диалога изменения/создания товара
                if (this.Text == "Управление поставщиками")
                {
                    // запуск окна выбора со всеми поставщиками 
                    ShowInfoForm suppliersShow = new ShowInfoForm();
                    suppliersShow.Text = "Выбор поставщика";
                    suppliersShow.ShowDialog();

                    // назначение текстовым полям формы удаления поставщика значений выбранного поставщика
                    textBox1.Text = MainForm.currentSupplier.Name;
                    textBox2.Text = MainForm.currentSupplier.Contacts;
                }

            }
        }

        // завершение процесса
        private void button2_Click(object sender, EventArgs e)
        {
            // проверка заполненности поля названия категории
            if (textBox1.Text == "")
            {
                MessageBox.Show("Имя поставщика пусто!");
                return;
            }
            else
            {
                // кнопка "Готово" после добавления поставщика
                if (radioButton1.Checked)
                {

                }
                // кнопка "Изменить" после выбора и редактирования поставщика
                else if (radioButton2.Checked)
                {
                    MainForm.currentSupplier.Name = textBox1.Text;
                    MainForm.currentSupplier.Contacts = textBox2.Text;

                    DB.EditSupplier(MainForm.currentSupplier);
                }
                // кнопка "Удалить" после выбора поставщика
                else
                {
                    // создание временной сущности поставщика для удаления
                    Supplier removeSupplier = new Supplier();
                    removeSupplier.Name = textBox1.Text;

                    // проверка изменения пользователем имени удаляемой сущности
                    if (MainForm.currentSupplier.Name != removeSupplier.Name)
                    {
                        // обновление ID удаляемой сущности
                        removeSupplier = DB.SearchSupplier(supplierName: removeSupplier.Name);
                        if (removeSupplier == null)
                        { MessageBox.Show("Неправильное имя поставщика или такого не существует!");
                            return;
                        }
                    }
                    else
                    {
                        // назначение ID текущей сущности поставщика временной сущности
                        removeSupplier.SupplierID = MainForm.currentSupplier.SupplierID;
                    }                    
                    DB.RemoveSupplier(removeSupplier);
                }
            this.Close();
            }
        }
    }
}

