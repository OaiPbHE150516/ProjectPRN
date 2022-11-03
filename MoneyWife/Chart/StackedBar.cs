using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWife.Chart
{
    public class StackedBar
    {

        public static void DrawStackedBar(Guna.Charts.WinForms.GunaChart chart,
            Bunifu.UI.WinForms.BunifuCheckBox ckbIncomeChart, Bunifu.UI.WinForms.BunifuCheckBox ckbExpenseChart,
            Bunifu.UI.WinForms.BunifuCheckBox ckbBienDongChart, Bunifu.UI.WinForms.BunifuCheckBox ckbSoDuChart,
            String chartTitle, Dictionary<DateTime, decimal> dataDay,
            Dictionary<DateTime, decimal> dataIncome, Dictionary<DateTime, decimal> dataExpense, 
            Dictionary<DateTime, decimal> dataBalanceDay)
        {
            chart.Datasets.Clear();
            chart.YAxes.GridLines.Display = false;
            chart.Title.Text = "Biểu đồ thu nhập - chi tiêu theo " + chartTitle;
            chart.Title.Position = Guna.Charts.WinForms.TitlePosition.Bottom;
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Bottom;
            chart.XAxes.Display = true;
            chart.YAxes.Display = true;

            //Spline of total
            var splineTotal = new Guna.Charts.WinForms.GunaSplineDataset();
            splineTotal.FillColor = System.Drawing.ColorTranslator.FromHtml("#87CEFA");
            splineTotal.BorderColor = System.Drawing.ColorTranslator.FromHtml("#87CEFA");
            splineTotal.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#87CEFA"));
            splineTotal.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#87CEFA"));
            splineTotal.PointBorderWidth = 1;
            splineTotal.BorderWidth = 1;
            splineTotal.Label = "Biến động số dư";

            foreach (var item in dataBalanceDay)
            {
                if (chartTitle.Equals("tháng"))
                {
                    splineTotal.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    splineTotal.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }


            //dataTotal
            var datasetTotal = new Guna.Charts.WinForms.GunaStackedBarDataset();
            datasetTotal.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#D3D3D3"));
            datasetTotal.Label = "Số dư";

            foreach (var item in dataBalanceDay)
            {
                if (chartTitle.Equals("tháng"))
                {
                    datasetTotal.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    datasetTotal.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }


            //dataTotalIncome
            var datasetIncome = new Guna.Charts.WinForms.GunaStackedBarDataset();
            datasetIncome.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#2E8B57"));
            datasetIncome.Label = "Thu nhập";

            foreach (var item in dataIncome)
            {
                if (chartTitle.Equals("tháng"))
                {
                    datasetIncome.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    datasetIncome.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }

            //dataTotalExpense
            var datasetExpense = new Guna.Charts.WinForms.GunaStackedBarDataset();
            datasetExpense.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#CD5C5C"));
            datasetExpense.Label = "Chi tiêu";

            foreach (var item in dataExpense)
            {
                if (chartTitle.Equals("tháng"))
                {
                    datasetExpense.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    datasetExpense.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }

            //chart.Datasets.Add(splineTotal);
            //chart.Datasets.Add(datasetTotal);
            //chart.Datasets.Add(datasetIncome);
            //chart.Datasets.Add(datasetExpense);
            //chart.Update();

            if (ckbBienDongChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(splineTotal);
            }

            if (ckbSoDuChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(datasetTotal);
            }

            if (ckbIncomeChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(datasetIncome);
            }

            if (ckbExpenseChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(datasetExpense);
            }
            chart.Update();
        }
        
        public static void DrawStackedBar1(Guna.Charts.WinForms.GunaChart chart,
            Bunifu.UI.WinForms.BunifuCheckBox ckbIncomeChart, Bunifu.UI.WinForms.BunifuCheckBox ckbExpenseChart,
            Bunifu.UI.WinForms.BunifuCheckBox ckbBienDongChart, Bunifu.UI.WinForms.BunifuCheckBox ckbSoDuChart,
            String chartTitle, Dictionary<DateTime, decimal> dataTotalIncome, Dictionary<DateTime, decimal> dataTotalExpense)
        {
            chart.Datasets.Clear();
            chart.YAxes.GridLines.Display = false;
            chart.Title.Text = "Biểu đồ thu nhập - chi tiêu theo " + chartTitle;
            chart.Title.Position = Guna.Charts.WinForms.TitlePosition.Bottom;
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Bottom;
            chart.XAxes.Display = true;
            chart.YAxes.Display = true;


            //một dictionary chứa dữ liệu là hiệu của thu nhập - chi tiêu
            Dictionary<DateTime, decimal> dataTotal = new Dictionary<DateTime, decimal>();
            foreach (var item in dataTotalIncome)
            {
                dataTotal.Add(item.Key, item.Value);
            }

            foreach (var item in dataTotalExpense)
            {
                if (dataTotal.ContainsKey(item.Key))
                {
                    dataTotal[item.Key] -= item.Value;
                }
                else if (ckbBienDongChart.Checked && ckbSoDuChart.Checked && !ckbIncomeChart.Checked && !ckbExpenseChart.Checked)
                {
                    dataTotal.Add(item.Key, -item.Value);
                }
            }
            //sort dataDay Dictionary by key
            dataTotal = dataTotal.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);


            //Spline of total
            var splineTotal = new Guna.Charts.WinForms.GunaSplineDataset();
            //fill color: LightBleueSky
            splineTotal.FillColor = System.Drawing.ColorTranslator.FromHtml("#87CEFA");
            //border color: LightBleueSky
            splineTotal.BorderColor = System.Drawing.ColorTranslator.FromHtml("#87CEFA");
            //border width: 2
            //ball color: LightBleueSky
            splineTotal.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#87CEFA"));
            //ball border color: LightBleueSky
            splineTotal.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#87CEFA"));
            //ball border width: 2
            splineTotal.PointBorderWidth = 1;
            splineTotal.BorderWidth = 1;
            splineTotal.Label = "Biến động số dư";

            foreach (var item in dataTotal)
            {
                if (chartTitle.Equals("tháng"))
                {
                    splineTotal.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    splineTotal.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }


            //dataTotal
            var datasetTotal = new Guna.Charts.WinForms.GunaStackedBarDataset();
            //add color to dataset: Web:LightGray
            datasetTotal.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#D3D3D3"));
            //name of dataset is: Tổng
            datasetTotal.Label = "Số dư";

            foreach (var item in dataTotal)
            {
                if (chartTitle.Equals("tháng"))
                {
                    datasetTotal.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    datasetTotal.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }


            //dataTotalIncome
            var datasetIncome = new Guna.Charts.WinForms.GunaStackedBarDataset();
            //add color to dataset: Web.SeaGreen
            datasetIncome.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#2E8B57"));
            //name of dataset is: Thu nhập
            datasetIncome.Label = "Thu nhập";

            foreach (var item in dataTotalIncome)
            {
                if (chartTitle.Equals("tháng"))
                {
                    datasetIncome.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    datasetIncome.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }




            //dataTotalExpense
            var datasetExpense = new Guna.Charts.WinForms.GunaStackedBarDataset();
            //add color to dataset: Web:IndianRed
            datasetExpense.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#CD5C5C"));
            //name of dataset is: Chi tiêu
            datasetExpense.Label = "Chi tiêu";

            foreach (var item in dataTotalExpense)
            {
                if (chartTitle.Equals("tháng"))
                {
                    datasetExpense.DataPoints.Add(item.Key.ToString("MM/yyyy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
                else
                {
                    datasetExpense.DataPoints.Add(item.Key.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("vi-VN")), (double)item.Value);
                }
            }

            if(ckbBienDongChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(splineTotal);
            }

            if (ckbSoDuChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(datasetTotal);
            }

            if (ckbIncomeChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(datasetIncome);
            }
            
            if(ckbExpenseChart.Checked)
            {
                chart.Update();
                chart.Datasets.Add(datasetExpense);
            }
            chart.Update();
        }
    }
}
