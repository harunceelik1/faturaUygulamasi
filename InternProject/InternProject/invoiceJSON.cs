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
    public partial class invoiceJSON : Form
    {
        Connect con = new Connect();
        public invoiceJSON()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SQLiteCommand komut = new SQLiteCommand("SELECT seri,number From invoice", con.Connection());
            SQLiteDataReader dr;

            dr = komut.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["seri"]);         // COMBOBOX'A SERİ numarası gönderiyor.
                comboBox1.SelectedIndex = 0;
            }
        }
        SQLiteCommand cmd;
        SQLiteCommand cmd2;
        SQLiteCommand cmd3;

        SQLiteDataReader dr;
        SQLiteDataReader dr2;
        private void button2_Click(object sender, EventArgs e)
        {
            jsonSave jsonform = new jsonSave();
            cmd = new SQLiteCommand("Select seri,id,number,totalAmount,discount,amountToPay,customerId From invoice where seri=@p1", con.Connection());
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);


            cmd2 = new SQLiteCommand("Select name From customer where ssnNumber=@p2", con.Connection());
            cmd3 = new SQLiteCommand("Select itemId,quantity,amount From invoiceItems where invoiceId=@p3", con.Connection());

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string number = dr["number"].ToString();
                jsonform.number = number;
                string total = dr["totalAmount"].ToString();
                jsonform.total = total;
                string discount = dr["discount"].ToString();
                jsonform.discount = discount;
                string pay = dr["amountToPay"].ToString();
                jsonform.pay = pay;
                string customerId = dr["customerId"].ToString();
                jsonform.TC = customerId;

                string id = dr["id"].ToString();

                string seri = dr["seri"].ToString();
                jsonform.Seri = seri;
                cmd2.Parameters.AddWithValue("@p2", customerId);
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    jsonform.name = name;
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
                    //jsonform.comboBox1.Items.Add(dr["quantity"] + " Fiyat : " + dr["amount"]);          //QUANTİTY DEĞERİ COMBOBOXA YAZDIRILIYOR O FATURAYA AİT.
                    //jsonform.comboBox1.Items.Add(dr["quantity"]);
                    //dt.Rows.Add(dr2["quantity"], dr2["amount"]);
                    jsonform.productInfo.DataSource = dt;

                    cmd3 = new SQLiteCommand("Select name,unitPrice From item where id=@itemid", con.Connection());
                    cmd3.Parameters.AddWithValue("@itemid", item);
                    dr = cmd3.ExecuteReader();
                    while (dr.Read())
                    {
                        dt.Rows.Add(dr["name"], dr2["quantity"], dr["unitPrice"], dr2["amount"]);
                        jsonform.productInfo.DataSource = dt;
                    }
                }
            }
            jsonform.Show();
            this.Hide();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SQLiteCommand("Select number From invoice where seri=@p1", con.Connection());
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string number = dr["number"].ToString();
                txtNo.Text = number;
            }
        }
    }
    }
