﻿using System;
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
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.StartButton.Focus();
        }


        private void StartButtonPressed(object sender, RoutedEventArgs args)
        {
            try
            {
                Int32 aCoeff = ReadInt32(ACoeffInput);
                Int32 mCoeff = ReadInt32(MCoeffInput);
                if (mCoeff == 0)
                {
                    throw new Exception("M coefficient must be a positive value!");
                }
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
                MessageWindow mw = new MessageWindow(this, e.Message);
                mw.ShowDialog();
                
            }
        }

        private void DrawHistogram(StatisticsResults sr)
        {
            HistogramData histogram = new HistogramData();
            histogram.Calculate(sr.Cycle);
            ICollection<HistogramData.IntervalData> intervals = histogram.Rows;
            const double targetProbabilityValue = (double)1/20;
            PlotModel tempPlotModel = new PlotModel("Histogram")
                {
                    LegendPlacement = LegendPlacement.Outside,
                    LegendPosition = LegendPosition.RightTop,
                    LegendOrientation = LegendOrientation.Vertical
                };
            
            ColumnSeries columnSeries = (ColumnSeries)HistogramPlot(intervals);
            tempPlotModel.Axes.Add(new LinearAxis(AxisPosition.Left, 0.0));
            tempPlotModel.Axes.Add(new CategoryAxis
            {
                LabelField = "Value",
                IntervalLength = targetProbabilityValue,
                ItemsSource = columnSeries.ItemsSource,
                GapWidth = 0.0
            });

            tempPlotModel.Series.Add( columnSeries );
            tempPlotModel.Series.Add(TargetProbabilityLine(targetProbabilityValue));       
            
            SamplePlot.Model = tempPlotModel;
        }

        private LineSeries TargetProbabilityLine(Double target)
        {
            LineSeries result = new LineSeries(OxyColors.Red, title: "1/m");
            result.Points.Add(new DataPoint(-1.0, target));
            result.Points.Add(new DataPoint( 20.0, target));
            return result;
        }

        private Series HistogramPlot(IEnumerable<HistogramData.IntervalData> intervals )
        {
            ColumnSeries columnSeries = new ColumnSeries
                {
                    IsStacked = false,
                    BaseValue = 0,
                    ColumnWidth = 0.05,
                    ValueField = "Value",
                    ItemsSource = intervals.ToList().ConvertAll(x => new ColumnItem
                        {
                            Value = x.Height
                        })
                };

            return columnSeries;
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
            PiValueReal.Text = (Math.PI/4).ToString(CultureInfo.InvariantCulture);
        }
    }
}
