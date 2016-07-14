
using Calculator.Abstract;

namespace Calculator.Operations
{
    class Subtraction : IOperation
    {
        public float ExecuteOperation(params float[] parameters)
        {
            return 0;
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
