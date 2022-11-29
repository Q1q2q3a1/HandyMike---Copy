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
    public partial class ServiceStatus : Form
    {
        String[] servicesavailable = { "Painting", "Plumbing", "Electrical maintainence", "Tiling" };
        List<string> servicesids = new List<string>();
        List<string> cusids = new List<string>();
        

        string service, cusid = "";

        private OleDbConnection connection = new OleDbConnection();
        public ServiceStatus()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ServiceStatus_Load(object sender, EventArgs e)
        {
            label4.Text = "";
            label16.Text = "";
            button1.Hide();
            button2.Hide();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //command.CommandType = CommandType.Text;
                string query = "Select CustomerName, CustomerSurname,Service.Service_id,Customer.Customer_id,ServiceName from Service, Customer, CustomerService, ServiceAvailable where" +
                    " (Service.Service_id = CustomerService.Service_id and Customer.Customer_id=CustomerService.Customer_id) and (ServiceStatus = 'Pending' or ServiceStatus = 'In Progress') " +
                    "and ServiceAvailable.ServiceName_id = Service.ServiceName_id";


                command.CommandText = query;

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                


                //getting the names
                comboBox2.Items.Clear();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    comboBox2.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + " " + dataGridView1.Rows[i].Cells[1].Value.ToString()
                        + " [" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "]");
                    servicesids.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    cusids.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            HandymanHomepage hh = new HandymanHomepage();
            hh.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Are you sure want to change the service status?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == DialogResult.Yes)
            {

                //update service table
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Update Service SET ServiceStatus = 'In Progress' where Service_id = @s";


                    command.CommandText = query;
                    command.Parameters.AddWithValue("@s", service);

                    command.ExecuteNonQuery();
                    connection.Close();




                    MessageBox.Show("Service status successfully changed");
                    this.Hide();
                    HandymanHomepage hh = new HandymanHomepage();
                    hh.ShowDialog();
                    this.Close();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Are you sure want to change the service status?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == DialogResult.Yes)
            {

                //update service table
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Update Service SET ServiceStatus = 'Completed' where Service_id = @s";


                    command.CommandText = query;
                    command.Parameters.AddWithValue("@s", service);
                    command.ExecuteNonQuery();

                    connection.Close();




                    MessageBox.Show("Service status successfully changed");
                    this.Hide();
                    HandymanHomepage hh = new HandymanHomepage();
                    hh.ShowDialog();
                    this.Close();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a customer");
            }
            else
            {
                button2.Show();
                button1.Show();
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    string query = "Select ServiceStatus, ServiceName_id from Service where" +
                        " @serviceid = Service_id";

                    command.CommandText = query;

                    command.Parameters.AddWithValue("@serviceid", servicesids[comboBox2.SelectedIndex]);
                    service = servicesids[comboBox2.SelectedIndex];


                    //Filling the datagrid

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = dt;


                    //Populating the labels when customer name is chosen
                    //ServiceName
                    int num = int.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                    string servicename = "";
                    for (int i = 1; i < servicesavailable.Length + 1; i++)
                    {
                        if (i == num)
                        {
                            servicename = servicesavailable[i - 1];
                        }
                    }
                    label4.Text = servicename;
                    label16.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();


                    connection.Close();




                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                if (label16.Text == "Pending")
                {
                    button2.Hide();
                }
                else if (label16.Text == "In Progress")
                {
                    button1.Hide();
                }
            }
        }
    }
}
