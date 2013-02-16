using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabOne
{
    public class LemerGenerator
    {
        private IList<Double> realization;

        private readonly Int32 aCoeff;

        private readonly Int32 mCoeff;

        private readonly Int32 startingValue;

        public LemerGenerator(Int32 aCoeff, Int32 mCoeff, Int32 startingValue)
        {
            this.aCoeff = aCoeff;
            this.mCoeff = mCoeff;
            this.startingValue = startingValue;
        }

        private Int32 NextNumber(Int32 previousNumber)
        {
            Int32 result = (aCoeff * previousNumber) % mCoeff;
            return result;
        }

        private void GenerateRealization()
        {
            IList<Int32> lemerSequence = new List<Int32>();
            Int32 currentNumber = this.NextNumber(startingValue);

            while(true)
            {
                if(lemerSequence.Contains(currentNumber))
                {
                    lemerSequence.Add(currentNumber);
                    break; // cycle in sequence
                }
                lemerSequence.Add(currentNumber);
                currentNumber = this.NextNumber(currentNumber);
            }
            this.realization = lemerSequence.ToList().ConvertAll(x => x / (Double)this.mCoeff );
        }

    }
}
