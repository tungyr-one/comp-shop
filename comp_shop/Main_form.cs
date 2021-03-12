using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Требования к функциональным характеристикам:
//-	программа должна обеспечивать доступ к информации о товарах магазина компьютерной техники, 
//    содержащейся в БД;
//-	программа должна выдавать информацию о категориях товаров, о поставщиках, 
//    продавцах и других записях в БД;
//-	программа должна обеспечивать поиск по информации в БД в соответствии с 
//    различными критериями поиска:
//a)	нужного товара по его названию; 
//товаров по диапазону цен;
//товаров по категории;
//товаров по категории и по диапазону цен;
//b)	по какой цене и сколько товара продал каждый продавец за указанный интервал времени; 
//какие товары поставляет определенный поставщик.
//-  программа должна обладать формой пользовательского интерфейса для ввода 
//    информации об объектах и корректировки данных в случае необходимости;

namespace comp_shop
{
    // TODO: Cпросить: про десятую лабу про фокус форм
    public partial class Main_form : Form
    {
        List<Article> working_items = new List<Article>();
        Article current_item = null;

        public Main_form()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        // нажатие кнопки добавить
        private void addItem_Click(object sender, EventArgs e)
        {
            NewItemForm new_item_form = new NewItemForm();
            new_item_form.ShowDialog();
            if (new_item_form.my_item == null)
                return;
            //TODO: спросить Почему my_item, будучи объектом класса Item не имеет доступа к приватным методам my_item.DBFormat()
            DB.addToDB(new_item_form.my_item.DBFormat());
            current_item = new_item_form.my_item;
            richTextBox1.Text += current_item.ToString();
        }

        // нажатие кнопки редактировать
        private void editItem_Click(object sender, EventArgs e)
        {
            if (current_item == null)
            {
                MessageBox.Show("Не выбрано ни одного товара!");
                return;
            }
            else
            {
                NewItemForm new_item_form = new NewItemForm();
                //TODO: Спровить:как правильно передача в новую форму выбранного товара для редактирования
                new_item_form.my_item = current_item;
                new_item_form.ShowDialog();
                // TODO: Спросить: проверка изменений, внесенных в данные товара, как?
                // Можно ли просто сравнить сами объекты до и после изменения?
            }
        }

        // нажатие кнопки поиск
        private void find_Click(object sender, EventArgs e)
        {
            // TODO: text fields validation first
            if (radioButton1.Checked)
            {
               var search_result = DB.SearchByName(searchBox1.Text);
                // почему не работает Concat?
                //working_items.Concat(search_result);
                SearchResultTreat(search_result);
            }

            if (radioButton2.Checked)
                try
                {
                    DB.SearchByPrice(double.Parse(searchBox1.Text), double.Parse(searchBox2.Text));
                }
                catch
                {
                    MessageBox.Show("Неправильная цена!");
                }

            if (radioButton3.Checked)
                try
                {
                    DB.SearchByCategory(searchBox1.Text);
                }
                catch
                {
                    MessageBox.Show("Неправильная категория!");
                }

            if (radioButton4.Checked)
                try
                {
                    DB.SearchByCategoryAndPrice(searchBox1.Text, double.Parse(searchBox2.Text));
                }
                catch
                {
                    MessageBox.Show("Неправильная категория или цена!");
                }

            if (radioButton5.Checked)
            {
                DB.SearchBySeller(searchBox1.Text);
            }

            if (radioButton6.Checked)
            {
                DB.SearchBySupplier(searchBox1.Text);
            }
        }

        private void SearchResultTreat(List<Article> items_list)
        {
            richTextBox1.Clear();
            foreach (Article list_item in items_list)
            {
                richTextBox1.Text += list_item.ToString();
                working_items.Add(list_item);
            }
            current_item = working_items[0];
        }



        // выбор чекбоксов
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                searchParam1.Text = "Название товара:";
                searchParam2.Visible = false;
                searchBox2.Visible = false;
            }

            if (radioButton2.Checked)
            {
                // TODO: поля увеличивать и смещать левее, координаты как установить?
                searchParam1.Text = "Цена от: ";
                searchParam2.Text = "Цена до: ";
                searchParam2.Visible = true;
                searchBox2.Visible = true;
            }

            if (radioButton3.Checked)
            {
                // TODO: Сделать комбо категории товара
                searchParam1.Text = "Категория товара:";
                searchParam2.Visible = false;
                searchBox2.Visible = false;
            }

            if (radioButton4.Checked)
            {
                // TODO: Сделать комбо категории товара
                searchParam1.Text = "Категория: ";
                searchParam2.Text = "Цена: ";
                searchParam2.Visible = true;
                searchBox2.Visible = true;
            }

            if (radioButton5.Checked)
            {
                searchParam1.Text = "Продавец:";
                searchParam2.Visible = false;
                searchBox2.Visible = false;
            }

            if (radioButton6.Checked)
            {
                searchParam1.Text = "Поставщик:";
                searchParam2.Visible = false;
                searchBox2.Visible = false;
            }
        }

        // проперти текущего товара
        public Article ItemGetter
        {
            get { return current_item; }
        }


        // https://www.entityframeworktutorial.net/basics/how-entity-framework-works.aspx
       

        private void Main_form_Load(object sender, EventArgs e)
        {
            DB.ShowAllItems();
            //dataGridView1.AutoGenerateColumns = true;
            //dataGridView1.DataSource = queryAllItems;
        }
    }
}
