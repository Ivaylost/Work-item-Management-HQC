using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Core.Commands.Global.Contracts
{
    public interface IExistable
    {
        bool IsPresentInCollection(string name);
    }
}
