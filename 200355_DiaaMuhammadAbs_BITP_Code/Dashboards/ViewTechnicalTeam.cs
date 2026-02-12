using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class ViewTechnicalTeam : Form
    {
        private string Username;

        public ViewTechnicalTeam()
        {
            InitializeComponent();
        }

        private void ViewTechnicalTeam_Load(object sender, EventArgs e)
        {
            try
            {
                // Clear existing data in the DataSet
                this.dataSet1.User.Clear();
                string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
                // Fill the DataSet with users having the "Technical Team" role
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [User] WHERE [Role] = @Role";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Role", "Technical Team");
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(this.dataSet1.User);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
