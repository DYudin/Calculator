using System;
using System.Collections.Generic;
using Calculator.Abstract;

namespace Calculator.BL.Repository
{
    public class OperationsRepository : IOperationsRepository
    {
        private List<IOperation> _availableOperations;

        public OperationsRepository()
        {
            _availableOperations = new List<IOperation>();
        }

        public IEnumerable<IOperation> AvailableOperations
        {
            get
            {
                return _availableOperations;
            }
        }

        public void AddOperation(IOperation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation", "shouldn't be null");
            }
            _availableOperations.Add(operation);
        }

        public void AddOperations(IEnumerable<IOperation> operations)
        {
            if (operations == null)
            {
                throw new ArgumentNullException("operations", "shouldn't be null");
            }
            _availableOperations.AddRange(operations);
        }
    }
}