using System;
using Calculator.BL.Logic;
using Calculator.UnitTests.Calculator.BL.Logic;
using Calculator.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.UnitTests.Calculator.BL.Logic
{
    [TestClass]
    public class CalculatorEngineTests
    {
        [TestMethod]
        public void CalculatorEngineCtor_GoodValues_Created()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            PostfixNotationParserTests.testInitialize(operationsRepositoryMock);
            CalculatorEngine target = new CalculatorEngine(operationsRepositoryMock);

            target.GetType();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "null repository parameter was specified")]
        public void CalculatorEngineCtor_NullRepository_Thrown()
        {
            CalculatorEngine target = new CalculatorEngine(null);
            target.GetType();
        }

        [TestMethod]
        public void CalculatorEngineCalculate_GoodValues_Calculated()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            PostfixNotationParserTests.testInitialize(operationsRepositoryMock);
            CalculatorEngine target = new CalculatorEngine(operationsRepositoryMock);
            string[] testArray = new string[] { "2", "2", "+", "4", "*", "6", "3", "/", "-" };
            double expectedResult = 14;

            double actualResult = target.Calculate(testArray);

            Assert.AreEqual(expectedResult, actualResult, "Unexpected result !!!");
        }

        [TestMethod]
        public void CalculatorEngineCalculate_GoodValues_Calculated2()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            PostfixNotationParserTests.testInitialize(operationsRepositoryMock);
            CalculatorEngine target = new CalculatorEngine(operationsRepositoryMock);
            string[] testArray = new string[] { "1", "2", "+", "4", "*", "3", "+" };
            double expectedResult = 15;

            double actualResult = target.Calculate(testArray);

            Assert.AreEqual(expectedResult, actualResult, "Unexpected result !!!");
        }

        [TestMethod]
        public void CalculatorEngineCalculate_FuncWithOneParam_Calculated()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            PostfixNotationParserTests.testInitialize(operationsRepositoryMock);
            CalculatorEngine target = new CalculatorEngine(operationsRepositoryMock);
            string[] testArray = new string[] { "45", "sin"};
            double expectedResult = 0.850903524534118;

            double actualResult = target.Calculate(testArray);

            Assert.AreEqual(expectedResult, actualResult, "Unexpected result !!!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "invalid string parameter was specified")]
        public void CalculatorEngineCalculate_UnknownOperation_Thrown()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            PostfixNotationParserTests.testInitialize(operationsRepositoryMock);
            CalculatorEngine target = new CalculatorEngine(operationsRepositoryMock);
            string[] testArray = new string[] { "2", "2", "^"};

            double actualResult = target.Calculate(testArray);
        }
    }
}
