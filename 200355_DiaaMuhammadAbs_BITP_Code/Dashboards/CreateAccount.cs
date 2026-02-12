using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
            // Call method to populate the ComboBox when the form loads
            PopulateComboBox();
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {

        }

        private void PopulateComboBox()
        {
            // Connection string to your SQL Server database
            string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";

            // SQL query to select the desired column from your table
            string query = "SELECT Project_Name FROM Project";

            // Create a SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object with the query and connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Create a SqlDataReader to retrieve data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Clear existing items in the ComboBox
                            guna2ComboBox2.Items.Clear();

                            // Loop through the result set
                            while (reader.Read())
                            {
                                // Add each value to the ComboBox
                                guna2ComboBox2.Items.Add(reader.GetString(0)); // Assuming column index 0
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Database connection
            string connection = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
            // Retrieve signup details from form fields
            string Username = username_textbox.Text;
            string Password = password_textbox.Text;
            string Role = guna2ComboBox1.Text;
            string Project_Name = guna2ComboBox2.Text;
            string Position = Position_TextBox.Text;
            string Confirm_password = ConfirmTextBox.Text;
            // INSERT query for the client table
            string query = $"INSERT INTO User VALUES ('{Username}', '{Password}','{Role}','{Project_Name}','{Position}')";
            // Nullity if statement
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Please set a new username");
            }
            // password length if statement
            else if (Password.Length < 8 || Password.Length > 12)
            {
                MessageBox.Show("Password Must be between 8 and 12");
            }
            // check if the Password and Confirm_password are the same if statement
            else if (Password != Confirm_password)
            {
                MessageBox.Show("Passwords dont match");

            }
            // check if the password has one uppercase and one lowercase char
            else if (!Password.Any(Char.IsUpper) || !Password.Any(Char.IsLower))
            {
                MessageBox.Show("Password must have 1 Uppercase and Lowercase Letter");
            }
            // Nullity if statement
            else if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Confirm_password))
            {
                MessageBox.Show("Please set a new password");
            }
            else if (string.IsNullOrEmpty(Role))
            {
                MessageBox.Show("Please select account role");
            }
            else if (string.IsNullOrEmpty(Project_Name))
            {
                MessageBox.Show("Please select the project name");
            }
            else if (string.IsNullOrEmpty(Position))
            {
                MessageBox.Show("Please write the position");
            }
            else
            {

                // Check if the username already exists
                if (IsUsernameUnique(Username))
                {
                    // Username is unique, proceed with the insertion
                    try
                    {
                        SqlConnection c1 = new SqlConnection(connection);
                        c1.Open();

                        // Execute the insertion query
                        SqlCommand c2 = new SqlCommand(query, c1);
                        c2.ExecuteNonQuery();

                        // Close the connection
                        c1.Close();
                        MessageBox.Show("Saved");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    // Username already exists, show an error message
                    MessageBox.Show("Username already exists. Please choose a different username.");
                }
            }
        }

        private bool IsUsernameUnique(string Username)
        {
            // Database connection
            string connection = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
            // SELECT query from client and freelancer database table
            string query = $"SELECT COUNT(*) FROM User WHERE Username = '{Username}'";
            try
            {
                using (SqlConnection c3 = new SqlConnection(connection))
                {
                    c3.Open();

                    using (SqlCommand command1 = new SqlCommand(query, c3))
                    {
                        int count1 = (int)command1.ExecuteScalar();
                        return count1 == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void ShowPassword_checkBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (ShowPassword_checkBox.Checked)
            {
                //when the checkbox is checked the password will appear
                password_textbox.UseSystemPasswordChar = false;
                ConfirmTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                //when the checkbox is not checked the password will not appear
                password_textbox.UseSystemPasswordChar = true;
                ConfirmTextBox.UseSystemPasswordChar = true;
            }
        }
    }
}