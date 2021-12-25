using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Aadarsha_Suppliers
{
    public partial class bill_history : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public bill_history()
        {
            InitializeComponent();
        }
        private void bill_history_Load(object sender, EventArgs e)
        {
            fillgrid();
            totalsale();
            totalReceived();
            totalRemaining();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            da = new SqlDataAdapter("select * from BillTbl where Bill_Date between '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and '" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' order by Bill_no asc", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "BillTbl");
            dataGridView1.DataSource = ds.Tables["BillTbl"];
            con.Close();
            totalsale();
            totalReceived();
            totalRemaining();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.Rows[0];
            counter.Text = dr.Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (counter.Text == "")
            {
                MessageBox.Show("Select bill to delete");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("You want to delete bill", "Deleting bill", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd = new SqlCommand("Delete * from BillTbl where Bill_no='" + counter.Text + "'", con);
                    cmd1 = new SqlCommand("Delete * from billitemTbl where Bill_no='" + counter.Text + "'", con);

                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    fillgrid();
                    totalsale();
                    totalReceived();
                    totalRemaining();
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }
        public void fillgrid()
        {
            con.Open();
            da = new SqlDataAdapter("Select * from BillTbl order by Bill_no asc", con);
            con.Close();
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void totalsale()
        {
            tb.Text = (dataGridView1.Rows.Count - 1).ToString();
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
            }
            ts.Text = sum.ToString();
        }
        public void totalReceived()
        {
            tb.Text = (dataGridView1.Rows.Count - 1).ToString();
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);
            }
            tr.Text = sum.ToString();
        }
        public void totalRemaining()
        {
            tb.Text = (dataGridView1.Rows.Count - 1).ToString();
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value);
            }
            tl.Text = sum.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            update_bill updt = new update_bill();
            updt.bill_no.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updt.c_name.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updt.address.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updt.phone.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            updt.dateTimePicker1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            updt.sub.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            updt.discount.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            updt.total.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            updt.paid.Text = this.dataGridView1.CurrentRow.Cells[8].Value.ToString();
            updt.balance.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
            updt.Show();




        }

        private void button4_Click(object sender, EventArgs e)
        {
            fillgrid();
            totalsale();
            totalReceived();
            totalRemaining();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
