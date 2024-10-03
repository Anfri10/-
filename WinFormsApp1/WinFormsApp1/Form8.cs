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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);
            LoadDataToForm();
        }
        List<int> servicesIdList = new List<int>();

        public List<int> ServicesIdList { get => servicesIdList; set => servicesIdList = value; }
        private void LoadDataToForm()
        {
            string q = "select * from [Дополнительные_услуги]";
            ControllerDB.LoadData(q, dataGridView1);

        }
        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                ServicesIdList.Add(Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value));
            }

            DialogResult = DialogResult.Yes;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            LoadDataToForm();
        }
    }
}
