using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bunifu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Factory", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Note", typeof(string));
            dt.Rows.Add("ProductA", "Factory1", "5000", "Note1");
            dt.Rows.Add("ProductB", "Factory1", "6000", "Note1");
            dt.Rows.Add("ProductC", "Factory2", "6000", "Note2");
            dt.Rows.Add("ProductD", "Factory3", "7000", "Note2");
            dt.Rows.Add("ProductE", "Factory3", "7000", "Note3");

            this.dataGridView1.DataSource = dt;
        }

        DataTable dt = new DataTable();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView1[column, row];
            DataGridViewCell cell2 = dataGridView1[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Bottom = dataGridView1.AdvancedCellBorderStyle.Bottom;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (e.RowIndex >= 0)
            {
                //DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //MessageBox.Show(row.Cells[0].Value.ToString());
                dataGridView1[e.ColumnIndex, e.RowIndex -1].Selected = true;
                dataGridView1[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }
    }
}
