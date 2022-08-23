using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InternProject
{
    public partial class customerMenu : Form
    {
        
        SqlLiteDataAdapter dt;

        
        public customerMenu()
        {
            InitializeComponent();
            
            
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
              
        }

        private void button2_Click(object sender, EventArgs e)
        {

            invoiceSave invoiceSave = (invoiceSave)Application.OpenForms["invoiceSave"];
            invoiceSave.txtName.Text = customers.CurrentRow.Cells[1].Value.ToString();
            invoiceSave.txtTC.Text = customers.CurrentRow.Cells[2].Value.ToString();              // Seçilen müşteriyi form2deki kullanıcı adı ve tc bilgi kısmına yollar.

            invoiceSave.Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var connections= "Data source=upload_system.db;Version=3;";
            using (var connection = new SQLiteConnection(connections))
            {
                connection.Open();
                var insertCmd = connection.CreateCommand();

                Connect con = new Connect();
                string sqlCom = "Select * from customer where ssnNumber=@ssnNumber";
                SQLiteCommand insertCmd2 = new SQLiteCommand(sqlCom, con.Connection());            //BURADA AYNI TC kimlik girimi engelleniyor.
                bool durum;

                insertCmd2.Parameters.AddWithValue("@ssnNumber", txtTC.Text);
                SQLiteDataReader dr = insertCmd2.ExecuteReader();
                if (dr.Read())
                {
                    durum = false;
                    MessageBox.Show("Aynı TC kimliği giremessiniz. ");
                    
                }
                else
                { 
                insertCmd.CommandText = "INSERT INTO customer(name, ssnNumber) VALUES (@name, @ssnNumber)";
                insertCmd.Parameters.AddWithValue("@name", txtName.Text);
                insertCmd.Parameters.AddWithValue("@ssnNumber", txtTC.Text);
                insertCmd.ExecuteNonQuery();
                    con.Connection().Close();
                connection.Close();
                }

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Connect con = new Connect();
            SQLiteDataAdapter sql = new SQLiteDataAdapter("Select * from customer", con.Connection());
            DataTable dt = new DataTable();
            sql.Fill(dt);
            customers.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = customers.Rows.Add();
                customers.Rows[n].Cells[0].Value = item[0].ToString();                                // Ürünleri listelemek için kullanıldı databaseden bilgi çekiliyor.
                customers.Rows[n].Cells[1].Value = item[1].ToString();
                customers.Rows[n].Cells[2].Value = item[2].ToString();

            }
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            invoiceSave save = (invoiceSave)Application.OpenForms["invoiceSave"];
            save.Show();
        }
    }
}
