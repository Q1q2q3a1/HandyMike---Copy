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

namespace HandyMike
{
    public partial class Customer_Statuses : Form
    {
        String[] servicesavailable = { "Painting", "Plumbing", "Electrical maintainence", "Tiling" };
        List<string> serviceids = new List<string>();
        
        List<string> services = new List<string>();
        Boolean clicked = false;

        public static string sid;
        public static string servicestatus;

        private OleDbConnection connection = new OleDbConnection();
        public Customer_Statuses()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerHomepage ch = new CustomerHomepage();
            ch.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Customer_Statuses_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //command.CommandType = CommandType.Text;
                //string query = "Select RequestServiceStatus,ServiceStatus from Service, Customer, CustomerService where" +
                 //   " Service.Service_id = CustomerService.Service_id and @cusid=CustomerService.Customer_id and (RequestServiceStatus = 'Pending' or RequestServiceStatus = 'Accepted')";

                string query = "Select CustomerService.Service_id,ServiceName from Service,CustomerService, ServiceAvailable where" +
                    " Service.Service_id = CustomerService.Service_id and @cusid=CustomerService.Customer_id " +
                    "and Service.ServiceName_id=ServiceAvailable.ServiceName_id";


                command.CommandText = query;
                string cusid = Form1.id;
                command.Parameters.AddWithValue("@cusid", cusid);

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                for (int p = 0; p < dataGridView1.Rows.Count - 1; p++)
                {
                    comboBox1.Items.Add(dataGridView1.Rows[p].Cells[1].Value.ToString());
                    serviceids.Add(dataGridView1.Rows[p].Cells[0].Value.ToString());
                }


                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            label7.Hide();
            label16.Hide();
            numericUpDown1.Hide();
            button2.Hide();
            button1.Hide();
            button5.Hide();
            label6.Hide();
            richTextBox1.Hide();
            label4.Hide();
            button7.Hide();
            pictureBox1.Hide();
            button5.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Hide();
            button7.Hide();
            pictureBox1.Hide();
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a service");
            }
            else
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Select RequestServiceStatus,ServiceStatus from Service where" +
                       " Service_id =@servid";

                    command.CommandText = query;
                    string servid = serviceids[comboBox1.SelectedIndex];
                    command.Parameters.AddWithValue("@servid", servid);


                    //Filling the datagrid
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    label4.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    label16.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                label4.Show();
                label16.Show();
                if (label16.Text == "Completed" && label4.Text == "Accepted")
                {
                    button7.Show();
                    pictureBox1.Show();
                    button5.Show();
                }
                
                if (label16.Text == "Pending" && label4.Text == "Accepted")
                {
                    button7.Show();
                    pictureBox1.Show();
                }
                
                if (label16.Text == "Recent Payment Invalid")
                {
                    pictureBox1.Show();
                    button7.Show();
                    button7.Text = "Remake payment";
                    button5.Hide();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (numericUpDown1.Value == 0)
            {
                
                MessageBox.Show("Please provide a rating", "Missing Rating", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                
            }
            else
            {
                label3.Hide();
                label5.Hide();
                label4.Hide();
                label2.Hide();
                label16.Hide();
                label7.Hide();
                button4.Hide();
                button6.Hide();
                button3.Hide();
                button2.Hide();
                button5.Hide();
                comboBox1.Hide();
                numericUpDown1.Hide();
                clicked = true;

                richTextBox1.Show();
                label6.Show();

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Insert into Review(Customer_id, Review_Details, Service_Rating) values (@cus,@rd,@rs)";

                    command.CommandText = query;
                    string servid = serviceids[comboBox1.SelectedIndex];
                    command.Parameters.AddWithValue("@cus", Form1.id);
                    command.Parameters.AddWithValue("@rd", richTextBox1.Text);
                    command.Parameters.AddWithValue("@rs", numericUpDown1.Value);

                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Review successfully posted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
            richTextBox1.Clear();
            richTextBox1.Hide();
            label6.Hide();
            comboBox1.SelectedIndex = -1;
            comboBox1.Show();
            button4.Show();
            label3.Show();
            label5.Show();
            label4.Text = "";
            label16.Text = "";
            label4.Show();
            label16.Show();
            label2.Show();
            button6.Show();
            button3.Show();
            button1.Hide();
            button2.Hide();
            numericUpDown1.Hide();
            label7.Hide();
            numericUpDown1.Value = 0;

        }
        public Boolean Validation()
        {
            Boolean valid = true;
            if (clicked == true)
            {
                if (richTextBox1.Text.All(char.IsWhiteSpace))
                {
                    valid = false;
                    MessageBox.Show("Please enter the note", "Missing Note", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    return valid;
                }
            }
            
            return valid;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Hide();
            label7.Show();
            numericUpDown1.Show();
            button2.Show();
            button1.Show();

            button7.Hide();
            pictureBox1.Hide();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            sid = serviceids[comboBox1.SelectedIndex];
            servicestatus = label16.Text ;
            this.Hide();
            Submit_Proof_Of_Payment mp = new Submit_Proof_Of_Payment();
            mp.ShowDialog();
            this.Close();

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
