using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        static string connectionString = ControllerDB.connectString;

        private List<SqlDataAdapter> dataAdapters;
        private List<BindingSource> bindingSources;
        List<string> q;
        List<DataGridView> dataGridViews;

        public Form4()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);

            dataAdapters = new List<SqlDataAdapter>();
            bindingSources = new List<BindingSource>();
            q = new List<string>() { "select * from Пользователи", "select * from Роли", "select * from Сотрудники", "select * from Клиенты", "select * from Номера", "select * from Бронирования", "select * from [Дополнительные_услуги]", "select * from [Бронирование дополнительных услуг]" };
            dataGridViews = new List<DataGridView>() { dataGridView1, dataGridView2, dataGridView3, dataGridView4, dataGridView5, dataGridView6, dataGridView7, dataGridView8 };

            StartDataLoad();

        }

        private void StartDataLoad()
        {
            for (int i = 0; i < q.Count; i++)
            {
                dataAdapters.Add(new SqlDataAdapter(q[i], connectionString));
                bindingSources.Add(new BindingSource());
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapters[i]);
                DataTable table = new DataTable();
                dataAdapters[i].Fill(table);
                bindingSources[i].DataSource = table;
                dataGridViews[i].DataSource = bindingSources[i];
            }

        }

        public void DataLoad()
        {
            for (int i = 0; i < q.Count; i++)
            {
                dataAdapters[i] = new SqlDataAdapter(q[i], connectionString);
                bindingSources[i] = new BindingSource();
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapters[i]);
                DataTable table = new DataTable();
                dataAdapters[i].Fill(table);
                bindingSources[i].DataSource = table;
                dataGridViews[i].DataSource = bindingSources[i];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataAdapters.Count; i++)
            {
                dataAdapters[i].Update((DataTable)bindingSources[i].DataSource);
            }
            DataLoad();

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
