using _200355_DiaaMuhammadAbs_BITP_Code.Dashboards;
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
using System.Windows.Forms;

namespace _200355_DiaaMuhammadAbs_BITP_Code
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void ShowPassword_checkBox_CheckedChanged(object sender, EventArgs e)
        {

            //when the checkbox is checked the password will appear
            

        }
        private void Login_button_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=D9H1;Initial Catalog=Documents_system;Integrated Security=True;Encrypt=False;";
            string Username = username_textbox.Text;
            string Password = password_textbox.Text;

            try
            {
                using (SqlConnection connections = new SqlConnection(connectionString))
                {
                    connections.Open();

                    // Query to get user details
                    string query = "SELECT Role FROM [User] WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connections);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);

                    // Execute the query
                    object roleObj = command.ExecuteScalar();

                    if (roleObj != null) // If user exists
                    {
                        string role = roleObj.ToString();

                        // Redirect based on user role
                        switch (role)
                        {
                            case "Admin":
                                // Redirect to Admin page
                                Admin_Dashboard adminDashboard = new Admin_Dashboard();
                                adminDashboard.Show();
                                this.Hide(); // Hide the login form
                                break;
                            case "Document Controller":
                                // Redirect to Document Controller page
                                DocumentController_Dashboard documentControllerDashboard = new DocumentController_Dashboard();
                                documentControllerDashboard.Show();
                                this.Hide(); // Hide the login form
                                break;
                            case "Technical Team":
                                // Redirect to Technical Team page
                                TechnicalTeam_Dashboard technicalTeamDashboard = new TechnicalTeam_Dashboard();
                                technicalTeamDashboard.Show();
                                this.Hide(); // Hide the login form
                                break;
                            case "Project Manager":
                                // Redirect to Project Manager page
                                ProjectManager_Dashboard projectManagerDashboard = new ProjectManager_Dashboard(Username);
                                projectManagerDashboard.Show();
                                this.Hide(); // Hide the login form
                                break;

                            default:
                                MessageBox.Show("Invalid role!"); // Handle invalid role
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!"); // Handle invalid username or password
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void password_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void ShowPassword_checkBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (ShowPassword_checkBox.Checked)
            {
                //when the checkbox is checked the password will appear
                password_textbox.UseSystemPasswordChar = true;
            }
            else
            {
                //when the checkbox is not checked the password will not appear
            }
        }
    }
}
