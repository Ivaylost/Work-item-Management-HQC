using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WorkItemManagement.Core.Commands;
using WorkItemManagement.Core.Commands.ChangeCommands;
using WorkItemManagement.Core.Commands.CreateCommands;
using WorkItemManagement.Core.Commands.Global;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Engine;
using WorkItemManagement.Core.Engine.Contracts;
using WorkItemManagement.Core.Providers;

namespace WorkItemManagement.Core
{
    public class StartUp
    {
        public static object ContinerConfig { get; private set; }

        public static void Main(string[] args)
        {
            
           var container = ContainerConfig.Configure();

            var engine = container.Resolve<IEngine>();

            engine.Run();
        }
    }
}
