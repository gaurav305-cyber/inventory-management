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
using System.Configuration;

namespace Aadarsha_Suppliers
{
    public partial class generate_bill : Form
    {
        public generate_bill()
        {
            InitializeComponent();
            countrow.Visible = false;
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        DataTable dt = new DataTable();
        int bill_no;
        int subtotal;
        int grand_total;
        int balance1;

        private void generate_bill_Load(object sender, EventArgs e)
        {
            product.Select();
            product.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Name from ProductTbl order by Name asc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                product.Items.Add(dr["Name"].ToString());
            }
            con.Close();
            LoadBillNo();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }
        public void LoadBillNo()
        {

            int a;
            string constr = "Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True";
            con = new SqlConnection(constr);
            con.Open();
            string query = "select Max(Bill_no) from BillTbl";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    bill.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    bill.Text = a.ToString();

                }
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (countrow.Text == "")
            {
                int row = 0;
                dataGridView1.Rows.Add();
                row = dataGridView1.Rows.Count - 1;
                dataGridView1["product_name", row].Value = product.Text;
                dataGridView1["pp", row].Value = price.Text;
                dataGridView1["qty", row].Value = quantity.Text;
                dataGridView1["amt", row].Value = amount.Text;
                dataGridView1["billno", row].Value = bill.Text;
                dataGridView1["date", row].Value = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                dataGridView1.Refresh();
                product.Focus();
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];

                }


            }
            else
            {
                int i;
                i = Convert.ToInt32(countrow.Text);
                DataGridViewRow row = dataGridView1.Rows[i - 1];
                row.Cells[1].Value = product.Text;
                row.Cells[2].Value = price.Text;
                row.Cells[3].Value = quantity.Text;
                row.Cells[4].Value = amount.Text;
                row.Cells[5].Value = bill.Text;
                row.Cells[6].Value = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                button1.Text = "Add";


            }
            countrow.Text="";
            clear();
            subTotal();

        }

        private void discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
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
            try
            {
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
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Add atleast one product");
            }
            else
            {
                con.Open();
                cmd = new SqlCommand("Insert into BillTbl values(" + Convert.ToInt32(bill.Text) + ",'" + c_name.Text + "','" + address.Text + "','" + phone.Text + "','" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','" + sub.Text + "','" + discount.Text + "','" + total.Text + "','" + paid.Text + "','" + balance.Text + "','" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "')", con);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int qty=0;
                    string prd="";
                    SqlCommand cmd1 = new SqlCommand("Insert into billitemTbl values(" + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'," + Convert.ToString(dataGridView1.Rows[i].Cells[3].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "'," + Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "','" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "')", con);
                    cmd1.ExecuteNonQuery();

                    qty= Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    prd= dataGridView1.Rows[i].Cells[1].Value.ToString();
                    SqlCommand cmd2=new SqlCommand("update ProductTbl set Quantity= Quantity-"+qty+" where Name='"+prd.ToString()+"'",con);
                    cmd2.ExecuteNonQuery();
                }
                MessageBox.Show("Bill saved");
                con.Close();
                Class1.strInv=bill.Text;
                LoadBillNo();
                clear();
                c_name.Text = "";
                address.Text = "";
                phone.Text = "";
                sub.Text = "";
                discount.Text = "";
                total.Text = "";
                paid.Text = "";
                balance.Text = "";
                dataGridView1.Rows.Clear();
                product.Select();
                billprint bp=new billprint();
                bp.ShowDialog();
            }

        }
        private void clear()
        {
            product.Text = "";
            price.Text = "";
            quantity.Text = "";
            amount.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (countrow.Text == "")
            {
                MessageBox.Show("Select Product to delete");
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) dataGridView1.Rows.Remove(row);

                }
            }
            button1.Text = "Add";
            clear();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
        int i;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[i];
            product.Text = row.Cells[1].Value.ToString();
            price.Text = row.Cells[2].Value.ToString();
            quantity.Text = row.Cells[3].Value.ToString();
            amount.Text = row.Cells[4].Value.ToString();
            // dateTimePicker1.Value.to= row.Cells[6].Value.ToString();
            countrow.Text = row.Cells[0].Value.ToString();
            button1.Text = "Update";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void CalAmount()
        {
            double a1, b1, i;
            double.TryParse(price.Text, out a1);
            double.TryParse(quantity.Text, out b1);
            i = a1 * b1;
            if (i > 0)
            {
                amount.Text = i.ToString("C").Remove(0, 1);
            }

        }
        public void subTotal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
            }
            sub.Text = sum.ToString();
        }
        public void CalDiscount()
        {
            double a1, b1, i;
            double.TryParse(sub.Text, out a1);
            double.TryParse(discount.Text, out b1);
            i = a1 - b1;
            if (i > 0)
            {
                total.Text = i.ToString("C").Remove(0, 1);
            }

        }
        public void CalBalance()
        {
            double a1, b1, i;
            double.TryParse(total.Text, out a1);
            double.TryParse(paid.Text, out b1);
            i = a1 - b1;

            balance.Text = i.ToString("C").Remove(0, 1);


        }

        private void quantity_Leave(object sender, EventArgs e)
        {
            CalAmount();
        }

        private void price_Leave(object sender, EventArgs e)
        {
            CalAmount();
        }

        private void sub_TextChanged(object sender, EventArgs e)
        {
            CalDiscount();
        }

        private void discount_Leave(object sender, EventArgs e)
        {
            CalDiscount();
        }

        private void total_TextChanged(object sender, EventArgs e)
        {
            CalBalance();
        }

        private void paid_Leave(object sender, EventArgs e)
        {
            CalBalance();
        }
    }
}
