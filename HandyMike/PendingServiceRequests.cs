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
    public partial class PendingServiceRequests : Form
        
    {
        String[] servicesavailable = { "Painting", "Plumbing", "Electrical maintainence", "Tiling"};
        List<string> servicesids = new List<string>();
        List<string> cusids = new List<string>();
        List<string> names = new List<string>();

        
        private OleDbConnection connection = new OleDbConnection();
        public PendingServiceRequests()
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


        private void PendingServiceRequests_Load(object sender, EventArgs e)
        {
            
            
            servicesids.Clear();
            Clearfields();
            if (comboBox1.SelectedIndex == -1)
            {
                button1.Hide();
                button2.Hide();
            }
            else
            {
                button1.Show();
                button2.Show();
            }

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //command.CommandType = CommandType.Text;
                string query = "Select CustomerName, CustomerSurname,Service.Service_id,Customer.Customer_id,ServiceName from Service, Customer, CustomerService,ServiceAvailable where" +
                    " Service.Service_id = CustomerService.Service_id and Customer.Customer_id=CustomerService.Customer_id and RequestServiceStatus = 'Pending' and ServiceAvailable.ServiceName_id = Service.ServiceName_id";
                

                command.CommandText = query;

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();


                //getting the names
                comboBox1.Items.Clear();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    comboBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + " " + dataGridView1.Rows[i].Cells[1].Value.ToString() + " [" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "]");
                    servicesids.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    cusids.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Are you sure want to ACCEPT this request?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == DialogResult.Yes)
            {

                //update service table
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Update Service SET RequestServiceStatus = 'Accepted' where Service_id = @s";
                    

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@s", servicesids[comboBox1.SelectedIndex]);

                    command.ExecuteNonQuery();

                    connection.Close();




                    MessageBox.Show("Service request successfully accepted");
                    Clearfields();
                    if (comboBox1.SelectedIndex == -1)
                    {
                        button1.Hide();
                        button2.Hide();
                    }
                    else
                    {
                        button1.Show();
                        button2.Show();
                    }



                }
                 catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                
            }
        }
        public void GoHome()
        {
            this.Hide();
            HandymanHomepage hh = new HandymanHomepage();
            hh.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dR= MessageBox.Show("Are you sure want want to REJECT this request?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == DialogResult.No)
            {
                
            }
            else if (dR == DialogResult.Yes)
            {
                //Reason column
                string Reason = InputBox("Please enter reason for rejecting this request", "Rejection Reason", "");
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandType = CommandType.Text;
                    string query = "Update Service SET RequestServiceStatus = 'Rejected',Reason = @reason where Service_id = @s";

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@reason", Reason);
                    command.Parameters.AddWithValue("@s", servicesids[comboBox1.SelectedIndex]);

                    command.ExecuteNonQuery();
                   
                    connection.Close();




                    
                    MessageBox.Show("Rejection reason captured");
                    Clearfields();
                    if (comboBox1.SelectedIndex == -1)
                    {
                        button1.Hide();
                        button2.Hide();
                    }
                    else
                    {
                        button1.Show();
                        button2.Show();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clearfields();
            if (comboBox1.SelectedIndex == -1)
            {
                button1.Hide();
                button2.Hide();
            }
            else
            {
                button1.Show();
                button2.Show();
            }


        }

        public void Clearfields()
        {
            comboBox1.SelectedIndex = -1;
            label15.Text = "";
            label16.Text = ""; label4.Text = ""; label11.Text = "";
            label13.Text = ""; label14.Text = ""; label18.Text = ""; label12.Text = ""; label21.Text = "";
            richTextBox1.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GoHome();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a customer");
            }
            else
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    button1.Hide();
                    button2.Hide();
                }
                else
                {
                    button1.Show();
                    button2.Show();
                }

                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;


                    string query = "Select ServiceName_id, StreetAddress, Suburb, ResidenceType, PostalCode, BuildingEstateName, HouseUnitNumber, " +
                        "RequestDate, ServiceDescription from Service where" +
                        " @serviceid = Service_id";

                    command.CommandText = query;


                    command.Parameters.AddWithValue("@serviceid", servicesids[comboBox1.SelectedIndex]);

                    //Filling the datagrid

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = dt;




                    //Populating the labels when customer name is chosen
                    //Customer Name
                    string[] names = comboBox1.Text.Split(' ');
                    label15.Text = names[0];
                    //ServiceName
                    int num = int.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    string servicename = "";
                    for (int i = 1; i < servicesavailable.Length + 1; i++)
                    {
                        if (i == num)
                        {
                            servicename = servicesavailable[i - 1];
                        }
                    }
                    label16.Text = servicename;
                    //Streetadrees
                    label4.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    //Suburb
                    label11.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    //ResidenceType
                    label13.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    //Postal Code
                    label14.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                    //Building/estate name
                    label18.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
                    //House or unit number
                    label12.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
                    //Date and time
                    label21.Text = dataGridView1.Rows[0].Cells[7].Value.ToString();
                    //desc
                    richTextBox1.Text = dataGridView1.Rows[0].Cells[8].Value.ToString();



                    connection.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
        }
    }
}
