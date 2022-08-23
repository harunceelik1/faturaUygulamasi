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
using System.Xml;
namespace InternProject
{
    public partial class xmlSave : Form
    {

        public xmlSave()
        {
            InitializeComponent();
        }
        public string number;
        public string total;
        public string discount;
        public string pay;
        public string TC;
        public string name;
        public string quantity;
        public string Seri;
        public void islemForm_Load(object sender, EventArgs e)
        {       
            txtName.Text = name;
            txtNo.Text = number;
            txtDiscount.Text = discount;
            txtPay.Text = pay;
            txtTC.Text =  TC;
            txtTotal.Text = total;
            txtSeri.Text = Seri;
            
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
            XmlTextWriter dosya = new XmlTextWriter(@"sample_output.xml",Encoding.UTF8);
            dosya.Formatting = Formatting.Indented;
            dosya.WriteStartDocument();
            
            dosya.WriteStartElement("uploadsystem");
            dosya.WriteStartElement("customer");       
            dosya.WriteAttributeString("type", "ŞAHIS");
            dosya.WriteElementString("name", txtName.Text);
            dosya.WriteElementString("ssnNumber", txtTC.Text);
            dosya.WriteEndElement();

            dosya.WriteStartElement("invoiceData");
            dosya.WriteAttributeString("seri",txtSeri.Text);
            dosya.WriteAttributeString("number", txtNo.Text);
            
            for(int i=0; i<productInfo.Rows.Count; i++)
            {
                dosya.WriteStartElement("item");
                dosya.WriteElementString("name", productInfo.Rows[i].Cells[0].Value.ToString());
                dosya.WriteElementString("quantity", productInfo.Rows[i].Cells[1].Value.ToString());
                dosya.WriteElementString("unitPrice", productInfo.Rows[i].Cells[2].Value.ToString());
                dosya.WriteElementString("amount", productInfo.Rows[i].Cells[3].Value.ToString());
                dosya.WriteEndElement();
            }
            dosya.WriteElementString("totalAmount", txtTotal.Text);
            dosya.WriteElementString("discount", txtDiscount.Text);
            dosya.WriteElementString("amountToPay", txtPay.Text);
            dosya.WriteEndElement();

            MessageBox.Show("Faturanız XML olarak kaydedilmiştir.");
            dosya.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            menu menu = (menu)Application.OpenForms["menu"];
            menu.Show();

        }
    }
}
