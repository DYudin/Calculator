using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Calculator.Abstract;
using Calculator.BL.Repository;
using Calculator.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.UnitTests.Calculator.BL.Repository
{
    [TestClass]
    public class AssemblyOperationLoaderTest
    {
        [TestMethod]
        public void AssemblyOperationLoaderCtor_GoodValues_Created()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            AssemblyOperationLoader target = new AssemblyOperationLoader(operationsRepositoryMock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "null repository parameter was specified")]
        public void AssemblyOperationLoaderCtor_NullRepository_Thrown()
        {
            AssemblyOperationLoader target = new AssemblyOperationLoader(null);
        }

        [TestMethod]
        [DeploymentItem("Calculator.Operations.dll")]
        public void AssemblyOperationLoaderLoad_GoodValues_Added()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            AssemblyOperationLoader target = new AssemblyOperationLoader(operationsRepositoryMock);
            string pathToTestAssembly =  "Calculator.Operations.dll";
            var expectedOperations = new List<IOperation>()
            {
                new OperationMock() {NumberOfParameters = 2, Priority = 1, Sign = "+"},
                new OperationMock() {NumberOfParameters = 2, Priority = 1, Sign = "-"},
                new OperationMock() {NumberOfParameters = 2, Priority = 2, Sign = "*"},
                new OperationMock() {NumberOfParameters = 2, Priority = 2, Sign = "/"},
                new OperationMock() {NumberOfParameters = 0, Priority = 0, Sign = "("},
                new OperationMock() {NumberOfParameters = 0, Priority = 0, Sign = ")"},
                new OperationMock() {NumberOfParameters = 1, Priority = 4, Sign = "sin"}
            };
            
            target.LoadOperations(pathToTestAssembly);
            var actualOperations = operationsRepositoryMock.AvailableOperations.ToList();

            foreach (var op in expectedOperations)
            {
                if (actualOperations.Find(
                    x=> x.NumberOfParameters == op.NumberOfParameters && x.Priority == op.Priority && x.Sign == op.Sign) == null)
                {
                    throw new ArgumentException(string.Format("Element with sign: {0} is absent in actual collection", op.Sign));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "null or empty filePath parameter was specified")]
        public void AssemblyOperationLoaderLoad_NullParameter_Added()
        {
            OperationsRepositoryMock operationsRepositoryMock = new OperationsRepositoryMock();
            AssemblyOperationLoader target = new AssemblyOperationLoader(operationsRepositoryMock);
            target.LoadOperations(null);
        }
    }
}
