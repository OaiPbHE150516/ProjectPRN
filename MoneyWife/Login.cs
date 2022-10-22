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

        MoneyWifeContext context = new MoneyWifeContext();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //get text from txtUsername and txtPassword
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            //check if username and password is empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Username or password is empty");
                return;
            }
            //check if username and password is correct
            User? user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("Username or password is incorrect");
                return;
            }
            //if username and password is correct, message box show "Login successfully"
            MessageBox.Show("Login successfully");
        }

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
    }
}
