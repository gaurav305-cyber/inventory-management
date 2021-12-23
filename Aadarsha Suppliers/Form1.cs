using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Aadarsha_Suppliers
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nButtonRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public Form1()
        {
            InitializeComponent();

            button_settings(button4, null);
            this.panelFormLoader.Controls.Clear();
            generate_bill Frmbill_vrb = new generate_bill() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmbill_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmbill_vrb);
            Frmbill_vrb.Show();
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panelFormLoader_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button_settings(button4, null);
            this.panelFormLoader.Controls.Clear();
            generate_bill Frmbill_vrb = new generate_bill() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmbill_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmbill_vrb);
            Frmbill_vrb.Show();
            
        }

        private void button_stock_Click(object sender, EventArgs e)
        {
            button_settings(button_stock, null);
            this.panelFormLoader.Controls.Clear();
            manage_product Frmmanage_vrb = new manage_product() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmmanage_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmmanage_vrb);
            Frmmanage_vrb.Show();
            
        }

        //private void button4_Leave(object sender, EventArgs e)
        //{
            //button4.BackColor = Color.FromArgb(255,255,255);
          //  button4.ForeColor = Color.FromArgb(0,0,0);
        //}

       // private void button_stock_Leave(object sender, EventArgs e)
        //{
          //  button_stock.BackColor = Color.FromArgb(255,255,255);
            //button_stock.ForeColor = Color.FromArgb(0, 0, 0);
        //}
        private void button_settings(object sender, EventArgs e)
        {
            foreach(Control c in panel4.Controls)
            {
                c.BackColor= Color.FromArgb(255,255,255);
                c.ForeColor = Color.FromArgb(0,0,0);
            }
            Control click = (Control)sender;
            click.BackColor= Color.FromArgb(0, 128, 255);
            click.ForeColor = Color.FromArgb(255,255,255);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button_settings(button5, null);
            this.panelFormLoader.Controls.Clear();
            bill_history Frmmanage_vrb = new bill_history() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmmanage_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmmanage_vrb);
            Frmmanage_vrb.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button_settings(button1, null);
            this.panelFormLoader.Controls.Clear();
            Dashboard Frmmanage_vrb = new Dashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmmanage_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmmanage_vrb);
            Frmmanage_vrb.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button_settings(button6, null);
            this.panelFormLoader.Controls.Clear();
            billprint Frmmanage_vrb = new billprint() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmmanage_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmmanage_vrb);
            Frmmanage_vrb.Show();
        }
    }
}
