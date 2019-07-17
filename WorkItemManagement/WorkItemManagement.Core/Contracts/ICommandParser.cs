using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Core.Contracts
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string commandLine);
    }
}
