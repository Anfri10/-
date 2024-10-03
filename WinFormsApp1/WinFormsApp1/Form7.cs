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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);
            LoadDataToForm();
        }
        int roomID;

        Form6 form6;
        private void LoadDataToForm()
        {
            string q = "select * from Номера";
            ControllerDB.LoadData(q, dataGridView1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form6 = (Form6)this.Owner;

            roomID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            Form8 form8 = new Form8();
            form8.ShowDialog();
            if(form8.DialogResult == DialogResult.Yes)
            {
               form6.ServicesIdList = form8.ServicesIdList;
                form6.RoomID = roomID;
                DialogResult = DialogResult.OK;
            }
            if(form8.DialogResult == DialogResult.No)
            {
                form6.RoomID = roomID;
                DialogResult= DialogResult.OK;
            }
            if(form8.DialogResult == DialogResult.Cancel)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
