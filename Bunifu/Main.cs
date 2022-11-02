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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 0;
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 1;
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            bnfPageMain.PageIndex = 2;
        }

        private void btnMeCho_Click(object sender, EventArgs e)
        {
            //disable this button
            btnMeCho.Enabled = false;
            //enable other button 
            btnTienLuong.Enabled = true;
            btnTienThuong.Enabled = true;
        }

        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            //disable this button
            btnTienLuong.Enabled = false;
            //enable other button
            btnMeCho.Enabled = true;
            btnTienThuong.Enabled = true;
            //focus on content
            txtContentIncome.Focus();
            //mouse over on content
            btnTienLuong.AllowAnimations = false;
            
            


        }

        private void btnTienThuong_Click(object sender, EventArgs e)
        {
            //disable this button
            btnTienThuong.Enabled = false;
            //enable other button
            btnMeCho.Enabled = true;
            btnTienLuong.Enabled = true;
        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {

        }

    }
}
