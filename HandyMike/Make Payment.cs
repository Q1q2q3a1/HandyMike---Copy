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

namespace HandyMike
{
    public partial class Submit_Proof_Of_Payment : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        String[] servicesavailable = { "Painting", "Plumbing", "Electrical maintainence", "Tiling" };
        List<string> iorf = new List<string>();
        List<string> sids = new List<string>();
        double amount = 0;
        public Submit_Proof_Of_Payment()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";
        }

       

        private void button4_Click(object sender, EventArgs e)

        {
            
            if (checkValid() == true)
            {
                
                //DialogResult dR = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dR == DialogResult.Yes)
                //{
                    //database insert initial
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "";
                        if (label1.Text == "Submit Initial Proof Of Payment")
                        {
                            //database insert initial
                            query = "insert into Payment(Customer_id,Service_id,PaymentDate,InitialProof,InitialPayment,TotalPaymentAmount) values (@cid, @sid, @date, @iproof,@ip,@fpa)";

                            command.CommandText = query;
                            command.Parameters.AddWithValue("@cid", Form1.id);
                            command.Parameters.AddWithValue("@sid", Customer_Statuses.sid);
                            command.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                            MemoryStream st = new MemoryStream();
                            pictureBox1.Image.Save(st, System.Drawing.Imaging.ImageFormat.Gif);
                            byte[] pic = st.ToArray();
                            command.Parameters.AddWithValue("@iproof", pic);
                            command.Parameters.AddWithValue("@ip", amount);
                            command.Parameters.AddWithValue("@fpa", amount);
                        //command.Parameters.AddWithValue("@amount", amount);
                        //command.Parameters.AddWithValue("@tamount", amount);

                    }
                        else
                        {
                            //database insert final
                            query = "insert into Payment(Customer_id,Service_id,PaymentDate,FinalProof,FinalPayment,TotalPaymentAmount) values (@cid, @sid, @date, @fproof,@fp,@fpa)";

                            command.CommandText = query;
                            command.Parameters.AddWithValue("@cid", Form1.id);
                            command.Parameters.AddWithValue("@sid", Customer_Statuses.sid);
                            command.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                            //Image to byte array
                            MemoryStream st = new MemoryStream();
                            pictureBox1.Image.Save(st, System.Drawing.Imaging.ImageFormat.Gif);
                            byte[] pic = st.ToArray();
                            command.Parameters.AddWithValue("@fproof", pic);
                            command.Parameters.AddWithValue("@fp", amount);
                            command.Parameters.AddWithValue("@fpa", amount);
                        //command.Parameters.AddWithValue("@amount", amount);
                        //command.Parameters.AddWithValue("@tamount", amount);

                    }

                        label6.Text = "Amount: R";
                        

                        command.ExecuteNonQuery();

                        connection.Close();
                        MessageBox.Show("Proof of payment successfully submitted ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //System.Threading.Thread.Sleep(3000);
                        this.Hide();
                        Customer_Statuses cs = new Customer_Statuses();
                        cs.ShowDialog();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex);
                    }

                   
                    
                //}
            }
            else
            {
                
                DialogResult dR = MessageBox.Show("Please upload the proof of payment screenshot", "Missing proof of payment", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (dR == DialogResult.Retry)
                {
                    OpenFileDialog o = new OpenFileDialog();
                    o.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
                    if (o.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image = new Bitmap(o.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    button4.Show();
                    button2.Hide();
                    pictureBox1.Show();
                }
                else if(dR == DialogResult.Cancel)
                {
                    this.Hide();
                    Customer_Statuses cs = new Customer_Statuses();
                    cs.ShowDialog();
                    this.Close();
                }
            }


        }
        public Boolean checkValid()
        {
            
            Boolean valid = true;
            if (pictureBox1.Image == null)
            {
                valid = false;
                
                return valid;
            }

            return valid;
            /*
            //Card No
            if (textBox1.Text.Length != 16)
            {
                valid = false;
                MessageBox.Show("Your card number should be 16 digits long", "Incorrect Card Number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if (!textBox1.Text.All(char.IsDigit))
            {
                valid = false;
                MessageBox.Show("Your card number should only be digits", "Incorrect Card Number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if (textBox1.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Missing Card Number", "Incorrect Card Number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //Year and month
            if (Int16.Parse(textBox4.Text) > 12 || Int16.Parse(textBox4.Text) < 01)
            {
                valid = false;
                MessageBox.Show("Month must be between 01 and 12", "Incorrect Expiry Date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (textBox4.Text.Length != 2 || textBox3.Text.Length != 2)
            {
                valid = false;
                MessageBox.Show("Your year and month should each be 2 digits long", "Incorrect Expiry Date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if (!(textBox4.Text.All(char.IsDigit) || textBox3.Text.All(char.IsDigit)))
            {
                valid = false;
                MessageBox.Show("Your year and month should only be digits", "Incorrect Expiry Date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if (textBox4.Text.All(char.IsWhiteSpace) || textBox3.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Missing Year or Month", "Incorrect Expiry Date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (Int16.Parse(textBox4.Text) > 12 || Int16.Parse(textBox4.Text) < 01)
            {
                valid = false;
                MessageBox.Show("Month must be between 01 and 12", "Incorrect Expiry Date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            if (Int16.Parse(textBox3.Text) < Int16.Parse(DateTime.Now.Year.ToString().Substring(2,2))){
                valid = false;
                MessageBox.Show("Year must not be earlier than the current year", "Incorrect Expiry Date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }


            //CVV
            if (textBox2.Text.Length != 3)
            {
                valid = false;
                MessageBox.Show("Your CVV should be 3 digits long", "Incorrect CVV", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if (!(textBox2.Text.All(char.IsDigit)))
            {
                valid = false;
                MessageBox.Show("Your CVV should only be digits", "Incorrect CVV", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if (textBox2.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Missing CVV", "Incorrect CVV", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;

            }
            */

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer_Statuses cs = new Customer_Statuses();
            cs.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Make_Payment_Load(object sender, EventArgs e)
        {
            //Checking if it is final or initail payment then change labels
            if (Customer_Statuses.servicestatus == "Pending" || Customer_Statuses.servicestatus == "Recent Payment Invalid")
            {
                label1.Text = "Submit Initial Proof Of Payment";
                //button4.Text = "Make initial payment";
            }

            else if (Customer_Statuses.servicestatus == "Completed")
            {
                //button4.Text = "Make final payment";
                label1.Text = "Submit Final Proof Of Payment";
            }


            button4.Hide();
            button2.Show();
            pictureBox1.Hide();
            label6.Hide();
           
            //FILL IN THE COMBOBOX
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "Select ServiceCost from Quote where Service_id = @sid";

                command.CommandText = query;


                command.Parameters.AddWithValue("@sid", Customer_Statuses.sid);

                //Filling the datagrid

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;

                connection.Close();
                //comboBox1.Items.Clear();
                amount = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString()) / 2;
                label6.Text = "Amount: R" + amount;
                label6.Show();

                /*
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    
                    iorf.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    //ServiceName
                    int num = int.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    string servicename = "";
                    //Making the servicename_id a name
                    for (int s = 1; s < servicesavailable.Length + 1; s++)
                    {
                        //MessageBox.Show("2");
                        if (s == num)
                        {
                            servicename = servicesavailable[s-1];
                        }
                    }
                    //comboBox1.Items.Add(servicename);
                    sids.Add(dataGridView1.Rows[0].Cells[2].Value.ToString());
                }
                */


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }



        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a service");
            }
            else
            {
                button1.Hide();
                label7.Hide();
                comboBox1.Hide();

                //Checking if it is final or initail payment then change labels
                if (iorf[comboBox1.SelectedIndex] == "Pending")
                {
                    label1.Text = "Submit Initial Proof Of Payment";
                    //button4.Text = "Make initial payment";
                }
                else if (iorf[comboBox1.SelectedIndex] == "Completed")
                {
                    //button4.Text = "Make final payment";
                    label1.Text = "Submit Final Proof Of Payment";
                }


                
                button2.Show();

                //Getting the price to show
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "Select ServiceCost from Quote where Service_id = @sid";

                    command.CommandText = query;


                    command.Parameters.AddWithValue("@sid", sids[comboBox1.SelectedIndex]);

                    //Filling the datagrid

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = dt;

                    connection.Close();
                    //comboBox1.Items.Clear();
                    amount = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString()) / 2;
                    label6.Text = "Amount: R" + amount + ".00 ";
                    label6.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }


            }



        }
        */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (o.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(o.FileName);
            }
            button4.Show();
            button2.Hide();
            pictureBox1.Show();
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //pictureBox1.BackColor = Color.DarkGray;
            //axAcroPDF1.Show();

        }
    }
    
}
/*
 * 
 * {  
                        byte[] ap = (byte[])(documentsDataGridView.SelectedRows[0].Cells["pdf_file"].Value);  
                        MemoryStream ms = new MemoryStream(ap);  
                        axAcroPDF1.src = LocalEncoding.GetString(ms.ToArray());  
                        //axAcroPDF1.LoadFile(ms);  
  
  
                    }  
                    else  
                    {  
                        axAcroPDF1.src = null;  
                    }  
*/
