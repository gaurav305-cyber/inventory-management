using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

using System.IO;


namespace Aadarsha_Suppliers
{
    public partial class manage_product : Form
    {
        string connStr = "Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True";
        public manage_product()
        {
            InitializeComponent();
               
        }
        //SqlConnection con=new SqlConnection(@"Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
         private void manage_product_Load(object sender, EventArgs e)
        {
           // generatesno();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd=new SqlCommand("insert into ProductTbl")
            int total=Convert.ToInt32(quantity.Text)*Convert.ToInt32(price.Text);
            //int.Parse(quantity.Text)*int.Parse(price.Text);
            int remaining = total - Convert.ToInt32(paid.Text);
            Console.WriteLine("total is"+total);
            string previousIdQuery = "SELECT MAX(Id) Id FROM ProductTbl";
            string newId = GenerateNewId(connStr, previousIdQuery);
            string insertquery= "insert into ProductTbl values('" + newId + "','" + name.Text + "','" + type.Text + "','" + unit.Text + "'," + Convert.ToInt32(price.Text) + "," + Convert.ToInt32(paid.Text) + "," + remaining + "," + Convert.ToInt32(quantity.Text) + "," + total + ")";

            // int id1=5;

            try {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    
                    SqlCommand cmd = new SqlCommand(insertquery,con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added");
                    con.Close();
                }
            }
            catch {
                MessageBox.Show("error Adding");
            }

        }
        private string GenerateNewId(string connection, string query)
        {
            string newId = string.Empty;
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
                        newId = "1";
                    }
                    else
                    {
                        int j = Convert.ToInt32(i);
                        j = j + 1;
                        newId = j.ToString();
                    }
                }
                con.Close();
            }

            return string.Concat(newId);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
