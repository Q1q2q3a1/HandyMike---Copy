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
    public partial class Advertise : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Advertise()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

        }

        private void Advertise_Load(object sender, EventArgs e)
        {
            button2.Hide();
            pictureBox1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminHomepage ah = new AdminHomepage();
            ah.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            pictureBox1.Show();
            button2.Show();

            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (o.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(o.FileName);
                //pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                //pictureBox1.BackColor = Color.DarkGray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkValid() == true)
            {


                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    string query = "Insert into Advertisement(Admin_id, AdvertisementPicture) values (10000000, @pic)";

                    command.CommandText = query;

                    //Image to byte array

                    MemoryStream st = new MemoryStream();
                    pictureBox1.Image.Save(st, System.Drawing.Imaging.ImageFormat.Gif);
                    byte[] pic = st.ToArray();

                    command.Parameters.AddWithValue("@pic", pic);
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Advertisement successfully uploaded");

                    this.Hide();
                    AdminHomepage ah = new AdminHomepage();
                    ah.ShowDialog();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }

                
            }
            else
            {
                    


                DialogResult dR = MessageBox.Show("Please upload the advertisement", "Missing advertisement", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error); if (dR == DialogResult.Retry)
                {
                    OpenFileDialog o = new OpenFileDialog();
                    o.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
                    if (o.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image = new Bitmap(o.FileName);
                    }
                    //button4.Show();
                    button1.Hide();
                    pictureBox1.Show();
                }
                else if (dR == DialogResult.Cancel)
                {
                    this.Hide();
                    AdminHomepage ah = new AdminHomepage();
                    ah.ShowDialog();
                    this.Close();
                }

            }


        }
        public Boolean checkValid()
        {

            Boolean valid = true;
            if (pictureBox1.Image == null)
            {
                valid = false;
                
                return valid;
            }

            return valid;
        }
       }
}
