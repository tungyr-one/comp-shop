﻿namespace comp_shop
{
    partial class Main_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchBox2 = new System.Windows.Forms.TextBox();
            this.searchParam2 = new System.Windows.Forms.Label();
            this.searchBox1 = new System.Windows.Forms.TextBox();
            this.searchParam1 = new System.Windows.Forms.Label();
            this.find = new System.Windows.Forms.Button();
            this.editItem = new System.Windows.Forms.Button();
            this.addItem = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьТоварToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьТоварToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вырезатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветФонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обАвтореToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.computerShopDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.computerShopDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Параметры поиска";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(36, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 291);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск по";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(6, 247);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(127, 24);
            this.radioButton6.TabIndex = 5;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "поставщику";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(7, 206);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(107, 24);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "продавцу";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(7, 165);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(239, 24);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "категории и цене товара";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 125);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(179, 24);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "категории товара";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 85);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(132, 24);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "цене товара";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 43);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(174, 24);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "названию товара";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.searchBox2);
            this.groupBox2.Controls.Add(this.searchParam2);
            this.groupBox2.Controls.Add(this.searchBox1);
            this.groupBox2.Controls.Add(this.searchParam1);
            this.groupBox2.Location = new System.Drawing.Point(36, 375);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 139);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры поиска";
            // 
            // searchBox2
            // 
            this.searchBox2.Location = new System.Drawing.Point(173, 84);
            this.searchBox2.Name = "searchBox2";
            this.searchBox2.Size = new System.Drawing.Size(131, 26);
            this.searchBox2.TabIndex = 3;
            this.searchBox2.Visible = false;
            // 
            // searchParam2
            // 
            this.searchParam2.AutoSize = true;
            this.searchParam2.Location = new System.Drawing.Point(6, 84);
            this.searchParam2.Name = "searchParam2";
            this.searchParam2.Size = new System.Drawing.Size(84, 20);
            this.searchParam2.TabIndex = 2;
            this.searchParam2.Text = "Цена до:";
            this.searchParam2.Visible = false;
            // 
            // searchBox1
            // 
            this.searchBox1.Location = new System.Drawing.Point(173, 42);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.Size = new System.Drawing.Size(131, 26);
            this.searchBox1.TabIndex = 1;
            // 
            // searchParam1
            // 
            this.searchParam1.AutoSize = true;
            this.searchParam1.Location = new System.Drawing.Point(6, 42);
            this.searchParam1.Name = "searchParam1";
            this.searchParam1.Size = new System.Drawing.Size(161, 20);
            this.searchParam1.TabIndex = 0;
            this.searchParam1.Text = "Название товара:";
            // 
            // find
            // 
            this.find.Location = new System.Drawing.Point(93, 532);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(170, 59);
            this.find.TabIndex = 7;
            this.find.Text = "Найти";
            this.find.UseVisualStyleBackColor = true;
            this.find.Click += new System.EventHandler(this.find_Click);
            // 
            // editItem
            // 
            this.editItem.Location = new System.Drawing.Point(65, 615);
            this.editItem.Name = "editItem";
            this.editItem.Size = new System.Drawing.Size(232, 37);
            this.editItem.TabIndex = 9;
            this.editItem.Text = "Редактировать товар";
            this.editItem.UseVisualStyleBackColor = true;
            this.editItem.Click += new System.EventHandler(this.editItem_Click);
            // 
            // addItem
            // 
            this.addItem.Location = new System.Drawing.Point(65, 676);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(237, 37);
            this.addItem.TabIndex = 10;
            this.addItem.Text = "Добавить новый товар";
            this.addItem.UseVisualStyleBackColor = true;
            this.addItem.Click += new System.EventHandler(this.addItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.правкаToolStripMenuItem,
            this.параметрыToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1130, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьТоварToolStripMenuItem,
            this.добавитьТоварToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // редактироватьТоварToolStripMenuItem
            // 
            this.редактироватьТоварToolStripMenuItem.Name = "редактироватьТоварToolStripMenuItem";
            this.редактироватьТоварToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.редактироватьТоварToolStripMenuItem.Text = "Редактировать товар";
            // 
            // добавитьТоварToolStripMenuItem
            // 
            this.добавитьТоварToolStripMenuItem.Name = "добавитьТоварToolStripMenuItem";
            this.добавитьТоварToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.добавитьТоварToolStripMenuItem.Text = "Добавить товар";
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.копироватьToolStripMenuItem,
            this.вырезатьToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            // 
            // вырезатьToolStripMenuItem
            // 
            this.вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            this.вырезатьToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.вырезатьToolStripMenuItem.Text = "Вырезать";
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.шрифтToolStripMenuItem,
            this.цветФонаToolStripMenuItem});
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // шрифтToolStripMenuItem
            // 
            this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
            this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.шрифтToolStripMenuItem.Text = "Шрифт";
            // 
            // цветФонаToolStripMenuItem
            // 
            this.цветФонаToolStripMenuItem.Name = "цветФонаToolStripMenuItem";
            this.цветФонаToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.цветФонаToolStripMenuItem.Text = "Цвет фона";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обАвтореToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // обАвтореToolStripMenuItem
            // 
            this.обАвтореToolStripMenuItem.Name = "обАвтореToolStripMenuItem";
            this.обАвтореToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.обАвтореToolStripMenuItem.Text = "Об авторе";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(382, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(29, 20);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(370, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(748, 675);
            this.dataGridView1.TabIndex = 14;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 725);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.addItem);
            this.Controls.Add(this.editItem);
            this.Controls.Add(this.find);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Main_form";
            this.Text = "Компьютерный магазин БД";
            this.Load += new System.EventHandler(this.Main_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.computerShopDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button find;
        private System.Windows.Forms.Button editItem;
        private System.Windows.Forms.Button addItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьТоварToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьТоварToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вырезатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шрифтToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветФонаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обАвтореToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.TextBox searchBox2;
        private System.Windows.Forms.Label searchParam2;
        private System.Windows.Forms.TextBox searchBox1;
        private System.Windows.Forms.Label searchParam1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.BindingSource computerShopDataSetBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

