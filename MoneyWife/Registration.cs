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

namespace MoneyWife
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        MoneyWifeContext context = new MoneyWifeContext();


        private void btnRegister_Click(object sender, EventArgs e)
        {
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
            //Add new user to database and notification if add new user successfully
            if (context.Users.Add(newUser) != null)
            {
                context.SaveChanges();
                MessageBox.Show("Register successfully");
                ////open setupyourmoney form and close this form
                //SetupMoneyWife setup = new SetupMoneyWife();
                //setup.Show();
                //this.Hide();
                ////open login form with username parameter and close register form
                //Password login = new Password(username);
                //login.Show();
                //this.Hide();
            }
            else
            {
                MessageBox.Show("Register failed");
            }
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
