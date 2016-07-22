using System;
using System.Collections.Generic;
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
        private const string strLeftBracket = "(";
        private const string strRightBracket = ")";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">Repository which contains all available operations</param>
        public PostfixNotationParser(IOperationsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "shouldn't be null");
            }

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
            if (string.IsNullOrWhiteSpace(inputString))
            {
                throw new ArgumentException("shouldn't be null or white space", "inputString");
            }

            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string currentElement in separate(inputString))
            {
                if (_supportedSigns.Contains(currentElement) || currentElement.Equals(strLeftBracket) ||
                    currentElement.Equals(strRightBracket))
                {
                    if (stack.Count > 0 && !currentElement.Equals(strLeftBracket))
                    {
                        if (currentElement.Equals(strRightBracket))
                        {
                            string s = stack.Pop();
                            while (s != strLeftBracket)
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (getPriority(currentElement) > getPriority(stack.Peek()))
                        {
                            stack.Push(currentElement);
                        }
                        else
                        {
                            while (stack.Count > 0 && getPriority(currentElement) <= getPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(currentElement);
                        }
                    }
                    else
                    {
                        stack.Push(currentElement);
                    }
                }
                else
                {
                    outputSeparated.Add(currentElement);
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
                        for (int i = position + 1;
                            i < inputString.Length && (Char.IsDigit(inputString[i]) || inputString[i] == ',');
                            i++)
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

        private int getPriority(string element)
        {
            // Brackets has a higher priority over other operations
            var priority = element.Equals(strLeftBracket) || element.Equals(strRightBracket)
                ? 0
                : _repository.AvailableOperations.First(e => e.Sign == element).Priority;

            return priority;
        }

        private List<string> extractSigns()
        {
            return this._repository.AvailableOperations.Select(e => e.Sign).ToList();
        }
    }
}