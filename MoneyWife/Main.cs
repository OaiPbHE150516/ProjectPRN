using Bunifu.UI.WinForms.BunifuButton;
using Guna.Charts.WinForms;
using MoneyWife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Speech.Synthesis.TtsEngine;
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
            btnDropdownExInTotal.SelectedIndex = 0;
            bnfDropdownDayMonth.SelectedIndex = 1;
            btnDropdownTime.SelectedIndex = 0;
            btnReport.Enabled = false;
            loadMoney();
            bnfDatePickerIncome.Value = DateTime.Now;
            bnfDatePickerExpense.Value = DateTime.Now;
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


        DateTime now = DateTime.Now;
        DateTime begin;
        DateTime end;

        private void btnDropdownTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataToDrawChart();
        }

        private void getDataToDrawChart()
        {
            //sử dụng switch case để xử lý các trường hợp của btnDropdownExInTotal, btnDropdownTime
            String typeInEx = "";
            switch (btnDropdownExInTotal.SelectedIndex)
            {
                case 0: //Income
                    typeInEx = "Income";
                    break;
                case 1:
                    typeInEx = "Expense";
                    break;
            }
            switch (btnDropdownTime.SelectedIndex)
            {
                case 0: // ngày tháng năm hôm nay
                    begin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                    end = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                    break;
                case 1: // Tuần này
                    //ngày tháng năm của ngày đầu tiên của tuần hiện tại
                    begin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddDays(-(int)now.DayOfWeek);
                    //ngày tháng năm của ngày cuối cùng của tuần hiện tại
                    end = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59).AddDays(6 - (int)now.DayOfWeek);
                    break;
                case 2: // Tháng này
                    //ngày tháng năm của ngày đầu tiên của tháng hiện tại
                    begin = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                    //ngày tháng năm của ngày cuối cùng của tháng hiện tại
                    end = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
                    break;
                case 3: // Năm nay
                    //ngày tháng năm của ngày đầu tiên của năm hiện tại
                    begin = new DateTime(now.Year, 1, 1, 0, 0, 0);
                    //ngày tháng năm của ngày cuối cùng của năm hiện tại
                    end = new DateTime(now.Year, 12, 31, 23, 59, 59);
                    break;
                case 4: // Tất cả
                    //get first record of user in table Transaction which have type = typeInEx
                    DateTime beginExpense = (from t in context.Transactions
                                             where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Expense"
                                             orderby t.DateUse ascending
                                             select t.DateUse).FirstOrDefault();

                    DateTime beginIncome = (from t in context.Transactions
                                            where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Income"
                                            orderby t.DateUse ascending
                                            select t.DateUse).FirstOrDefault();
                    //lấy begin của Income và Expense, so sánh lấy begin xa hơn
                    begin = beginExpense > beginIncome ? beginIncome : beginExpense;
                    //get last record of user in table Transaction which have type = typeInEx
                    DateTime endExpense = (from t in context.Transactions
                                           where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Expense"
                                           orderby t.DateUse descending
                                           select t.DateUse).FirstOrDefault();
                    DateTime endIncome = (from t in context.Transactions
                                          where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Income"
                                          orderby t.DateUse descending
                                          select t.DateUse).FirstOrDefault();
                    //lấy end của Income và Expense, so sánh lấy end gần hơn
                    end = endExpense < endIncome ? endIncome : endExpense;
                    break;
                default:
                    begin = DateTime.Now;
                    end = DateTime.Now;

                    break;
            }
            fixBugUndefined();
            bnfDatePickerChartFrom.Value = begin;
            bnfDatePickerChartTo.Value = end;
            countPercent(begin, end);
        }

        private void btnDropdownExInTotal_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataToDrawChart();
            //nếu selectedindex = 2 thì bnfDropdownDayMonth.Visibile = true
            if (btnDropdownExInTotal.SelectedIndex == 2)
            {
                bnfDropdownDayMonth.Visible = true;
                panelInExBdSd.Visible = true;
                bnfDgvReportTotal.Visible = true;
                bnfDgvReport.Visible = false;
                //gunaChart1.Visible = false;
            }
            else
            {
                bnfDropdownDayMonth.Visible = false;
                panelInExBdSd.Visible = false;
                bnfDgvReportTotal.Visible = false;
                bnfDgvReport.Visible = true;
                //gunaChart1.Visible = true;
            }
        }
        private void fixBugUndefined()
        {
            gunaChart1.Datasets.Clear();
            gunaChart1.Datasets.Add(new Guna.Charts.WinForms.GunaDoughnutDataset());
            Dictionary<string, double> dataPercent = new Dictionary<string, double>();
            switch (btnDropdownExInTotal.SelectedIndex)
            {
                case 0: //Income
                    dataPercent.Add("Mẹ cho", 0);
                    dataPercent.Add("Tiền lương", 0);
                    dataPercent.Add("Tiền thưởng", 0);
                    dataPercent.Add("Đầu tư", 0);
                    dataPercent.Add("Lợi nhuận", 0);
                    dataPercent.Add("Thu lãi", 0);
                    dataPercent.Add("Cho thuê", 0);
                    dataPercent.Add("Stock", 0);
                    break;
                case 1: //Expense
                    dataPercent.Add("Ăn uống", 0);
                    dataPercent.Add("Quần áo", 0);
                    dataPercent.Add("Đi lại", 0);
                    dataPercent.Add("Internet", 0);
                    dataPercent.Add("Tiền nhà", 0);
                    dataPercent.Add("Giáo dục", 0);
                    dataPercent.Add("Y tế", 0);
                    dataPercent.Add("Hiếu hỉ", 0);
                    dataPercent.Add("Giải trí", 0);
                    dataPercent.Add("Khác", 0);
                    break;
            }
            Chart.Donut.DrawDonut(gunaChart1, "Biểu đồ:", dataPercent);
        }

        private void bnfDatePickerChartTo_CloseUp(object sender, EventArgs e)
        {
            begin = bnfDatePickerChartFrom.Value;
            end = bnfDatePickerChartTo.Value;
            //MessageBox.Show(bnfDatePickerChartTo.Value.ToString());
            fixBugUndefined();
            btnDropdownTime.SelectedIndex = 5;
            countPercent(begin, end);
        }

        private void bnfDatePickerChartFrom_CloseUp(object sender, EventArgs e)
        {
            begin = bnfDatePickerChartFrom.Value;
            end = bnfDatePickerChartTo.Value;
            //MessageBox.Show(bnfDatePickerChartFrom.Value.ToString());
            fixBugUndefined();
            btnDropdownTime.SelectedIndex = 5;
            countPercent(begin, end);
        }

        private void countPercent(DateTime begin, DateTime end)
        {
            gunaChart1.Datasets.Clear();
            String typeInEx = "";
            String chartTitle = "";
            switch (btnDropdownExInTotal.SelectedIndex)
            {
                case 0: //Income
                    typeInEx = "Income";

                    //nếu ngày tháng năm của begin = ngày tháng năm của end thì chỉ hiển thị ngày tháng năm của begin
                    if (begin.Date == end.Date && begin.Month == end.Month && begin.Year == end.Year)
                    {
                        chartTitle = "Biểu đồ thu nhập ngày " + begin.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN"));
                    }
                    else
                    {
                        chartTitle = "Biểu đồ thu nhập trong khoảng thời gian "
                       + begin.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN"))
                       + " - " + end.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN"));
                    }
                    break;
                case 1: //Expense
                    typeInEx = "Expense";
                    //nếu ngày tháng năm của begin = ngày tháng năm của end thì chỉ hiển thị ngày tháng năm của begin
                    if (begin.Date == end.Date && begin.Month == end.Month && begin.Year == end.Year)
                    {
                        chartTitle = "Biểu đồ chi tiêu ngày " + begin.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN"));
                    }
                    else
                    {
                        chartTitle = "Biểu đồ chi tiêu trong khoảng thời gian "
                        + begin.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN"))
                        + " - " + end.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN"));
                    }
                    break;
                case 2: //Total 
                    drawChartTotal(begin, end);

                    return;
            }
            //lấy ra các loại chi tiêu trong transaction của user, tên và tổng tiền của chúng trong một khoảng thời gian, sắp xếp theo tổng tiền giảm dần
            Dictionary<string, int> dataTransaction = new Dictionary<string, int>();
            var sqlExpense = from t in context.Transactions
                             where t.UserId == user.Id && t.MoneyTypeNavigation.Category == typeInEx && t.DateUse > begin && t.DateUse < end
                             group t by t.MoneyTypeNavigation.Name into g
                             select new
                             {
                                 Name = g.Key,
                                 Total = g.Sum(t => t.MoneyNum)
                             };
            //gán name và total vào dictionary
            foreach (var item in sqlExpense)
            {
                dataTransaction.Add(item.Name, (int)item.Total);
            }
            // sort dataExpense Dictionary by value
            dataTransaction = dataTransaction.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            loadDgvReport(dataTransaction, typeInEx);
            // tính % của từng loại chi tiêu với tổng tiền chi tiêu, làm tròn đến 2 chữ số thập phân
            Dictionary<string, double> dataPercent = new Dictionary<string, double>();
            double total = dataTransaction.Sum(x => x.Value);
            foreach (var item in dataTransaction)
            {
                dataPercent.Add(item.Key, Math.Round((item.Value / total) * 100, 0));
            }
            //nếu dictionary mà rỗng thì hiển thị thông báo
            if (dataPercent.Count == 0)
            {
                //MessageBox.Show("Không có dữ liệu trong khoảng thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //nếu begin và end có chung ngày tháng năm thì hiển thị thông báo
                if (begin.Year == end.Year && begin.Month == end.Month && begin.Day == end.Day)
                {
                    lblNotificationChartData.Text = "Không có dữ liệu trong ngày <br>"
                        + " hôm nay <strong><i>" + begin.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN")) + "</i></strong>";
                }
                else
                {
                    lblNotificationChartData.Text = "Không có dữ liệu trong khoảng thời gian <br>"
                        + " từ: <strong><i>" + begin.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN")) + "</i></strong> <br>"
                        + " đến: <strong><i>" + end.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN")) + "</i></strong>";
                }
                lblNotificationChartData.Visible = true;
                fixBugUndefined();
            }
            else
            {
                fixBugUndefined();
                lblNotificationChartData.Text = "";
                lblNotificationChartData.Visible = false;
                //vẽ biểu đồ
                Chart.Donut.DrawDonut(gunaChart1, chartTitle, dataPercent);
            }
            if (btnDropdownExInTotal.SelectedIndex == 2)
            {
                //Chart.StackedBar.DrawStackedBar(gunaChart1, chartTitle);
            }
        }

        private void bnfDropdownDayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawChartTotal(bnfDatePickerChartFrom.Value, bnfDatePickerChartTo.Value);
        }
        

        private void drawChartTotal(DateTime begin, DateTime end)
        {
            //nếu không trong số checkbox nào được chọn thì hiển thị thông báo
            if (!ckbIncomeChart.Checked && !ckbExpenseChart.Checked && !ckbBienDongChart.Checked && !ckbSoDuChart.Checked)
            {
                lblNotificationChartData.Text = "Vui lòng chọn ít nhất một loại biểu đồ";
                lblNotificationChartData.Visible = true;
                return;
            }
            else
            {
                lblNotificationChartData.Text = "";
                lblNotificationChartData.Visible = false;
            }
            if (bnfDropdownDayMonth.SelectedIndex == 0) //Ngày
            {
                //lấy ra transaction của user sau mỗi ngày, sắp xếp theo thứ tự thời gian từ ngày trước đến ngày sau
                var queryDay = from t in context.Transactions
                               where t.UserId == user.Id && t.DateUse > begin && t.DateUse < end
                               group t by new { t.DateUse.Year, t.DateUse.Month, t.DateUse.Day } into g
                               select new
                               {
                                   Year = g.Key.Year,
                                   Month = g.Key.Month,
                                   Day = g.Key.Day,
                                   Total = g.Sum(t => t.MoneyNum)
                               };
                // gán dữ liệu vào biểu đồ
                Dictionary<DateTime, decimal> dataDay = new Dictionary<DateTime, decimal>();
                foreach (var item in queryDay)
                {
                    dataDay.Add(new DateTime(item.Year, item.Month, item.Day), (decimal)item.Total);
                }

                //sắp xếp lại theo thứ tự thời gian từ ngày trước đến ngày sau
                dataDay = dataDay.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                //lấy ra thu nhập của user sau mỗi ngày, sắp xếp theo thứ tự thời gian từ ngày trước đến ngày sau
                var queryIncomeDay = from t in context.Transactions
                                     where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Income" && t.DateUse > begin && t.DateUse < end
                                     group t by new { t.DateUse.Year, t.DateUse.Month, t.DateUse.Day } into g
                                     select new
                                     {
                                         Year = g.Key.Year,
                                         Month = g.Key.Month,
                                         Day = g.Key.Day,
                                         Total = g.Sum(t => t.MoneyNum)
                                     };
                // gán dữ liệu vào biểu đồ
                Dictionary<DateTime, decimal> dataIncomeDay = new Dictionary<DateTime, decimal>();
                foreach (var item in queryIncomeDay)
                {
                    dataIncomeDay.Add(new DateTime(item.Year, item.Month, item.Day), (decimal)item.Total);
                }

                //sắp xếp lại theo thứ tự thời gian từ ngày trước đến ngày sau
                //dataIncomeDay = dataIncomeDay.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                //lấy ra chi tiêu của user sau mỗi ngày, sắp xếp theo thứ tự thời gian từ ngày trước đến ngày sau
                var queryExpenseDay = from t in context.Transactions
                                      where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Expense" && t.DateUse > begin && t.DateUse < end
                                      group t by new { t.DateUse.Year, t.DateUse.Month, t.DateUse.Day } into g
                                      select new
                                      {
                                          Year = g.Key.Year,
                                          Month = g.Key.Month,
                                          Day = g.Key.Day,
                                          Total = g.Sum(t => t.MoneyNum)
                                      };
                // gán dữ liệu vào biểu đồ
                Dictionary<DateTime, decimal> dataExpenseDay = new Dictionary<DateTime, decimal>();
                foreach (var item in queryExpenseDay)
                {
                    dataExpenseDay.Add(new DateTime(item.Year, item.Month, item.Day), (decimal)item.Total);
                }

                //sắp xếp lại theo thứ tự thời gian từ ngày trước đến ngày sau
                //dataExpenseDay = dataExpenseDay.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                foreach (var item in dataDay)
                {
                    if (!dataIncomeDay.ContainsKey(item.Key))
                    {
                        dataIncomeDay.Add(item.Key, 0);
                    }
                    if (!dataExpenseDay.ContainsKey(item.Key))
                    {
                        dataExpenseDay.Add(item.Key, 0);
                    }
                }

                Dictionary<DateTime, decimal> dataBalanceDay = new Dictionary<DateTime, decimal>();
                foreach (var item in dataDay)
                {
                    dataBalanceDay.Add(item.Key, dataIncomeDay[item.Key] - dataExpenseDay[item.Key]);
                }

                Chart.StackedBar.DrawStackedBar(gunaChart1, ckbIncomeChart, ckbExpenseChart,
                    ckbBienDongChart, ckbSoDuChart, "ngày", dataDay, dataIncomeDay, dataExpenseDay, dataBalanceDay);

                //load dataDay vào bnfDgvReportTotal
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Ngày");
                dataTable.Columns.Add("Thu nhập");
                dataTable.Columns.Add("Chi tiêu");
                dataTable.Columns.Add("Số dư");
                dataTable.Rows.Add("", "", "", "");
                foreach (var item in dataDay)
                {
                    dataTable.Rows.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), 
                        dataIncomeDay[item.Key].ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")), 
                        dataExpenseDay[item.Key].ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")), 
                        dataBalanceDay[item.Key].ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")));
                }
                //tính tổng
                decimal totalIncome = dataIncomeDay.Values.Sum();
                decimal totalExpense = dataExpenseDay.Values.Sum();
                decimal totalBalance = dataBalanceDay.Values.Sum();
                dataTable.Rows.Add("Tổng", totalIncome.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                    totalExpense.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                    totalBalance.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")));
                bnfDgvReportTotal.DataSource = dataTable;               
            }
            else
            {
                //lấy ra transaction của mỗi tháng, sắp xếp theo thứ tự thời gian từ tháng trước đến tháng sau
                var queryMonth = from t in context.Transactions
                                 where t.UserId == user.Id && t.DateUse > begin && t.DateUse < end
                                 group t by new { t.DateUse.Year, t.DateUse.Month } into g
                                 select new
                                 {
                                     Year = g.Key.Year,
                                     Month = g.Key.Month,
                                     Total = g.Sum(t => t.MoneyNum)
                                 };
                // gán dữ liệu vào biểu đồ
                Dictionary<DateTime, decimal> dataMonth = new Dictionary<DateTime, decimal>();
                foreach (var item in queryMonth)
                {
                    dataMonth.Add(new DateTime(item.Year, item.Month, 1), (decimal)item.Total);
                }

                //lấy ra thu nhập của user sau mỗi tháng, sắp xếp theo thứ tự thời gian từ tháng trước đến tháng sau
                var queryIncomeMonth = from t in context.Transactions
                                       where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Income" && t.DateUse > begin && t.DateUse < end
                                       group t by new { t.DateUse.Year, t.DateUse.Month } into g
                                       select new
                                       {
                                           Year = g.Key.Year,
                                           Month = g.Key.Month,
                                           Total = g.Sum(t => t.MoneyNum)
                                       };
                // gán dữ liệu vào biểu đồ
                Dictionary<DateTime, decimal> dataIncomeMonth = new Dictionary<DateTime, decimal>();
                foreach (var item in queryIncomeMonth)
                {
                    dataIncomeMonth.Add(new DateTime(item.Year, item.Month, 1), (decimal)item.Total);
                }

                //lấy ra chi tiêu của user sau mỗi tháng, sắp xếp theo thứ tự thời gian từ tháng trước đến tháng sau
                var queryExpenseMonth = from t in context.Transactions
                                        where t.UserId == user.Id && t.MoneyTypeNavigation.Category == "Expense" && t.DateUse > begin && t.DateUse < end
                                        group t by new { t.DateUse.Year, t.DateUse.Month } into g
                                        select new
                                        {
                                            Year = g.Key.Year,
                                            Month = g.Key.Month,
                                            Total = g.Sum(t => t.MoneyNum)
                                        };
                // gán dữ liệu vào biểu đồ
                Dictionary<DateTime, decimal> dataExpenseMonth = new Dictionary<DateTime, decimal>();
                foreach (var item in queryExpenseMonth)
                {
                    dataExpenseMonth.Add(new DateTime(item.Year, item.Month, 1), (decimal)item.Total);
                }

                //kiểm tra nếu có tháng nào không có thu nhập hoặc chi tiêu thì thêm vào
                foreach (var item in dataMonth)
                {
                    if (!dataIncomeMonth.ContainsKey(item.Key))
                    {
                        dataIncomeMonth.Add(item.Key, 0);
                    }
                    if (!dataExpenseMonth.ContainsKey(item.Key))
                    {
                        dataExpenseMonth.Add(item.Key, 0);
                    }
                }

                //tính số dư của mỗi tháng
                Dictionary<DateTime, decimal> dataBalanceMonth = new Dictionary<DateTime, decimal>();
                foreach (var item in dataMonth)
                {
                    dataBalanceMonth.Add(item.Key, dataIncomeMonth[item.Key] - dataExpenseMonth[item.Key]);
                }
                Chart.StackedBar.DrawStackedBar(gunaChart1, ckbIncomeChart, ckbExpenseChart,
                    ckbBienDongChart, ckbSoDuChart, "tháng", dataMonth, dataIncomeMonth, dataExpenseMonth, dataBalanceMonth);

                //load dataMonth vào bnfDgvReportTotal
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Tháng", typeof(string));
                dataTable.Columns.Add("Thu nhập", typeof(string));
                dataTable.Columns.Add("Chi tiêu", typeof(string));
                dataTable.Columns.Add("Số dư", typeof(string));
                dataTable.Rows.Add("", "", "", "");
                foreach (var item in dataMonth)
                {
                    dataTable.Rows.Add(item.Key.ToString("MM/yyyy"),
                        dataIncomeMonth[item.Key].ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                        dataExpenseMonth[item.Key].ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                        dataBalanceMonth[item.Key].ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")));
                }
                //tính tổng
                decimal totalIncome = dataIncomeMonth.Values.Sum();
                decimal totalExpense = dataExpenseMonth.Values.Sum();
                decimal totalBalance = dataBalanceMonth.Values.Sum();
                dataTable.Rows.Add("Tổng", totalIncome.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                    totalExpense.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                    totalBalance.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")));

                bnfDgvReportTotal.DataSource = dataTable;
               
            }
        }
        private void bnfDgvReportTotal_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //style middleright to all cell
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (e.RowIndex != -1)
            {
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                {
                    return;
                }
                if ( e.RowIndex != bnfDgvReportTotal.Rows.Count - 1)
                {
                    //nếu là cột 1 (thu thu nhập) thì có màu xanh
                    if(e.ColumnIndex == 1)
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    //nếu là cột 2 (chi tiêu) thì có màu đỏ
                    else if (e.ColumnIndex == 2)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    //nếu là cột 3 (biến động) thì có màu xanh dương
                    else if (e.ColumnIndex == 3)
                    {
                        if (e.Value.ToString().Contains("-"))
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Green;
                        }
                    }
                }

                //nếu row ở hàng cuối cùng thì style bold
                if (e.RowIndex == bnfDgvReportTotal.Rows.Count - 1)
                {
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
        }

        private void bnfDgvReportTotal_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //row đầu tiên height = 0
            bnfDgvReportTotal.Rows[0].Height = 0;
            //chỉnh height của bnfDgvReport theo số dòng
            bnfDgvReportTotal.Height = bnfDgvReportTotal.Rows.Count * bnfDgvReport.Rows[1].Height;
        }
        private void loadDgvReport(Dictionary<string, int> dataTransaction, String inEx)
        {
            //load dataTransaction vào bnfDgvReport
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Nội dung");
            dataTable.Columns.Add("Số tiền");
            dataTable.Rows.Add("", "");
            foreach (var item in dataTransaction)
            {
                dataTable.Rows.Add(item.Key,
                    inEx == "Income" ? "+ " + item.Value.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")) : "- " + item.Value.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")));
            }
            dataTable.Rows.Add("Tổng tiền", dataTransaction.Sum(x => x.Value).ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")));
            bnfDgvReport.DataSource = dataTable;

        }

        private void bnfDgvReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //row đầu tiên height = 0
            bnfDgvReport.Rows[0].Height = 0;
            //chỉnh height của bnfDgvReport theo số dòng
            bnfDgvReport.Height = bnfDgvReport.Rows.Count * bnfDgvReport.Rows[1].Height;


        }

        private void bnfDgvReport_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //style middleright to all cell
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (e.RowIndex != -1)
            {
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == 1 && e.RowIndex != bnfDgvReport.Rows.Count - 1)
                {
                    //nếu btnDropdownInExTotal SelectedIndex = 0 thì là Income, ngược lại là Expense
                    if (btnDropdownExInTotal.SelectedIndex == 0)
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else if (btnDropdownExInTotal.SelectedIndex == 1)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }

                //nếu row ở hàng cuối cùng thì style bold
                if (e.RowIndex == bnfDgvReport.Rows.Count - 1)
                {
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
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
                    //format date thao dạng như là Thứ năm, 26 thg 10, 2022

                    item.DateUse.ToString("dddd, dd MMM, yyy", CultureInfo.CreateSpecificCulture("vi-VN")),
                    //dạng tiền tệ VND, ví dụ: 12.000đ,
                    //nếu category = income thì dấu + trước số tiền và có màu xanh lá cây
                    //nếu category = expense thì dấu - trước số tiền và có màu đỏ
                    item.Category == "income" ? "+ " + item.MoneyNum.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")) : "- " + item.MoneyNum.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN")),
                    item.MoneyContent,
                    item.Name,
                    item.CashOrBank == "cash" ? "Tiền mặt" : "Tài khoản",
                    item.Category);
            }
            dgvHistoryTransaction.DataSource = dt;
            //clear selected row
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
            if (e.RowIndex != -1)
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
            try
            {
                dgvHistoryTransaction.Rows[0].Height = 0;
                dgvHistoryTransaction.Columns[0].Width = 180;
                dgvHistoryTransaction.Columns[1].Width = 120;
                dgvHistoryTransaction.Columns[3].Width = 100;
                dgvHistoryTransaction.Columns[4].Width = 1;
                dgvHistoryTransaction.Columns[5].Width = 1;
            }
            catch (Exception)
            {
            }

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
        private string convertVND(string? v)
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
            //drawChartTotal(bnfDatePickerChartFrom.Value, bnfDatePickerChartTo.Value);
            getDataToDrawChart();
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
                txtNoteIncome.Text = "";
                txtNoteIncome.BorderColorIdle = System.Drawing.Color.Silver;
                lblRequiredMoneyIncome.Visible = false;
                lblRequiredTypeIncome.Visible = false;
                inVisibleBtnSoIncome();
                resetBtnIncome();
                foreach (BunifuButton2 btn in listBtnTypeIncome)
                {
                    if (btn.Enabled == false)
                    {
                        btn.Enabled = true;
                    }
                }
                btnBankIncome.Enabled = true;
                btnCashIncome.Enabled = true;
            }
            else if (v == "expense")
            {
                txtMoneyExpense.Text = "";
                txtMoneyExpense.BorderColorIdle = System.Drawing.Color.Silver;
                txtNoteExpense.Text = "";
                txtNoteExpense.BorderColorIdle = System.Drawing.Color.Silver;
                btnCashExpense.IdleBorderColor = System.Drawing.Color.Silver;
                btnBankExpense.IdleBorderColor = System.Drawing.Color.Silver;
                lblRequiredMoneyExpense.Visible = false;
                lblRequiredTypeExpense.Visible = false;
                inVisibleBtnSoExpense();
                resetBtnExpense();
                foreach (BunifuButton2 btn in listBtnTypeExpense)
                {
                    if (btn.Enabled == false)
                    {
                        btn.Enabled = true;
                    }
                }
                btnCashExpense.Enabled = true;
                btnBankExpense.Enabled = true;
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
            if (context.SaveChanges() > 0)
            {
                //load lại tiền của user
                loadMoney();
                clearBtnTxt("income");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
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

        private void ckbIncomeChart_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            drawChartTotal(bnfDatePickerChartFrom.Value, bnfDatePickerChartTo.Value);
        }

        private void cknExpenseChart_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            drawChartTotal(bnfDatePickerChartFrom.Value, bnfDatePickerChartTo.Value);
        }

        private void ckbBienDongChart_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            drawChartTotal(bnfDatePickerChartFrom.Value, bnfDatePickerChartTo.Value);
        }

        private void ckbSoDuChart_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            drawChartTotal(bnfDatePickerChartFrom.Value, bnfDatePickerChartTo.Value);
        }



        //end page expense

        //page report/////////page report/////////page report/////////page report/////////page report/////////page report/////////

    }

}
