using Autofac;
using System.Linq;
using System.Reflection;
using WorkItemManagement.Core.Commands.Global;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;

namespace WorkItemManagement.Core.Providers
{
    public static class ContainerConfig
    {
        public static IContainer Configure()

        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces();

            var commandTypes = assembly.DefinedTypes.Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(ICommand))).ToList();
            foreach (var commandType in commandTypes)
            {
                //builder.RegisterType(commandType.AsType()).Named<ICommand>(commandType.Name.Replace("Command", ""));
                builder.RegisterType(commandType.AsType()).Named<ICommand>(commandType.Name.ToLower());
            }

            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();

            var container = builder.Build();

            return container;
            
        }
    }
}
