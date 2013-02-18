using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Modeling.LabOne.WpfGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void StartButtonPressed(object sender, RoutedEventArgs args)
        {
            try
            {
                Int32 aCoeff = ReadInt32(ACoeffInput);
                Int32 mCoeff = ReadInt32(MCoeffInput);
                Int32 startingNumber = ReadInt32(StartingValueInput);

                LemerGenerator lg = new LemerGenerator(aCoeff, mCoeff, startingNumber);
                lg.GenerateRealization();
                IList<Double> lemerRealization = lg.Realization;

                StatisticsResults sr = new StatisticsResults();
                sr.Calculate(lemerRealization);

                OutStatisticsResults(sr);
                DrawHistogram(sr);
            }
            catch (Exception e)
            {
                // ignore Go button press if exception thrown
            }
        }

        private void DrawHistogram(StatisticsResults sr)
        {
            HistogramData histogram = new HistogramData();
            histogram.Calculate(sr.Cycle);
            ICollection<HistogramData.IntervalData> intervals = histogram.Rows;
            const double targetProbabilityValue = (double) 1/20;
            PlotModel tempPlotModel = new PlotModel("Histogram");
            tempPlotModel.Axes.Add( new LinearAxis(AxisPosition.Left, 0.0, 0.1));
            tempPlotModel.Axes.Add(new LinearAxis(AxisPosition.Bottom, 0.0, 1.1));
            tempPlotModel.Series.Add( TargetProbabilityLine( targetProbabilityValue ) );       
            tempPlotModel.Series.Add( HistogramPlot(intervals));

            SamplePlot.Model = tempPlotModel;
        }

        private LineSeries TargetProbabilityLine(Double target)
        {
            LineSeries result = new LineSeries(OxyColors.Red, title: "1/m");
            result.Points.Add( new DataPoint(0.0, target));
            result.Points.Add(new DataPoint(1.0, target));
            return result;
        }

        private Series HistogramPlot(ICollection<HistogramData.IntervalData> intervals )
        {
            StairStepSeries result = new StairStepSeries();
            result.Title = "Histogram";
            //result. = intervals.ElementAt(0).UpperBound;
            //result.ItemsSource = intervals.ToList().ConvertAll(i => new ColumnItem(i.Height)).AsEnumerable();
            result.Points = intervals.ToList().ConvertAll(i => (IDataPoint)new DataPoint(i.LowerBound, i.Height)).ToList();
            return result;
        }

        static Int32 ReadInt32( TextBox tb )
        {
            return Int32.Parse(tb.Text);
        }

        private void OutStatisticsResults(StatisticsResults sr)
        {
            PeriodOutput.Text = sr.Period.ToString(CultureInfo.InvariantCulture);
            AperiodicOutput.Text = sr.Aperiodic.ToString(CultureInfo.InvariantCulture);
            ExpectedValueOutput.Text = sr.ExpectedValue.ToString(CultureInfo.InvariantCulture);
            DeviationValue.Text = sr.Deviation.ToString(CultureInfo.InvariantCulture);
            SigmaOutput.Text = sr.Variance.ToString(CultureInfo.InvariantCulture);
            PiValue.Text = sr.PI.ToString(CultureInfo.InvariantCulture);
        }
    }
}
