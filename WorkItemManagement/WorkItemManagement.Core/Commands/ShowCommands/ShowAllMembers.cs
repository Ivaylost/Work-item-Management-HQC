using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.ShowCommands
{
    public class ShowAllMembers : ICommand
    {
        private readonly IDatabase database;

        public ShowAllMembers(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 0)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("All members: ");

            foreach (var member in database.ListAllMembers)
            {
                sb.AppendLine($"\"{member.Name}\"");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
