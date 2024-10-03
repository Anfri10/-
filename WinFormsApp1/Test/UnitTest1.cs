using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        private const string ConnectionString = "Data Source= ADCLG1; Initial catalog=Фрич_УП; Integrated Security=True; TrustServerCertificate=true";
        int clientID = 0;
        int userID;

        [TestMethod]
        public void UserTestMethod1_AddNewUser()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "INSERT INTO Пользователи (Логин, Пароль, Роль) VALUES (@Login, @Password, @Role); SELECT SCOPE_IDENTITY();";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2, pr3;

            pr1 = new SqlParameter("@Login", "user11");
            pr2 = new SqlParameter("@Password", "password1");
            pr3 = new SqlParameter("@Role", 2);

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);
            userID = Convert.ToInt32(cmd1.ExecuteScalar());

            comDel = "SELECT Логин FROM Пользователи WHERE [Id Пользователя] = @Id";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Id", userID);
            cmd1.Parameters.Add(pr1);
            string result = cmd1.ExecuteScalar().ToString();
            string expected = "user11";

            dataBase.CloseConnection();

            Assert.AreEqual(expected, result, "Ожидаемый логин не был получен!");
        }

        [TestMethod]
        public void UserTestMethod2_UpdateUserPassword()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "UPDATE Пользователи SET Пароль = @Password WHERE  Логин= @Логин";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@Логин", "user11");
            pr2 = new SqlParameter("@Password", "password1_updated");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Пароль FROM Пользователи WHERE Логин= @Логин";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Логин", "user11");
            cmd1.Parameters.Add(pr1);
            string result = (cmd1.ExecuteScalar()).ToString();
            string expected = "password1_updated";
            dataBase.CloseConnection();

            Assert.AreEqual(expected, result, "Ожидаемый пароль не был получен!");
        }

        [TestMethod]
        public void UserTestMethod3_DeleteUser()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();
            string comDel = "DELETE FROM Пользователи WHERE Логин= @Логин";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1;
            pr1 = new SqlParameter("@Логин", "user11");
            cmd1.Parameters.Add(pr1);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT COUNT(*) FROM Пользователи WHERE Логин= @Логин";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Логин", "user11");
            cmd1.Parameters.Add(pr1);
            var result = Convert.ToInt32(cmd1.ExecuteScalar());
            dataBase.CloseConnection();

            Assert.AreEqual(0, result, "Ожидаемый пользователь не был удален!");
        }
        [TestMethod]
        public void UserTestMethod4_AddUserWithInvalidData()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "INSERT INTO Пользователи (Логин, Пароль, Роль) VALUES (@Login, @Password, @Role)";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2, pr3;

            pr1 = new SqlParameter("@Login", null); // Invalid login
            pr2 = new SqlParameter("@Password", "password1");
            pr3 = new SqlParameter("@Role", 2);

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);

            try
            {
                cmd1.ExecuteNonQuery();
                dataBase.CloseConnection();
                Assert.Fail("Ожидаемое исключение не было получено!");
            }
            catch (SqlException)
            {
                dataBase.CloseConnection();
            }
        }


        [TestMethod]
        public void ClientTestMethod1_AddNewClient()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "INSERT INTO Клиенты (ФИО, [Номер телефона], email, [Id пользователя]) VALUES (@Fio, @Phone, @Email, @UserId)";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2, pr3, pr4;

            pr1 = new SqlParameter("@Fio", "Иванов Иван Иванович");
            pr2 = new SqlParameter("@Phone", "1234567890");
            pr3 = new SqlParameter("@Email", "ivanov@example.com");
            pr4 = new SqlParameter("@UserId", 6);

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);
            cmd1.Parameters.Add(pr4);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT ФИО FROM Клиенты WHERE [Номер телефона] = @Phone";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Phone", "1234567890");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            var expected = "Иванов Иван Иванович";

            dataBase.CloseConnection();

            Assert.AreEqual(expected, result, "Ожидаемое ФИО не было получено!");
        }

        [TestMethod]
        public void ClientTestMethod2_UpdateClientFIO()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "UPDATE Клиенты SET ФИО = @Fio WHERE [Номер телефона] = @Phone";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@Fio", "Иванов Иван Иванович_updated");
            pr2 = new SqlParameter("@Phone", "1234567890");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT ФИО FROM Клиенты WHERE [Номер телефона] = @Phone";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Phone", "1234567890");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            string expected = "Иванов Иван Иванович_updated";
            dataBase.CloseConnection();

            Assert.AreEqual(expected, result, "Ожидаемое ФИО не было получено!");
        }

        [TestMethod]
        public void ClientTestMethod4_DeleteClient()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();
            string comDel = "DELETE FROM Клиенты WHERE [Номер телефона] = @Phone";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1;
            pr1 = new SqlParameter("@Phone", "1234567890");
            cmd1.Parameters.Add(pr1);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT ФИО FROM Клиенты WHERE [Номер телефона] = @Phone";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Phone", "1234567890");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            dataBase.CloseConnection();

            Assert.AreEqual(null, result, "Ожидаемый клиент не был удален!");
        }
        

        [TestMethod]
        public void ClientTestMethod3_UpdateClientEmail()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "UPDATE Клиенты SET email = @Email WHERE [Номер телефона] = @Phone";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@Email", "newemail@example.com");
            pr2 = new SqlParameter("@Phone", "1234567890");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT email FROM Клиенты WHERE [Номер телефона] = @Phone";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Phone", "1234567890");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            string expected = "newemail@example.com";
            dataBase.CloseConnection();

            Assert.AreEqual(expected, result, "Ожидаемый email не был получен!");
        }
        [TestMethod]
        public void ServiceTestMethod1_AddNewService()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();

            string comDel = "INSERT INTO Дополнительные_услуги (Название, Описание, Стоимость) VALUES (@Name, @Description, @Cost)";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2, pr3;

            pr1 = new SqlParameter("@Name", "Завтрак");
            pr2 = new SqlParameter("@Description", "Континентальный завтрак");
            pr3 = new SqlParameter("@Cost", 500.00);

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Название FROM Дополнительные_услуги WHERE Описание = @Description";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Description", "Континентальный завтрак");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            var expected = "Завтрак";

            dataBase.CloseConnection();

            Assert.AreEqual(expected, result, "Ожидаемое название услуги не было получено!");
        }

        [TestMethod]
        public void ServiceTestMethod2_DeleteService()
        {
            DataBase dataBase = new DataBase(ConnectionString);
            dataBase.OpenConnection();
            string comDel = "DELETE FROM Дополнительные_услуги WHERE Название = @Name";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1;
            pr1 = new SqlParameter("@Name", "Завтрак");
            cmd1.Parameters.Add(pr1);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Название FROM Дополнительные_услуги WHERE Название = @Name";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@Name", "Завтрак");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            dataBase.CloseConnection();

            Assert.AreEqual(null, result, "Ожидаемая услуга не была удалена!");
        }
    }
}
