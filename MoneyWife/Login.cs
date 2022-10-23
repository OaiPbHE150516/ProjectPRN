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

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    //get text from txtUsername and txtPassword
        //    string username = txtUsername.Text;
        //    string password = txtPassword.Text;
        //    MessageBox.Show("username: " + username + " password: " + password);
        //    //check if username and password is empty
        //    if (username == "" || password == "")
        //    {
        //        MessageBox.Show("Username or password is empty");
        //        return;
        //    }
        //    //check if username and password is correct
        //    User? user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        //    MessageBox.Show("user: " + user.Username);
        //    if (user != null)
        //    {
        //        MessageBox.Show("Login successfully");
        //    }
        //    else
        //    {
        //        //if incorrect then show message
        //        MessageBox.Show("Username or password is incorrect");
        //    }

        //}

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
            //get text from txtUsername and txtPassword
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            MessageBox.Show("username: " + username + " password: " + password);
            //check if username and password is empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Username or password is empty");
                return;
            }
            //check if username and password is correct
            User? user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            MessageBox.Show("user: " + user.Username);
            if (user != null)
            {
                MessageBox.Show("Login successfully");
            }
            else
            {
                //if incorrect then show message
                MessageBox.Show("Username or password is incorrect");
            }
        }
    }
}
