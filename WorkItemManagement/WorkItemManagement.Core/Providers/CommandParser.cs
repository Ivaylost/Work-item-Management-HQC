using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Autofac;
using WorkItemManagement.Core.Contracts;

namespace WorkItemManagement.Core.Providers
{
    public class CommandParser : ICommandParser
    {
        private IComponentContext componentContext;

        public CommandParser(IComponentContext context)
        {
            this.componentContext = context;
        }

        public ICommand ParseCommand(string commandName)
        {
            var command = this.componentContext
                .ResolveNamed<ICommand>(commandName.ToLower());

            return command;
        }
    }
}
