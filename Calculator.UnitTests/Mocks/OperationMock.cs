using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Abstract;

namespace Calculator.UnitTests.Mocks
{
    public class OperationMock : IOperation
    {
        public double ExecuteOperation(params double[] parameters)
        {
            double result = 0; 

            switch (Sign)
            {
                case "+":
                    result = parameters[0] + parameters[1];
                    break;
                case "-":
                    result = parameters[0] - parameters[1];
                     break;
                case "*":
                    result = parameters[0] * parameters[1];
                     break;
                case "/":
                    result = parameters[0] / parameters[1];
                     break;
                case "sin":
                    result = Math.Sin(parameters[0]);
                     break;
            }

            return result;
        }

        public int Priority { get; set;  }

        public string Sign { get; set; }

        public int NumberOfParameters { get; set; }
    }
}
