using System;
using System.Collections.Generic;
using System.Reflection;
using Calculator.Abstract;

namespace Calculator.BL.Repository
{
    public class AssemblyOperationLoader : IOperationsLoader
    {
        private IOperationsRepository _repository;

        public AssemblyOperationLoader(IOperationsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "shouldn't be null");
            }

            _repository = repository;
        }

        public void LoadOperations(string pathToFile)
        {
            if (string.IsNullOrWhiteSpace(pathToFile))
            {
                throw new ArgumentException("shouldn't be null or white space", "pathToFile");
            }

            List<IOperation> types = new List<IOperation>();

            Assembly assembly = Assembly.LoadFrom(pathToFile);

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass && typeof(IOperation).IsAssignableFrom(type))
                {
                    var instance = Activator.CreateInstance(type);
                    types.Add((IOperation)instance);
                }
            }

            _repository.AddOperations(types);
        }
    }
}
