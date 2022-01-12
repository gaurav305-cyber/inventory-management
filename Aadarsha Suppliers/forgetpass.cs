using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Aadarsha_Suppliers
{
    public partial class forgetpass : Form
    {
        string randomcode;
        public static string to;
        public forgetpass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string from, pass, messagebody;
            Random rand = new Random();
            randomcode = (rand.Next(999999)).ToString();
            MailMessage msg = new MailMessage();
            to = "gauravheart306@gmail.com";
            from = "gauravraut305@gmail.com";
            pass = "g.a.u.r.a.b305";
            messagebody = "Your Resetting Code is" + randomcode;
            msg.To.Add(to);
            msg.From = new MailAddress(from);
            msg.Body = messagebody;
            msg.Subject = "Password resetting Code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try {
                smtp.Send(msg);
                MessageBox.Show("Code sent Successfully to ga**********6@gmailcom");
                label9.Visible = true;
                linkLabel1.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button1.Visible = false;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            login lg = new login();
            lg.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (randomcode == (textBox1.Text).ToString())
            {
                to = "gauravheart306@gmail.com";
                this.Hide();
                resetpass rp = new resetpass();
                rp.Show();
            }
            else
            {
                MessageBox.Show("Incorrect verification code", "Verification Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string from, pass, messagebody;
            Random rand = new Random();
            randomcode = (rand.Next(999999)).ToString();
            MailMessage msg = new MailMessage();
            to = "gauravheart306@gmail.com";
            from = "gauravraut305@gmail.com";
            pass = "g.a.u.r.a.b305";
            messagebody = "Your Resetting Code is" + randomcode;
            msg.To.Add(to);
            msg.From = new MailAddress(from);
            msg.Body = messagebody;
            msg.Subject = "Password resetting Code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(msg);
                MessageBox.Show("Code sent Successfully to ga**********6@gmailcom");

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
