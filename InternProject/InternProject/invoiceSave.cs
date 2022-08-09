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
    public partial class invoiceSave : Form
    {
        public invoiceSave()
        {
            InitializeComponent();
        }
        public string text;
        public string text2;
        public string urunad;

        public string price;
        public string urunmiktar;


        float amount;
        float discount;

        public string text3;
        public string text4;
        public float amount2;
        Random rnd = new Random();
        Connect con = new Connect();

        private void button1_Click(object sender, EventArgs e)
        {
            customerMenu customerMenu = new customerMenu();
            customerMenu.Show();
            this.Hide();

        }
        public void Hesapla()
        {
            amount = Convert.ToSingle(price);                    // Burada amount değişkenine single ile dönüştürerek urunun fiyatını atıyoruz.
            discount = Convert.ToSingle(txtDiscount.Text);              // discount yazılan textboxteki değeri discount değişkenine atıyoruz.

            amount2 = amount;
            
            amount = amount - (amount * discount / 100);            // Fatura Bilgileri ekranında toplam amount ve discountli halinden sonraki amount hesaplanıyor.

            lblDiscount.Text = amount.ToString() + " TL";
            total2.Text = price.ToString() + " TL";
            

        }

        public void print()
        {
            txtName.Text = text;
            txtTC.Text = text2;
        }

        public void print2()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Adı");
            dt.Columns.Add("Fiyat");
            dt.Columns.Add("Miktar");
            dt.Columns.Add("Tutar");
            products.DataSource = dt;
        }
        public void Form2_Load(object sender, EventArgs e)
        {

            print2();
            
            print();

        }

        public void button3_Click(object sender, EventArgs e)
        {

            //productMenu productMenu = (productMenu)Application.OpenForms["productMenu"];
            //productMenu.Show();
            productMenu pm = new productMenu();
            pm.Show();
            this.Hide();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                MessageBox.Show("Lütfen İndirim Oranı Giriniz..");
            }
            else
            {
                Hesapla();
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            
            var connections = "Data source=upload_system.db;Version=3;";
            using (var connection = new SQLiteConnection(connections))
            {
                connection.Open();
                var insertCmd = connection.CreateCommand();
                int randomNumber = rnd.Next(10, 90000);

                    insertCmd.CommandText = "INSERT INTO invoice(id,seri,number,totalAmount,discount,amountToPay,customerId) VALUES (@id,@seri,@number,@totalAmount,@discount,@amountToPay,@customerId)";
                    insertCmd.Parameters.AddWithValue("@id", randomNumber);
                    insertCmd.Parameters.AddWithValue("@seri", txtSeri.Text);
                    insertCmd.Parameters.AddWithValue("@number", txtNo.Text);           //Kaydettikten sonra invoice tablosuna bilgiler kaydedilir.
                    insertCmd.Parameters.AddWithValue("@totalAmount", total2.Text);
                    insertCmd.Parameters.AddWithValue("@discount", txtDiscount.Text);
                    insertCmd.Parameters.AddWithValue("@amountToPay", lblDiscount.Text);
                    insertCmd.Parameters.AddWithValue("@customerId", txtTC.Text);
                    insertCmd.ExecuteNonQuery();
                    connection.Close();


                using (var bagla = new SQLiteConnection(connections))
                {
                    for (int i = 0; i < products.Rows.Count ; i++)     //İNVOİCEİTEMS'E ELEMAN EKLEME YAPILIR.
                    {
                        bagla.Open();
                        var ekle = bagla.CreateCommand();
                        ekle.CommandText = "INSERT INTO invoiceItems(invoiceId,itemId,quantity,amount) VALUES (@invoiceId,@itemId,@quantity,@amount)";
                        ekle.Parameters.AddWithValue("@invoiceId", randomNumber);
                        ekle.Parameters.AddWithValue("@itemId", products.Rows[i].Cells["Id"].Value.ToString());
                        ekle.Parameters.AddWithValue("@quantity", products.Rows[i].Cells["Miktar"].Value.ToString());
                        ekle.Parameters.AddWithValue("@amount", products.Rows[i].Cells["Tutar"].Value.ToString());
                        ekle.ExecuteNonQuery();
                        bagla.Close();
                    }
                }
            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ana Menüye Dönüyorusunz ! ");
            this.Close();
            menu menu = (menu)Application.OpenForms["menu"];
            menu.Show();
        }

        private void txtSeri_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);        // SERİ TEXT'ine sadece harf girilmesini sağlar
        }

        private void txtSeri_TextChanged(object sender, EventArgs e)
        {   
            txtSeri.Text = txtSeri.Text.ToUpper();                                    
            txtSeri.SelectionStart = txtSeri.Text.Length;                   
        }

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);         //Fatura No text kısmına sadece rakam girişine izin verilir.
        }

    }
}
