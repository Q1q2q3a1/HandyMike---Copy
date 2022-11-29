using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandyMike
{
    public partial class HandymanHomepage : Form
    {
        public HandymanHomepage()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            PendingServiceRequests psr = new PendingServiceRequests();
            psr.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Management_Page mp = new Management_Page();
            mp.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ServiceStatus ss = new ServiceStatus();
            ss.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            HandymanProfile hp = new HandymanProfile();
            hp.ShowDialog();
            this.Close();
        }

        private void HandymanHomepage_Load(object sender, EventArgs e)
        {

        }
    }
}
