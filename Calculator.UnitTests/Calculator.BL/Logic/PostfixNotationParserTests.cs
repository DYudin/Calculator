using System;
using System.Collections.Generic;
using Calculator.Abstract;
using Calculator.BL.Logic;
using Calculator.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.UnitTests.Calculator.BL.Logic
{
    [TestClass]
    public class PostfixNotationParserTests
    {
        [TestMethod]
        public void PostfixNotationParserCtor_GoodValues_Created()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            testInitialize(operationsRepositoryMock);
            PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);

            target.GetType();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "null repository parameter was specified")]
        public void PostfixNotationParserCtor_NullRepository_Thrown()
        {
            PostfixNotationParser target = new PostfixNotationParser(null);
            target.GetType();
        }

        [TestMethod]
        public void PostfixNotationParserParse_GoodValues_Calculated()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            testInitialize(operationsRepositoryMock);
            PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);
            string inputString = "(2+2)*4-6/3";
            string[] expectedArray = new string[] { "2", "2", "+", "4", "*", "6", "3", "/", "-" };

            string[] actualArray = target.Parse(inputString);

            CollectionAssert.AreEqual(expectedArray, actualArray, "Unexpected result !!!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "null or empty string parameter was specified")]
        public void PostfixNotationParserParse_EmptyInputString_Thrown()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            testInitialize(operationsRepositoryMock);
            PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);
            string testString = "";

            target.Parse(testString);
        }
        
        [TestMethod]
        public void PostfixNotationParserParse_GoodDecimalValues_Calculated()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            testInitialize(operationsRepositoryMock);
            PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);
            string inputString = "2+2,5*4,2";
            string[] expectedArray = new string[] {"2", "2,5", "4,2", "*", "+" };

            string[] actualArray = target.Parse(inputString);

            CollectionAssert.AreEqual(expectedArray, actualArray, "Unexpected result !!!");
        }

        [TestMethod]
        public void PostfixNotationParserParse_FuncWithOneParam_Calculated()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            testInitialize(operationsRepositoryMock);
            PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);
            string inputString = "sin45";
            string[] expectedArray = new string[] { "45", "sin" };

            string[] actualArray = target.Parse(inputString);

            CollectionAssert.AreEqual(expectedArray, actualArray, "Unexpected result !!!");
        }
        
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "invalid string parameter was specified")]
        //public void PostfixNotationParserConvertThrowsOnInvalidInputTest()
        //{
        //    OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
        //    testInitialize(operationsRepositoryMock);
        //    PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);
        //    string inputString = "(2+2*4-6/3";

        //    string[] actualArray = invokeConvertToPostfixNotation(target, inputString);
        //}
        
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "invalid string parameter was specified")]
        //public void PostfixNotationParserConvertThrowsOnInvalidInputTest_2()
        //{
        //    OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
        //    testInitialize(operationsRepositoryMock);
        //    PostfixNotationParser target = new PostfixNotationParser(operationsRepositoryMock);
        //    string inputString = "2+2*4.-6/3";

        //    string[] actualArray = invokeConvertToPostfixNotation(target, inputString);
        //}

        internal static void testInitialize(OperationsRepositoryMock operationsRepositoryMock)
        {
            operationsRepositoryMock.AvailableOperations = new List<IOperation>()
            {
                new OperationMock() {NumberOfParameters = 2, Priority = 1, Sign = "+"},
                new OperationMock() {NumberOfParameters = 2, Priority = 1, Sign = "-"},
                new OperationMock() {NumberOfParameters = 2, Priority = 2, Sign = "*"},
                new OperationMock() {NumberOfParameters = 2, Priority = 2, Sign = "/"},
                new OperationMock() {NumberOfParameters = 0, Priority = 0, Sign = "("},
                new OperationMock() {NumberOfParameters = 0, Priority = 0, Sign = ")"},
                new OperationMock() {NumberOfParameters = 1, Priority = 4, Sign = "sin"}
            };
        }
    }
}