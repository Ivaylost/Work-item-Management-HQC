using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Core.Contracts.IO;

namespace WorkItemManagement.Core.Providers
{
    public class ConsoleInputReader : IInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
