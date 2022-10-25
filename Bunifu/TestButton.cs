using Bunifu.UI.WinForms.BunifuButton;
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
    public partial class TestButton : Form
    {
        public TestButton()
        {
            InitializeComponent();
        }

        List<BunifuButton2> bunifuButton2s = new List<BunifuButton2>();

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton21);
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton22);

        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton23);

        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton24);

        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton25);

        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton26);

        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton27);

        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            chooseBtn(bunifuButton28);

        }

        private void TestButton_Load(object sender, EventArgs e)
        {
            //add all button in form to list
            bunifuButton2s.Add(bunifuButton21);
            bunifuButton2s.Add(bunifuButton22);
            bunifuButton2s.Add(bunifuButton23);
            bunifuButton2s.Add(bunifuButton24);
            bunifuButton2s.Add(bunifuButton25);
            bunifuButton2s.Add(bunifuButton26);
            bunifuButton2s.Add(bunifuButton27);
            bunifuButton2s.Add(bunifuButton28);
        }
        private void chooseBtn(BunifuButton2 btnDisable)
        {
            foreach (BunifuButton2 btn in bunifuButton2s)
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
    }
}
