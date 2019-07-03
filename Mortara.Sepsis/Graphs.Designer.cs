namespace Mortara.Sepsis
{
    partial class Graphs
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.indicatorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.samplesListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.indicatorChart)).BeginInit();
            this.SuspendLayout();
            // 
            // indicatorChart
            // 
            chartArea1.Name = "ChartArea1";
            this.indicatorChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.indicatorChart.Legends.Add(legend1);
            this.indicatorChart.Location = new System.Drawing.Point(0, 0);
            this.indicatorChart.Name = "indicatorChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "SBP";
            series1.XValueMember = "Hour";
            series1.YValueMembers = "SBP";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "DBP";
            series2.XValueMember = "Hour";
            series2.YValueMembers = "DBP";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "HeartRate";
            series3.XValueMember = "Hour";
            series3.YValueMembers = "HR";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "SBP_NS";
            series4.XValueMember = "Hour";
            series4.YValueMembers = "SBP_NS";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "DB_NS";
            series5.XValueMember = "Hour";
            series5.YValueMembers = "DBP_NS";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "HR_NS";
            series6.XValueMember = "Hour";
            series6.YValueMembers = "HR_NS";
            this.indicatorChart.Series.Add(series1);
            this.indicatorChart.Series.Add(series2);
            this.indicatorChart.Series.Add(series3);
            this.indicatorChart.Series.Add(series4);
            this.indicatorChart.Series.Add(series5);
            this.indicatorChart.Series.Add(series6);
            this.indicatorChart.Size = new System.Drawing.Size(1001, 589);
            this.indicatorChart.TabIndex = 0;
            this.indicatorChart.Text = "indicatorChart";
            // 
            // samplesListView
            // 
            this.samplesListView.HideSelection = false;
            this.samplesListView.Location = new System.Drawing.Point(1007, 13);
            this.samplesListView.Name = "samplesListView";
            this.samplesListView.Size = new System.Drawing.Size(170, 576);
            this.samplesListView.TabIndex = 4;
            this.samplesListView.UseCompatibleStateImageBehavior = false;
            this.samplesListView.View = System.Windows.Forms.View.List;
            // 
            // Graphs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 601);
            this.Controls.Add(this.samplesListView);
            this.Controls.Add(this.indicatorChart);
            this.Name = "Graphs";
            this.Text = "Septic Indicators";
            ((System.ComponentModel.ISupportInitialize)(this.indicatorChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart indicatorChart;
        private System.Windows.Forms.ListView samplesListView;
    }
}

