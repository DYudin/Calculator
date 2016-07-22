using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Abstract;
using Calculator.BL.Repository;

namespace Calculator.BL.Logic
{
    /// <summary>
    /// Class which provides calculating with the supported operations
    /// </summary>
    public class CalculatorEngine : ICalculatorEngine
    {
        private readonly List<string> _supportedSigns;
        private readonly IOperationsRepository _repository;

        public CalculatorEngine(IOperationsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "shouldn't be null");
            }

            _repository = repository;
            _supportedSigns = extractSigns();
        }

        /// <summary>
        /// Calculates result from a set of operations and operands
        /// </summary>
        /// <param name="inputArray">Array with operations and operands in postfix notation</param>
        /// <returns>Result of calculation</returns>
        public double Calculate(string[] inputArray)
        {
            if (inputArray == null)
            {
                throw new ArgumentNullException("inputArray", "shouldn't be null");
            }

            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(inputArray);
            string str = queue.Dequeue();

            while (queue.Count >= 0)
            {
                double temp;
                if (!_supportedSigns.Contains(str) && Double.TryParse(str, out temp))
                {
                    stack.Push(str);
                    str = queue.Dequeue();
                }
                else
                {
                    double summ = 0;

                    IOperation operation = _repository.AvailableOperations.FirstOrDefault(x => x.Sign == str);
                    if (operation == null)
                    {
                        throw new InvalidOperationException(string.Format("The operation: {0} is not supported", str));
                    }

                    validate(operation.NumberOfParameters, stack.Count);

                    if (operation.NumberOfParameters == 1)
                    {
                        summ = operation.ExecuteOperation(Convert.ToDouble(stack.Pop()));
                    }
                    else if (operation.NumberOfParameters == 2)
                    {
                        double secondParam = Convert.ToDouble(stack.Pop());
                        double firstParam = Convert.ToDouble(stack.Pop());
                        summ = operation.ExecuteOperation(firstParam, secondParam);
                    }

                    stack.Push(summ.ToString());
                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;
                }
            }

            return Convert.ToDouble(stack.Pop());
        }

        private List<string> extractSigns()
        {
            return this._repository.AvailableOperations.Select(e => e.Sign).ToList();
        }

        private void validate(int operandsForOperation, int stackLength)
        {
            if (operandsForOperation > stackLength)
            {
                throw new ArgumentException("Input string hasn't needed count of operands");
            }
        }
    }
}