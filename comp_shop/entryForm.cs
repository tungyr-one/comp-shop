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
    public partial class entryForm : Form
    {
        // переменная состояния логина
        internal bool entrySuccess = false;

        public entryForm()
        {
            InitializeComponent();
            //TODO: temp field values - delete
            textBox1.Text = "admin";
            textBox2.Text = "1111";
        }

        // обработка кнопки войти
        private void button1_Click(object sender, EventArgs e)
        {
            // поиск в БД юзера
            MainForm.currentAccount = DB.SearchSeller(textBox1.Text);

            // если юзера не нашлось
            if (MainForm.currentAccount == null)
            {
                MessageBox.Show("Нет такого пользователя!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                entrySuccess = false;
                return;
            }

            // если пароль неверный
            else if (MainForm.currentAccount.Password != textBox2.Text)
            {
                MessageBox.Show("Неверный пароль!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                entrySuccess = false;
                return;
            }
            else {
                entrySuccess = true;
                this.Close(); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            entrySuccess = false;            
            this.Close();
        }

        // обработка нажатия Enter
        private void entryForm_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Enter)
                {
                    button1.PerformClick();
                }
        }
    }
}
