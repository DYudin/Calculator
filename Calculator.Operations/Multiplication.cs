﻿
using Calculator.Abstract;

namespace Calculator.Operations
{
    class Multiplication : IOperation
    {
        public float ExecuteOperation(params float[] parameters)
        {
            return 0;
        }

        public int Priority
        {
            get { return 3; }
        }

        public string Sign
        {
            get { return "*"; }
        }

        public int NumberOfParameters
        {
            get { return 2; }
        }
    }
}