using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using HandyMike.Properties;



namespace HandyMike
{
    public partial class Form1 : Form
    {
        public static string id;
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

        }

        public static string InputBox(string prompt, string title, string defaultValue)
        {
            InputBoxDialog ib = new InputBoxDialog();
            ib.FormPrompt = prompt;
            ib.FormCaption = title;
            ib.DefaultValue = defaultValue;
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            //Admin
            //textBox1.Text = "RMoshoeu@fnb.co.za";
            //textBox2.Text = "RachelAdmin?321";

            //Handyman
            //textBox1.Text = "RaphaelSmith@gmail.com";
            //textBox2.Text = "Smithyfixe21";

            //Customer
            //textBox1.Text = "JohnTerry@gmail.com";
            //
            //textBox2.Text = "John-Terry?321";

            



            //To do: select last service after run
            string sid = "";
            try {

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                //Getting the latest service id in the Service table
                string query = "select MAX(Service_id) from Service";

                command.CommandText = query;

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();

                sid = dataGridView1.Rows[0].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }

            //Getting the saved variables before application exit
            bool rc = Convert.ToBoolean(Settings.Default["RequestClicked"].ToString());
            string cid = Settings.Default["Customer_id"].ToString();

            //first check if request was made or not
            if (rc == true)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    //The CustomerService table
                    string query = "insert into CustomerService(Service_id, Customer_id) values (@sid, @cid)";


                    command.CommandText = query;
                    command.Parameters.AddWithValue("@sid",sid);
                    command.Parameters.AddWithValue("@cid",cid);

                    command.ExecuteNonQuery();
                    connection.Close();

                    Settings.Default["RequestClicked"] = "false";




                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Customer")
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Select Customer_id from Customer where CustomerEmailAddress = @email and CustomerPassword = @password";
                    // rest for now

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@email", textBox1.Text);
                    command.Parameters.AddWithValue("@password", textBox2.Text);

                    //Filling the datagrid
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    

                    connection.Close();
                    if (dataGridView1.Rows.Count > 1)
                    {
                        id = (dataGridView1.Rows[0].Cells[0].Value.ToString());
                        this.Hide();
                        CustomerHomepage ch = new CustomerHomepage();
                        ch.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please correctly enter details or sign up", "Incorrect details", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                
            }


            if (comboBox1.Text == "Handyman")
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Select Handyman_id from Handyman where HandymanEmailAddress = @email and HandymanPassword = @password";
                    // rest for now

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@email", textBox1.Text);
                    command.Parameters.AddWithValue("@password", textBox2.Text);

                    //Filling the datagrid
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    

                    connection.Close();
                    if (dataGridView1.Rows.Count > 1)
                    {
                        id = (dataGridView1.Rows[0].Cells[0].Value.ToString());
                        this.Hide();
                        HandymanHomepage hh = new HandymanHomepage();
                        hh.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please correctly enter details or sign up", "Incorrect details", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                
            }

            if (comboBox1.Text == "Admin")
            {
                
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Select Admin_id from Administrator where AdminEmailAddress = @email and AdminPassword = @password";
                    // rest for now

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@email", textBox1.Text);
                    //textBox2.PasswordChar = '\0';
                    command.Parameters.AddWithValue("@password", textBox2.Text);

                    //Filling the datagrid
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    

                    connection.Close();
                    if (dataGridView1.Rows.Count > 1)
                    {
                        id = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        this.Hide();
                        AdminHomepage ah = new AdminHomepage();
                        ah.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please correctly enter details", "Incorrect details", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }

            }
            if (comboBox1.Text.All(char.IsWhiteSpace))
            {
                MessageBox.Show("Please choose a user type", "No user type chosen");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp su = new SignUp();
            su.ShowDialog();
            this.Close();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void label10_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
            await Task.Delay(TimeSpan.FromSeconds(2));
            textBox2.PasswordChar = '*';
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //insta
            System.Diagnostics.Process.Start("https://instagram.com/hand_ymike?igshid=YmMyMTA2M2Y=");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //facebook
            System.Diagnostics.Process.Start("https://www.facebook.com/handmanmike");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            PostQuery pq= new PostQuery();
            pq.ShowDialog();
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("HandyMike Queries Email: handymikequeries@handymike.com \nHandyMike Queries Phone Number: 081 455 0291","HandyMike Queries");
        }
    }
}
