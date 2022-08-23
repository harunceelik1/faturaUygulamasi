namespace InternProject
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            invoiceSave save= new invoiceSave();  
            save.Show();
            this.Hide();   
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form invoiceXML = new invoiceXML();
            invoiceXML.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form delete = new invoiceDelete();
            delete.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form json = new invoiceJSON();
            json.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}