namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyStyles.GetInstance().SetTheme(this);
            label1.Font = new Font(label1.Font.FontFamily, 20, FontStyle.Bold);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            if (form3.DialogResult == DialogResult.OK)
            {
                this.Hide();
            }
        }

    }
}
