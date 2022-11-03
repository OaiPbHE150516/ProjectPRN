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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public Login(User newUser)
        {
            this.newUser = newUser;
            InitializeComponent();
            txtUsername.Text = newUser.Username;
        }

        MoneyWifeContext context = new MoneyWifeContext();
        private User newUser;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //open register form and close login form
            Registration register = new Registration();
            register.Show();
            this.Hide();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (ckbRememberMe.Checked)
            {
                Properties.Settings.Default.Username = txtUsername.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
            //get text from txtUsername and txtPassword
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            //check if username and password is empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Username or password is empty");
                return;
            }
            //disable login button and show loading
            btnLogin.Enabled = false;
            bnfLoader.Visible = true;
            //check if username and password is correct
            User? user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                //open main form and close login form
                Main main = new Main(user);
                main.Show();
                btnLogin.Enabled = true;
                bnfLoader.Visible = false;
                this.Hide();
            }
            else
            {
                //if incorrect then show message
                MessageBox.Show("Username or password is incorrect");
                btnLogin.Enabled = true;
                bnfLoader.Visible = false;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Username != "")
            {
                txtUsername.Text = Properties.Settings.Default.Username;
                txtPassword.Text = Properties.Settings.Default.Password;
                ckbRememberMe.Checked = true;
            }
            else
            {
                txtUsername.Text = "";
                txtPassword.Text = "";
                ckbRememberMe.Checked = false;
            }
        }
    }
}
