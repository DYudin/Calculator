using System.Collections.Generic;
using Calculator.Abstract;

namespace Calculator.BL.Repository
{
    public interface IOperationsRepository
    {
        IEnumerable<IOperation> AvailableOperations { get; }
        void AddOperation(IOperation operation);
        void AddOperations(IEnumerable<IOperation> operations);
    }
}
