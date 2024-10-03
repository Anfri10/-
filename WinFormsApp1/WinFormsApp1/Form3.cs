using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int role = 0;
            string login = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            string q = "select Роль from Пользователи where Логин = @Логин and Пароль = @Пароль";
            List<ParametdSqlCmd> sqlParameterCollection = new List<ParametdSqlCmd>();
            sqlParameterCollection.Add(new ParametdSqlCmd("@Логин", login));
            sqlParameterCollection.Add(new ParametdSqlCmd("@Пароль", password));

            role = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection));
            
            if (role == 0)
            {
                MessageBox.Show("Неверные данные, проверьте логин и пароль");
                return;
            }

            if (role == 1)
            {
                Form4 form4 = new Form4();
                form4.Show();
                DialogResult = DialogResult.OK;
            }
            else if (role == 2)
            {
                
                q = "select [Id Пользователя] from Пользователи where Логин = @Логин and Пароль = @Пароль";
                int userID = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection));
                sqlParameterCollection.Add(new ParametdSqlCmd("@userID", userID));
                q = "select [Id Клиента] from Клиенты where [Id Пользователя] = @userID";
                int clientId = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection));
                Form5 form5 = new Form5(userID, clientId);
                form5.Show();
                DialogResult = DialogResult.OK;
            }
            else if (role == 3)
            {
                Form9 form9 = new Form9();
                form9.Show();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
