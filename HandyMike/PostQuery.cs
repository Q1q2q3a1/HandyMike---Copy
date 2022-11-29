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
    public partial class PostQuery : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public PostQuery()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\27715\Documents\IS Labs\HandyMike.accdb; Persist Security Info = False; ";
        }
    

        private void button3_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Please enter a query or exit");
            }
            else
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //LOGIN info coming here
                    string query = "insert into Query(Customer_id,Admin_id,Query_details) " +
                        "values (@cid,10000000,@query)";


                    command.CommandText = query;
                    //1000016
                   
                    command.Parameters.AddWithValue("@cid", "1000016");
                    command.Parameters.AddWithValue("@query", richTextBox1.Text);
                   
                    command.ExecuteNonQuery();

                    connection.Close();

                    
                        MessageBox.Show("Query successfully posted");
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.ShowDialog();
                        this.Close();
                        
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
        }
    }
}
