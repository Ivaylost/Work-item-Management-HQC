using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.ShowCommands
{
    public class ShowAllTeams : ICommand
    {
        private readonly IDatabase database;

        public ShowAllTeams(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 0)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            if (this.database.ListAllTeams.Count == 0)
            {
                return $"There are no teams";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("All teams: ");

            foreach (var team in database.ListAllTeams)
            {
                sb.AppendLine($"\"{team.Name}\"");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
