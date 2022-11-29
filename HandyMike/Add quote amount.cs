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
    public partial class Add_quote_amount : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        String[] servicesavailable = { "Painting", "Plumbing", "Electrical maintainence", "Tiling" };
        List<string> servicesids = new List<string>();
        List<string> cusids = new List<string>();
        List<string> names = new List<string>();

        //string service, cusid = "";

        public Add_quote_amount()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

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
        bool CostsValid()
        {
            bool valid = true;
            if (textBox1.Text.All(char.IsWhiteSpace))
            {

                MessageBox.Show("Please enter the material cost", "Missing Amount", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                valid = false;
                return valid;

            }
            if (textBox2.Text.All(char.IsWhiteSpace))
            {

                MessageBox.Show("Please enter the service cost", "Missing Amount", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                valid = false;
                return valid;

            }
            if (!(textBox1.Text.All(char.IsDigit)) || !(textBox2.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Amounts should be digits only", "Incorrect Amounts", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                valid = false;
                return valid;
            }
            return valid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a service");
            }
            else
            {
                if (CostsValid() == true) { 
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;

                        string query = "INSERT into Quote(Admin_id,Customer_id,Service_id,MaterialCost, ServiceCost, TotalCost) values (@aid,@cid,@sid,@mcost,@scost,@cost)";
                        command.CommandText = query;


                        command.Parameters.AddWithValue("@aid", Form1.id);
                        command.Parameters.AddWithValue("@cid", cusids[comboBox1.SelectedIndex]);
                        command.Parameters.AddWithValue("@sid", servicesids[comboBox1.SelectedIndex]);
                        command.Parameters.AddWithValue("@mcost", textBox1.Text);
                        command.Parameters.AddWithValue("@scost", textBox2.Text);
                        command.Parameters.AddWithValue("@cost", (double.Parse(textBox1.Text) + double.Parse(textBox2.Text)).ToString());

                        command.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Quote amount successfully added");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex);
                    }
                    comboBox1.SelectedIndex = -1;
                    textBox1.Text = "";
                    textBox2.Text = "";

                }
            }
        }

        private void Add_quote_amount_Load(object sender, EventArgs e)
        {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                
                string query = "Select ServiceName_id,RequestDate,CustomerHomeAddress,Service.Service_id,Customer.Customer_id from Service,Customer,CustomerService" +
                    " where Service.Service_id = CustomerService.Service_id and Customer.Customer_id=CustomerService.Customer_id and " +
                    "ServiceStatus='Pending' and RequestServiceStatus='Accepted' and Service.Service_id NOT IN (select Service_id from Quote)";

                command.CommandText = query;
                

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;


                connection.Close();
                //populating combobox with the services who have been accepted but still service status is pending
                for (int p = 0; p < dataGridView1.Rows.Count - 1; p++)
                {
                    int num = int.Parse(dataGridView1.Rows[p].Cells[0].Value.ToString());
                    string servicename = "";
                    for (int i = 1; i < servicesavailable.Length + 1; i++)
                    {
                        if (i == num)
                        {
                            servicename = servicesavailable[i-1];
                        }
                    }
                    comboBox1.Items.Add("[" + dataGridView1.Rows[p].Cells[1].Value.ToString() + "] " + servicename + " at " + dataGridView1.Rows[p].Cells[2].Value.ToString());
                    servicesids.Add(dataGridView1.Rows[p].Cells[3].Value.ToString());
                    cusids.Add(dataGridView1.Rows[p].Cells[4].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
    }
}


/*
  * int num = int.Parse(dataGridView1.Rows[p].Cells[0].Value.ToString());
                    string servicename = "";
                    for (int i = 1; i < servicesavailable.Length + 1; i++)
                    {
                        if (i == num)
                        {
                            servicename = servicesavailable[i-1];
                        }
                    }
*/
