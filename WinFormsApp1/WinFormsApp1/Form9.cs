using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);
            LoadDataToForm();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            LoadDataToForm();
        }
        private void LoadDataToForm()
        {
            radioButton1.Checked = true;
            comboBox1.SelectedIndex = 0;
            string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера], (select Дополнительные_услуги.Название) AS ДопУслуги from Дополнительные_услуги, [Бронирование дополнительных услуг], Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Бронирование дополнительных услуг].[Id бронирования] = Бронирования.[Id бронирования] and Дополнительные_услуги.[Id услуги] = [Бронирование дополнительных услуг].[Id услуги]";
            ControllerDB.LoadData(q, dataGridView1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера], (select Дополнительные_услуги.Название) AS ДопУслуги from Дополнительные_услуги, [Бронирование дополнительных услуг], Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Бронирование дополнительных услуг].[Id бронирования] = Бронирования.[Id бронирования] and Дополнительные_услуги.[Id услуги] = [Бронирование дополнительных услуг].[Id услуги]";
                    ControllerDB.LoadData(q, dataGridView1);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера], (select Дополнительные_услуги.Название) AS ДопУслуги from Дополнительные_услуги, [Бронирование дополнительных услуг], Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Бронирование дополнительных услуг].[Id бронирования] = Бронирования.[Id бронирования] and Дополнительные_услуги.[Id услуги] = [Бронирование дополнительных услуг].[Id услуги] \r\nand [Дата заезда] < GETDATE() and [Дата выезда] > GETDATE() ";
                    ControllerDB.LoadData(q, dataGridView1);
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера], (select Дополнительные_услуги.Название) AS ДопУслуги from Дополнительные_услуги, [Бронирование дополнительных услуг], Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Бронирование дополнительных услуг].[Id бронирования] = Бронирования.[Id бронирования] and Дополнительные_услуги.[Id услуги] = [Бронирование дополнительных услуг].[Id услуги] \r\nand [Дата заезда] > GETDATE()";
                    ControllerDB.LoadData(q, dataGridView1);
                }
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера] from Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера]";
                    ControllerDB.LoadData(q, dataGridView1);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера] from Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Дата заезда] < GETDATE() and [Дата выезда] > GETDATE() ";
                    ControllerDB.LoadData(q, dataGridView1);
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], Номера.[Id номера], [Тип номера] from Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Дата заезда] > GETDATE() ";
                    ControllerDB.LoadData(q, dataGridView1);
                }
            }
        }

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
