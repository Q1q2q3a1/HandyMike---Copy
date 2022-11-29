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
    public partial class User_Satisfaction : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public User_Satisfaction()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";
        
        }

        private void User_Satisfaction_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'handyMikeData.Review' table. You can move, or remove it, as needed.
            //this.reviewTableAdapter.Fill(this.handyMikeData.Review);
            richTextBox1.ReadOnly = true;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //command.CommandType = CommandType.Text;
                string query = "Select CustomerName AS [Name], Service_Rating AS [Service Rating] from Review,Customer where Review.Customer_id = Customer.Customer_id";
                //YEAR(PaymentDate) = @Year AND 

                command.CommandText = query;

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();

                //Getting the monthly total
                int avg_rating = 0;
                int total = 0;
                int count = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    total += int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    count++;
                }
                avg_rating = total / count;
                label4.Text = "Average rating: " + Convert.ToString(avg_rating);



                //Rating pie chart
                int ones = 0, twos = 0, threes = 0, fours = 0, fives = 0;
                chart1.Titles.Add("Ratings distribution");
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    
                    
                    double x = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                  
                    if (x==1)
                    {
                        ones++;
                    }
                    if (x== 2)
                    {
                        twos++;
                    }
                    if (x == 3)
                    {
                        threes++;
                    }
                    if (x== 4)
                    {
                        fours++;
                    }
                    if (x == 5)
                    {
                        fives++;
                    }
                }
                if (ones != 0)
                {
                    chart1.Series["one"].Points.AddXY("1", ones);
                }
                if (twos != 0)
                {
                    chart1.Series["one"].Points.AddXY("2", twos);
                }
                if (threes != 0)
                {
                    chart1.Series["one"].Points.AddXY("3", threes);
                }
                if (fours != 0)
                {
                    chart1.Series["one"].Points.AddXY("4", fours);
                }
                if (fives != 0)
                {
                    chart1.Series["one"].Points.AddXY("5", fives);

                }
                /*
                chart1.Series["one"].Points.Add(ones);
                chart1.Series["one"].Points.Add(twos);
                chart1.Series["one"].Points.Add(threes);
                chart1.Series["one"].Points.Add(fours);
                chart1.Series["one"].Points.Add(fives);
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
