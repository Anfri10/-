using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace WinFormsApp1
{

    internal class ControllerDB
    {
        public static string connectString = " Data Source= ADCLG1; Initial catalog=Фрич_УП; Integrated Security=True; TrustServerCertificate=true";

        //выполенение запроса с возвращением результата
        public static object DoQueryWithReturnScalar(string q)
        {
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();    //Открываем соединение
            SqlCommand cmd = new SqlCommand(q, myConnection);

            var result = cmd.ExecuteScalar();
            myConnection.Close(); // разрываем соединение с БД
            return result;
        }
        public static void LoadDataToComboBox(string q, ComboBox cmb, string nameColumnValueMember)
        // метод загрузки данных в падающий список с любым запросом
        {

            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();    // открываем соединение
            SqlCommand cmd = new SqlCommand(q, myConnection);  // создание SQL команды с запросом
            SqlDataAdapter da = new SqlDataAdapter(cmd);    // выполнение команды
            DataTable tb = new DataTable(); // создание таблицы
            da.Fill(tb);        // загрузка данных в таблицу
            cmb.DataSource = tb;  // привязка полученной таблицы к компоненту              
            cmb.DisplayMember = nameColumnValueMember;  // значения для вывода
            cmb.ValueMember = nameColumnValueMember;   // фактические значения
            cmb.SelectedIndex = -1;
            myConnection.Close(); // разрываем соединение с БД

        }

        public static void LoadData(string q, DataGridView dgv)
        // метод загрузки данных в любую таблицу с любым запросом
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(connectString);
                myConnection.Open();    //Открываем соединение
                SqlCommand cmd = new SqlCommand(q, myConnection);
                // создание SQL команды с запросом
                SqlDataAdapter da = new SqlDataAdapter(cmd);    // выполнение команды
                DataTable tb = new DataTable(); // создание таблицы
                da.Fill(tb);    // загрузка данных в таблицу
                dgv.DataSource = tb;  // привязка полученной таблицы к компоненту
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                myConnection.Close(); // разрываем соединение с БД
            }
            catch
            {
                
            }
        }
        public static int DoQuery(string q)
        {
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();    //Открываем соединение
            SqlCommand cmd = new SqlCommand(q, myConnection);
            int count = cmd.ExecuteNonQuery();
            myConnection.Close(); // разрываем соединение с БД
            return count;
        }
        public static object DoQueryWithReturnScalar(string q, List<ParametdSqlCmd> sqlparCollection)
        {
            try
            {

                SqlConnection myConnection = new SqlConnection(connectString);
                myConnection.Open();    //Открываем соединение
                SqlCommand cmd = new SqlCommand(q, myConnection);
                foreach (var param in sqlparCollection)
                {
                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
                var result = cmd.ExecuteScalar();
                myConnection.Close(); // разрываем соединение с БД
                return result;
            }
            catch { }
            return null;
        }
        
        public static object DoQueryWithReturnScalar(string q, List<ParametdSqlCmd> sqlparCollection, SqlParameter sqlParameter)
        {

            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();    //Открываем соединение
            SqlCommand cmd = new SqlCommand(q, myConnection);
            foreach (var param in sqlparCollection)
            {
                cmd.Parameters.AddWithValue(param.Name, param.Value);
            }
            cmd.Parameters.Add(sqlParameter);
            cmd.ExecuteNonQuery();
            myConnection.Close(); // разрываем соединение с БД
            var result = sqlParameter.Value;
            return result;
        }
        public static List<object> DoQueryWithReturnList(string q, List<ParametdSqlCmd> sqlparCollection)
        {
            List<object> list = new List<object>();
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();    //Открываем соединение
            SqlCommand cmd = new SqlCommand(q, myConnection);
            foreach (var param in sqlparCollection)
            {
                cmd.Parameters.AddWithValue(param.Name, param.Value);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);    // выполнение команды
            DataTable tb = new DataTable(); // создание таблицы
            da.Fill(tb);

            foreach (DataRow row in tb.Rows)
            {
                //Console.WriteLine("--- Row ---");
                foreach (var item in row.ItemArray)
                {
                    //    Console.Write("Item: ");
                    list.Add(item);
                }
            }
            myConnection.Close(); // разрываем соединение с БД
            return list;
        }

    }
    public class ParametdSqlCmd
    {
        string name;
        object value;

        public string Name { get => name; set => name = value; }
        public object Value { get => value; set => this.value = value; }
        public ParametdSqlCmd(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
