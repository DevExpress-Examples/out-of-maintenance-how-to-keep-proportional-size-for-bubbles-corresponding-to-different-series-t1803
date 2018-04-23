using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using System.Linq;

namespace WindowsApplication12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            chartControl1.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Bubble);
            chartControl1.SeriesTemplate.ArgumentDataMember = "Date";
            chartControl1.SeriesDataMember = "Name";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value", "Weight" });
            chartControl1.DataSource = CreateTable();
            chartControl1.RefreshData();

        }

        private DataTable CreateTable()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Name", typeof(string));
            data.Columns.Add("Date", typeof(DateTime));
            data.Columns.Add("Value", typeof(double));
            data.Columns.Add("Weight", typeof(double));

            data.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(0), 7, 4 });
            data.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(1), 17, 1 });
            data.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(2), 11, 5 });

            data.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(0), 5, 6 });
            data.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1), 13, 9 });
            data.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(2), 9, 5 });

            data.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(0), 9, 4 });
            data.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1), 8, 6 });
            data.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(2), 7, 5 });

            return data;
        }

        private void chartControl1_BoundDataChanged(object sender, EventArgs e)
        {
            RecalculateSize((ChartControl)sender);

        }

        void RecalculateSize(ChartControl chart)
        {
            var infos = chart.Series.Cast<Series>().Where(s => s.View as BubbleSeriesView != null).Select(s => new SeriesInfo()
            {
                View = s.View as BubbleSeriesView,
                Min = s.Points.Cast<SeriesPoint>().Min(p => p.Values[1]),
                Max = s.Points.Cast<SeriesPoint>().Max(p => p.Values[1])
            });
            if (infos.Count() > 1)
            {
                double minValue = infos.Min(s => s.Min);
                double maxValue = infos.Max(s => s.Max);
                double valueDelta = maxValue - minValue;
                double minSize = infos.Min(s => s.View.MinSize);
                double maxSize = infos.Max(s => s.View.MaxSize);
                double sizeDelta = maxSize - minSize;
                foreach (var si in infos)
                {
                    si.View.MaxSize = maxSize;
                    si.View.MinSize = minSize;
                    si.View.MaxSize = maxSize - sizeDelta * (maxValue - si.Max) / valueDelta;
                    si.View.MinSize = minSize + sizeDelta * (si.Min - minValue) / valueDelta;
                    si.View.AutoSize = false;
                }
            }

        }

    }
    public class SeriesInfo
    {
        public BubbleSeriesView View { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}