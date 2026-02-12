using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class EditeUsers : Form
    {
        public EditeUsers()
        {
            InitializeComponent();
            // Call method to populate the ComboBox when the form loads
            PopulateComboBox();
        }

        private void AddEditeUsers_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.dataSet1.User);

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

        private void Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            string keyword = Search_TextBox.Text.Trim();

            using (SqlConnection connection = new SqlConnection(@"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;"))
            {
                connection.Open();

                // Create a SQL query to retrieve data
                string sql = "SELECT * FROM User";

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add a WHERE clause to filter by keyword
                    sql += $" WHERE Username LIKE '%{keyword}%' OR Password LIKE '%{keyword}%' OR Role LIKE '%{keyword}%' OR Project_Name LIKE '%{keyword}%' OR Position LIKE '%{keyword}%'";
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "User");

                // Bind the DataGridView to the DataSet
                dataGridView1.DataSource = dataSet.Tables["User"];
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            // Check if a user is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            // Get the ID of the selected user
            int User_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["userIDDataGridViewTextBoxColumn"].Value);

            // Get updated values from textboxes
            string Username = username_textbox.Text;
            string Password = password_textbox.Text;
            string Role = guna2ComboBox1.Text;
            string Project_Name = guna2ComboBox2.Text;
            string Position = Position_TextBox.Text;

            // Validate the inputs
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Role) ||
                string.IsNullOrEmpty(Project_Name) || string.IsNullOrEmpty(Position))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            // Confirm with the user before updating
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this user?", "Confirm Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Create a connection to the database
                string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL UPDATE query with a WHERE clause to update by ID
                    string sql = "UPDATE [User] SET Username = @Username, Password = @Password, " +
                                 "Role = @Role, Project_Name = @Project_Name, Position = @Position " +
                                 "WHERE User_ID = @User_ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Role", Role);
                        command.Parameters.AddWithValue("@Project_Name", Project_Name);
                        command.Parameters.AddWithValue("@Position", Position);
                        command.Parameters.AddWithValue("@User_ID", User_ID); // Add User_ID parameter for the WHERE clause

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User updated successfully.");

                            // Refresh the DataGridView
                            this.userTableAdapter.Fill(this.dataSet1.User);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update user.");
                        }
                    }
                }
            }
        }
    }
}
