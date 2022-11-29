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
    public partial class HandymanProfile : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        List<string> ids = new List<string>();
        public HandymanProfile()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Manage_users.hclicked == true)
            {

                this.Hide();
                Manage_users mu = new Manage_users();
                mu.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                HandymanHomepage hh = new HandymanHomepage();
                hh.ShowDialog();
                this.Close();
            }
            
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Hide();
            //verify email
            label3.Show();
            textBox5.Show();
            //verify password
            label10.Show();
            textBox9.Show();
            //button
            button4.Show();
            //show passwords
            label6.Show();
            label7.Show();

            // Customer Name
            textBox1.ReadOnly = false;
            //surname
            textBox8.ReadOnly = false;

            //Phone Number
            textBox6.ReadOnly = false;
            //Email
            textBox3.ReadOnly = false;
            //Password
            textBox4.ReadOnly = false;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public Boolean Validation()
        {
            Boolean valid = true;
            //Name check
            if (textBox1.Text.All(char.IsWhiteSpace) || !textBox1.Text.All(char.IsLetter))
            {
                valid = false;
                MessageBox.Show("Please enter a correct name without digits", "Incorrect Name", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //Surame check
            if (textBox8.Text.All(char.IsWhiteSpace) || !textBox8.Text.All(char.IsLetter))
            {
                valid = false;
                MessageBox.Show("Please enter a correct surname without digits", "Incorrect Surname", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //Check Phone Number
            if (textBox6.Text.Length != 10)
            {
                valid = false;
                MessageBox.Show("Phone Number must be 10 digits long", "Incorrect Phone Number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (textBox6.Text.All(char.IsWhiteSpace) || !textBox6.Text.All(char.IsDigit))
            {
                valid = false;
                MessageBox.Show("Please enter a correct phone number", "Incorrect Phone Number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (!(textBox6.Text.Substring(0, 1) == "0"))
            {
                valid = false;
                MessageBox.Show("Your phone number should start with a 0", "Incorrect Phone Number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //Check email
            if (textBox3.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Please enter an email address", "Missing Email Address", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (!textBox3.Text.Contains("@"))
            {
                valid = false;
                MessageBox.Show("Email address must have an @ ", "Incorrect Email Address", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //check email correspondence
            if (textBox5.Text != textBox3.Text)
            {
                valid = false;
                MessageBox.Show("Email Addresses should match", "Email Addresses don't match", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //Check password
            if (textBox4.Text.Length < 8)
            {
                valid = false;
                MessageBox.Show("Password must be 8 characters or longer", "Incorrect password", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (textBox4.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Please enter a password", "Missing password", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            //check password correspondence
            if (textBox4.Text != textBox9.Text)
            {
                valid = false;
                MessageBox.Show("Passwords should match", "Passwords don't match", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }



            return valid;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //LOGIN info coming here
                    string query = "update Handyman SET HandymanName = @name , HandymanSurname =  @surname,  HandymanEmailAddress = @eaddress," +
                        "HandymanPhoneNumber = @pno, HandymanPassword = @password where  @hid=Handyman_id";



                    command.CommandText = query;

                    //String haddress = textBox7.Text + "," + textBox2.Text;
                    command.Parameters.AddWithValue("@name", textBox1.Text);
                    command.Parameters.AddWithValue("@surname", textBox8.Text);
                    //command.Parameters.AddWithValue("@haddress", haddress);
                    command.Parameters.AddWithValue("@eaddress", textBox3.Text);
                    command.Parameters.AddWithValue("@pno", textBox6.Text);
                    command.Parameters.AddWithValue("@password", textBox9.Text);
                    if (Manage_users.hclicked == true)
                    {
                        command.Parameters.AddWithValue("@hid", ids[comboBox1.SelectedIndex]);
                    }
                    else
                    {
                        string hid = Form1.id;
                        command.Parameters.AddWithValue("@hid", hid);
                    }

                    command.ExecuteNonQuery();
                    connection.Close();

                    if (Manage_users.hclicked != true)
                    {
                        MessageBox.Show("Profile successfully updated");
                        this.Hide();
                        HandymanHomepage hh = new HandymanHomepage();
                        hh.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Profile successfully updated");
                        //verify email
                        label3.Hide();
                        textBox5.Hide();
                        //verify password
                        label10.Hide();
                        textBox9.Hide();
                        //save button
                        button4.Hide();

                        //show passwords
                        label6.Hide();
                        label7.Hide();

                        //Customer Name
                        textBox1.Text = "";
                        //surname
                        textBox8.Text = "";
                      
                        //Phone Number
                        textBox6.Text = "";
                        //Email
                        textBox3.Text = "";

                        //Password
                        textBox4.Text = "";
                        comboBox1.SelectedIndex = -1;

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
            
            label1.Text = textBox1.Text + "'s Profile";
        }

        private void HandymanProfile_Load(object sender, EventArgs e)
        {
            //verify email
            label3.Hide();
            textBox5.Hide();
            //verify password
            label10.Hide();
            textBox9.Hide();
            //save button
            button4.Hide();

            //show passwords
            label6.Hide();
            label7.Hide();

            //Admin choosing
            button2.Hide();
            comboBox1.Hide();
            label11.Hide();

            //Admin coming through
            if (Manage_users.hclicked == true)
            {
                button2.Show();
                comboBox1.Show();
                label11.Show();
                button1.Hide();
            }

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                string query = "";
                if (Manage_users.hclicked == true)
                {
                    query = "Select HandymanName, HandymanSurname, HandymanEmailAddress," +
                        "HandymanPhoneNumber, HandymanPassword, Handyman_id from Handyman";
                    command.CommandText = query;

                }
                else
                {
                    query = "Select HandymanName, HandymanSurname, HandymanEmailAddress," +
                        "HandymanPhoneNumber, HandymanPassword from Handyman where" +
                    "@hid=Handyman_id";


                    command.CommandText = query;
                    string hid = Form1.id;
                    command.Parameters.AddWithValue("@hid", hid);
                }

                //Filling the datagrid
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;

                connection.Close();






                if (Manage_users.hclicked != true)
                {
                    //Name
                    textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    //surname
                    textBox8.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();

                    //Phone Number
                    textBox6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    //Email
                    textBox3.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    textBox5.Hide();
                    //Password
                    textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                    textBox9.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                    textBox9.Hide();
                }
                else
                {

                    //Populating the combobox if admin is the one coming through
                    comboBox1.Items.Clear();
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        comboBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + " " + dataGridView1.Rows[i].Cells[1].Value.ToString());
                        ids.Add(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            

            //Name
            textBox1.ReadOnly = true;
            //surname
            textBox8.ReadOnly = true;
            
            //Phone Number
            textBox6.ReadOnly = true;
            //Email
            textBox3.ReadOnly = true;
            textBox5.Hide();
            //Password
            textBox4.ReadOnly = true;
            textBox9.Hide();

            label1.Text = textBox1.Text + "'s Profile";
            if (Manage_users.hclicked==true)
            {
                button1.Hide();
            }
            

        }

        private async void label6_Click(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '\0';
            await Task.Delay(TimeSpan.FromSeconds(2));
            textBox4.PasswordChar = '*';
        }

        private async void label7_Click(object sender, EventArgs e)
        {
            textBox9.PasswordChar = '\0';
            await Task.Delay(TimeSpan.FromSeconds(2));
            textBox9.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a handyman");
            }
            else
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    string query = "Select HandymanName, HandymanSurname, HandymanEmailAddress," +
                            "HandymanPhoneNumber, HandymanPassword from Handyman where" +
                        "@hid=Handyman_id";


                    command.CommandText = query;

                    command.Parameters.AddWithValue("@hid", ids[comboBox1.SelectedIndex]);


                    //Filling the datagrid
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = dt;

                    connection.Close();



                    //Populating the textboxes when customer name is chosen
                    //Name
                    textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    //surname
                    textBox8.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();

                    //Phone Number
                    textBox6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    //Email
                    textBox3.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    textBox5.Hide();
                    //Password
                    textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                    textBox9.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                    textBox9.Hide();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }


                //Name
                textBox1.ReadOnly = true;
                //surname
                textBox8.ReadOnly = true;

                //Phone Number
                textBox6.ReadOnly = true;
                //Email
                textBox3.ReadOnly = true;
                label3.Hide();
                textBox5.Hide();
                //Password
                textBox4.ReadOnly = true;
                textBox9.Hide();
                label10.Hide();
                //show passwords
                label6.Hide();
                label7.Hide();

                //edit button
                button1.Hide();
                button4.Hide();

                label1.Text = textBox1.Text + "'s Profile";

                button1.Show();
            }
        }
    }
}
