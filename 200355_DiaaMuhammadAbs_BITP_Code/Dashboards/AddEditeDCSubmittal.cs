using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class AddEditeDCSubmittal : Form
    {
        public AddEditeDCSubmittal()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            string Submittals_Name = SubmittalName_textbox.Text;
            string Project_Name = ProjectName_TextBox.Text;
            string Assign_to = AssignTo_TextBox.Text;
            string Project_Manager = ProjectManager_TextBox.Text;
            string Submittals_type = SubmittalType_TextBox.Text;
            string Submittals_ref = Ref_TextBox.Text;
            string Submittals_rev = Rev_TextBox.Text;
            string Status = Status_ComboBox.Text;
            string Cloud_link = CloudLink_TextBox.Text;

            // Validate Freelancer Name and Evaluation (assuming these are TextBoxes)
            if (string.IsNullOrEmpty(SubmittalName_textbox.Text))
            {
                MessageBox.Show("You must fill Submittal Name");
                return;
            }
            else if (string.IsNullOrEmpty(ProjectName_TextBox.Text))
            {
                MessageBox.Show("You must fill Project Name");
                return;
            }
            else if (string.IsNullOrEmpty(AssignTo_TextBox.Text))
            {
                MessageBox.Show("You must fill to it assign");
                return;
            }
            else if (string.IsNullOrEmpty(ProjectManager_TextBox.Text))
            {
                MessageBox.Show("You must fill Project Manager name");
                return;
            }
            else if (string.IsNullOrEmpty(SubmittalType_TextBox.Text))
            {
                MessageBox.Show("You must fill Project Name");
                return;
            }
            else if (string.IsNullOrEmpty(Ref_TextBox.Text))
            {
                MessageBox.Show("You must fill Submittal Reference");
                return;
            }
            else if (string.IsNullOrEmpty(Rev_TextBox.Text))
            {
                MessageBox.Show("You must fill Submittal Rev");
                return;
            }
            else if (string.IsNullOrEmpty(Status_ComboBox.Text))
            {
                MessageBox.Show("You must choose the status");
                return;
            }
            else if (string.IsNullOrEmpty(CloudLink_TextBox.Text))
            {
                MessageBox.Show("You must fill cloud link");
                return;
            }


            // Create a connection to the database
            string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL INSERT query
                string sql = "INSERT INTO Submittals (Submittals_Name, Project_Name, Assign_to, Project_Manager, Submittals_type, Submittals_ref, Submittals_rev, Status, Cloud_link) " +
                             "VALUES (@Submittals_Name, @Project_Name, @Assign_to, @Project_Manager, @Submittals_type, @Submittals_ref, @Submittals_rev, @Status, @Cloud_link)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Submittals_Name", Submittals_Name);
                    command.Parameters.AddWithValue("@Project_Name", Project_Name);
                    command.Parameters.AddWithValue("@Assign_to", Assign_to);
                    command.Parameters.AddWithValue("@Project_Manager", Project_Manager);
                    command.Parameters.AddWithValue("@Submittals_type", Submittals_type);
                    command.Parameters.AddWithValue("@Submittals_ref", Submittals_ref);
                    command.Parameters.AddWithValue("@Submittals_rev", Submittals_rev);
                    command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@Cloud_link", Cloud_link);

                    // Execute the insertion query
                    command.ExecuteNonQuery();
                }

                // Show success message after successful execution
                MessageBox.Show("Your Submittal has been added successfully.");
            }

        }

        private void AddEditeDCSubmittal_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Submittals' table. You can move, or remove it, as needed.
            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);

        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            // Check if a submittal is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a submittal to delete.");
                return;
            }

            // Get the ID of the selected submittal
            int Submittals_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["submittalsIDDataGridViewTextBoxColumn"].Value);

            // Confirm with the user before deleting
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this submittal?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Create a connection to the database
                string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL DELETE query
                    string sql = "DELETE FROM [Submittals] WHERE Submittals_ID = @Submittals_ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Submittals_ID", Submittals_ID);

                        // Execute the deletion query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Submittal deleted successfully.");

                            // Refresh the DataGridView
                            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete submittal.");
                        }
                    }
                }
            }
        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            // Check if a submittal is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a submittal to update.");
                return;
            }

            // Get the ID of the selected submittal
            int Submittals_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["submittalsIDDataGridViewTextBoxColumn"].Value);

            // Get updated values from textboxes
            string Submittals_Name = SubmittalName_textbox.Text;
            string Project_Name = ProjectName_TextBox.Text;
            string Assign_to = AssignTo_TextBox.Text;
            string Project_Manager = ProjectManager_TextBox.Text;
            string Submittals_type = SubmittalType_TextBox.Text;
            string Submittals_ref = Ref_TextBox.Text;
            string Submittals_rev = Rev_TextBox.Text;
            string Status = Status_ComboBox.Text;
            string Cloud_link = CloudLink_TextBox.Text;

            // Validate the inputs
            if (string.IsNullOrEmpty(Submittals_Name) || string.IsNullOrEmpty(Project_Name) || string.IsNullOrEmpty(Assign_to) || string.IsNullOrEmpty(Project_Manager) || string.IsNullOrEmpty(Submittals_type) ||
                string.IsNullOrEmpty(Submittals_ref) || string.IsNullOrEmpty(Submittals_rev))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            // Confirm with the user before updating
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this submittal?", "Confirm Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Create a connection to the database
                string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL UPDATE query
                    string sql = "UPDATE [Submittals] SET Submittals_Name = @Submittals_Name, Project_Name = @Project_Name, Assign_to = @Assign_to" +
                                 "Project_Manager = @Project_Manager, Submittals_type = @Submittals_type, Submittals_ref = @Submittals_ref, Submittals_rev = @Submittals_rev, " +
                                 "Status = @Status, Cloud_link = @Cloud_link WHERE Submittals_ID = @Submittals_ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Submittals_Name", Submittals_Name);
                        command.Parameters.AddWithValue("@Project_Name", Project_Name);
                        command.Parameters.AddWithValue("@Assign_to", Assign_to);
                        command.Parameters.AddWithValue("@Project_Manager", Project_Manager);
                        command.Parameters.AddWithValue("@Submittals_type", Submittals_type);
                        command.Parameters.AddWithValue("@Submittals_ref", Submittals_ref);
                        command.Parameters.AddWithValue("@Submittals_rev", Submittals_rev);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@Cloud_link", Cloud_link);

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Submittal updated successfully.");

                            // Refresh the DataGridView
                            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update submittal.");
                        }
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
                string sql = "SELECT * FROM [Submittals]";

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add a WHERE clause to filter by keyword
                    sql += $" WHERE Submittals_Name LIKE '%{keyword}%' OR Project_Name LIKE '%{keyword}%' OR Assign_to LIKE '%{keyword}%' OR Project_Manager LIKE '%{keyword}%' OR Submittals_ref LIKE '%{keyword}%' OR Submittals_rev LIKE '%{keyword}%' OR Assign_to LIKE '%{keyword}%' OR Status LIKE '%{keyword}%' OR Cloud_link LIKE '%{keyword}%'";
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Submittals");

                // Bind the DataGridView to the DataSet
                dataGridView1.DataSource = dataSet.Tables["Submittals"];
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            // Reload all data into the DataGridView
            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);

            // Clear the search textbox
            Search_TextBox.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Reload all data into the DataGridView
            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);

            // Clear the search textbox
            Search_TextBox.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

