using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Modeling.LabOne
{
    public class HistogramData
    {
        
        public class IntervalData
        {
            public Double LowerBound
            {
                get;
                set;
            }

            public Double UpperBound
            {
                get;
                set;
            }

            public Int32 Hits
            {
                get;
                set;
            }

            public Double Height
            {
                get;
                set;
            }
        }
        
        
        private Double variationRange;

        private const Int32 IntervalNumber = 20;

        private Double intervalLength;

        public ICollection<IntervalData> Rows
        {
            get;
            private set;
        }


        public void Calculate(ICollection<Double> realization )
        {
            Rows = new Collection<IntervalData>();
            this.variationRange = realization.Max() - realization.Min();
            this.intervalLength = this.variationRange/IntervalNumber;
            this.FindIntervalHits(realization);
        }


        private void FindIntervalHits(ICollection<double> realization )
        {
            for( int i = 1; i < IntervalNumber + 1; ++i)
            {
                Double upperBound = i * this.intervalLength;
                Double lowerBound = (i-1) * this.intervalLength;

                Int32 hits = realization.Count(x => x > lowerBound && x <= upperBound);
                Rows.Add( new IntervalData
                              {
                                  Hits = hits,
                                  LowerBound = lowerBound,
                                  UpperBound = upperBound,
                                  Height = (Double)hits/realization.Count
                              });
            }
        }
    }
}
