
using Calculator.Abstract;

namespace Calculator.Operations
{
    class SinusCalculation : IOperation
    {
        public float ExecuteOperation(params float[] parameters)
        {
            return 0;
        }

        public int Priority
        {
            get { return 1; }
        }

        public string Sign
        {
            get { return "sin"; }
        }

        public int numberOfParameters
        {
            get { return 1; }
        }
    }
}
