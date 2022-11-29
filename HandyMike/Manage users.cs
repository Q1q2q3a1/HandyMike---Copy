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
    public partial class Manage_users : Form
    {
        public static Boolean cclicked = false;
        public static Boolean hclicked = false;
        public static Boolean aclicked = false;
        public static Boolean nclicked = false;
        public Manage_users()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminHomepage ah = new AdminHomepage();
            ah.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            aclicked = true;

            this.Hide();
            Admin_Profile ap = new Admin_Profile();
            ap.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hclicked = true;

            this.Hide();
            HandymanProfile hp = new HandymanProfile();
            hp.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cclicked = true;

            this.Hide();
            CustomerProfile cp = new CustomerProfile();
            cp.ShowDialog();
            this.Close();
        }

        private void Manage_users_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            nclicked = true;
            this.Hide();
            SignUp su = new SignUp();
            su.ShowDialog();
            this.Close();
        }
    }
}
