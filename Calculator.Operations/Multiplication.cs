
using System;
using Calculator.Abstract;

namespace Calculator.Operations
{
    internal class Multiplication : IOperation
    {
        private const int numberOfParameters = 2;

        public double ExecuteOperation(params double[] parameters)
        {
            if (parameters.Length != numberOfParameters)
            {
                throw new ArgumentException(string.Format("Invalid count of input parameters. Must be: {0}", numberOfParameters));
            }

            return parameters[0] * parameters[1];
        }

        public int Priority
        {
            get { return 2; }
        }

        public string Sign
        {
            get { return "*"; }
        }

        public int NumberOfParameters
        {
            get { return numberOfParameters; }
        }
    }
}
