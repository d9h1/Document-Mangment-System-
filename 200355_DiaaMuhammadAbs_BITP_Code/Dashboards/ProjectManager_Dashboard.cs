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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class ProjectManager_Dashboard : Form
    {
        private ViewDocuments Documents;
        private ViewTechnicalTeam TechnicalTeam;

        public ProjectManager_Dashboard(string Username)
        {
            InitializeComponent();

            // Initialize Documents before using it
            Documents = new ViewDocuments(Username);

            // Initialize TechnicalTeam
            TechnicalTeam = new ViewTechnicalTeam();

            // Initialize forms after they're instantiated
            InitializeForms();

        }

        private void SetFormProperties(Form form)
        {
            form.TopLevel = false;
            form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Hide();
        }

        private void ShowForm(Form formToShow)
        {
            formToShow.Show();
            formToShow.BringToFront();

            // Hide other forms
            foreach (Form form in new Form[] { Documents })
            {
                if (form != formToShow)
                    form.Hide();
            }
            // Hide other forms
            foreach (Form form in new Form[] { TechnicalTeam })
            {
                if (form != formToShow)
                    form.Hide();
            }
        }
        private void InitializeForms()
        {
            // Set properties for each form
            SetFormProperties(Documents);


            // Add forms to the panel
            panel2.Controls.Add(Documents);

            // Set properties for each form
            SetFormProperties(TechnicalTeam);

            // Add forms to the panel
            panel2.Controls.Add(TechnicalTeam);


            // Show the initial form in the panel
            Documents.Show();
        }
        private void ProjectManager_Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void Documents_button_Click(object sender, EventArgs e)
        {
            ShowForm(Documents);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowForm (TechnicalTeam);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            MessageBox.Show("Logout Succeeded.");
            login.Show();
        }
    }
}
