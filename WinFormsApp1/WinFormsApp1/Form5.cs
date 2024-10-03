using Microsoft.VisualBasic.Logging;
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
    public partial class Form5 : Form
    {
        private int userID;
        private int clientId;
        public Form5(int userID, int clientId)
        {
            this.userID = userID;
            this.clientId = clientId;
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);

        }

        public int UserID { get => userID; set => userID = value; }
        public int ClientId { get => clientId; set => clientId = value; }

        private void LoadDataToForm()
        {
            string q = "select [Дата заезда], [Дата выезда], [Статус бронирования], [Тип номера], Стоимость from Бронирования inner join Номера on Бронирования.[Id номера] = Номера.[Id номера] where [Id клиента] = " + ClientId;
            ControllerDB.LoadData(q, dataGridView1);

            List<ParametdSqlCmd> sqlParameterCollection = new List<ParametdSqlCmd>();
            sqlParameterCollection.Add(new ParametdSqlCmd("@Id", ClientId));
            q = "select ФИО, email, [Номер телефона] from Клиенты where [Id клиента] = @Id";
            List<object> clientData = ControllerDB.DoQueryWithReturnList(q, sqlParameterCollection);
            if (clientData.Count > 0)
            {

                textBox1.Text = clientData[0].ToString();
                textBox2.Text = clientData[1].ToString();
                textBox3.Text = clientData[2].ToString();
            }

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadDataToForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ParametdSqlCmd> sqlParameterCollection = new List<ParametdSqlCmd>();
            sqlParameterCollection.Add(new ParametdSqlCmd("@Id", ClientId));
            sqlParameterCollection.Add(new ParametdSqlCmd("@ФИО", textBox1.Text.Trim()));
            sqlParameterCollection.Add(new ParametdSqlCmd("@email", textBox2.Text.Trim()));
            sqlParameterCollection.Add(new ParametdSqlCmd("@НомерТ", textBox3.Text.Trim()));

            string q = "update Клиенты set ФИО = @ФИО, email =@email, [Номер телефона]=@НомерТ where [Id клиента] = @Id";
            ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.ClientID = ClientId;
            form6.ShowDialog();
            if (form6.DialogResult == DialogResult.OK)
            {
                LoadDataToForm();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (textBox1.Text.Trim().Length == 0 || textBox2.Text.Trim().Length == 0 || textBox3.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Заполните все данные в полях");
                    tabControl1.SelectedIndex = 0;
                }
            }
            List<ParametdSqlCmd> sqlParameterCollection = new List<ParametdSqlCmd>();
            sqlParameterCollection.Add(new ParametdSqlCmd("@Id", ClientId));
            sqlParameterCollection.Add(new ParametdSqlCmd("@ФИО", textBox1.Text.Trim()));
            sqlParameterCollection.Add(new ParametdSqlCmd("@email", textBox2.Text.Trim()));
            sqlParameterCollection.Add(new ParametdSqlCmd("@НомерТ", textBox3.Text.Trim()));

            string q = "update Клиенты set ФИО = @ФИО, email =@email, [Номер телефона]=@НомерТ where [Id клиента] = @Id";
            ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection);

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
