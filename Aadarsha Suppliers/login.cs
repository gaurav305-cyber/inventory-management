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
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VRHT5ES;Initial Catalog=Aadarshadb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

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
            
            
       
            
            

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel3.BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;
        }

       

        private void button1_Click_2(object sender, EventArgs e)
        {

            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Please enter username and password", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter username", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter password", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                con.Open();
                string login = "SELECT * from [User] WHERE Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'";
                cmd = new SqlCommand(login, con);
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.Read() == true)
                {
                    Form1 f1 = new Form1();
                    this.Hide();
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Please enter correct username or password", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                con.Close();

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            forgetpass fs = new forgetpass();
            fs.Show();
        }
    }
}
