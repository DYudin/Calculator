using System;
using Calculator.Abstract;

namespace Calculator.Operations
{
    internal class LeftBracket : IOperation
    {
        public double ExecuteOperation(params double[] parameters)
        {
            throw new NotSupportedException();
        }

        public int Priority
        {
            get { return 0; }
        }

        public string Sign
        {
            get { return "("; }
        }

        public int NumberOfParameters
        {
            get { return 0; }
        }
    }
}