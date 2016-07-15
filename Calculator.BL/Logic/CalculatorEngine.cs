using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Calculator.Abstract;
using Calculator.BL.Repository;

namespace Calculator.BL.Logic
{
    public class CalculatorEngine : ICalculatorEngine
    {
        private readonly List<string> _supportedSigns;

        private readonly IOperationsRepository _repository;

        public CalculatorEngine(IOperationsRepository repository)
        {
            Contract.Requires<ArgumentNullException>(repository != null, "repository");

            _repository = repository;
            _supportedSigns = this._repository.AvailableOperations.Select(e => e.Sign).ToList();
            _supportedSigns =
            new List<string>(new string[] { "(", ")", "+", "-", "*", "/" }); //"^"
        }

        public float Calculate(string[] chain)
        {
            Contract.Requires<ArgumentNullException>(chain != null, "chain");

            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(chain);
            string str = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!_supportedSigns.Contains(str))
                {
                    stack.Push(str);
                    str = queue.Dequeue();
                }
                else
                {
                    float summ = 0;

                    IOperation operation = _repository.AvailableOperations.First(x => x.Sign == str);
                    if (operation == null)
                    {
                        throw new InvalidOperationException("There are now needed operation in repository");
                    }

                    if (operation.NumberOfParameters == 1)
                    {
                        operation.ExecuteOperation(Convert.ToSingle(stack.Pop()));
                    }
                    else if (operation.NumberOfParameters == 2)
                    {
                        float secondParam = Convert.ToSingle(stack.Pop());
                        float firstParam = Convert.ToSingle(stack.Pop());
                        operation.ExecuteOperation(firstParam, secondParam);
                    }

                    stack.Push(summ.ToString());
                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;
                }

            }
            return Convert.ToSingle(stack.Pop());
        }

          //switch (str)
          //              {

          //                  case "+":
          //                      {
          //                          decimal a = Convert.ToDecimal(stack.Pop());
          //                          decimal b = Convert.ToDecimal(stack.Pop());
          //                          summ = a + b;
          //                          break;
          //                      }
          //                  case "-":
          //                      {
          //                          decimal a = Convert.ToDecimal(stack.Pop());
          //                          decimal b = Convert.ToDecimal(stack.Pop());
          //                          summ = b - a;
          //                          break;
          //                      }
          //                  case "*":
          //                      {
          //                          decimal a = Convert.ToDecimal(stack.Pop());
          //                          decimal b = Convert.ToDecimal(stack.Pop());
          //                          summ = b * a;
          //                          break;
          //                      }
          //                  case "/":
          //                      {
          //                          decimal a = Convert.ToDecimal(stack.Pop());
          //                          decimal b = Convert.ToDecimal(stack.Pop());
          //                          summ = b / a;
          //                          break;
          //                      }
          //                  case "^":
          //                      {
          //                          decimal a = Convert.ToDecimal(stack.Pop());
          //                          decimal b = Convert.ToDecimal(stack.Pop());
          //                          summ = Convert.ToDecimal(Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a)));
          //                          break;
          //                      }
          //              }
    }
}


