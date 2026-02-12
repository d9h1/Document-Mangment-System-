using _200355_DiaaMuhammadAbs_BITP_Code.Dashboards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _200355_DiaaMuhammadAbs_BITP_Code
{
    public partial class Admin_Dashboard : Form
    {
        private AddEditeProject addUpdateProject;
        private EditeUsers addDeleteUpdateUser;
        private CreateAccount createaccount;

        public Admin_Dashboard()
        {
            InitializeComponent();
            addUpdateProject = new AddEditeProject();
            addDeleteUpdateUser = new EditeUsers();
            createaccount = new CreateAccount();


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
            foreach (Form form in new Form[] { addUpdateProject })
            {
                if (form != formToShow)
                    form.Hide();
            }
            // Hide other forms
            foreach (Form form in new Form[] { addDeleteUpdateUser })
            {
                if (form != formToShow)
                    form.Hide();
            }
            foreach (Form form in new Form[] { createaccount })
            {
                if (form != formToShow)
                    form.Hide();
            }
        }
        private void InitializeForms()
        {
            // Set properties for each form
            SetFormProperties(addUpdateProject);


            // Add forms to the panel
            panel2.Controls.Add(addUpdateProject);

            // Set properties for each form
            SetFormProperties(addDeleteUpdateUser);

            // Add forms to the panel
            panel2.Controls.Add(addDeleteUpdateUser);

            // Set properties for each form
            SetFormProperties(createaccount);

            // Add forms to the panel
            panel2.Controls.Add(createaccount);

            // Show the initial form in the panel
            createaccount.Show();
        }



        private void button6_Click(object sender, EventArgs e)
        {
            ShowForm(addUpdateProject);
        }
        private void user_button_Click(object sender, EventArgs e)
        {
            ShowForm(addDeleteUpdateUser);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            MessageBox.Show("Logout Succeeded.");
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(createaccount);
        }
    }
}
