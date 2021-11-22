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
        int id;
       // string connStr = "Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True";
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
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
            populate();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd=new SqlCommand("insert into ProductTbl")
            int total=Convert.ToInt32(quantity.Text)*Convert.ToInt32(price.Text);
            //int.Parse(quantity.Text)*int.Parse(price.Text);
            int remaining = total - Convert.ToInt32(paid.Text);
           // Console.WriteLine("total is"+total);
            string previousIdQuery = "SELECT MAX(Id) Id FROM ProductTbl";
            string newId = GenerateNewId("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True", previousIdQuery);
            id = Convert.ToInt32(newId);
            string insertquery= "insert into ProductTbl values('"+ newId +"','" + name.Text + "','" + type.Text + "','" + unit.Text + "'," + Convert.ToInt32(price.Text) + "," + Convert.ToInt32(paid.Text) + "," + remaining + "," + Convert.ToInt32(quantity.Text) + "," + total + ",'" + DateTime.Now.ToString("MMM dd yyyy") + "','" +""+ "')";

            // int id1=5;

            
                    con.Open();
                    
                    SqlCommand cmd = new SqlCommand(insertquery,con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added");
                    con.Close();
                    populate();
                    clear();
            

        }
        public void clear()
        {
            name.Text = "";
            type.Text = "";
            unit.Text = "";
            price.Text = "";
            paid.Text = "";
            quantity.Text = "";
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
        void populate()
        {
            try {
                con.Open();
                string myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                
                con.Close();

            
            }
            catch { }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()); 
            name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            type.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            unit.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            price.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            paid.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            quantity.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ProductTbl where Id='" + id + "'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data deleted");
            con.Close();
            clear();
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(quantity.Text) * Convert.ToInt32(price.Text);
            //int.Parse(quantity.Text)*int.Parse(price.Text);
            int remaining = total - Convert.ToInt32(paid.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("update ProductTbl set Name='" + name.Text + "', Type='"+type.Text+ "',Unit='" + unit.Text + "',Price=" + Convert.ToInt32(price.Text) + ",Paid=" + Convert.ToInt32(paid.Text) + ",Remaining=" + remaining + ",Quantity=" + Convert.ToInt32(quantity.Text) + ",Total=" + total + ",Last_Updated='" + DateTime.Now.ToString("MMM dd yyyy") + "' where Id='" + id + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data updated");
            con.Close();
            clear();
            populate();
        }
    }
}
