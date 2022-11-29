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
using System.IO;
using PdfSharp.Pdf;

namespace HandyMike
{
    
    public partial class Verify_payment : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        List<string> paymentids = new List<string>();
        List<string> sds = new List<string>();
        List<string> ps = new List<string>();
        string p = "";
        public Verify_payment()
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            radioButton2.Checked = false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
        }

        private void Verify_payment_Load(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            //FILL IN THE COMBOBOX
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "Select CustomerName, CustomerSurname,Service.Service_id," +
                    "Customer.Customer_id,ServiceName,InitialProof,FinalProof,InitialValid," +
                    "FinalValid,Payment_id from Service, Customer, CustomerService,ServiceAvailable,Payment where" +
                    " Service.Service_id = CustomerService.Service_id and" +
                    " Customer.Customer_id=CustomerService.Customer_id and " +
                    "RequestServiceStatus = 'Accepted' and " +
                    "ServiceAvailable.ServiceName_id = Service.ServiceName_id and " +
                    "Payment.Service_id = Service.Service_id and" +
                    "(InitialProof IS NOT NULL or FinalProof IS NOT NULL) and" +
                    "(InitialValid IS NUll or FinalValid IS NULL)";


                command.CommandText = query;

                //string cusid = Form1.id;
                //command.Parameters.AddWithValue("@cusid", cusid);


                //Filling the datagrid

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;
                ((DataGridViewImageColumn)this.dataGridView1.Columns["InitialProof"]).DefaultCellStyle.NullValue = null;
                ((DataGridViewImageColumn)this.dataGridView1.Columns["FinalProof"]).DefaultCellStyle.NullValue = null;
                //sd = dataGridView1.Rows[i].Cells[5].Value.ToString();
                connection.Close();
                comboBox1.Items.Clear();

                //dataGridView1.Show();
                //MessageBox.Show(dataGridView1.Rows[1].Cells[5].Value.ToString());

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    p = "";
                    if (dataGridView1.Rows[i].Cells[5].Value.ToString() == "")
                    {
                        p = "Final Proof";
                        //MessageBox.Show("checked 5");                   
                    }
                    else
                    {
                        p = "Initial Proof";
                        //MessageBox.Show("checked 6");
                    }
                    ps.Add(p);
                    
                    
                    comboBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + " " + dataGridView1.Rows[i].Cells[1].Value.ToString() + " ["+ p +" for "+  dataGridView1.Rows[i].Cells[4].Value.ToString() + "]");
                    paymentids.Add(dataGridView1.Rows[i].Cells[9].Value.ToString());
                    sds.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer's proof of payment");
            }
            else
            {
                //Show the proof
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "";
                    //MessageBox.Show(ps[comboBox1.SelectedIndex]);
                    if (ps[comboBox1.SelectedIndex] == "Initial Proof")
                    {
                        query = "Select InitialProof from Payment where Service_id=@sd and Payment_id=(select MIN(Payment_id) from Payment)";
                    }
                    else if (ps[comboBox1.SelectedIndex] == "Final Proof")
                    {
                        query = "Select FinalProof from Payment where Service_id=@sd and Payment_id=(select MAX(Payment_id) from Payment)";
                    }

                    command.CommandText = query;

                    command.Parameters.AddWithValue("@sd", sds[comboBox1.SelectedIndex]);


                    //MessageBox.Show("checked 5");
                    //dispaly proof of payment
                    
                    OleDbDataReader r = command.ExecuteReader();
                    while (r.Read())
                    {
                        //MessageBox.Show("checked 5");
                        byte[] imgData = (byte[])r[0];
                        MemoryStream st = new MemoryStream(imgData);
                        pictureBox1.Image = Image.FromStream(st);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        
                        //pictureBox1.BackColor = Color.DarkGray;
                    }
                    r.Close();


                    connection.Close();                    
                    //string cusid = Form1.id;
                    //command.Parameters.AddWithValue("@cusid", cusid);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
           
            pictureBox1.Show();
        }


        public void ValidorElse()
        {
            string sid = "";
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "";
            //get the service id
            query = "select Service.Service_id from Service,Payment where Payment.Service_id = Service.Service_id and Payment_id=(select MAX(Payment_id) from Payment)";
            command.CommandText = query;


            OleDbDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                sid=r[0].ToString();
               
            }
            r.Close();


            //database update to invalid in service table
            query = "Update Service set ServiceStatus='Recent Payment Invalid' where Service_id = @sid";

            command.CommandText = query;

            command.Parameters.AddWithValue("@sid", sid);

            command.ExecuteNonQuery();

            connection.Close();
        }      

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked || radioButton2.Checked)
            {

                DialogResult dR = MessageBox.Show("Are you sure you want to process?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.Yes)
                {
                    //database insert initial
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "";
                        if (ps[comboBox1.SelectedIndex] == "Inital Proof")
                        {
                            if (radioButton1.Checked)
                            {

                                //database update valid
                                query = "Update Payment set InitialValid='Yes' where Service_id=@sd and Payment_id=(select MIN(Payment_id) from Payment)";

                                command.CommandText = query;

                                command.Parameters.AddWithValue("@sd", sds[comboBox1.SelectedIndex]);

                            }
                            else if (radioButton2.Checked)
                            {
                                //database delete invalid
                                query = "Delete from Payment where Service_id=@sd and Payment_id=(select MIN(Payment_id) from Payment)";

                                //query = "Update Payment set InitialValid='No' where Service_id=@sd and Payment_id=(select MIN(Payment_id) from Payment)";

                                command.CommandText = query;

                                command.Parameters.AddWithValue("@sd", sds[comboBox1.SelectedIndex]);

                            }

                        }
                        else if (ps[comboBox1.SelectedIndex] == "Final Proof")
                        {
                            if (radioButton1.Checked)
                            {

                                //database update valid
                                query = "Update Payment set FinalValid='Yes' where Service_id=@sd and Payment_id=(select MAX(Payment_id) from Payment)";

                                command.CommandText = query;
                                command.Parameters.AddWithValue("@sd", sds[comboBox1.SelectedIndex]);

                            }
                            else if (radioButton2.Checked)
                            {
                                //database delete invalid
                                query = "Delete from Payment where Service_id=@sd and Payment_id=(select MAX(Payment_id) from Payment)";

                                //query = "Update Payment set FinalValid='No' where Service_id=@sd and Payment_id=(select MAX(Payment_id) from Payment)";

                                command.CommandText = query;
                                command.Parameters.AddWithValue("@sd", sds[comboBox1.SelectedIndex]);

                            }

                        }

                        //label6.Text = "Amount: R";


                        command.ExecuteNonQuery();

                        connection.Close();
                        ValidorElse();
                        MessageBox.Show("Verification successfully submitted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //System.Threading.Thread.Sleep(3000);
                        this.Hide();
                        AdminHomepage ah = new AdminHomepage();
                        ah.ShowDialog();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex);
                    }

                    
                }
               
            }
            else
            {
                MessageBox.Show("Please select state of proof of payment");
            }
            comboBox1.SelectedIndex = -1;
            
        }
            
    }
}
