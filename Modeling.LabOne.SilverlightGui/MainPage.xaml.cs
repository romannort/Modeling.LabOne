using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Modeling.LabOne.SilverlightGui
{
    public partial class MainPage : UserControl
    {

        public PlotModel MyPlotModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            var temp = new PlotModel("Square wave");
            var ls = new LineSeries("sin(x)+sin(3x)/3+sin(5x)/5+...");
            const int n = 10;
            for (double x = -10; x < 10; x += 1)
            {
                double y = 0;
                for (int i = 0; i < n; i++)
                {
                    int j = i * 2 + 1;
                    y += Math.Sin(j * x) / j;
                }
                ls.Points.Add(new DataPoint(x, y));
            }
            temp.Series.Add(ls);
            temp.Axes.Add(new LinearAxis(AxisPosition.Left, -4, 4));
            temp.Axes.Add(new LinearAxis(AxisPosition.Bottom));
            SamplePlot.Model = temp;
            //MyPlotModel = temp;         // this is raising the INotifyPropertyChanged event
        }
    }
}
