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
    public partial class Dashboard : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        ReportDocument cryrpt = new ReportDocument();
        SqlDataAdapter da;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            //-- set to - current date (with 00.00.00 time)
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            con.Open();
            da = new SqlDataAdapter("select * from BillTbl where Last_updated between '"+dtFrom+ "' and  '" + dtTo + "' order by Bill_no", con);
            DataSet dst = new DataSet();
            da.Fill(dst, "SalesReportPrint");
            cryrpt.Load("salesreport.rpt");
            cryrpt.SetDataSource(dst);
            crystalReportViewer1.ReportSource = cryrpt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
            da = new SqlDataAdapter("select * from BillTbl where Last_updated between '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and  '" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' order by Bill_no", con);
            DataSet dst = new DataSet();
            da.Fill(dst, "SalesReportPrint");
            cryrpt.Load("salesreport.rpt");
            cryrpt.SetDataSource(dst);
            crystalReportViewer1.ReportSource = cryrpt;
            con.Close();
        }
    }
}
