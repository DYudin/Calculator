
using System;
using Calculator.Abstract;

namespace Calculator.Operations
{
    internal class SinusCalculation : IOperation
    {
        private const int numberOfParameters = 1;

        public double ExecuteOperation(params double[] parameters)
        {
            if (parameters.Length != numberOfParameters)
            {
                throw new ArgumentException(string.Format("Invalid count of input parameters. Must be: {0}", numberOfParameters));
            }

            return Math.Sin(parameters[0]);
        }

        public int Priority
        {
            get { return 4; }
        }

        public string Sign
        {
            get { return "sin"; }
        }

        public int NumberOfParameters
        {
            get { return numberOfParameters; }
        }
    }
}
