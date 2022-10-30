using Bunifu.UI.WinForms.BunifuButton;
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
using Type = MoneyWife.Models.Type;

namespace MoneyWife
{
    public partial class Main : Form
    {
        private User user;
        private bool expenseMoney = false;
        private bool incomeMoney = false;
        MoneyWifeContext context = new MoneyWifeContext();
        List<BunifuButton2> listBtnTypeIncome = new List<BunifuButton2>();
        List<BunifuButton2> listBtnTypeExpense = new List<BunifuButton2>();

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
            loadMoney();
            loadHistoryTransaction();
            //load bnfDatePicker with current date
            bnfDatePickerIncome.Value = DateTime.Now;
            bnfDatePickerExpense.Value = DateTime.Now;
            //
            offBtn();
            //add all income button to list
            listBtnTypeIncome.Add(btnMeCho);
            listBtnTypeIncome.Add(btnTienLuong);
            listBtnTypeIncome.Add(btnTienThuong);
            listBtnTypeIncome.Add(btnDauTu);
            listBtnTypeIncome.Add(btnLoiNhuan);
            listBtnTypeIncome.Add(btnThuLai);
            listBtnTypeIncome.Add(btnChoThue);
            listBtnTypeIncome.Add(btnStock);
            listBtnTypeIncome.Add(btnKhac);
            //add all expense button to list
            listBtnTypeExpense.Add(btnAnUong);
            listBtnTypeExpense.Add(btnQuanAo);
            listBtnTypeExpense.Add(btnDiLai);
            listBtnTypeExpense.Add(btnInternet);
            listBtnTypeExpense.Add(btnTienNha);
            listBtnTypeExpense.Add(btnGiaoDuc);
            listBtnTypeExpense.Add(btnYTe);
            listBtnTypeExpense.Add(btnHieuHi);
            listBtnTypeExpense.Add(btnGiaiTri);
            listBtnTypeExpense.Add(btnKhac);

            //duyệt qua các button của listBtnType để AllowAnimations = false
            foreach (BunifuButton2 btn in listBtnTypeIncome)
            {
                btn.AllowAnimations = false;
            }


        }

        private void loadHistoryTransaction()
        {
            //clear selected row
            dgvHistoryTransaction.ClearSelection();
            //clear data in dgvHistoryTransaction
            dgvHistoryTransaction.DataBindings.Clear();
            //load history transaction to datagridview from database using LINQ context
            var sql = from t in context.Transactions
                      join ty in context.Types on t.MoneyType equals ty.Id
                      where t.UserId == user.Id
                      orderby t.DateUse descending
                      select new
                      {
                          t.DateUse,
                          t.MoneyNum,
                          t.MoneyContent,
                          t.CashOrBank,
                          ty.Name,
                          ty.Category
                      };
            //format DateUse sang dạng dd/MM/yyyy
            DataTable dt = new DataTable();
            //cột số thứ tự
            //dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Ngày");
            dt.Columns.Add("Số tiền");
            dt.Columns.Add("Note");
            dt.Columns.Add("Phân loại");
            dt.Columns.Add("Category");
            dt.Columns.Add("Ví");
            //first row để trống cho dễ nhìn
            dt.Rows.Add("", "", "", "", "", "");
            //ẩn cột category đi 
            foreach (var item in sql)
            {
                dt.Rows.Add(
                    //dt.Rows.Count + 1,
                    //format date thao dạng như là Thứ năm, 26 thg 10
                    item.DateUse.ToString("dddd, dd MMM", CultureInfo.CreateSpecificCulture("vi-VN")),
                    //dạng tiền tệ VND, ví dụ: 12.000đ,
                    //nếu category = income thì dấu + trước số tiền và có màu xanh lá cây
                    //nếu category = expense thì dấu - trước số tiền và có màu đỏ
                    item.Category == "income" ? "+ " + item.MoneyNum.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")) : "- " + item.MoneyNum.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                    item.MoneyContent,
                    item.Name,
                    item.CashOrBank == "cash" ? "Tiền mặt" : "Tài khoản",
                    //nếu category = income thì có màu xanh lá cây
                    //nếu category = expense thì có màu đỏ
                    item.Category);
            }
            dgvHistoryTransaction.DataSource = dt;
            //clear selected row
            dgvHistoryTransaction.ClearSelection();
            dgvHistoryTransaction.Rows[0].Height = 0;
            dgvHistoryTransaction.Columns[0].Width = 150;
            dgvHistoryTransaction.Columns[1].Width = 120;
            dgvHistoryTransaction.Columns[3].Width = 100;
            dgvHistoryTransaction.Columns[4].Width = 1;
            dgvHistoryTransaction.Columns[5].Width = 1;
        }

        // so sánh 2 ngày có cùng ngày tháng năm không
        private bool isSameDay(int column, int row)
        {
            DataGridViewCell cell1 = dgvHistoryTransaction.Rows[row].Cells[column];
            DataGridViewCell cell2 = dgvHistoryTransaction.Rows[row - 1].Cells[column];
            //if one of them null, return false
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
        private void dgvHistoryTransaction_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (isSameDay(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = dgvHistoryTransaction.AdvancedCellBorderStyle.Top;
                }
                //nếu cột thứ 2 (cột số tiền) có giá trị là income thì màu xanh lá cây
                //nếu cột thứ 2 (cột số tiền) có giá trị là expense thì màu đỏ
                if (e.ColumnIndex == 1)
                {
                    if (dgvHistoryTransaction.Rows[e.RowIndex].Cells[5].Value.ToString() == "income")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
            if (e.ColumnIndex == 1 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (e.ColumnIndex == 2)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void dgvHistoryTransaction_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (isSameDay(e.ColumnIndex, e.RowIndex))
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvHistoryTransaction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvHistoryTransaction.Rows[0].Height = 0;
            dgvHistoryTransaction.Columns[0].Width = 150;
            dgvHistoryTransaction.Columns[1].Width = 120;
            dgvHistoryTransaction.Columns[3].Width = 100;
            dgvHistoryTransaction.Columns[4].Width = 1;
            dgvHistoryTransaction.Columns[5].Width = 1;

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
            btnHistory.Enabled = true;
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 1;
            //disable this button
            btnIncome.Enabled = false;
            //enable other buttons
            btnReport.Enabled = true;
            btnExpense.Enabled = true;
            btnHistory.Enabled = true;
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 2;
            //disable this button
            btnExpense.Enabled = false;
            //enable other buttons
            btnReport.Enabled = true;
            btnIncome.Enabled = true;
            btnHistory.Enabled = true;
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            loadHistoryTransaction();
            bnfPageMain.PageIndex = 3;
            //disable this button
            btnHistory.Enabled = false;
            //enable other buttons
            btnExpense.Enabled = true;
            btnReport.Enabled = true;
            btnIncome.Enabled = true;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //end nav button

        private void loadMoney()
        {
            //load money of user
            Money? money = context.Money.FirstOrDefault(m => m.UserId == user.Id);
            if (money == null)
            {
                MessageBox.Show("Không tìm thấy tiền của người dùng!");
                return;
            }
            //set text cho txtCash và txtBank
            btnCash.ButtonText = convertVND(money.Cash.ToString());
            btnBank.ButtonText = convertVND(money.Bank.ToString());
            btnTotal.ButtonText = convertVND((money.Cash + money.Bank).ToString());
        }

        private void clearBtnTxt(string v)
        {
            if (v == "income")
            {
                txtMoneyIncome.Text = "";
                txtMoneyIncome.BorderColorIdle = System.Drawing.Color.Silver;
                lblRequiredMoneyIncome.Visible = false;
                lblRequiredTypeIncome.Visible = false;
            }
            else if (v == "expense")
            {
                txtMoneyExpense.Text = "";
                txtMoneyExpense.BorderColorIdle = System.Drawing.Color.Silver;
                btnCashExpense.IdleBorderColor = System.Drawing.Color.Silver;
                btnBankExpense.IdleBorderColor = System.Drawing.Color.Silver;
                lblRequiredMoneyExpense.Visible = false;
                lblRequiredTypeExpense.Visible = false;
                inVisibleBtnSoExpense();
                resetBtnExpense();
            }

        }

        //page income////////////////page income//////////////////////page income////////////////////////////////
        private void chooseBtnIncome(BunifuButton2 btnDisable)
        {
            foreach (BunifuButton2 btn in listBtnTypeIncome)
            {
                if (btn != btnDisable)
                {
                    if (btn.Enabled == false)
                    {
                        btn.Enabled = true;
                    }
                }
                else
                {
                    if (btn.Enabled == true)
                    {
                        btn.Enabled = false;
                    }
                }
            }
        }
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
            chooseBtnIncome(btnMeCho);
        }

        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnTienLuong);
        }

        private void btnTienThuong_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnTienThuong);

        }

        private void btnDauTu_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnDauTu);
        }

        private void btnLoiNhuan_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnLoiNhuan);

        }

        private void btnThuLai_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnThuLai);

        }

        private void btnChoThue_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnChoThue);

        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            chooseBtnIncome(btnStock);

        }

        private void btnAddIncome_Click(object sender, EventArgs e)
        {
            //get date of bnfDatePickerIncome dưới dạng smalldatetime trong sql server
            DateTime? date = bnfDatePickerIncome.Value;
            //nếu date null hoặc rỗng thì set date = ngày hiện tại
            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            //get income money từ txtInComeMoney
            string incomeMoney = txtMoneyIncome.Text;
            Decimal incomeMoneyDecimal = 0;
            //nếu incomeMoney null hoặc rỗng thì yêu cầu người dùng nhập lại
            if (incomeMoney == null || incomeMoney == "")
            {
                txtMoneyIncome.BorderColorIdle = System.Drawing.Color.IndianRed;
                lblRequiredMoneyIncome.Text = "Vui lòng nhập số tiền!";
                lblRequiredMoneyIncome.Visible = true;
                return;
            }
            else
            {
                txtMoneyIncome.BorderColorIdle = System.Drawing.Color.ForestGreen;
                incomeMoneyDecimal = Convert.ToDecimal(convertDecimal(incomeMoney));

            }
            string cashOrBank = "";
            //nếu một trong hai button cash hoặc bank chưa được chọn thì yêu cầu người dùng chọn
            if (btnCashIncome.Enabled == true && btnBankIncome.Enabled == true)
            {
                lblRequiredCashBankIncome.Text = "Vui lòng chọn một trong hai!";
                lblRequiredCashBankIncome.Visible = true;
                btnCashIncome.IdleBorderColor = System.Drawing.Color.IndianRed;
                btnBankIncome.IdleBorderColor = System.Drawing.Color.IndianRed;
                return;
            }
            else
            {
                //button nào đang disable thì set màu border thành màu xanh lá cây
                if (btnCashIncome.Enabled == false)
                {
                    btnCashIncome.IdleBorderColor = System.Drawing.Color.ForestGreen;
                    cashOrBank = "cash";
                }
                else
                {
                    btnBankIncome.IdleBorderColor = System.Drawing.Color.ForestGreen;
                    cashOrBank = "bank";
                }
            }
            //lấy nội dung từ txtNoteIncome
            string note = txtNoteIncome.Text;
            string moneyType = "";
            //nếu không button nào trong listBtnType được chọn thì yêu cầu người dùng chọn
            if (listBtnTypeIncome.Where(btn => btn.Enabled == false).Count() == 0)
            {
                lblRequiredTypeIncome.Text = "Vui lòng chọn \nloại thu nhập!";
                lblRequiredTypeIncome.Visible = true;
                foreach (BunifuButton2 btn in listBtnTypeIncome)
                {
                    btn.IdleBorderColor = System.Drawing.Color.IndianRed;
                }
                return;
            }
            else
            {
                foreach (BunifuButton2 btn in listBtnTypeIncome)
                {
                    if (btn.Enabled == false)
                    {
                        btn.IdleBorderColor = System.Drawing.Color.ForestGreen;
                        moneyType = btn.Text;
                    }
                }
            }
            Type? type = context.Types.Where(t => t.Name == moneyType).FirstOrDefault();
            if (type == null)
            {
                MessageBox.Show("Loại thu nhập không tồn tại!");
                return;
            }
            //Tạo một đối tượng Transaction
            Transaction transaction = new Transaction();
            //set thuộc tính cho đối tượng transaction
            transaction.UserId = user.Id;
            transaction.MoneyNum = incomeMoneyDecimal;
            transaction.CashOrBank = cashOrBank;
            transaction.MoneyContent = note;
            transaction.MoneyType = type.Id;
            transaction.DateUse = (DateTime)date;
            //thêm đối tượng transaction vào database, thông báo thành công hoặc thất bại
            if (context.Transactions.Add(transaction) != null)
            {
                context.SaveChanges();
                MessageBox.Show("Thêm thành công!");
                addMoney(cashOrBank, incomeMoneyDecimal);
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }

        }

        private void addMoney(string cashOrBank, decimal incomeMoneyDecimal)
        {
            //load money of user
            Money? money = context.Money.FirstOrDefault(m => m.UserId == user.Id);
            if (money == null)
            {
                MessageBox.Show("Không tìm thấy tiền của người dùng!");
                return;
            }
            //nếu cashOrBank = cash thì cộng tiền vào cash
            if (cashOrBank == "cash")
            {
                money.Cash += incomeMoneyDecimal;
            }
            else
            {
                money.Bank += incomeMoneyDecimal;
            }
            //save money of user to database
            context.SaveChanges();
            //load lại tiền của user
            loadMoney();
        }



        private void txtMoneyIncome_Leave(object sender, EventArgs e)
        {
            //nếu số tiền nhập vào lớn hơn 1000 thì border màu xanh lá
            string income = convertDecimal(txtMoneyIncome.Text);
            Decimal incomeDecimal = Convert.ToDecimal(income);
            if (incomeDecimal > 1000)
            {
                txtMoneyIncome.BorderColorIdle = System.Drawing.Color.ForestGreen;
            }
            else
            {
                txtMoneyIncome.BorderColorIdle = System.Drawing.Color.IndianRed;
            }
        }
        //end page income


        //page expense/////////////////////////////////page expense////////////////////////page expense///////////////////page expense
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
            checkExpenseMoney();
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

        private void btnAnUong_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnAnUong);
        }

        private void chooseBtnExpense(BunifuButton2 btnDisable)
        {
            foreach (BunifuButton2 btn in listBtnTypeExpense)
            {
                if (btn != btnDisable)
                {
                    if (btn.Enabled == false)
                    {
                        btn.Enabled = true;
                    }
                }
                else
                {
                    if (btn.Enabled == true)
                    {
                        btn.Enabled = false;
                    }
                }
            }
        }

        private void btnQuanAo_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnQuanAo);
        }

        private void btnDiLai_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnDiLai);

        }

        private void btnInternet_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnInternet);

        }

        private void btnTienNha_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnTienNha);

        }

        private void btnGiaoDuc_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnGiaoDuc);

        }

        private void btnYTe_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnYTe);

        }

        private void btnHieuHi_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnHieuHi);

        }

        private void btnGiaiTri_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnGiaiTri);

        }

        private void btnKhac_Click(object sender, EventArgs e)
        {
            chooseBtnExpense(btnKhac);

        }

        private void checkExpenseMoney()
        {
            //check số tiền nhập vào có hợp lệ không
            if (txtMoneyExpense.Text == "" || txtMoneyExpense.Text == null)
            {
                txtMoneyExpense.BorderColorIdle = System.Drawing.Color.IndianRed;
                lblRequiredMoneyExpense.Text = "Vui lòng nhập số tiền!";
                lblRequiredMoneyExpense.Visible = true;
                return;
            }
            Decimal expenseMoneyDecimal = Convert.ToDecimal(convertDecimal(txtMoneyExpense.Text));
            //check số tiền nhập vào có lớn hơn 1000 không
            if (expenseMoneyDecimal <= 1000)
            {

                txtMoneyExpense.BorderColorIdle = System.Drawing.Color.IndianRed;
                lblRequiredMoneyExpense.Text = "Số tiền phải lớn hơn 1000!";
                lblRequiredMoneyExpense.Visible = true;
                return;
            }
            else
            {
                txtMoneyExpense.BorderColorIdle = System.Drawing.Color.ForestGreen;
                lblRequiredMoneyExpense.Text = "";
                lblRequiredMoneyExpense.Visible = false;
            }
            string cashOrBank = btnCashExpense.Enabled == false ? "cash" : "bank";
            Money? money = context.Money.FirstOrDefault(m => m.UserId == user.Id);
            // nếu expenseMoneyDecimal > money.Cash hoặc expenseMoneyDecimal > money.Bank thì yêu cầu người dùng nhập lại
            if (cashOrBank == "cash")
            {
                if (expenseMoneyDecimal > money.Cash)
                {
                    txtMoneyExpense.BorderColorIdle = System.Drawing.Color.IndianRed;
                    lblRequiredMoneyExpense.Text = "Số tiền chi không được lớn hơn số tiền trong ví!";
                    lblRequiredMoneyExpense.Visible = true;
                    return;
                }
            }
            else
            {
                if (expenseMoneyDecimal > money.Bank)
                {
                    txtMoneyExpense.BorderColorIdle = System.Drawing.Color.IndianRed;
                    lblRequiredMoneyExpense.Text = "Số tiền chi không được lớn hơn số tiền trong tài khoản ngân hàng!";
                    lblRequiredMoneyExpense.Visible = true;
                    return;
                }
            }

            // nếu số tiền nhập vào hợp lệ thì cho phép người dùng tiếp tục
            txtMoneyExpense.BorderColorIdle = System.Drawing.Color.ForestGreen;
            lblRequiredMoneyExpense.Text = "";
            lblRequiredMoneyExpense.Visible = false;
            inVisibleBtnSoExpense();
            resetBtnExpense();
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            //get date of bnfDatePickerExpense dưới dạng datetime
            DateTime? date = bnfDatePickerExpense.Value;
            //nếu date null hoặc empty thì set date = ngày hiện tại
            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            checkExpenseMoney();
            //get expense money từ txtMoneyExpense
            Decimal expenseMoneyDecimal = Convert.ToDecimal(convertDecimal(txtMoneyExpense.Text));
            string cashOrBank = "";
            //nếu một trong hai button cash hoặc bank chưa được chọn thì yêu cầu người dùng chọn
            if (btnCashExpense.Enabled == true && btnBankExpense.Enabled == true)
            {
                lblRequiredCashBankExpense.Text = "Vui lòng chọn một trong hai!";
                lblRequiredCashBankExpense.Visible = true;
                btnCashExpense.IdleBorderColor = System.Drawing.Color.IndianRed;
                btnBankExpense.IdleBorderColor = System.Drawing.Color.IndianRed;
                return;
            }
            else
            {
                if (btnCashExpense.Enabled == false)
                {
                    btnCashExpense.IdleBorderColor = System.Drawing.Color.ForestGreen;
                    cashOrBank = "cash";
                }
                else
                {
                    btnBankExpense.IdleBorderColor = System.Drawing.Color.ForestGreen;
                    cashOrBank = "bank";
                }
            }
            //lấy nội dung của txtNoteExpense
            string note = txtNoteExpense.Text;
            string moneyType = "";
            //nếu không button nào trong listBtnTypeExpense được chọn thì yêu cầu người dùng chọn
            if (listBtnTypeExpense.Where(btn => btn.Enabled == false).Count() == 0)
            {
                lblRequiredTypeExpense.Text = "Vui lòng chọn một trong các loại chi tiêu!";
                lblRequiredTypeExpense.Visible = true;
                foreach (BunifuButton2 btn in listBtnTypeExpense)
                {
                    btn.IdleBorderColor = System.Drawing.Color.IndianRed;
                }
                return;
            }
            else
            {
                foreach (BunifuButton2 btn in listBtnTypeExpense)
                {
                    if (btn.Enabled == false)
                    {
                        btn.IdleBorderColor = System.Drawing.Color.ForestGreen;
                        moneyType = btn.Text;
                    }
                }
            }
            Type? type = context.Types.Where(t => t.Name == moneyType).FirstOrDefault();
            if (type == null)
            {
                MessageBox.Show("Loại chi tiêu không tồn tại!");
                return;
            }

            //Tạo một đối tượng Transaction
            Transaction transaction = new Transaction();
            //set thuộc tính cho đối tượng transaction
            transaction.UserId = user.Id;
            transaction.MoneyNum = expenseMoneyDecimal;
            transaction.CashOrBank = cashOrBank;
            transaction.MoneyContent = note;
            transaction.MoneyType = type.Id;
            transaction.DateUse = (DateTime)date;

            //thêm đối tượng transaction vào database, thông báo thành công hoặc thất bại
            if (context.Transactions.Add(transaction) != null)
            {
                context.SaveChanges();
                MessageBox.Show("Thêm khoản chi thành công!");
                minusMoney(cashOrBank, expenseMoneyDecimal);
                clearBtnTxt("expense");
            }
            else
            {
                MessageBox.Show("Thêm khoản chi thất bại!");
            }
        }


        private void minusMoney(string cashOrBank, decimal expenseMoneyDecimal)
        {
            //load money of user
            Money? money = context.Money.FirstOrDefault(m => m.UserId == user.Id);
            if (money == null)
            {
                MessageBox.Show("Không tìm thấy tiền của người dùng!");
                return;
            }
            //nếu cashOrBank = cash thì trừ tiền vào cash
            if (cashOrBank == "cash")
            {
                money.Cash -= expenseMoneyDecimal;
            }
            //nếu cashOrBank = bank thì trừ tiền vào bank
            else
            {
                money.Bank -= expenseMoneyDecimal;
            }
            //lưu thay đổi vào database
            context.SaveChanges();
            //load lại tiền của người dùng
            loadMoney();
        }

        private void txtMoneyExpense_Leave(object sender, EventArgs e)
        {
            checkExpenseMoney();
        }

        private void btnCashExpense_Click(object sender, EventArgs e)
        {
            //disable btnCashExpense
            btnCashExpense.Enabled = false;
            //enable btnBankExpense
            if (btnBankExpense.Enabled == false)
            {
                btnBankExpense.Enabled = true;
            }
            checkExpenseMoney();
        }

        private void btnBankExpense_Click(object sender, EventArgs e)
        {
            //disable btnBankExpense
            btnBankExpense.Enabled = false;
            //enable btnCashExpense
            if (btnCashExpense.Enabled == false)
            {
                btnCashExpense.Enabled = true;
            }
            checkExpenseMoney();
        }


        //end page expense

        //page report/////////page report/////////page report/////////page report/////////page report/////////page report/////////

    }

}
