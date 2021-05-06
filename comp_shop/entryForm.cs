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
        public string login = "";
        public string password = "";

        public entryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            password = textBox2.Text;

            if (login != "admin" && password != "0000")
            {
                MessageBox.Show("Wrong credentials!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login = "exitApp";
            this.Close();
        }
    }
}
