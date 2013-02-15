using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabOne
{
    public class Core
    {

        private const Int32 targetCycleLength = 10000;

        private Int32 aCoeff;

        private Int32 mCoeff;

        private Int32 startNumber;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aCoeff"></param>
        /// <param name="mCoeff"></param>
        public Core(Int32 aCoeff, Int32 mCoeff, Int32 startNumber)
        {
            this.aCoeff = aCoeff;
            this.mCoeff = mCoeff;
            this.startNumber = startNumber;
        }


        public 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previousNumber"></param>
        /// <returns></returns>
        private Int32 LemerCalculation(Int32 previousNumber)
        {
            Int32 result = (aCoeff * previousNumber) % mCoeff;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startingNumber"></param>
        /// <returns></returns>
        private ICollection<Int32> GenerateSequence(Int32 startingNumber)
        {
            Int32 nextNumber = LemerCalculation(startingNumber);
            if (nextNumber == startingNumber)
            {
                return default(ICollection<Int32>);
            }
            IList<Int32> sequence = new List<Int32>();
            
            while ( true )
            {
                if ( sequence.Contains(nextNumber) )
                {
                    break;
                }
                nextNumber = LemerCalculation(nextNumber);
                sequence.Add(nextNumber);
            }

            if (!IsTargetLengthReached(sequence))
            {
                throw new Exception("Target length not reached");
            }
            // cut down sequence
            //Int32 firstIndex = sequence.IndexOf(nextNumber);
            ICollection<Int32> appendix = sequence.TakeWhile(x => x != nextNumber).ToList();
            ICollection<Int32> result = sequence.Except(appendix.AsEnumerable()).ToList();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private Int32 FindCycleLength(IList<Int32> sequence)
        {
            return default(Int32);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private Boolean IsTargetLengthReached(IList<Int32> sequence)
        {
            Int32 currentCycleLength = FindCycleLength(sequence);
            if (currentCycleLength == targetCycleLength)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private Double ExpectedValueEstimation(IList<Int32> sequence)
        {
            return sequence.Average();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private Double VarianceEstimation(IList<Int32> sequence)
        {
            Double expectedValueEstimation = ExpectedValueEstimation(sequence);
            Double result = 0.0;
            result = sequence.Average( x => Math.Pow( x - expectedValueEstimation, 2));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private Double DeviationEstimation(IList<Int32> sequence)
        {
            Double result = 0.0;
            Double average = sequence.Average();
            result = Math.Sqrt(sequence.Count / (sequence.Count - 1) * sequence.Average(x => Math.Pow(x - average, 2)));
            return result;
        }

    }
}
