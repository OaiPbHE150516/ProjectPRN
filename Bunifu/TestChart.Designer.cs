namespace Bunifu
{
    partial class TestChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestChart));
            this.bunifuBarChart1 = new Bunifu.Charts.WinForms.ChartTypes.BunifuBarChart(this.components);
            this.bunifuChartCanvas1 = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            this.bunifuPieChart1 = new Bunifu.Charts.WinForms.ChartTypes.BunifuPieChart(this.components);
            this.bunifuChartCanvas2 = new Bunifu.Charts.WinForms.BunifuChartCanvas();
            this.SuspendLayout();
            // 
            // bunifuBarChart1
            // 
            this.bunifuBarChart1.BackgroundColor = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("bunifuBarChart1.BackgroundColor")));
            this.bunifuBarChart1.BorderColor = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("bunifuBarChart1.BorderColor")));
            this.bunifuBarChart1.BorderSkipped = null;
            this.bunifuBarChart1.BorderWidth = 0;
            this.bunifuBarChart1.Data = ((System.Collections.Generic.List<double>)(resources.GetObject("bunifuBarChart1.Data")));
            this.bunifuBarChart1.HoverBackgroundColor = System.Drawing.Color.Empty;
            this.bunifuBarChart1.HoverBorderColor = System.Drawing.Color.Empty;
            this.bunifuBarChart1.HoverBorderWidth = 0;
            this.bunifuBarChart1.Label = "Label here";
            this.bunifuBarChart1.TargetCanvas = this.bunifuChartCanvas1;
            // 
            // bunifuChartCanvas1
            // 
            this.bunifuChartCanvas1.AnimationDuration = 1000;
            this.bunifuChartCanvas1.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuad;
            this.bunifuChartCanvas1.BackColor = System.Drawing.SystemColors.Control;
            this.bunifuChartCanvas1.CanvasPadding = new System.Windows.Forms.Padding(0);
            this.bunifuChartCanvas1.Labels = new string[] {
        "Ăn uống",
        "Xăng xe",
        "Tiền nhà"};
            this.bunifuChartCanvas1.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            this.bunifuChartCanvas1.LegendDisplay = true;
            this.bunifuChartCanvas1.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuChartCanvas1.LegendForeColor = System.Drawing.Color.DarkGray;
            this.bunifuChartCanvas1.LegendFullWidth = true;
            this.bunifuChartCanvas1.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.bunifuChartCanvas1.LegendRevese = false;
            this.bunifuChartCanvas1.LegendRTL = false;
            this.bunifuChartCanvas1.Location = new System.Drawing.Point(2, 2);
            this.bunifuChartCanvas1.Name = "bunifuChartCanvas1";
            this.bunifuChartCanvas1.ShowXAxis = true;
            this.bunifuChartCanvas1.ShowYAxis = true;
            this.bunifuChartCanvas1.Size = new System.Drawing.Size(476, 478);
            this.bunifuChartCanvas1.TabIndex = 0;
            this.bunifuChartCanvas1.Title = "Báo cáo thu chi";
            this.bunifuChartCanvas1.TitleLineHeight = 1.2D;
            this.bunifuChartCanvas1.TitlePadding = 10;
            this.bunifuChartCanvas1.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.bunifuChartCanvas1.TooltipBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas1.TooltipFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas1.TooltipForeColor = System.Drawing.Color.WhiteSmoke;
            this.bunifuChartCanvas1.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            this.bunifuChartCanvas1.TooltipsEnabled = true;
            this.bunifuChartCanvas1.XAxesBeginAtZero = true;
            this.bunifuChartCanvas1.XAxesDrawTicks = true;
            this.bunifuChartCanvas1.XAxesFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas1.XAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas1.XAxesGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas1.XAxesGridLines = true;
            this.bunifuChartCanvas1.XAxesLabel = "";
            this.bunifuChartCanvas1.XAxesLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas1.XAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas1.XAxesLineWidth = 1;
            this.bunifuChartCanvas1.XAxesStacked = false;
            this.bunifuChartCanvas1.XAxesZeroLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas1.XAxesZeroLineWidth = 1;
            this.bunifuChartCanvas1.YAxesBeginAtZero = true;
            this.bunifuChartCanvas1.YAxesDrawTicks = true;
            this.bunifuChartCanvas1.YAxesFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas1.YAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas1.YAxesGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas1.YAxesGridLines = true;
            this.bunifuChartCanvas1.YAxesLabel = "";
            this.bunifuChartCanvas1.YAxesLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas1.YAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas1.YAxesLineWidth = 1;
            this.bunifuChartCanvas1.YAxesStacked = false;
            this.bunifuChartCanvas1.YAxesZeroLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas1.YAxesZeroLineWidth = 1;
            this.bunifuChartCanvas1.Load += new System.EventHandler(this.bunifuChartCanvas1_Load);
            // 
            // bunifuPieChart1
            // 
            this.bunifuPieChart1.BackgroundColor = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("bunifuPieChart1.BackgroundColor")));
            this.bunifuPieChart1.BorderColor = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("bunifuPieChart1.BorderColor")));
            this.bunifuPieChart1.BorderWidth = 0;
            this.bunifuPieChart1.Data = ((System.Collections.Generic.List<double>)(resources.GetObject("bunifuPieChart1.Data")));
            this.bunifuPieChart1.HoverBackgroundColor = System.Drawing.Color.Empty;
            this.bunifuPieChart1.HoverBorderColor = System.Drawing.Color.Empty;
            this.bunifuPieChart1.HoverBorderWidth = 0;
            this.bunifuPieChart1.Label = "Label here";
            this.bunifuPieChart1.TargetCanvas = this.bunifuChartCanvas2;
            // 
            // bunifuChartCanvas2
            // 
            this.bunifuChartCanvas2.AnimationDuration = 1000;
            this.bunifuChartCanvas2.AnimationType = Bunifu.Charts.WinForms.BunifuChartCanvas.AnimationOptions.easeOutQuart;
            this.bunifuChartCanvas2.BackColor = System.Drawing.SystemColors.Control;
            this.bunifuChartCanvas2.CanvasPadding = new System.Windows.Forms.Padding(0);
            this.bunifuChartCanvas2.Labels = new string[0];
            this.bunifuChartCanvas2.LegendAlignment = Bunifu.Charts.WinForms.BunifuChartCanvas.LegendAlignmentOptions.center;
            this.bunifuChartCanvas2.LegendDisplay = true;
            this.bunifuChartCanvas2.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuChartCanvas2.LegendForeColor = System.Drawing.Color.DarkGray;
            this.bunifuChartCanvas2.LegendFullWidth = true;
            this.bunifuChartCanvas2.LegendPosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.bunifuChartCanvas2.LegendRevese = false;
            this.bunifuChartCanvas2.LegendRTL = false;
            this.bunifuChartCanvas2.Location = new System.Drawing.Point(494, 60);
            this.bunifuChartCanvas2.Name = "bunifuChartCanvas2";
            this.bunifuChartCanvas2.ShowXAxis = true;
            this.bunifuChartCanvas2.ShowYAxis = true;
            this.bunifuChartCanvas2.Size = new System.Drawing.Size(656, 374);
            this.bunifuChartCanvas2.TabIndex = 1;
            this.bunifuChartCanvas2.Title = "Thu chi";
            this.bunifuChartCanvas2.TitleLineHeight = 1.2D;
            this.bunifuChartCanvas2.TitlePadding = 10;
            this.bunifuChartCanvas2.TitlePosition = Bunifu.Charts.WinForms.BunifuChartCanvas.PositionOptions.top;
            this.bunifuChartCanvas2.TooltipBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas2.TooltipFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas2.TooltipForeColor = System.Drawing.Color.WhiteSmoke;
            this.bunifuChartCanvas2.TooltipMode = Bunifu.Charts.WinForms.BunifuChartCanvas.TooltipModeOptions.nearest;
            this.bunifuChartCanvas2.TooltipsEnabled = true;
            this.bunifuChartCanvas2.XAxesBeginAtZero = true;
            this.bunifuChartCanvas2.XAxesDrawTicks = true;
            this.bunifuChartCanvas2.XAxesFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas2.XAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas2.XAxesGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas2.XAxesGridLines = true;
            this.bunifuChartCanvas2.XAxesLabel = "";
            this.bunifuChartCanvas2.XAxesLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas2.XAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas2.XAxesLineWidth = 1;
            this.bunifuChartCanvas2.XAxesStacked = false;
            this.bunifuChartCanvas2.XAxesZeroLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas2.XAxesZeroLineWidth = 1;
            this.bunifuChartCanvas2.YAxesBeginAtZero = true;
            this.bunifuChartCanvas2.YAxesDrawTicks = true;
            this.bunifuChartCanvas2.YAxesFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas2.YAxesForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas2.YAxesGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas2.YAxesGridLines = true;
            this.bunifuChartCanvas2.YAxesLabel = "";
            this.bunifuChartCanvas2.YAxesLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuChartCanvas2.YAxesLabelForeColor = System.Drawing.SystemColors.ControlText;
            this.bunifuChartCanvas2.YAxesLineWidth = 1;
            this.bunifuChartCanvas2.YAxesStacked = false;
            this.bunifuChartCanvas2.YAxesZeroLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuChartCanvas2.YAxesZeroLineWidth = 1;
            // 
            // TestChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 736);
            this.Controls.Add(this.bunifuChartCanvas2);
            this.Controls.Add(this.bunifuChartCanvas1);
            this.Name = "TestChart";
            this.Text = "TestChart";
            this.ResumeLayout(false);

        }

        #endregion

        private Charts.WinForms.ChartTypes.BunifuBarChart bunifuBarChart1;
        private Charts.WinForms.BunifuChartCanvas bunifuChartCanvas1;
        private Charts.WinForms.ChartTypes.BunifuPieChart bunifuPieChart1;
        private Charts.WinForms.BunifuChartCanvas bunifuChartCanvas2;
    }
}