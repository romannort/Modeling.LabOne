using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabOne.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("aCoeff");
            Int32 aCoeff = ReadInt32();
            System.Console.WriteLine("mCoeff");
            Int32 mCoeff = ReadInt32();
            System.Console.WriteLine("startingNumber");
            Int32 startingNumber = ReadInt32();

            LemerGenerator lg = new LemerGenerator(aCoeff, mCoeff, startingNumber);
            lg.GenerateRealization();
            IList<Double> lemerRealization = lg.Realization;

            StatisticsResults sr = new StatisticsResults();
            sr.Calculate(lemerRealization);

            OutStatitisticsResults(sr);
        }

        static Int32 ReadInt32()
        {
            return Int32.Parse(System.Console.ReadLine());
        }

        static void OutStatitisticsResults(StatisticsResults sr)
        {
            System.Console.WriteLine("Period {0}", sr.Period);
            System.Console.WriteLine("Aperiodic {0}", sr.Aperiodic);
            System.Console.WriteLine("Expected Value {0}", sr.ExpectedValue);
            System.Console.WriteLine("Deviation {0}", sr.Deviation);
            System.Console.WriteLine("Variance {0}", sr.Variance);
            System.Console.WriteLine("PI {0}", sr.PI);
        }
    }
}
