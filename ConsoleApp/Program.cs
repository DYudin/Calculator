using System;
using System.IO;
using System.Reflection;
using Microsoft.Practices.Unity;
using Calculator.BL.Logic;
using Calculator.BL.Repository;

namespace ConsoleApp
{
    internal class Program
    {
        // TODO:
        private static string pathToFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + "Calculator.Operations.dll";

        private static void Main(string[] args)
        {
            // 3. create unity container and build all components
            using (IUnityContainer unity = new UnityContainer())
            {
                buildUnity(unity);

                var cmd = string.Empty;
                while (cmd != "exit")
                {
                    Console.WriteLine("Enter expression:");
                    cmd = Console.ReadLine();

                    if (string.IsNullOrEmpty(cmd)) continue;
                    try
                    {
                        // 1. Parse input line
                        IParser parser = unity.Resolve<IParser>();
                        string[] separatedPostFixNotationString = parser.Parse(cmd);

                        // 2. Execute calculation
                        ICalculatorEngine engine = unity.Resolve<ICalculatorEngine>();
                        var result = engine.Calculate(separatedPostFixNotationString);

                        Console.WriteLine("Result of calculating: {0}", result);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Registers types in the unity and resolves some instances
        /// </summary>
        /// <param name="unityContainer">Container of components</param>
        /// <exception cref="Exception">Some application exception</exception>
        private static void buildUnity(IUnityContainer unityContainer)
        {
            try
            {
                // 1. Build Repositories
                unityContainer.RegisterType<IOperationsRepository, OperationsRepository>(
                    new ContainerControlledLifetimeManager());
                IOperationsRepository repository = unityContainer.Resolve<OperationsRepository>();

                // 2. Build Loaders
                unityContainer.RegisterType<IOperationsLoader, AssemblyOperationLoader>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(repository));
                IOperationsLoader loader = unityContainer.Resolve<IOperationsLoader>();
                loader.LoadOperations(pathToFile);

                // 3. Build Parser
                unityContainer.RegisterType<IParser, PostfixNotationParser>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(repository));

                // 4. Build Calculator engine
                unityContainer.RegisterType<ICalculatorEngine, CalculatorEngine>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(repository));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}