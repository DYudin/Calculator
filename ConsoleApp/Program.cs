using System;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            // TODO:
            string pathToOperationsDescriptionFile = string.Empty;

            // 3. create unity container and build all components
            using (IUnityContainer unity = new UnityContainer())
            {
                buildUnity(unity, pathToOperationsDescriptionFile);

                var cmd = string.Empty;
                while (cmd != "exit")
                {
                    cmd = Console.ReadLine();

                    if (string.IsNullOrEmpty(cmd)) continue;
                    try
                    {
                        // 1. Parse input line
                        IParser parser = unity.Resolve<IParser>();
                        var expression = parser.Parse(cmd);
                       
                        // 2. Build chain of operations to execute
                        IExecutionChainBuilder executionChainBuilder = unity.Resolve<IExecutionChainBuilder>();
                        var executionChain = executionChainBuilder.BuildChain(expression);
                        
                        // 3. Execute chain of operations
                        ICalculatorEngine engine = unity.Resolve<CalculatorEngine>();
                        var result = engine.Calculate(executionChain);

                        Console.WriteLine("Result of calculating: {0}", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: {0}. Message: {1}", e, e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Registers types in the unity and resolves some instances
        /// </summary>
        /// <param name="unityContainer">Container of components</param>
        /// <exception cref="Exception">Some application exception</exception>
        private static void buildUnity(IUnityContainer unityContainer, string pathToOperationDescriptionFile)
        {
            try
            {
                // 1. Build Repositories
                unityContainer.RegisterType<OperationsRepository, OperationsRepository>(
                  new ContainerControlledLifetimeManager());
                OperationsRepository repository = unityContainer.Resolve<OperationsRepository>();
                
                // 2. Build Loaders
                unityContainer.RegisterType<IOperationsLoader, XmlOperationLoader>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(repository));
                IOperationsLoader loader = unityContainer.Resolve<IOperationsLoader>();
                loader.LoadOperations(string pathToOperationDescriptionFile);
                
                // 3. Build Parser
                unityContainer.RegisterType<IParser, ExpressionParser>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(repository));
        
                // 4. Build execution chain builder
                unityContainer.RegisterType<IExecutionChainBuilder, ExecutionChainBuilder>(
                    new ContainerControlledLifetimeManager());
        
                // 5. Build Calculator engine
                unityContainer.RegisterType<ICalculatorEngine, CalculatorEngine>(
                    new ContainerControlledLifetimeManager());
                
                // Network viewmodel
                unityContainer.RegisterType<NetworkTabViewModel, NetworkTabViewModel>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        transmitterManager,
                        busyIndicatorWithCancel,
                        userInteraction));
                NetworkTabViewModel networkTabViewModel = unityContainer.Resolve<NetworkTabViewModel>();
                
                // Main viewmodel
                unityContainer.RegisterType<MainViewModel, MainViewModel>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        networkTabViewModel,
                        transmitterInformationTabViewModel,
                        sensorInfomationTabViewModel,
                        transmitterSettingTabViewModel,
                        diagnosticsTabViewModel,
                        variableEditorViewModel,
                        transmitterManager,
                        statusOutputViewModel,
                        responseOutputViewModel,
                        isFactoryMode));
            }
            catch (ModbusMasterException ex)
            {
                throw new ModbusMasterException(Localization.strProgramError, ex);
            }
            catch (Exception ex)
            {
                throw new ModbusMasterException(Localization.strProgramError, ex);
            }
        }
    }
}
