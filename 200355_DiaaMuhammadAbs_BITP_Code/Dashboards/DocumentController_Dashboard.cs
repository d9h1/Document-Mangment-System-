using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _200355_DiaaMuhammadAbs_BITP_Code.Dashboards
{
    public partial class DocumentController_Dashboard : Form
    {
        private AddEditeDCSubmittal addDeleteUpdateDCSubmittal;
        private ViewTechnicalTeam technicalTeam;
        public DocumentController_Dashboard()
        {
            InitializeComponent();
            addDeleteUpdateDCSubmittal = new AddEditeDCSubmittal();
            technicalTeam = new ViewTechnicalTeam();
            InitializeForms();
        }
       
        private void Document_controllor_dashboard_Load(object sender, EventArgs e)
        {

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
            foreach (Form form in new Form[] { addDeleteUpdateDCSubmittal })
            {
                if (form != formToShow)
                    form.Hide();
            }
            // Hide other forms
            foreach (Form form in new Form[] { technicalTeam })
            {
                if (form != formToShow)
                    form.Hide();
            }
        }
        private void InitializeForms()
        {
            // Set properties for each form
            SetFormProperties(addDeleteUpdateDCSubmittal);


            // Add forms to the panel
            panel3.Controls.Add(addDeleteUpdateDCSubmittal);

            // Set properties for each form
            SetFormProperties(technicalTeam);


            // Add forms to the panel
            panel3.Controls.Add(technicalTeam);


            // Show the initial form in the panel
            addDeleteUpdateDCSubmittal.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ShowForm(addDeleteUpdateDCSubmittal);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowForm(technicalTeam);
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
