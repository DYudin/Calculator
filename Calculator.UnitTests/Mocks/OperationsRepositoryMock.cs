
using System.Collections.Generic;
using Calculator.Abstract;
using Calculator.BL.Repository;

namespace Calculator.UnitTests.Mocks
{
    public class OperationsRepositoryMock : IOperationsRepository
    {
        private List<IOperation> _operations;

        public OperationsRepositoryMock()
        {
            _operations = new List<IOperation>();
        }

        public IEnumerable<IOperation> AvailableOperations
        {
            get { return _operations; }
            set { _operations = (List<IOperation>)value; }
        }

        public void AddOperation(IOperation operation)
        {
            _operations.Add(operation);
        }

        public void AddOperations(IEnumerable<IOperation> operations)
        {
            _operations.AddRange(operations);
        }
    }
}