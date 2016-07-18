using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Calculator.BL.Repository;

namespace Calculator.BL.Logic
{
    /// <summary>
    /// Class which provides parsing using postfix notation
    /// </summary>
    public class PostfixNotationParser : IParser
    {
        private readonly List<string> _supportedSigns;

        private readonly IOperationsRepository _repository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">Repository which contains all available operations</param>
        public PostfixNotationParser(IOperationsRepository repository)
        {
            Contract.Requires<ArgumentNullException>(repository != null, "repository");

            _repository = repository;
            _supportedSigns = extractSigns();
        }

        /// <summary>
        /// Parse input string into string array with postfix notation
        /// </summary>
        /// <param name="inputString">String to be parsed</param>
        /// <returns>Array in postfix notation</returns>
        public string[] Parse(string inputString)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(inputString), "inputString");

            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in separate(inputString))
            {
                if (_supportedSigns.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (getPriority(c) > getPriority(stack.Peek()))
                        {
                            stack.Push(c);
                        }
                        else
                        {
                            while (stack.Count > 0 && getPriority(c) <= getPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                    {
                        stack.Push(c);
                    }
                }
                else
                {
                    outputSeparated.Add(c);
                }
            }

            if (stack.Count > 0)
            {
                foreach (string c in stack)
                {
                    outputSeparated.Add(c);
                }
            }

            return outputSeparated.ToArray();
        }

        /// <summary>
        /// Separates input string
        /// </summary>
        /// <param name="inputString">String to be separate</param>
        /// <returns>A set of elements</returns>
        private IEnumerable<string> separate(string inputString)
        {
            int position = 0;
            while (position < inputString.Length)
            {
                string piece = string.Empty + inputString[position];
                if (!_supportedSigns.Contains(inputString[position].ToString()) ||
                    !_supportedSigns.Any(x => x.StartsWith(inputString[position].ToString())))
                {
                    if (Char.IsDigit(inputString[position]))
                    {
                        for (int i = position + 1; i < inputString.Length && (Char.IsDigit(inputString[i]) || inputString[i] == ','); i++)
                        {
                            piece += inputString[i];
                        }
                    }
                    else if (Char.IsLetter(inputString[position]))
                    {
                        for (int i = position + 1; i < inputString.Length && (Char.IsLetter(inputString[i])); i++)
                        {
                            piece += inputString[i];
                        }
                    }
                }

                yield return piece;
                position += piece.Length;
            }
        }

        private int getPriority(string s)
        {
            return _repository.AvailableOperations.First(e => e.Sign == s).Priority;
        }

        private List<string> extractSigns()
        {
            return this._repository.AvailableOperations.Select(e => e.Sign).ToList();
        }
    }
}
