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
    public partial class login : Form
    {
        
        public login()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
            SqlDataAdapter da=new SqlDataAdapter("select count(*) from User where Username='"+textBox1.Text+"' and Password='"+textBox2.Text+"'",con);
            DataTable dt=new DataTable();
            da.Fill(dt);
           // if(dt.Rows[0][0].ToString()=="1")**/
           if(textBox1.Text=="admin" && textBox2.Text=="admin" )
            {
                this.Hide();
                
                Form1 f1=new Form1();
                f1.Show();
                MessageBox.Show("Login success!");
            }
            else
            {
                MessageBox.Show("Incorrest usernme or password");
            }
            
            

        }
    }
}
