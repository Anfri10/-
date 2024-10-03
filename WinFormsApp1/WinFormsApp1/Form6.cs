using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }
        int clientID;
        int roomID;
        List<int> servicesIdList = new List<int>();

        public int RoomID { get => roomID; set => roomID = value; }
        public List<int> ServicesIdList { get => servicesIdList; set => servicesIdList = value; }
        public int ClientID { get => clientID; set => clientID = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Owner = this;
            form7.ShowDialog();
            if(form7.DialogResult == DialogResult.OK)
            {
                string q = "insert into Бронирования values(@clientID, @date1, @date2, @status, @roomID); SELECT SCOPE_IDENTITY();";

                List<ParametdSqlCmd> sqlParameterCollection = new List<ParametdSqlCmd>();
                sqlParameterCollection.Add(new ParametdSqlCmd("@clientID", ClientID));
                sqlParameterCollection.Add(new ParametdSqlCmd("@date1", dateTimePicker1.Value.Date));
                sqlParameterCollection.Add(new ParametdSqlCmd("@date2", dateTimePicker2.Value.Date));
                sqlParameterCollection.Add(new ParametdSqlCmd("@status", "Активно"));
                sqlParameterCollection.Add(new ParametdSqlCmd("@roomID", RoomID));
                //ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection);
                //q = "SELECT SCOPE_IDENTITY();";
                //int bookingId = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q));
                
                int bookingId = Convert.ToInt32(ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection));
                
                q = "insert into [Бронирование дополнительных услуг] values(@bookingId, @serviseId)";
                sqlParameterCollection.Add(new ParametdSqlCmd("@bookingId", bookingId));
                for (int i = 0; i < servicesIdList.Count; i++)
                {
                    sqlParameterCollection.Add(new ParametdSqlCmd("@serviseId", servicesIdList[i]));
                    ControllerDB.DoQueryWithReturnScalar(q, sqlParameterCollection);
                }
                MessageBox.Show("Бронирование прошло успешно");
                DialogResult = DialogResult.OK;
            }
        }
    }
}
