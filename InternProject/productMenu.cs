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
    public partial class productMenu : Form
    {
        public productMenu()
        {
            InitializeComponent();
            
        }


        Connect con = new Connect();
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        double total;
        private void button1_Click(object sender, EventArgs e)
        {   
            invoiceSave invoiceSave = (invoiceSave)Application.OpenForms["invoiceSave"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Adı");
            dt.Columns.Add("Fiyat");
            dt.Columns.Add("Miktar");
            dt.Columns.Add("Tutar");
            


            foreach (DataGridViewRow drv in productsTable.Rows)
            {
                bool CheckBoxselect = Convert.ToBoolean(drv.Cells["Column1"].Value);

                if (CheckBoxselect)
                {
                    drv.Cells["Total"].Value = Convert.ToSingle(drv.Cells["Fiyat"].Value) * Convert.ToSingle(drv.Cells["Miktar"].Value);    //TOPLAM HESAPLAMASI YAPILIYOR.
                    dt.Rows.Add(drv.Cells[1].Value,drv.Cells[2].Value, drv.Cells[3].Value, drv.Cells[4].Value, drv.Cells[5].Value);         // Form2'deki datagridview tablosuna ürünlerin bilgisi aktarılıyor.

                    total += Convert.ToDouble(drv.Cells["Total"].Value);

                    invoiceSave.price = total.ToString("0.##");
                }
                invoiceSave.products.DataSource = dt;
            }
            invoiceSave.Show();
            this.Hide();
        }
        private void urunsec_Load(object sender, EventArgs e)
        {
          
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            var connections = "Data source=upload_system.db;Version=3;";
            using (var connection = new SQLiteConnection(connections))
            {               
                for (int i = 0; i < productsTable.SelectedRows.Count; i++)
                {
                    connection.Open();
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO item(name, unitPrice) VALUES (@name, @unitPrice)";         //Eklenen ürünler database'e kaydediliyor.
                    insertCmd.Parameters.AddWithValue("@name", txtAD.Text);
                    insertCmd.Parameters.AddWithValue("@unitPrice", txtFİYAT.Text);
                    insertCmd.ExecuteNonQuery();

                    connection.Close();
                }
                MessageBox.Show("Ürün Eklendi.");
            }

        }
        private void liste_Click(object sender, EventArgs e)
        {
            SQLiteDataAdapter sql = new SQLiteDataAdapter("Select * from item", con.Connection());
            DataTable dt = new DataTable();
            sql.Fill(dt);
            productsTable.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = productsTable.Rows.Add();
                productsTable.Rows[n].Cells[1].Value = item[0].ToString();                                // Ürünleri listelemek için kullanıldı databaseden bilgi çekiliyor.
                productsTable.Rows[n].Cells[2].Value = item[1].ToString();
                productsTable.Rows[n].Cells[3].Value = item[2].ToString();
                productsTable.Rows[n].Cells[4].Value = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            invoiceSave save = (invoiceSave)Application.OpenForms["invoiceSave"];
            save.Show();
        }
    }
}
