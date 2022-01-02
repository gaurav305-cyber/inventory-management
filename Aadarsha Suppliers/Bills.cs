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
    public partial class Bills : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        ReportDocument cryrpt = new ReportDocument();
        SqlDataAdapter da;
        public Bills()
        {
            InitializeComponent();
        }

        private void Bills_Load(object sender, EventArgs e)
        {
            con.Open();
            da = new SqlDataAdapter(@"select BillTbl.Bill_no, BillTbl.Customer_Name, BillTbl.Bill_Date, BillTbl.Sub_Total, BillTbl.Discount, BillTbl.Total, BillTbl.Paid, BillTbl.Balance, BillTbl.Last_updated, billitemTbl.Sno, billitemTbl.Product, billitemTbl.Price, billitemTbl.Quantity, billitemTbl.Amount, billitemTbl.Bill_no from BillTbl inner join billitemTbl on BillTbl.Bill_no=billitemTbl.Bill_no  ", con);
            DataSet dst = new DataSet();
            da.Fill(dst, "PrintBill");
            cryrpt.Load("customerbills.rpt");
            cryrpt.SetDataSource(dst);
            crystalReportViewer1.ReportSource = cryrpt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            da = new SqlDataAdapter(@"select BillTbl.Bill_no, BillTbl.Customer_Name, BillTbl.Bill_Date, BillTbl.Sub_Total, BillTbl.Discount, BillTbl.Total, BillTbl.Paid, BillTbl.Balance, BillTbl.Last_updated, billitemTbl.Sno, billitemTbl.Product, billitemTbl.Price, billitemTbl.Quantity, billitemTbl.Amount, billitemTbl.Bill_no from BillTbl inner join billitemTbl on BillTbl.Bill_no=billitemTbl.Bill_no where BillTbl.Customer_Name like '%" + textBox1.Text + "%' ", con);
            DataSet dst = new DataSet();
            da.Fill(dst, "PrintBill");
            cryrpt.Load("customerbills.rpt");
            cryrpt.SetDataSource(dst);
            crystalReportViewer1.ReportSource = cryrpt;
            con.Close();
        }
    }
}
