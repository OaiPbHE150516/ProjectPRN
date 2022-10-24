using MoneyWife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyWife
{
    public partial class Main : Form
    {
        private User user;
        private bool expenseMoney = false;
        private bool incomeMoney = false;
        MoneyWifeContext context = new MoneyWifeContext();

        public Main()
        {
            InitializeComponent();
        }

        public Main(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //load money of user
            Money? money = context.Money.FirstOrDefault(m => m.UserId == user.Id);
            if (money != null)
            {
                btnCash.ButtonText = convertVND(money.Cash.ToString());
                btnBank.ButtonText = convertVND(money.Bank.ToString());
                btnTotal.ButtonText = convertVND((money.Cash + money.Bank).ToString());
            }
            //load bnfDatePicker with current date
            bnfDatePickerIncome.Value = DateTime.Now;
            bnfDatePickerExpense.Value = DateTime.Now;
            //
            offBtn();
        }

        private void offBtn()
        {
            btn3soIncome.Visible = false;
            btn4soIncome.Visible = false;
            btn5soIncome.Visible = false;
            btn6soIncome.Visible = false;

            btn3soExpense.Visible = false;
            btn4soExpense.Visible = false;
            btn5soExpense.Visible = false;
            btn6soExpense.Visible = false;
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
        
        //nav button
        private void btnReport_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 0;
            //disable this button
            btnReport.Enabled = false;
            //enable other buttons
            btnIncome.Enabled = true;
            btnExpense.Enabled = true;
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 1;
            //disable this button
            btnIncome.Enabled = false;
            //enable other buttons
            btnReport.Enabled = true;
            btnExpense.Enabled = true;
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 2;
            //disable this button
            btnExpense.Enabled = false;
            //enable other buttons
            btnReport.Enabled = true;
            btnIncome.Enabled = true;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //end nav button

        //page expense
        private void resetBtnExpense()
        {
            btn3soExpense.ButtonText = ".000 VND";
            btn4soExpense.ButtonText = "0.000 VND";
            btn5soExpense.ButtonText = "00.000 VND";
            btn6soExpense.ButtonText = "000.000 VND";
        }

        private void setExpense(string v)
        {
            string expense = convertVND(v);
            txtMoneyExpense.Text = expense;
        }

        private void changeBtnExpense()
        {
            if (expenseMoney == true)
            {
                btn3soExpense.ButtonText = convertVND(txtMoneyExpense.Text + "000");
                btn4soExpense.ButtonText = convertVND(txtMoneyExpense.Text + "0000");
                btn5soExpense.ButtonText = convertVND(txtMoneyExpense.Text + "00000");
                btn6soExpense.ButtonText = convertVND(txtMoneyExpense.Text + "000000");
            }
        }
        private void btn3soExpense_Click(object sender, EventArgs e)
        {
            if (expenseMoney == true)
            {
                string expense = txtMoneyExpense.Text;
                setExpense(expense + "000");
                inVisibleBtnSoExpense();
                resetBtnExpense();
                expenseMoney = false;
            }
        }

        private void btn4soExpense_Click(object sender, EventArgs e)
        {
            if (expenseMoney == true)
            {
                string expense = txtMoneyExpense.Text;
                setExpense(expense + "0000");
                inVisibleBtnSoExpense();
                resetBtnExpense();
                expenseMoney = false;
            }
        }

        private void btn5soExpense_Click(object sender, EventArgs e)
        {
            if (expenseMoney == true)
            {
                string expense = txtMoneyExpense.Text;
                setExpense(expense + "00000");
                inVisibleBtnSoExpense();
                resetBtnExpense();
                expenseMoney = false;
            }
        }

        private void btn6soExpense_Click(object sender, EventArgs e)
        {
            if (expenseMoney == true)
            {
                string expense = txtMoneyExpense.Text;
                setExpense(expense + "000000");
                inVisibleBtnSoExpense();
                resetBtnExpense();
                expenseMoney = false;
            }
        }

        private void txtMoneyExpense_TextChange(object sender, EventArgs e)
        {
            changeBtnExpense();
        }

        private void txtMoneyExpense_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                expenseMoney = true;
            }
        }

        private void txtMoneyExpense_Enter(object sender, EventArgs e)
        {
            btn3soExpense.Visible = true;
            btn4soExpense.Visible = true;
            btn5soExpense.Visible = true;
            btn6soExpense.Visible = true;
        }

        private void inVisibleBtnSoExpense()
        {
            btn3soExpense.Visible = false;
            btn4soExpense.Visible = false;
            btn5soExpense.Visible = false;
            btn6soExpense.Visible = false;
        }

        //end page expense

        //page income
        private void txtMoneyIncome_TextChange(object sender, EventArgs e)
        {
            changeBtnIncome();

        }

        private void changeBtnIncome()
        {
            if (incomeMoney == true)
            {
                btn3soIncome.ButtonText = convertVND(txtMoneyIncome.Text + "000");
                btn4soIncome.ButtonText = convertVND(txtMoneyIncome.Text + "0000");
                btn5soIncome.ButtonText = convertVND(txtMoneyIncome.Text + "00000");
                btn6soIncome.ButtonText = convertVND(txtMoneyIncome.Text + "000000");
            }
        }

        private void txtMoneyIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                incomeMoney = true;
            }

        }

        private void btn3soIncome_Click(object sender, EventArgs e)
        {
            if (incomeMoney == true)
            {
                string income = txtMoneyIncome.Text;
                setIncome(income + "000");
                inVisibleBtnSoIncome();
                resetBtnIncome();
                incomeMoney = false;
            }

        }

        private void resetBtnIncome()
        {
            btn3soIncome.ButtonText = ".000 VND";
            btn4soIncome.ButtonText = "0.000 VND";
            btn5soIncome.ButtonText = "00.000 VND";
            btn6soIncome.ButtonText = "000.000 VND";
        }

        private void setIncome(string v)
        {
            string income = convertVND(v);
            txtMoneyIncome.Text = income;
        }

        private void btn4soIncome_Click(object sender, EventArgs e)
        {
            if (incomeMoney == true)
            {
                string income = txtMoneyIncome.Text;
                setIncome(income + "0000");
                inVisibleBtnSoIncome();
                resetBtnIncome();
                incomeMoney = false;
            }

        }

        private void btn5soIncome_Click(object sender, EventArgs e)
        {
            if (incomeMoney == true)
            {
                string income = txtMoneyIncome.Text;
                setIncome(income + "00000");
                inVisibleBtnSoIncome();
                resetBtnIncome();
                incomeMoney = false;
            }

        }

        private void btn6soIncome_Click(object sender, EventArgs e)
        {
            if (incomeMoney == true)
            {
                string income = txtMoneyIncome.Text;
                setIncome(income + "000000");
                inVisibleBtnSoIncome();
                resetBtnIncome();
                incomeMoney = false;
            }

        }

        private void btnCashIncome_Click(object sender, EventArgs e)
        {
            //disable this button
            btnCashIncome.Enabled = false;
            //enable other buttons
            if (btnBankIncome.Enabled == false)
            {
                btnBankIncome.Enabled = true;
            }
        }

        private void btnBankIncome_Click(object sender, EventArgs e)
        {
            //disable this button
            btnBankIncome.Enabled = false;
            //enable other buttons
            if (btnCashIncome.Enabled == false)
            {
                btnCashIncome.Enabled = true;
            }
        }

        private void txtMoneyIncome_Enter(object sender, EventArgs e)
        {
            btn3soIncome.Visible = true;
            btn4soIncome.Visible = true;
            btn5soIncome.Visible = true;
            btn6soIncome.Visible = true;
        }

        private void inVisibleBtnSoIncome()
        {
            btn3soIncome.Visible = false;
            btn4soIncome.Visible = false;
            btn5soIncome.Visible = false;
            btn6soIncome.Visible = false;
        }

        private void btnMeCho_Click(object sender, EventArgs e)
        {
          
        }

        private void btnTienLuong_Click(object sender, EventArgs e)
        {

        }

        private void btnTienThuong_Click(object sender, EventArgs e)
        {

        }

        private void btnDauTu_Click(object sender, EventArgs e)
        {

        }

        private void btnLoiNhuan_Click(object sender, EventArgs e)
        {

        }

        private void btnThuLai_Click(object sender, EventArgs e)
        {

        }

        private void btnChoThue_Click(object sender, EventArgs e)
        {

        }

        private void btnStock_Click(object sender, EventArgs e)
        {

        }


        //end page income
    }
}
