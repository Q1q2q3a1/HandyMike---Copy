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
using HandyMike.Properties;

namespace HandyMike
{
    public partial class RequestService : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public static Boolean requestclicked = false;
        bool descclicked = false;
        public static string scid = Form1.id;
        public RequestService()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = HandyMike.accdb; Persist Security Info = False; ";
        }
    

        private void RequestService_Load(object sender, EventArgs e)
        {
            richTextBox1.Hide();
            button2.Hide();
            label12.Hide();
            dateTimePicker1.Value = DateTime.Now;

            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation()== true){
                Hiding();
                label12.Show();
                richTextBox1.Show();
                button2.Show();
            }
            descclicked = true;
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(richTextBox1.Text.All(char.IsWhiteSpace)))
            {
                
                //database insert for request service
                try
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        //LOGIN info coming here
                        string query = "insert into Service(ServiceName_id, ServiceStatus, RequestServiceStatus, RequestDate, ResidenceType," +
                            "StreetAddress, Suburb, PostalCode, ServiceDescription) " +
                            "values (@servicename, 'Pending', 'Pending', @schedueleddate, @ResidenceType," +
                            "@StreetAddress,@Suburb, @PostalCode, @ServiceDescription)";


                        command.CommandText = query;

                        int Valu = 0;
                        if (checkBox1.Checked)
                        {
                            Valu = 1;
                        }

                        if (checkBox2.Checked)
                        {
                            Valu = 2;
                        }
                        if (checkBox3.Checked)
                        {
                            Valu = 3;

                        }
                        if (checkBox4.Checked)
                        {
                            Valu = 4;
                        }
                        
                        command.Parameters.AddWithValue("@servicename", Valu);
                        String date = dateTimePicker1.Value.ToString() + " " + dateTimePicker2.Value.ToString();
                        string[] wow = date.Split(' ');
                        command.Parameters.AddWithValue("@schedueleddate", (wow[0] + " " + wow[3]));
                        command.Parameters.AddWithValue("@ResidenceType", comboBox2.Text);
                        command.Parameters.AddWithValue("@StreetAddress", textBox1.Text);
                        command.Parameters.AddWithValue("@Suburb", textBox2.Text);
                        command.Parameters.AddWithValue("@PostalCode", textBox3.Text);
                        command.Parameters.AddWithValue("@ServiceDescription", richTextBox1.Text);

                        command.ExecuteNonQuery();
                        connection.Close();

                        requestclicked = true;

                        //To save outside: requestclicked, scid. So i know what to insert into CustomerService when the app start from the bottom.

                        Settings.Default["RequestClicked"] = requestclicked.ToString();
                        Settings.Default["Customer_id"] = scid;
                        Settings.Default.Save();

                    }
                    else
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        //LOGIN info coming here
                        string query = "insert into Service(ServiceName_id, ServiceStatus, RequestServiceStatus, RequestDate, ResidenceType," +
                            "StreetAddress, Suburb, PostalCode, HouseUnitNumber, BuildingEstateName, ServiceDescription) " +
                            "values (@servicename, 'Pending', 'Pending', @schedueleddate, @ResidenceType," +
                            "@StreetAddress,@Suburb, @PostalCode, @HouseUnitNumber, @BuildingEstateName, @ServiceDescription)";


                        command.CommandText = query;

                        int Valu = 0;
                        if (checkBox1.Checked)
                        {
                            Valu = 1;
                        }

                        if (checkBox2.Checked)
                        {
                            Valu = 2;
                        }
                        if (checkBox3.Checked)
                        {
                            Valu = 3;

                        }
                        if (checkBox4.Checked)
                        {
                            Valu = 4;
                        }
                        
                        command.Parameters.AddWithValue("@servicename", Valu);
                        String date = dateTimePicker1.Value.ToString() + " " + dateTimePicker2.Value.ToString();
                        string[] wow = date.Split(' ');
                        
                        command.Parameters.AddWithValue("@schedueleddate", (wow[0] + " " + wow[3]));
                        command.Parameters.AddWithValue("@ResidenceType", comboBox2.Text);
                        command.Parameters.AddWithValue("@StreetAddress", textBox1.Text);
                        command.Parameters.AddWithValue("@Suburb", textBox2.Text);
                        command.Parameters.AddWithValue("@PostalCode", textBox3.Text);
                        command.Parameters.AddWithValue("@HouseUnitNumber", textBox4.Text);
                        command.Parameters.AddWithValue("@BuildingEstateName", textBox5.Text);
                        command.Parameters.AddWithValue("@ServiceDescription", richTextBox1.Text);

                        command.ExecuteNonQuery();

                        connection.Close();
                        requestclicked = true;

                        //To save outside: requestclicked, scid. So i know what to insert into CustomerService when the app start from the bottom.

                        Settings.Default["RequestClicked"] = requestclicked.ToString();
                        Settings.Default["Customer_id"] = scid;
                        Settings.Default.Save();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }


                MessageBox.Show("Service successfully requested");
                this.Hide();
                CustomerHomepage ch = new CustomerHomepage();
                ch.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Please enter your service description", "Missing service description", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (descclicked != true)
            {
                this.Hide();
                CustomerHomepage ch = new CustomerHomepage();
                ch.ShowDialog();
                this.Close();
            }
            else
            {
                //Show the fields
                label5.Show();
                label6.Show();
                textBox2.Show();
                textBox3.Show();
                label4.Show();
                label11.Show();
                textBox1.Show();
                textBox5.Show();
                label8.Show();
                label7.Show();
                label3.Show();
                textBox4.Show();
                comboBox2.Show();
                groupBox1.Show();
                label2.Show();
                label10.Show();
                label9.Show();
                dateTimePicker1.Show();
                dateTimePicker2.Show();

                button2.Hide();
                button1.Show();

                richTextBox1.Hide();
                label12.Hide();

                descclicked = false;
            }


        }
        public Boolean Validation()
        {
            Boolean valid=true;
            //Checking of datetime pickkers
            

            //Only pick tommorow
            if (dateTimePicker1.Value.Day<=DateTime.Now.Day)
            {
                valid = false;
                MessageBox.Show("Sorry you can only pick a day after today", "Incorrect date", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            //Cannot pick Sunday
            if (dateTimePicker1.Text.Contains("Sunday"))
            {
                valid = false;
                MessageBox.Show("Sorry you cannot pick a Sunday", "Closed Time", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;

            }
            if (dateTimePicker1.Text.Contains("Saturday")) // sat 9-1
            {
                if (!(dateTimePicker2.Value.TimeOfDay > TimeSpan.Parse("09:00") && dateTimePicker2.Value.TimeOfDay < TimeSpan.Parse("13:00")))
                {
                    valid = false;
                    MessageBox.Show("Sorry you can only pick a time between 9am and 1pm on a Saturday", "Closed Time", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    return valid;
                }
            }
            else //during the week times, 8-5
            {
                if (!(dateTimePicker2.Value.TimeOfDay > TimeSpan.Parse("08:00") && dateTimePicker2.Value.TimeOfDay < TimeSpan.Parse("17:00")))
                {
                    valid = false;
                    MessageBox.Show("Sorry you can only pick a time between 8am and 5pm on a weekday", "Closed Time", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    return valid;
                }
            }
            //Service Type Selection
            int n = 0;
            if (checkBox1.Checked)
            {
                n++;
            }

            if (checkBox2.Checked)
            {
                n++;
            }
            if (checkBox3.Checked)
            {
                n++;

            }               
            if (checkBox4.Checked)
            {
                n++;
            }
            if (n > 1)
            {
                valid = false;
                MessageBox.Show("Please select only one service", "Incorrect service", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            else if(n<1)
            {
                valid = false;
                MessageBox.Show("Please select a service", "Missing service", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            //Building type selection
            if (comboBox2.SelectedIndex == -1)
            {
                valid = false;
                MessageBox.Show("Please select a residence type", "Missing residence type", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            // Street address checking
            if (textBox1.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Please enter a street address", "Missing street adrress", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            // Surbub check
            if (textBox2.Text.All(char.IsWhiteSpace))
            {
                valid = false;
                MessageBox.Show("Please enter a surburb", "Missing residence type", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            //Postal code check
            if (textBox3.Text.All(char.IsWhiteSpace) || !textBox3.Text.All(char.IsDigit))
            {
                valid = false;
                MessageBox.Show("Please enter a correct postal code", "Incorrect Postal Code", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            if (textBox3.Text.Length != 4)
            {
                valid = false;
                MessageBox.Show("Postal code must be 4 digits long", "Incorrect Postal Code", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return valid;
            }
            //Building type
            if (comboBox2.Text == "Complex Building" || comboBox2.Text == "Estate")
            {
                //House number
                if (textBox4.Text.All(char.IsWhiteSpace) || !textBox4.Text.All(char.IsDigit))
                {
                    valid = false;
                    MessageBox.Show("Please enter a correct house/unit number", "Incorrect house/unit number", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    return valid;
                }
                //buliding name
                if (textBox5.Text.All(char.IsWhiteSpace))
                {
                        valid = false;
                        MessageBox.Show("Please enter a Building/Estate name", "Missing Building/Estate name", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        return valid;
                }
            }
                
            
            return valid;
        }
        public void Hiding(){
            //Hiding the fields
            label5.Hide();
            label6.Hide();
            textBox2.Hide();
            textBox3.Hide();
            label4.Hide();
            label11.Hide();
            textBox1.Hide();
            textBox5.Hide();
            label8.Hide();
            label7.Hide();
            label3.Hide();
            textBox4.Hide();
            comboBox2.Hide();
            groupBox1.Hide();
            label2.Hide();
            label10.Hide();
            label9.Hide();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            button1.Hide();

        }
    }
}
