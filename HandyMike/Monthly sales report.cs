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
    public partial class Monthly_sales_report : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Monthly_sales_report()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";
        }

        private void Monthly_sales_report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'handyMikeData.Payment' table. You can move, or remove it, as needed.
            //this.paymentTableAdapter.Fill(this.handyMikeData.Payment);
            /*try
            {
                
                
                connection.Open();
                //Logic here

                label2.Text = "CS";

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error"+ex);
            }
            */


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDisplayCriteria_Click(object sender, EventArgs e)
        {
            String[] months = {"January","February","March","April","May","June","July","August","September","October","November","December"};
            int month = 0;
            for (int i = 0; i < months.Length; i++)
            {
                if (comboBox2.Text == months[i])
                {
                     month = i + 1;
                }
            }

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //command.CommandType = CommandType.Text;
                string query = "select CustomerName AS [Name], CustomerSurname AS [Surname], ServiceName AS [Service]," +
                " PaymentDate AS [Last Payment Date], TotalPaymentAmount AS [Total Amount] from Payment,Customer,Service,ServiceAvailable where" +
                " Payment.Service_id =  Service.Service_id and Payment.Customer_id = Customer.Customer_id and Service.ServiceName_id = ServiceAvailable.ServiceName_id" +
                " and YEAR(PaymentDate) = @Year" +
                " and MONTH(PaymentDate) = @Month";
                // AND 

                command.CommandText = query;

                
                command.Parameters.AddWithValue("@Year", comboBox1.Text);
                command.Parameters.AddWithValue("@Month", month);



                /*
                OleDbParameter[] parameters = new OleDbParameter[]
                        {
                            new OleDbParameter("@Year", comboBox1.Text),
                            new OleDbParameter("@Month", comboBox2.Text)
                        };
                command.Parameters.AddRange(parameters);*/


                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();

                //Getting the monthly total
                double Monthsales = 0;
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                     Monthsales += double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                }
                label2.Text = comboBox2.Text+" "+comboBox1.Text + " sales: R" + Convert.ToString(Monthsales);

            }
            catch(Exception ex)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
