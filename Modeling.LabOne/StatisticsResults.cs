using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabOne
{
    public class StatisticsResults
    {
        public Double ExpectedValue { get; private set; }

        public Double Variance { get; private set; }

        public Double Deviation { get; private set; }

        public Int32 Period
        {
            get;
            private set;
        }

        public Int32 Aperiodic
        {
            get;
            private set;
        }

        public IList<Double> Cycle
        {
            get;
            private set;
        }

        public IList<Double> Appendix
        {
            get;
            private set;
        }


        public Boolean Calculate(IList<Double> realization)
        {
            try
            {
                this.ParseRealization(realization);
                ExpectedValue = ExpectedValueEstimation(Cycle);
                Variance = this.VarianceEstimation(Cycle);
                Deviation = this.DeviationEstimation(Cycle);
                Aperiodic = Appendix.Count;
                Period = Cycle.Count;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        private void ParseRealization(IList<Double> realization)
        {
            Double repeatedValue = realization.Last(); // last value it's the same as cycle start value
            Int32 cycleStartIndex = realization.IndexOf(repeatedValue);
            Appendix = realization.Take(cycleStartIndex).ToList();
            Cycle = realization.Skip(cycleStartIndex).Take(realization.Count - cycleStartIndex - 2).ToList();
        }

        private static Double ExpectedValueEstimation(IEnumerable<Double> sequence)
        {
            return sequence.Average();
        }

        private Double VarianceEstimation(IEnumerable<Double> sequence)
        {
            Double result;
            result = sequence.Average(x => Math.Pow(x - ExpectedValue, 2));
            return result;
        }


        private Double DeviationEstimation(ICollection<Double> sequence)
        {
            Double result;
            result = Math.Sqrt(sequence.Count / (Double)(sequence.Count - 1) * sequence.Average(x => Math.Pow(x - ExpectedValue, 2)));
            return result;
        }

    }
}
