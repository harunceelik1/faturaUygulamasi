using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace InternProject
{
    public partial class invoiceDelete : Form
    {
        public invoiceDelete()
        {
            InitializeComponent();
        }
        Connect con = new Connect();
        SQLiteCommand cmd;
        SQLiteCommand cmd2;
        SQLiteCommand cmd3;

        SQLiteDataReader dr;
        SQLiteDataReader dr2;

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtFNo.Text))
            {
                MessageBox.Show("Lütfen Fatura Numarası Giriniz ! ");
            }
            else if (String.IsNullOrWhiteSpace(txtSeri1.Text))
            {
                MessageBox.Show("Lütfen Fatura Serisi Giriniz ! ");
            }
            else
            {

                using (var con = new SQLiteConnection("Data source=upload_system.db;Version=3;"))
                {
                    using (cmd = new SQLiteCommand("Select seri,id,number,totalAmount,discount,amountToPay,customerId From invoice where number=@p2 AND seri=@p1", con))
                    {
                        cmd.Connection.Open();
                        cmd.Parameters.AddWithValue("@p1", txtSeri1.Text);    // p1 parametresine seri numarasını atıyoruz seri numarasına eşitse bilgiler geliyor.
                        cmd.Parameters.AddWithValue("@p2", txtFNo.Text);    // p2 parametresin seriye ait fatura numarasını yolluyoruz.
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {

                            txtNo.Text = dr["number"].ToString();
                            txtTotal.Text = dr["totalAmount"].ToString();
                            txtDiscount.Text = dr["discount"].ToString();
                            txtPay.Text = dr["amountToPay"].ToString();

                            txtTC.Text = dr["customerId"].ToString();
                            label10.Text = dr["id"].ToString();
                            txtSeri.Text = dr["seri"].ToString();

                        }

                        cmd.Connection.Close();
                    }
                }

            using (var con = new SQLiteConnection("Data source=upload_system.db;Version=3;"))
                {
                using (var cmd2 = new SQLiteCommand("Select name From customer where ssnNumber=@p2", con))
                {
                    cmd2.Connection.Open();
                    cmd2.Parameters.AddWithValue("@p2", txtTC.Text);
                    dr = cmd2.ExecuteReader();
                    while (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                    }
                }

            }
                using (var con = new SQLiteConnection("Data source=upload_system.db;Version=3;"))
                {
                    using (cmd3 = new SQLiteCommand("Select itemId,quantity,amount From invoiceItems where invoiceId=@p3", con))
                    {
                        cmd3.Connection.Open();
                        cmd3.Parameters.AddWithValue("@p3", label10.Text);
                        dr2 = cmd3.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Ürün Adı");
                        dt.Columns.Add("Adet");
                        dt.Columns.Add("Fiyat");
                        dt.Columns.Add("Ürün Toplam Fiyatı");


                        while (dr2.Read())
                        {
                            string item = dr2["itemId"].ToString();

                            productInfo.DataSource = dt;

                            cmd3 = new SQLiteCommand("Select name,unitPrice From item where id=@itemid", con);
                            cmd3.Parameters.AddWithValue("@itemid", item);
                            dr = cmd3.ExecuteReader();
                            while (dr.Read())
                            {
                                dt.Rows.Add(dr["name"], dr2["quantity"], dr["unitPrice"], dr2["amount"]);
                                productInfo.DataSource = dt;
                            }
                            
                        }
                    }
                    cmd3.Connection.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (var con= new SQLiteConnection("Data source=upload_system.db;Version=3;"))
            {
                using (var delete = new SQLiteCommand($"DELETE FROM invoice Where customerId='{txtTC.Text}' AND seri='{txtSeri1.Text}' AND number='{txtNo.Text}' AND totaLAmount='{txtTotal.Text}' AND amountToPay='{txtPay.Text}' AND discount='{txtDiscount.Text}' ", con))
                {
                    
                    delete.Connection.Open();
                    delete.ExecuteNonQuery();
                    MessageBox.Show("Fatura silindi..");                //Fatura deleteme işlemleri yapılıyor.
                    delete.Dispose()   ;
                }

            }
            using (var con = new SQLiteConnection("Data source=upload_system.db;Version=3;"))
            {
                using (var delete2 = new SQLiteCommand($"DELETE FROM invoiceItems Where invoiceId='{label10.Text}'", con))       
                {
                    delete2.Connection.Open();
                    delete2.ExecuteNonQuery();

                    delete2.Dispose();
                }

            }
        }
        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(txtFNo.Text))
            {
                MessageBox.Show("Lütfen Fatura Numarası Giriniz ! ");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtSeri1.Text = txtSeri1.Text.ToUpper();
            txtSeri1.SelectionStart = txtSeri1.Text.Length;         //txtSerideki girileni büyük harfe çevirir.
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);     // fatura no ya sadece rakam girişi izin verir.
        }

        private void txtSeri1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);            // txtseriye sadece harf izni verir.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            menu menu = (menu)Application.OpenForms["menu"];
            menu.Show();

        }
    }
}