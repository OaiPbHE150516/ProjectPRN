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
using System.Globalization;
using System.Security.Policy;

namespace MoneyWife
{
    public partial class SetupMoneyWife : Form
    {
        public SetupMoneyWife()
        {
            InitializeComponent();
        }

        public SetupMoneyWife(User newUser)
        {
            this.newUser = newUser;
            InitializeComponent();
        }

        MoneyWifeContext context = new MoneyWifeContext();
        private bool cashJustFocused = false;
        private bool bankJustFocused = false;
        private User newUser;
        private void SetupMoneyWife_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private string convertVND(string v)
        {
            // trả về string dưới dạng tiền tệ VNĐ với dấu chấm

            //if v contain " VND" or ".", remove them
            if (v.Contains(" VND"))
            {
                v = v.Replace(" VND", "");
            }
            //if v contain ".", remove it
            if (v.Contains("."))
            {
                v = v.Replace(".", "");
            }
            //if v contain " ", remove it
            if (v.Contains(" "))
            {
                v = v.Replace(" ", "");
            }
            return string.Format(CultureInfo.CreateSpecificCulture("vi-VN"), "{0:#,##0} VND", Convert.ToDecimal(v));
        }
        private string convertDecimal(string raw)
        {
            //remove " VND" and "." from cash
            if (raw.Contains(" VND"))
            {
                raw = raw.Replace(" VND", "");
            }
            if (raw.Contains("."))
            {
                raw = raw.Replace(".", "");
            }
            //try to convert cash to decimal
            try
            {
                return Convert.ToDecimal(raw).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        private void resetBtn()
        {
            btn3so.ButtonText = ".000 VND";
            btn4so.ButtonText = "0.000 VND";
            btn5so.ButtonText = "00.000 VND";
            btn6so.ButtonText = "000.000 VND";
        }
        private void changeBtn()
        {
            if (cashJustFocused == true)
            {
                btn3so.ButtonText = convertVND(txtCash.Text + "000");
                btn4so.ButtonText = convertVND(txtCash.Text + "0000");
                btn5so.ButtonText = convertVND(txtCash.Text + "00000");
                btn6so.ButtonText = convertVND(txtCash.Text + "000000");
            }
            if (bankJustFocused == true)
            {
                btn3so.ButtonText = convertVND(txtBank.Text + "000");
                btn4so.ButtonText = convertVND(txtBank.Text + "0000");
                btn5so.ButtonText = convertVND(txtBank.Text + "00000");
                btn6so.ButtonText = convertVND(txtBank.Text + "000000");
            }
        }

        private void btn3so_Click(object sender, EventArgs e)
        {
            if (cashJustFocused == true)
            {
                string cash = txtCash.Text;
                setCash(cash + "000");
                resetBtn();
                cashJustFocused = false;
            }
            if (bankJustFocused == true)
            {
                string bank = txtBank.Text;
                setBank(bank + "000");
                resetBtn();
                bankJustFocused = false;
            }
        }
        private void btn4so_Click(object sender, EventArgs e)
        {
            if (cashJustFocused == true)
            {
                string cash = txtCash.Text;
                setCash(cash + "0000");
                resetBtn();
                cashJustFocused = false;
            }
            if (bankJustFocused == true)
            {
                string bank = txtBank.Text;
                setBank(bank + "0000");
                resetBtn();
                bankJustFocused = false;
            }
        }

        private void btn5so_Click(object sender, EventArgs e)
        {
            if (cashJustFocused == true)
            {
                string cash = txtCash.Text;
                setCash(cash + "00000");
                resetBtn();
                cashJustFocused = false;
            }
            if (bankJustFocused == true)
            {
                string bank = txtBank.Text;
                setBank(bank + "00000");
                resetBtn();
                bankJustFocused = false;
            }
        }

        private void btn6so_Click(object sender, EventArgs e)
        {
            if (cashJustFocused == true)
            {
                string cash = txtCash.Text;
                setCash(cash + "000000");
                resetBtn();
                cashJustFocused = false;
            }
            if (bankJustFocused == true)
            {
                string bank = txtBank.Text;
                setBank(bank + "000000");
                resetBtn();
                bankJustFocused = false;
            }
        }
        private void btnBackToRegister_Click(object sender, EventArgs e)
        {
            //open Register form with newUser
            Registration registration = new Registration(newUser);
            registration.Show();
            this.Hide();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            //check if cash and bank, both of them are empty
            if (txtCash.Text == "" && txtBank.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số tiền trong ví của mình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //check if cash and bank are valid when them > = 0
            if (Convert.ToDecimal(convertDecimal(txtCash.Text)) < 0 || Convert.ToDecimal(convertDecimal(txtBank.Text)) < 0)
            {
                MessageBox.Show("Số tiền bạn đang có không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Add new user to database and notification if add new user successfully
            if (context.Users.Where(u => u.Username == newUser.Username).Count() == 0)
            {
                if (context.Users.Add(newUser) != null)
                {
                    context.SaveChanges();
                    //get user id by username
                    int userId = context.Users.Where(u => u.Username == newUser.Username).Select(u => u.Id).FirstOrDefault();
                    //add new money to database
                    Money money = new Money();
                    money.UserId = userId;
                    money.Cash = Convert.ToDecimal(convertDecimal(txtCash.Text));
                    money.Bank = Convert.ToDecimal(convertDecimal(txtBank.Text));
                    if (context.Money.Add(money) != null)
                    {
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi lưu thông tin tiền của: " + newUser.Username);
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu thông tin của: " + newUser.Username);
                }
            }
            //return to login form with new user
            Login login = new Login(newUser);
            login.Show();
            this.Hide();
        }
        private void SetupMoneyWife_Load(object sender, EventArgs e)
        {

        }
        //cash begin
        private void setCash(string v)
        {
            string cash = convertVND(v);
            txtCash.Text = cash;
        }

        private void txtCash_TextChange(object sender, EventArgs e)
        {
            changeBtn();
            Decimal total = Convert.ToDecimal(convertDecimal(txtCash.Text)) + Convert.ToDecimal(convertDecimal(txtBank.Text));
            txtTotal.Text = convertVND(total.ToString());
        }
        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                cashJustFocused = true;
            }
        }
        private void txtCash_Enter(object sender, EventArgs e)
        {
            resetBtn();
        }
        private void txtCash_Leave(object sender, EventArgs e)
        {
            resetBtn();
        }
        //cash end

        //bank begin
        private void setBank(string v)
        {
            txtBank.Text = convertVND(v);
        }

        private void txtBank_TextChange(object sender, EventArgs e)
        {
            changeBtn();
            Decimal total = Convert.ToDecimal(convertDecimal(txtCash.Text)) + Convert.ToDecimal(convertDecimal(txtBank.Text));
            txtTotal.Text = convertVND(total.ToString());
        }

        private void txtBank_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                bankJustFocused = true;
            }
        }

        private void txtBank_Enter(object sender, EventArgs e)
        {
            resetBtn();
        }

        private void txtBank_Leave(object sender, EventArgs e)
        {
            resetBtn();
        }
        //bank end
    }
}
