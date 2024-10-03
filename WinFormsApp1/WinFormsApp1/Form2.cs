using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            string q = "select COUNT(*) from Пользователи where Логин = @Логин;";
            List<ParametdSqlCmd> sqlParameterCollection = new List<ParametdSqlCmd>();
            sqlParameterCollection.Add(new ParametdSqlCmd("@Логин", login));

            int count = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection));
            if (count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует, введите другой логин");
                return;
            }
            else
            {
               
                q = "insert into Пользователи values(@Логин, @Пароль, @Роль); SELECT SCOPE_IDENTITY();";
                
                sqlParameterCollection.Add(new ParametdSqlCmd("@Пароль", password));
                sqlParameterCollection.Add(new ParametdSqlCmd("@Роль", 2));
                int id = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection));

                q = "insert into Клиенты([Id пользователя]) values(@Id);";

                sqlParameterCollection.Add(new ParametdSqlCmd("@Id", id));
                ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection);
                MessageBox.Show("Регистрация прошла успешно");
                DialogResult = DialogResult.OK;

            }
        }
    }
}
