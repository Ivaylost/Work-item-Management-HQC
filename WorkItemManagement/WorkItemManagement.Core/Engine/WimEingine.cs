using System;
using System.Linq;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Contracts.IO;
using WorkItemManagement.Core.Engine.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Engine
{
    public class WimEingine : IEngine 
    {
        private readonly ICommandParser parser;
        private readonly IInputReader inputReader;
        private readonly IOutputWriter outputWriter;

        public WimEingine(IInputReader inputReader,
                      IOutputWriter outputWriter,
                      ICommandParser commandParser)
                     
        {
            this.parser = commandParser;
            this.inputReader = inputReader;
            this.outputWriter = outputWriter;
        }

        public virtual void Run()
        {
            string commandLine = inputReader.ReadLine();

            while (commandLine != "End".ToLower())
            {
                try
                {
                    var lineParameters = commandLine.Trim().Split(
                        new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var commandName = lineParameters[0];
                    var parameters = lineParameters.Skip(1);

                    var command = this.parser.ParseCommand(commandName);
                    var output = command.Execute(parameters.ToList());

                    this.outputWriter.WriteLine(output);
                    this.outputWriter.WriteLine(GlobalConstants.Delimiter);

                    commandLine = inputReader.ReadLine();       

                }
                catch (Exception ex)
                {
                    this.outputWriter.WriteLine($"ERROR: {ex.Message}");
                    commandLine = inputReader.ReadLine();
                }
            }
        }
    }
}

