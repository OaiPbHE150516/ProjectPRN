using MoneyWife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MoneyWife
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        public Registration(User newUser)
        {
            this.newUser = newUser;
            InitializeComponent();
            txtName.Text = newUser.Username;
            txtPassword.Text = newUser.Password;
            txtConfirmPassword.Text = newUser.Password;
            txtFullname.Text = newUser.Fullname;
            if (newUser.Phone == "")
            {
                txtPhoneEmail.Text = newUser.Email;
            }
            else
            {
                txtPhoneEmail.Text = newUser.Phone;
            }
            //if gender of newUser is male then radMale is checked
            switch (newUser.Gender)
            {
                case "male":
                    radMale.Checked = true;
                    break;
                case "female":
                    radFemale.Checked = true;
                    break;
                case "other":
                    radOther.Checked = true;
                    break;
            }
            txtLocation.Text = newUser.Location;
        }

        MoneyWifeContext context = new MoneyWifeContext();
        private User newUser;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //disable this button
            btnRegister.Enabled = false;
            //get text from txtUsername, txtPassword, txtConfirmPassword, txtFullname, txtPhoneMail, rad gender, txtLocation
            string username = txtName.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string fullname = txtFullname.Text;
            string phoneEmail = txtPhoneEmail.Text;
            string phone = "";
            string email = "";
            string gender = "";
            if (radMale.Checked)
            {
                gender = "male";
            }
            else if (radFemale.Checked)
            {
                gender = "female";
            }
            else
            {
                gender = "other";
            }
            string location = txtLocation.Text;

            //check if username, password, confirm password, fullname is empty
            if (username == "" || password == "" || confirmPassword == "" || fullname == "")
            {
                MessageBox.Show("Username, password, confirm password, fullname is empty");
                return;
            }

            //check if password and confirm password is not match
            if (password != confirmPassword)
            {
                MessageBox.Show("Password and confirm password is not match");
                return;
            }

            //check if username is exist
            User? user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                MessageBox.Show("Username is exist");
                return;
            }

            //check if phoneEmail just have number and don't have '@' then phoneEmail is phone
            if (phoneEmail.All(char.IsDigit) && !phoneEmail.Contains("@"))
            {

                phone = phoneEmail;
            }
            else if (phoneEmail.Contains("@"))
            {
                email = phoneEmail;
            }
            else
            {
                MessageBox.Show("Phone or email is not valid");
                return;
            }
            // New user
            User newUser = new User()
            {
                Username = username,
                Password = password,
                Fullname = fullname,
                Phone = phone,
                Email = email,
                Gender = gender,
                Location = location
            };
            SetupMoneyWife setup = new SetupMoneyWife(newUser);
            setup.Show();
            this.Hide();
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            //close this form and open login form
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
