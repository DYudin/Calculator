using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(operation != null, "operation");
            _availableOperations.Add(operation);
        }

        public void AddOperations(IEnumerable<IOperation> operations)
        {
            Contract.Requires<ArgumentNullException>(operations != null, "operations");
            _availableOperations.AddRange(operations);
        }
    }
}