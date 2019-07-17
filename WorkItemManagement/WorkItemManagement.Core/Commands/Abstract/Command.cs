using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Factory.Contracts;

namespace WorkItemManagement.Core.Commands.Abstract
{
    public abstract class Command : ICommand
    {
        private readonly IWIMFactory factory;
        private readonly IDatabase database;

        public Command(IWIMFactory factory, IDatabase database)
            
        {
            this.factory = factory;
            this.database = database;
        }

        public abstract string Execute(IList<string> commandLine);
    }
}
