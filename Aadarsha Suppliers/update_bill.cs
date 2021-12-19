using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Aadarsha_Suppliers
{
    public partial class update_bill : Form
    {
        public update_bill()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void update_bill_Load(object sender, EventArgs e)
        {
            con.Open();
            cmd=con.CreateCommand();
            cmd.CommandType=CommandType.Text;
            cmd.CommandText="select * from billitemTbl where Bill_no like ('"+bill_no.Text+"%')";
            cmd.ExecuteNonQuery();
            dt=new DataTable();
            da=new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource=dt;
            con.Close();
            dataGridView1.Columns[1].AutoSizeMode=DataGridViewAutoSizeColumnMode.Fill;

            product.Select();
            product.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Name from ProductTbl order by Name asc";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                product.Items.Add(dr["Name"].ToString());
            }
            con.Close();

        }
    }
}
