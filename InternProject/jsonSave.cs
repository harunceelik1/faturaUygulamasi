using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace InternProject
{
    public partial class jsonSave : Form
    {
        public jsonSave()
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

        private void jsonform_Load(object sender, EventArgs e)
        {
            txtName.Text = name;
            txtNo.Text = number;
            txtDiscount.Text = discount;
            txtPay.Text = pay;
            txtTC.Text = TC;
            txtTotal.Text = total;
            txtSeri.Text = Seri;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            try
            {
                var customer = GetCustomer();

                var jsonToWrite = JsonConvert.SerializeObject(customer, Formatting.Indented);

                using (var writer = new StreamWriter(@"sample_output.json"))
                {
                    writer.Write(jsonToWrite);
                }
            }
            catch (Exception ex)
            {
                // ignored

            }
            MessageBox.Show("Faturanız JSON olarak kaydedilmiştir.");
        }
        private Customer GetCustomer()
        {
            List<Item> Itemss = new List<Item>();
            
            for (int i=0;i<productInfo.Rows.Count;i++)
            {
                Item a = new Item();
                a.name = productInfo.Rows[i].Cells[0].Value.ToString();
                a.quantity = productInfo.Rows[i].Cells[1].Value.ToString();
                a.unitPrice = productInfo.Rows[i].Cells[2].Value.ToString();
                a.amount = productInfo.Rows[i].Cells[3].Value.ToString();
                Itemss.Add(a);
            }
            
            var customer = new Customer
            {
                uploadSystem = new uploadSystem
                {

                    customer = new customer
                    {
                        name = txtName.Text,
                        ssnNumber = txtTC.Text,
                    },
                    invoiceData = new invoiceData
                    {
                        seri = txtSeri.Text,
                        number = txtNo.Text,
                        Items = Itemss,
                        totalAmount = txtTotal.Text,
                        discount = txtDiscount.Text,
                        amountToPay = txtPay.Text
                    }
                }
            };
            return customer;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            menu menu = (menu)Application.OpenForms["menu"];
            menu.Show();

        }

      
    }
}
            public class Customer
            {
            public uploadSystem uploadSystem { get; set; }
           
            }

        public class customer
        {
            public string name { get; set; }
            public string ssnNumber { get; set; }
        }

        public class Item
        {
            
            public string name { get; set; }
            public string quantity { get; set; }
            public string unitPrice { get; set; }
            
            public string amount { get; set; }
        }

        public class invoiceData
        {
            public string seri { get; set; }
            public string number { get; set; }
            public List<Item> Items { get; set; }
            public string totalAmount { get; set; }
            public  string discount { get; set; }
            public string amountToPay { get; set; }
        }
        public class uploadSystem
        {
            public customer customer { get; set; }
            public invoiceData invoiceData { get; set; }        
        }