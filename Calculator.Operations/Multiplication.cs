
using Calculator.Abstract;

namespace Calculator.Operations
{
    internal class Multiplication : IOperation
    {
        public float ExecuteOperation(params float[] parameters)
        {
            return parameters[0] * parameters[1];
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
