using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Core.Contracts
{
    public interface ICommand
    {
        string Execute(IList<string> commandLine);
    }
}
