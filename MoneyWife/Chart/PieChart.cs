using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWife.Chart
{
    public class PieChart
    {
        public static void DrawPieChart(Guna.Charts.WinForms.GunaChart chart)
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            //chart configuration
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            chart.XAxes.Display = false;
            chart.YAxes.Display = false;

            //create a new dataset
            var dataset = new Guna.Charts.WinForms.GunaPieDataset();
            var r = new Random();
            //for (int i = 0; i < months.Length; i++)
            //{
            //    //random number
            //    int num = r.Next(10, 100);
            //    dataset.DataPoints.Add(months[i], num);
            //}

            //tạo ngẫu nhiên một mảng giá trị từ 10 đến 100, mảng có 12 phần tử, sao cho tổng các phần tử bằng 100
            int[] nums = new int[12];
            int sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = r.Next(10, 100);
                sum += nums[i];
            }
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = nums[i] * 100 / sum;
            }
            //sort mảng tăng dần
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        int temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                    }
                }
            }

            for (int i = 0; i < months.Length; i++)
            {
                //random number
                int num = nums[i];
                dataset.DataPoints.Add(months[i], num);
            }


            //sort the dataset by value in descending order

            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(dataset);

            //An update was made to re-render the chart
            chart.Update();
        }
    }
}
