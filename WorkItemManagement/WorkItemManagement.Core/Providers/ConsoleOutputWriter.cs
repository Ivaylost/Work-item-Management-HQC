using System;
using WorkItemManagement.Core.Contracts.IO;

namespace WorkItemManagement.Core.Providers
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
