using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace HandyMike
{
    public partial class CustomerHomepage : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public CustomerHomepage()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

        }

        public Image ConvertByteToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length);
            ms.Write(byteArray, 0, byteArray.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private void CustomerHomepage_Load(object sender, EventArgs e)
        {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
               
                string query = "Select AdvertisementPicture from Advertisement where Advertisement_id=(select MAX(Advertisement_id) from Advertisement)";

                command.CommandText = query;
                /*
                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();

                if (dataGridView1.Rows[0].Cells[0].Value.ToString() != null)
                {
                    byte[] imgData = ((byte[])dataGridView1.Rows[0].Cells[0].Value);
                    MemoryStream st = new MemoryStream(imgData);
                    pictureBox1.Image = Image.FromStream(st);
                }
                */
                OleDbDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    byte[] imgData = (byte[])r[0];
                    MemoryStream st = new MemoryStream(imgData);
                    pictureBox5.Image = Image.FromStream(st);
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pictureBox5.BackColor = Color.DarkGray;
                }
                r.Close();
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            RequestService rs = new RequestService();
            rs.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            this.Hide();
            Submit_Proof_Of_Payment mp= new Submit_Proof_Of_Payment();
            mp.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer_Statuses cs = new Customer_Statuses();
            cs.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerProfile cp = new CustomerProfile();
            cp.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
