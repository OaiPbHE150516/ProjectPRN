using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWife.Chart
{
    public class StackedBar
    {
        public static void DrawStackedBar(Guna.Charts.WinForms.GunaChart chart, String chartTitle,
            Dictionary<string, decimal>  dataTotalIncome, Dictionary<string, decimal> dataTotalExpense, Dictionary<string, decimal> dataTotal)
        {

            chart.Datasets.Clear();
            chart.YAxes.GridLines.Display = false;
            chart.Title.Text = "MyChart";
            chart.Title.Position = Guna.Charts.WinForms.TitlePosition.Bottom;
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Bottom;
            chart.XAxes.Display = true;
            chart.YAxes.Display = true;

            //dataTotalIncome
            var datasetIncome = new Guna.Charts.WinForms.GunaStackedBarDataset();
            //add color to dataset: Web.SeaGreen
            datasetIncome.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#2E8B57"));
            //name of dataset is: Thu nhập
            datasetIncome.Label = "Thu nhập";

            foreach (var item in dataTotalIncome)
            {
                datasetIncome.DataPoints.Add(item.Key, (double)item.Value);
            }

            //dataTotalExpense
            var datasetExpense = new Guna.Charts.WinForms.GunaStackedBarDataset();
            //add color to dataset: Web:IndianRed
            datasetExpense.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#CD5C5C"));
            //name of dataset is: Chi tiêu
            datasetExpense.Label = "Chi tiêu";

            foreach (var item in dataTotalExpense)
            {
                datasetExpense.DataPoints.Add(item.Key, (double)item.Value);
            }

            //dataTotal
            var datasetTotal = new Guna.Charts.WinForms.GunaStackedBarDataset();
            //add color to dataset: Web:LightGray
            datasetTotal.FillColors = Guna.Charts.WinForms.ChartUtils.Colors(1, color: System.Drawing.ColorTranslator.FromHtml("#D3D3D3"));
            //name of dataset is: Tổng
            datasetTotal.Label = "Số dư";

            foreach (var item in dataTotal)
            {
                datasetTotal.DataPoints.Add(item.Key, (double)item.Value);
            }

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
                splineTotal.DataPoints.Add(item.Key, (double)item.Value);
            }

            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(splineTotal);
            chart.Datasets.Add(datasetTotal);
            chart.Datasets.Add(datasetIncome);
            chart.Datasets.Add(datasetExpense);
            chart.Update();

            //string[] months = { "January", "February", "March", "April", "May", "June", "July" };

            ////Chart configuration 
            //chart.YAxes.GridLines.Display = false;

            ////Create new barDataset
            //var barDataset = new Guna.Charts.WinForms.GunaBarDataset();
            //var r = new Random();
            //for (int i = 0; i < months.Length; i++)
            //{
            //    //random number
            //    int num = r.Next(10, 100);

            //    barDataset.DataPoints.Add(months[i], num);
            //}

            ////Create new areaDataset
            //var splineDataset = new Guna.Charts.WinForms.GunaSplineDataset();
            //splineDataset.FillColor = Guna.Charts.WinForms.ChartUtils.RandomColor();
            //splineDataset.BorderColor = splineDataset.FillColor;
            //for (int i = 0; i < months.Length; i++)
            //{
            //    //random number
            //    int num = r.Next(10, 100);

            //    splineDataset.DataPoints.Add(months[i], num);
            //}

            ////Add a new dataset to a chart.Datasets  
            //chart.Datasets.Add(splineDataset);
            //chart.Datasets.Add(barDataset);

            ////An update was made to re-render the chart
            //chart.Update();
        }
    }
}
