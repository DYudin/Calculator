
using Calculator.Abstract;

namespace Calculator.Operations
{
    internal class Subtraction : IOperation
    {
        public float ExecuteOperation(params float[] parameters)
        {
            return parameters[0] - parameters[1];
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
            get { return 2; }
        }
    }
}
