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
    public partial class SignUp : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public SignUp()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False;";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Manage_users.nclicked == true)
            {
                this.Hide();
                Manage_users mu = new Manage_users();
                mu.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.ShowDialog();
                this.Close();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                    string query = "insert into Customer(Admin_id, CustomerName, CustomerSurname, CustomerHomeAddress, CustomerEmailAddress," +
                        "CustomerPhoneNumber, CustomerPassword, Date_Joined) " +
                        "values (10000000, @name, @surname, @haddress, @eaddress, @pno,@password,@dj)";


                    command.CommandText = query;

                    DateTime Now = DateTime.Now;
                    String haddress = textBox7.Text + "," + textBox2.Text;
                    command.Parameters.AddWithValue("@name", textBox1.Text);
                    command.Parameters.AddWithValue("@surname", textBox8.Text);
                    command.Parameters.AddWithValue("@haddress", haddress);
                    command.Parameters.AddWithValue("@eaddress", textBox3.Text);
                    command.Parameters.AddWithValue("@pno", textBox6.Text);
                    command.Parameters.AddWithValue("@password", textBox9.Text);
                    command.Parameters.AddWithValue("@dj", Now.ToString());

                    command.ExecuteNonQuery();

                    connection.Close();

                    if (Manage_users.nclicked == true)
                    {
                        MessageBox.Show("New customer account successfully created");
                        this.Hide();
                        Manage_users mu = new Manage_users();
                        mu.ShowDialog();
                        this.Close();
                    }
                    else
                    //Start again and login
                    {
                        MessageBox.Show("New account successfully created");
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.ShowDialog();
                        this.Close();
                        //Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
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

            //Check HomeAddress
            if (textBox7.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Please enter your home address", "Missing Home Address", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }

            //Check Postal Code
            if (textBox2.Text.All(char.IsWhiteSpace) || !textBox2.Text.All(char.IsDigit))
            {
                valid = false;
                MessageBox.Show("Please enter a correct postal code", "Incorrect Postal Code", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (textBox2.Text.Length != 4)
            {
                valid = false;
                MessageBox.Show("Postal code must be 4 digits long", "Incorrect Postal Code", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        private async void label12_Click(object sender, EventArgs e)
        {
            textBox9.PasswordChar = '\0';
            await Task.Delay(TimeSpan.FromSeconds(2));
            textBox9.PasswordChar = '*';
        }

        private async void label11_Click(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '\0';
            await Task.Delay(TimeSpan.FromSeconds(2));
            textBox4.PasswordChar = '*';
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
