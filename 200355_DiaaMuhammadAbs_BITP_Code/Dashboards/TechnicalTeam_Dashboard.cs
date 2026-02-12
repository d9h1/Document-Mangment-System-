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
    public partial class TechnicalTeam_Dashboard : Form
    {
        public TechnicalTeam_Dashboard()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            MessageBox.Show("Logout Succeeded.");
            login.Show();
        }

        private void TechnicalTeam_Dashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Submittals' table. You can move, or remove it, as needed.
            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            string Status = Status_ComboBox.Text;
            string Cloud_link = CloudLink_TextBox.Text;

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
                    string sql = "UPDATE Submittals SET Status = @Status, Cloud_link = @Cloud_link WHERE Submittals_ID = @Submittals_ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Submittals_ID", Submittals_ID);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Reload all data into the DataGridView
            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);

            // Clear the search textbox
            Search_TextBox.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Reload all data into the DataGridView
            this.submittalsTableAdapter.Fill(this.dataSet1.Submittals);

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
                string sql = "SELECT * FROM Submittals";

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Add a WHERE clause to filter by keyword
                    sql += $" WHERE Submittals_Name LIKE '%{keyword}%' OR Project_Name LIKE '%{keyword}%' OR Submittals_type LIKE '%{keyword}%' OR Submittals_ref LIKE '%{keyword}%' OR Submittals_rev LIKE '%{keyword}%' OR Status LIKE '%{keyword}%' OR Cloud_link LIKE '%{keyword}%'";
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Submittals");

                // Bind the DataGridView to the DataSet
                dataGridView1.DataSource = dataSet.Tables["Submittals"];
            }
        }
    }
}
