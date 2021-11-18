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
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            button_stock.BackColor = Color.FromArgb(46, 51, 73);
            button_stock.ForeColor = Color.FromArgb(255,255,255);
            this.panelFormLoader.Controls.Clear();
            manage_product Frmmanage_vrb = new manage_product() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Frmmanage_vrb.FormBorderStyle = FormBorderStyle.None;
            this.panelFormLoader.Controls.Add(Frmmanage_vrb);
            Frmmanage_vrb.Show();
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
    }
}
