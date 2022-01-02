﻿using System;
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
    public partial class update_bill : Form
    {
        int qty=0 ;
        
        public update_bill()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        int subtotal;
        int grand_total;
        int balance1;
        private void update_bill_Load(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from billitemTbl where Bill_no like ('" + bill_no.Text + "%')";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
        public void CalAmount()
        {
            double a1, b1, i;
            double.TryParse(price.Text, out a1);
            double.TryParse(quantity.Text, out b1);
            i = a1 * b1;
            if (i > 0)
            {
                amount.Text = i.ToString();
            }

        }
        private void LoadSerialNo()
        {
            int i = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = i;
                i++;
            }
        }

        private void price_Leave(object sender, EventArgs e)
        {
            CalAmount();
        }

        private void quantity_Leave(object sender, EventArgs e)
        {
            CalAmount();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadSerialNo();
        }
        int i;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[i];
            product.Text = row.Cells[1].Value.ToString();
            price.Text = row.Cells[3].Value.ToString();
            quantity.Text = row.Cells[2].Value.ToString();
            amount.Text = row.Cells[5].Value.ToString();
            // dateTimePicker1.Value.to= row.Cells[6].Value.ToString();
            count.Text = row.Cells[0].Value.ToString();
            button1.Text="Update";
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) dataGridView1.Rows.Remove(row);
                   LoadSerialNo();
                }
           
            button1.Text="Add";
           
            clear();
            subTotal();
            CalDiscount();
            CalBalance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ia;
            if (count.Text == "")
            {
                
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    
                    dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
                if (!(product.Text == dataGridView1.Rows[i].Cells[1].Value.ToString())) { 
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow row1 = dt.NewRow();

                row1[1] = product.Text.ToString();
                row1[3] = price.Text.ToString();
                row1[2] = quantity.Text.ToString();
                row1[5] = amount.Text.ToString();
                row1[4] = bill_no.Text.ToString();
                row1[6] = DateTime.Now.ToString("MM/dd/yyyy");
                product.Focus();
                dt.Rows.Add(row1);
                dataGridView1.Refresh();
                }
                else
                {
                    
                    DataGridViewRow row = dataGridView1.Rows[i];
                   // product.Text = row.Cells[1].Value.ToString();
                   // price.Text = row.Cells[3].Value.ToString();
                    int qty3=Convert.ToInt32(row.Cells[2].Value.ToString())+Convert.ToInt32(quantity.Text);
                    double amt =Convert.ToDouble(row.Cells[5].Value.ToString())+Convert.ToDouble(amount.Text);
                    
                    row.Cells[2].Value=qty3.ToString();
                    row.Cells[5].Value=amt.ToString();
                    dataGridView1.Refresh();
                    // dateTimePicker1.Value.to= row.Cells[6].Value.ToString();
                    //count.Text = row.Cells[0].Value.ToString();
                    //button1.Text = "Update";
                }

            }
            else
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                row.Cells[1].Value = product.Text;
                row.Cells[3].Value = price.Text;
                row.Cells[2].Value = quantity.Text;
                row.Cells[5].Value = amount.Text;
                row.Cells[4].Value = bill_no.Text;
                row.Cells[6].Value = DateTime.Now.ToString("MM/dd/yyyy");
               
               button1.Text = "Add";


            }
            clear();
            subTotal();

        }
        private void clear()
        {
            product.Text = "";
            price.Text = "";
            quantity.Text = "";
            amount.Text = "";
            count.Text = "";
        }
        public void subTotal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
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

        private void sub_TextChanged(object sender, EventArgs e)
        {
            CalDiscount();
        }

        private void discount_Leave(object sender, EventArgs e)
        {
            CalDiscount();
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
                //paid.Text = "";
            }
        }

        private void total_TextChanged(object sender, EventArgs e)
        {
            CalBalance();
        }

        private void paid_Leave(object sender, EventArgs e)
        {
            CalBalance();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select Quantity from billitemTbl where Bill_no='" + bill_no.Text + "'", con);
            SqlDataReader da1 = cmd2.ExecuteReader();
            while (da1.Read())
            {
                qty = Convert.ToInt32(da1.GetValue(0).ToString());
                
            }
            con.Close();
            try
            {

                con.Open();
                cmd = new SqlCommand("Delete from billitemTbl where Bill_no='" + bill_no.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "1");
            }
            try
            {
                con.Open();
                
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int qty1=0;
                    string prd1="";
                    SqlCommand cmd1 = new SqlCommand("Insert into billitemTbl values(" + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'," + Convert.ToString(dataGridView1.Rows[i].Cells[2].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "'," + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "')", con);
                    
                    cmd1.ExecuteNonQuery();
                    qty1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()) - qty;
                    prd1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    SqlCommand cmd3 = new SqlCommand("update ProductTbl set Quantity= Quantity-" + qty1 + " where Name='" + prd1.ToString() + "'", con);
                    cmd3.ExecuteNonQuery();
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "2");
            }
            try
            {
                con.Open();
                cmd = new SqlCommand("update BillTbl set Customer_Name='" + c_name.Text + "',Address='" + address.Text + "',Phone_Number='" + phone.Text + "',Last_updated='" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "',Sub_Total='" + sub.Text + "',Discount='" + discount.Text + "',Total='" + total.Text + "',Paid='" + paid.Text + "',Balance='" + balance.Text + "' where Bill_no='" + bill_no.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Bill Updated");
                Class1.strInv=bill_no.Text;
                this.Hide();
                billprint bp=new billprint();
                bp.Show();
                c_name.Text = "";
                address.Text = "";
                phone.Text = "";
                sub.Text = "";
                discount.Text = "";
                total.Text = "";
                paid.Text = "";
                balance.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //con.Close();
            clear();
            this.Hide();
            //dataGridView1.Rows.Clear();
            product.Select();
            
        }

        private void update_bill_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
        }
        
    }
}
