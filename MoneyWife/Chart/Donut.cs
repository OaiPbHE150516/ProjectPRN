using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWife.Chart
{
    public class Donut
    {
        public static void DrawDonut(Guna.Charts.WinForms.GunaChart chart, String chartTitle, Dictionary<String, double> data)
        {
            //clear chart
            chart.Datasets.Clear();
            //Chart configuration
            chart.Title.Text = chartTitle;
            chart.Title.Position = Guna.Charts.WinForms.TitlePosition.Bottom;
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            chart.XAxes.Display = false;
            chart.YAxes.Display = false;

            var dataset = new Guna.Charts.WinForms.GunaDoughnutDataset();

            //duyệt qua key và value của dictionary, add vào dataset
            foreach (var item in data)
            {
                dataset.DataPoints.Add(item.Key + " " + item.Value + "%", item.Value);
            }

            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(dataset);

            //An update was made to re-render the chart
            chart.Update();
        }
    }
}
