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
using CrystalDecisions.CrystalReports.Engine;

namespace Aadarsha_Suppliers
{
    public partial class billprint : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        ReportDocument cryrpt=new ReportDocument();
        SqlDataAdapter da;
        
        public billprint()
        {
            InitializeComponent();
        }

        private void billprint_Load(object sender, EventArgs e)
        {
            textBox1.Text=Class1.strInv;
            try
            {
                con.Open();
                da = new SqlDataAdapter("select BillTbl.Bill_no, BillTbl.Customer_Name, BillTbl.Bill_Date, BillTbl.Sub_Total, BillTbl.Discount, BillTbl.Total, BillTbl.Paid, BillTbl.Balance, billitemTbl.Sno, billitemTbl.Product, billitemTbl.Price, billitemTbl.Quantity, billitemTbl.Amount, billitemTbl.Bill_no from BillTbl inner join billitemTbl on BillTbl.Bill_no=billitemTbl.Bill_no where BillTbl.Bill_no='" + textBox1.Text + "'", con);
                DataSet dst = new DataSet();
                da.Fill(dst, "PrintBill");
                cryrpt.Load("report.rpt");
                cryrpt.SetDataSource(dst);
                crystalReportViewer1.ReportSource = cryrpt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Class1.strInv="";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                
                da = new SqlDataAdapter("select BillTbl.Bill_no, BillTbl.Customer_Name, BillTbl.Bill_Date, BillTbl.Sub_Total, BillTbl.Discount, BillTbl.Total, BillTbl.Paid, BillTbl.Balance, billitemTbl.Sno, billitemTbl.Product, billitemTbl.Price, billitemTbl.Quantity, billitemTbl.Amount, billitemTbl.Bill_no from BillTbl inner join billitemTbl on BillTbl.Bill_no=billitemTbl.Bill_no where BillTbl.Bill_no='" + textBox1.Text + "'", con);
                DataSet dst = new DataSet();
                da.Fill(dst, "PrintBill");
                cryrpt.Load("report.rpt");
                cryrpt.SetDataSource(dst);
                crystalReportViewer1.ReportSource = cryrpt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
