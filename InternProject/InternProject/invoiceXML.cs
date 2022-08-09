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
    public partial class invoiceXML : Form
    {
        Connect con = new Connect();
        public invoiceXML()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteCommand komut = new SQLiteCommand("SELECT seri,number From invoice", con.Connection());
            SQLiteDataReader dr;  

            dr = komut.ExecuteReader();
            comboBox1.Items.Clear();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr["seri"]);         // COMBOBOX'A SERİ numarası gönderiliyor.
                comboBox1.SelectedIndex = 0;
            }
        }
        SQLiteCommand cmd;
        SQLiteCommand cmd2;
        SQLiteCommand cmd3;

        SQLiteDataReader dr;
        SQLiteDataReader dr2;
        private void Form5_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

            xmlSave islemForm = new xmlSave(); 
            cmd = new SQLiteCommand("Select seri,id,number,totalAmount,discount,amountToPay,customerId From invoice where seri=@p1",con.Connection());
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);             // invoice tablosundan comboboxtaki seçili seriye göre bilgiler getirtilip diğer forma aktarmak için bilgiler alınıyor.


            cmd2 = new SQLiteCommand("Select name From customer where ssnNumber=@p2", con.Connection());
            cmd3 = new SQLiteCommand("Select itemId,quantity,amount From invoiceItems where invoiceId=@p3", con.Connection());    // Yukarıdaki işlemin aynısı burada fakat farklı tablolar.
            
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string number = dr["number"].ToString();
                islemForm.number = number;
                string total = dr["totalAmount"].ToString();                                    
                islemForm.total = total;
                string discount = dr["discount"].ToString();
                islemForm.discount = discount;
                string pay = dr["amountToPay"].ToString();
                islemForm.pay = pay;
                string customerId = dr["customerId"].ToString();            // Diğer forma aktarmak için önce değişkene atayıp ardıkdan o değişkeni islemForm'daki değişkene eşitliyoruz. Böylece diğer forma bilgi aktarılıyor.
                islemForm.TC = customerId;

                string id = dr["id"].ToString();

                string seri = dr["seri"].ToString();
                islemForm.Seri = seri;

                cmd2.Parameters.AddWithValue("@p2", customerId);
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    islemForm.name = name;
                }

                cmd3.Parameters.AddWithValue("@p3", id);
                dr2 = cmd3.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Ürün Adı");
                dt.Columns.Add("Adet Fiyatı");
                dt.Columns.Add("Adet");
                dt.Columns.Add("Ürün Toplam Fiyatı");
                while (dr2.Read())
                {
                    string item = dr2["itemId"].ToString();
                    //islemForm.comboBox1.Items.Add(dr["quantity"] + " Fiyat : " + dr["amount"]);          //QUANTİTY DEĞERİ COMBOBOXA YAZDIRILIYOR O FATURAYA AİT.
                    //islemForm.comboBox1.Items.Add(dr["quantity"]);
                    //dt.Rows.Add(dr2["quantity"], dr2["amount"]);
                    islemForm.productInfo.DataSource = dt;

                    cmd3 = new SQLiteCommand("Select name,unitPrice From item where id=@itemid", con.Connection());
                    cmd3.Parameters.AddWithValue("@itemid", item);
                    dr = cmd3.ExecuteReader();
                    while (dr.Read())
                    {
                            dt.Rows.Add(dr["name"],dr2["quantity"], dr["unitPrice"],dr2["amount"]);
                        islemForm.productInfo.DataSource = dt;
                    }
                }
            }
            islemForm.Show();
            this.Hide();
        }
      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            cmd = new SQLiteCommand("Select number From invoice where seri=@p1", con.Connection());
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);
      
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                string number = dr["number"].ToString();              //Burada comboboxta seçili olan fatura numarasına göre textboxa o serinin numarası getiriliyor.
                txtNo.Text = number;
            }
        }
    }
}
