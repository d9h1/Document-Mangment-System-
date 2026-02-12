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
    public partial class AddEditeProject : Form
    {
        public AddEditeProject()
        {
            InitializeComponent();
        }

        private void AddUpdateProject_Dashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Project' table. You can move, or remove it, as needed.
            this.projectTableAdapter.Fill(this.dataSet1.Project);

        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            // Check if a project is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a project to update.");
                return;
            }

            // Get the ID of the selected project
            int Project_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["projectIDDataGridViewTextBoxColumn"].Value);

            // Get updated values from textboxes
            string Project_Name = ProjectName_textbox.Text;
            string Client = Client_TextBox.Text;
            string Main_Controctor = MainControctor_TextBox.Text;
            string Document_Controller = DocumentscTextBox1.Text;
            string Project_Manager = ProjectManagerName_TextBox.Text;
            string Project_Duration = ProjectDuration_TextBox.Text;
            string Start_Date = StartDatePicker1.Text;
            string End_Date = EndDatePicker.Text;

            // Validate the inputs
            if (string.IsNullOrEmpty(Project_Name) || string.IsNullOrEmpty(Client) || string.IsNullOrEmpty(Main_Controctor) ||
                string.IsNullOrEmpty(Document_Controller) || string.IsNullOrEmpty(Project_Manager) || string.IsNullOrEmpty(Project_Duration) || string.IsNullOrEmpty(Start_Date) || string.IsNullOrEmpty(End_Date))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            // Confirm with the user before updating
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this Project?", "Confirm Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Create a connection to the database
                string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL UPDATE query
                    string sql = "UPDATE [Project] SET Project_Name = @Project_Name, Client = @Client, " +
                                 "Main_Controctor = @Main_Controctor, Document_Controller = @Document_Controller, " +
                                 "Project_Manager = @Project_Manager, Project_Duration = @Project_Duration, " +
                                 "Start_Date = @Start_Date, End_Date = @End_Date WHERE Project_ID = @Project_ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Project_ID", Project_ID); // Add this line
                        command.Parameters.AddWithValue("@Project_Name", Project_Name);
                        command.Parameters.AddWithValue("@Client", Client);
                        command.Parameters.AddWithValue("@Main_Controctor", Main_Controctor);
                        command.Parameters.AddWithValue("@Document_Controller", Document_Controller);
                        command.Parameters.AddWithValue("@Project_Manager", Project_Manager);
                        command.Parameters.AddWithValue("@Project_Duration", Project_Duration);
                        command.Parameters.AddWithValue("@Start_Date", Start_Date);
                        command.Parameters.AddWithValue("@End_Date", End_Date);

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Project updated successfully.");

                            // Refresh the DataGridView
                            this.projectTableAdapter.Fill(this.dataSet1.Project);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update Project.");
                        }
                    }
                }
            }
        
    }
     

        private void Add_Button_Click(object sender, EventArgs e)
        {
            string Project_Name = ProjectName_textbox.Text;
            string Client = Client_TextBox.Text;
            string Main_Controctor = MainControctor_TextBox.Text;
            string Document_Controller = DocumentscTextBox1.Text;
            string Project_Manager = ProjectManagerName_TextBox.Text;
            string Project_Duration = ProjectDuration_TextBox.Text;
            string Start_Date = StartDatePicker1.Text;
            string End_Date = EndDatePicker.Text;

            // Validate Freelancer Name and Evaluation (assuming these are TextBoxes)
            if (string.IsNullOrEmpty(ProjectName_textbox.Text))
            {
                MessageBox.Show("You must fill project Name");
                return;
            }
            else if (string.IsNullOrEmpty(Client_TextBox.Text))
            {
                MessageBox.Show("You must fill client Name");
                return;
            }
            else if (string.IsNullOrEmpty(MainControctor_TextBox.Text))
            {
                MessageBox.Show("You must fill main controctor Name");
                return;
            }
            else if (string.IsNullOrEmpty(DocumentscTextBox1.Text))
            {
                MessageBox.Show("You must fill  document controller");
                return;
            }
            else if (string.IsNullOrEmpty(ProjectManagerName_TextBox.Text))
            {
                MessageBox.Show("You must fill Project manager name");
                return;
            }
            else if (string.IsNullOrEmpty(ProjectDuration_TextBox.Text))
            {
                MessageBox.Show("You must fill project duration");
                return;
            }
            else if (string.IsNullOrEmpty(StartDatePicker1.Text))
            {
                MessageBox.Show("You must choose the start date");
                return;
            }
            else if (string.IsNullOrEmpty(EndDatePicker.Text))
            {
                MessageBox.Show("You must choose the end date");
                return;
            }


            // Create a connection to the database
            string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create an SQL INSERT query
                string sql = "INSERT INTO Project (Project_Name, Client, Main_Controctor, Document_Controller, Project_Manager, Project_Duration, Start_Date, End_Date) " +
                             "VALUES (@Project_Name, @Client, @Main_Controctor, @Document_Controller, @Project_Manager, @Project_Duration, @Start_Date, @End_Date)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Project_Name", Project_Name);
                    command.Parameters.AddWithValue("@Client", Client);
                    command.Parameters.AddWithValue("@Main_Controctor", Main_Controctor);
                    command.Parameters.AddWithValue("@Document_Controller", Document_Controller);
                    command.Parameters.AddWithValue("@Project_Manager", Project_Manager);
                    command.Parameters.AddWithValue("@Project_Duration", Project_Duration);
                    command.Parameters.AddWithValue("@Start_Date", Start_Date);
                    command.Parameters.AddWithValue("@End_Date", End_Date);

                    // Execute the insertion query
                    command.ExecuteNonQuery();
                }

                // Show success message after successful execution
                MessageBox.Show("Your project has been added successfully.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Reload all data into the DataGridView
            this.projectTableAdapter.Fill(this.dataSet1.Project);

            // Clear the search textbox
            Search_TextBox.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Reload all data into the DataGridView
            this.projectTableAdapter.Fill(this.dataSet1.Project);

            // Clear the search textbox
            Search_TextBox.Text = "";
        }

        private void Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            string keyword = Search_TextBox.Text.Trim();

            using (SqlConnection connection = new SqlConnection(@"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;"))
            {
                connection.Open();

                // Create a SQL query to retrieve data
                string sql = "SELECT * FROM Project";

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add a WHERE clause to filter by keyword
                    sql += $" WHERE Project_Name LIKE '%{keyword}%' OR Client LIKE '%{keyword}%' OR Main_Controctor LIKE '%{keyword}%' OR Document_Controller LIKE '%{keyword}%' OR Project_Manager LIKE '%{keyword}%' OR Project_Duration LIKE '%{keyword}%' OR Start_Date LIKE '%{keyword}%' OR End_Date LIKE '%{keyword}%'";
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Project");

                // Bind the DataGridView to the DataSet
                dataGridView1.DataSource = dataSet.Tables["Project"];
            }
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            // Check if a submittal is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Project to delete.");
                return;
            }

            // Get the ID of the selected submittal
            int Project_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["projectIDDataGridViewTextBoxColumn"].Value);

            // Confirm with the user before deleting
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Project?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Create a connection to the database
                string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create an SQL DELETE query
                    string sql = "DELETE FROM Project WHERE Project_ID = @Project_ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Project_ID", Project_ID);

                        // Execute the deletion query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Project deleted successfully.");

                            // Refresh the DataGridView
                            this.projectTableAdapter.Fill(this.dataSet1.Project);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete Project.");
                        }
                    }
                }
            }
        }
    }
}
