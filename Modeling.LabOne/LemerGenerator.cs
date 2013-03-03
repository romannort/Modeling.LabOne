using System;
using System.Collections.Generic;
using System.Linq;

namespace Modeling.LabOne
{
    public class LemerGenerator
    {
        private IList<Double> realization;

        private readonly ulong aCoeff;

        private readonly ulong mCoeff;

        private readonly ulong startingValue;

        public IList<Double> Realization
        {
            get
            {
                return realization;
            }
            private set
            {

            }
        }

        public LemerGenerator(Int32 aCoeff, Int32 mCoeff, Int32 startingValue)
        {
            this.aCoeff = (ulong)aCoeff;
            this.mCoeff = (ulong)mCoeff;
            this.startingValue = (ulong)startingValue;
        }

        private UInt64 NextNumber(UInt64 previousNumber)
        {
            UInt64 result = (aCoeff * previousNumber) % mCoeff;
            return result;
        }

        public void GenerateRealization()
        {
            ICollection<ulong> lemerSequence = new List<ulong>();
            ulong currentNumber = this.NextNumber(startingValue);

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
