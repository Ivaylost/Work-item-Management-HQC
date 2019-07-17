using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Core.Contracts.IO
{
    public interface IOutputWriter
    {
        void WriteLine(string message);
    }
}
