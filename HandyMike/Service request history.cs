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
    public partial class Service_request_history : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Service_request_history()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";
        }

        private void Service_request_history_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'handyMikeData.Service' table. You can move, or remove it, as needed.
            //this.serviceTableAdapter.Fill(this.handyMikeData.Service);
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "Months";
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "Service requests";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Clearing the chart content
            chart2.Titles.Clear();
            chart2.Series["Total Answered Service Requests"].Points.Clear();
            chart2.Series["Accepted Requests"].Points.Clear();
            chart2.Series["Rejected Requests"].Points.Clear();
           
            

            //Adding title
            chart2.Titles.Add("Service Request History (" + comboBox1.Text + ")");
            String[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            //By month
            /*
            
            int month = 0;
            for (int i = 0; i < months.Length; i++)
            {
                if (comboBox2.Text == months[i])
                {
                    month = i + 1;
                }
            }
            */
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                string query = "";

                OleDbDataAdapter da = new OleDbDataAdapter();
                DataTable dt = new DataTable();

                int rej = 0, acc = 0;
                int total = 0,yeartotal =0, ryear=0, ayear=0;
                

                //By year
                for (int j = 1; j < months.Length+1; j++)
                {
                    command.Parameters.Clear();
                    
                    query = "select Service_id , RequestServiceStatus from Service where YEAR(RequestDate) = @Year and Month(RequestDate) = @Month";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Year", comboBox1.Text);
                    command.Parameters.AddWithValue("@Month", j );

                    //Filling the datagrid
                    da = new OleDbDataAdapter(command);
                    dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    //Request bar graph

                    rej = 0; acc = 0; total = 0;

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {

                        String x = (dataGridView1.Rows[i].Cells[1].Value.ToString());

                        if (x == "Rejected")
                        {
                            rej++;
                           
                        }
                        if (x == "Accepted")
                        {
                            acc++;
                        }

                    }
                    //MessageBox.Show(rej.ToString());
                    total = rej + acc;
                    yeartotal += total; ryear += rej; ayear += acc;

                    chart2.Series["Total Answered Service Requests"].Points.AddXY(months[j-1], total);
                    chart2.Series["Accepted Requests"].Points.AddXY(months[j - 1], acc);
                    chart2.Series["Rejected Requests"].Points.AddXY(months[j - 1], rej);
                    dt.Clear();
                }
                label4.Text="Total Accepted Requests: " + ayear.ToString();
                label5.Text="Total Rejected Requests: " + ryear.ToString();
                label6.Text="Total for "+ comboBox1.Text+ ": " +yeartotal.ToString();
                connection.Close();








                /*
                //By month
                query = "select Service_id , RequestServiceStatus from Service where YEAR(RequestDate) = @Year and Month(RequestDate) = @Month";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Year", comboBox1.Text);
                command.Parameters.AddWithValue("@Month", month);

                //Filling the datagrid
               
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();

                //Request bar graph
               
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {

                    String x = (dataGridView1.Rows[i].Cells[1].Value.ToString());

                    if (x == "Rejected" )
                    {
                        rej++;
                    }
                    if (x == "Accepted")
                    {
                        acc++;
                    }
                    
                }
                total = rej + acc;

                
                 chart2.Series["Total Answered Service Requests"].Points.AddXY(comboBox2.Text, total);
                 chart2.Series["Accepted Requests"].Points.AddXY(comboBox2.Text, acc);
                 chart2.Series["Rejected Requests"].Points.AddXY(comboBox2.Text, rej);
                */


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports r = new Reports();
            r.ShowDialog();
            this.Close();
        }
    }
}
