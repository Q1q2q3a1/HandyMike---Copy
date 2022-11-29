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
    public partial class Management_Page : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        String[] servicesavailable = { "Painting", "Plumbing", "Electrical maintainence", "Tiling" };
        List<string> servicesids = new List<string>();
        public Management_Page()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

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

        private void Management_Page_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            lbDetailedAppointments.Items.Clear();
            string[] datetimesep = (monthCalendar1.SelectionRange.Start.ToString()).Split(' '); 
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //LOGIN info coming here
                string query = "Select ServiceName_id,RequestDate,CustomerHomeAddress,Service.Service_id,Note from Service,Customer,CustomerService " +
                    "where CustomerService.Service_id =  Service.Service_id and CustomerService.Customer_id = Customer.Customer_id and RequestDate like" +
                    " @date and RequestServiceStatus = 'Accepted'";// and (ServiceStatus = 'Pending' or ServiceStatus = 'In Progress')";

                command.CommandText = query;
                String date = monthCalendar1.SelectionRange.Start.ToString();
                
                command.Parameters.AddWithValue("@date", datetimesep[0]+"%");

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;




                int appointmentcount = 0;
                //populating the list view with apppointments of the day
                for (int p = 0; p < dataGridView1.Rows.Count - 1; p++)
                {
                    servicesids.Add(dataGridView1.Rows[p].Cells[3].Value.ToString());
                    int num = int.Parse(dataGridView1.Rows[p].Cells[0].Value.ToString());
                    string servicename = "";
                    for (int i = 1; i < servicesavailable.Length + 1; i++)
                    {
                        if (i == num)
                        {
                            servicename = servicesavailable[i - 1];
                        }
                    }
                    lbDetailedAppointments.Items.Add("- [" + dataGridView1.Rows[p].Cells[1].Value.ToString() + "] " + servicename + " AT " + dataGridView1.Rows[p].Cells[2].Value.ToString());
                    appointmentcount++;
                }

                //Populating the appointment and date labels
                label4.Text = appointmentcount.ToString();

                label3.Text = datetimesep[0];
                if (dataGridView1.Rows.Count >1)
                {
                    richTextBox1.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                }
                
                connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            lbDetailedAppointments.Items.Clear();
            string[] datetimesep = (monthCalendar1.SelectionRange.Start.ToString()).Split(' ');
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //LOGIN info coming here
                string query = "update Service set [Note]=@note where [RequestDate] like @date";
                //string query = "Insert into Service([Note]) values (@note) where [RequestDate] like @date";
                command.CommandText = query;

                command.Parameters.AddWithValue("@note", richTextBox1.Text);
                command.Parameters.AddWithValue("@date", datetimesep[0] + "%");

                command.ExecuteNonQuery();

                

                connection.Close();



                label4.Text = "0";
                label3.Text = "0000/00/00";
                //monthCalendar1.Day.IsSelectable = false;
                MessageBox.Show("Note for "+ datetimesep[0] + " successfully updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            richTextBox1.Clear();
        }

    }
}
