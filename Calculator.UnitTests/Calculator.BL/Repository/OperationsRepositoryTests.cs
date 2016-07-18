using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Abstract;
using Calculator.BL.Repository;
using Calculator.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.UnitTests.Calculator.BL.Repository
{
    [TestClass]
    public class OperationsRepositoryTests
    {
        [TestMethod]
        public void OperationsRepositoryCtor_GoodValues_Created()
        {
            OperationsRepository target = new OperationsRepository();
        }

        [TestMethod]
        public void OperationsRepositoryAddOperation_GoodValues_Added()
        {
            OperationsRepository target = new OperationsRepository();
            var testOperation = new OperationMock()
            {
                NumberOfParameters = 2,
                Priority = 1,
                Sign = "+"
            };

            target.AddOperation(testOperation);
            var actualOperation = target.AvailableOperations.ToList()[0];

            Assert.AreEqual(testOperation, actualOperation, "Unexpected result !!!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "null input parameter was specified")]
        public void OperationsRepositoryAddOperation_NullOperationParam_Thrown()
        {
            OperationsRepository target = new OperationsRepository();
            target.AddOperation(null);
        }

        [TestMethod]
        public void OperationsRepositoryAddOperations_GoodValues_Added()
        {
            OperationsRepository target = new OperationsRepository();
            var testOperations = new List<IOperation>
            {
                new OperationMock() {NumberOfParameters = 2, Priority = 1, Sign = "+"},
                new OperationMock() {NumberOfParameters = 2, Priority = 1, Sign = "-"}
            };

            target.AddOperations(testOperations);
            var actualOperations = target.AvailableOperations;

            CollectionAssert.AreEqual(testOperations, actualOperations.ToList(), "Unexpected result !!!");
            Assert.AreEqual(testOperations.Count, actualOperations.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "null input parameter was specified")]
        public void OperationsRepositoryAddOperations_NullOperationParam_Thrown()
        {
            OperationsRepository target = new OperationsRepository();
            target.AddOperations(null);
        }
    }
}
