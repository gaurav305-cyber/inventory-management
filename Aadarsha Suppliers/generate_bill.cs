using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace Aadarsha_Suppliers
{
    public partial class generate_bill : Form
    {
        public generate_bill()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        int bill_no;
        int subtotal;
        int grand_total;
        int balance1;
        // string connStr = "Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True";
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        private void generate_bill_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("ConatctNumber");
            dt.Columns.Add("Email");
            dt.Columns.Add("Product");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");

            dataGridView1.DataSource = dt;
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr[0] = c_name.Text;
            dr[1] = address.Text;
            dr[2] = phone.Text;
            dr[3] = email.Text;
            dr[4] = product.Text;
            dr[6] = price.Text;
            dr[5] = quantity.Text;
            dt.Rows.Add(dr);
            subtotal = Convert.ToInt32(price.Text) * Convert.ToInt32(quantity.Text);
            sub.Text = subtotal.ToString();
            
        }
        private void discount_TextChanged(object sender, EventArgs e)
        {
            try { 
                grand_total = subtotal - Convert.ToInt32(discount.Text);
                total.Text = grand_total.ToString();
                
            }

            catch
            {
                total.Text = "";
            }
        }

        private void paid_TextChanged(object sender, EventArgs e)
        {
            try {
                balance1 = grand_total - Convert.ToInt32(paid.Text);
                balance.Text = balance1.ToString();
               
            }
            catch
            {
                paid.Text = "";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
            string previousbill_no = "SELECT MAX(Bill_no) Bill_no FROM BillTbl";
            string newbill_no = GenerateBillNo("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True", previousbill_no);
            bill_no = Convert.ToInt32(newbill_no);
            string insertquery = "insert into BillTbl values('" + newbill_no + "','" + c_name.Text + "','" + address.Text + "','" + phone.Text + "','" + email.Text + "','" + product.Text + "'," + Convert.ToInt32(price.Text) + "," + Convert.ToInt32(quantity.Text) + ",'" + dateTimePicker1.Text.ToString() + "',"+subtotal+ ", "+ Convert.ToInt32(discount.Text) + ","+grand_total+"," + Convert.ToInt32(paid.Text) + "," + balance1 + ") ";
            con.Open();

            SqlCommand cmd = new SqlCommand(insertquery, con);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Invoice " + newbill_no +" Added: ");
            //populate();
            clear();
        }
        private void clear()
        {
            c_name.Text="";
            address.Text="";
            phone.Text="";
            email.Text="";
            product.Text="";
            price.Text="";
            quantity.Text="";
            sub.Text = "";
            discount.Text = "";
            total.Text = "";
            paid.Text = "";
            balance.Text = "";
            
        }
        private string GenerateBillNo(string connection, string query)
        {
            string newbill_no = string.Empty;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string i = dr[0].ToString();
                    if (string.IsNullOrEmpty(i))
                    {
                        newbill_no = "1";
                    }
                    else
                    {
                        int j = Convert.ToInt32(i);
                        j = j + 1;
                        newbill_no = j.ToString();
                    }
                }
                con.Close();
            }

            return string.Concat(newbill_no);
        }
        private void generate_bill_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

       
    }
}
