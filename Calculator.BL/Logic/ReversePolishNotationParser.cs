using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Calculator.BL.Repository;

namespace Calculator.BL.Logic
{
    public class ReversePolishNotationParser : IParser
    {
        private readonly List<string> _supportedSigns;

        private readonly IOperationsRepository _repository;

        public ReversePolishNotationParser(IOperationsRepository repository)
        {
            Contract.Requires<ArgumentNullException>(repository != null, "repository");

            _repository = repository;
            _supportedSigns = this._repository.AvailableOperations.Select(e => e.Sign).ToList();
            _supportedSigns =
            new List<string>(new string[] { "(", ")", "+", "-", "*", "/" }); //"^"
        }

        public string[] Parse(string inputString)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrWhiteSpace(inputString), "inputString");

            string[] separatedString = convertToPostfixNotation(inputString);

            return separatedString;
        }

        private string[] convertToPostfixNotation(string input)
        {
            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in separate(input))
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
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && getPriority(c) <= getPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                        stack.Push(c);
                }
                else
                    outputSeparated.Add(c);
            }
            if (stack.Count > 0)
                foreach (string c in stack)
                    outputSeparated.Add(c);

            return outputSeparated.ToArray();
        }

        private IEnumerable<string> separate(string inputString)
        {
            int pos = 0;
            while (pos < inputString.Length)
            {
                string s = string.Empty + inputString[pos];
                if (!_supportedSigns.Contains(inputString[pos].ToString()))
                {
                    if (Char.IsDigit(inputString[pos]))
                        for (int i = pos + 1; i < inputString.Length &&
                            (Char.IsDigit(inputString[i]) || inputString[i] == ',' || inputString[i] == '.'); i++)
                            s += inputString[i];
                    else if (Char.IsLetter(inputString[pos]))
                        for (int i = pos + 1; i < inputString.Length &&
                            (Char.IsLetter(inputString[i]) || Char.IsDigit(inputString[i])); i++)
                            s += inputString[i];
                }
                yield return s;
                pos += s.Length;
            }
        }

        private byte getPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }
    }
}
