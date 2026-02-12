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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class ViewDocuments : Form
    {

        private string Username;

        public ViewDocuments(string Username)
        {
            InitializeComponent();
            this.Username = Username;
            LoadProjects();
        }

        private void LoadProjects()
        {
            string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [Submittals] WHERE Project_Manager = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", Username);

                    // Use a DataTable to store the result of the query
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading projects: " + ex.Message);
            }
        }
    }
}
