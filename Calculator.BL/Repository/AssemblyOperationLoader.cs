using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using Calculator.Abstract;

namespace Calculator.BL.Repository
{
    public class AssemblyOperationLoader : IOperationsLoader
    {
        private IOperationsRepository _repository;

        public AssemblyOperationLoader(IOperationsRepository repository)
        {
            Contract.Requires<ArgumentNullException>(repository != null, "repository");

            _repository = repository;
        }

        public void LoadOperations(string pathToFile)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrWhiteSpace(pathToFile), "pathToFile");
            string fileName = "Calculator.Operations.dll";
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            List<IOperation> types = new List<IOperation>();

            Assembly assembly = Assembly.LoadFrom(directory + "\\" +  fileName);

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass && typeof (IOperation).IsAssignableFrom(type))
                {
                    types.Add((IOperation) type);
                }
            }

            _repository.AddOperations(types);
        }
    }
}
