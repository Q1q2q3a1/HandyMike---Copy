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
    public partial class Growth_Report : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Growth_Report()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //command.CommandType = CommandType.Text;
                string query = "select Customer_id, Date_Joined from Customer where YEAR(Date_Joined) = @Year";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Year", comboBox1.Text);
                

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();
                
                //int jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
                int[] monthnums = new int[12];
                String[] months = {"01","02","03","04","05","06","07","08","09","10","11","12"};
                for (int j = 0; j < months.Length; j++) {
                    int count = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        String x = (dataGridView1.Rows[i].Cells[1].Value.ToString());
                        if (months[j] == x.Substring(5, 2))
                        {
                            count++;
                        }

                    }
                    monthnums[j] = count;
                }
                label13.Text = "January: "+ monthnums[0].ToString();
                label12.Text = "February: " + monthnums[1].ToString();
                label11.Text = "March: " + monthnums[2].ToString();
                label10.Text = "April: " + monthnums[3].ToString();
                label9.Text = "May: " + monthnums[4].ToString();
                label8.Text = "June: " + monthnums[5].ToString();
                label7.Text = "July: " + monthnums[6].ToString();
                label6.Text = "August: " + monthnums[7].ToString();
                label5.Text = "September: " + monthnums[8].ToString();
                label4.Text = "October: " + monthnums[9].ToString();
                label3.Text = "November: " + monthnums[10].ToString();
                label2.Text = "December: " + monthnums[11].ToString();
                int total = 0;
                for (int i = 0; i<monthnums.Length;i++)
                {
                    total += monthnums[i];
                }
                label20.Text = "Total new users: " + total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports r = new Reports();
            r.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Growth_Report_Load(object sender, EventArgs e)
        {
            /*
            label13.Text = "0";
            label12.Text = "0";
            label11.Text = "0";
            label10.Text = "0";
            label9.Text = "0";
            label8.Text = "0";
            label7.Text = "0";
            label6.Text = "0";
            label5.Text = "0";
            label4.Text = "0";
            label3.Text = "0";
            label2.Text = "0";
            */
        }
    }
}
