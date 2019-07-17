using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.ShowCommands
{
    public class ShowAllTeamMembers : ICommand
    {
        private readonly IDatabase database;

        public ShowAllTeamMembers(IDatabase database)
        {
            this.database = database;
        }
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            var teamName = parameters[0];

            bool containsTeam = database.ListAllTeams.Any(x => x.Name == teamName);

            if (!containsTeam)
            {
                return (string.Format(GlobalConstants.TeamDoesNotExist, teamName));
            }

            if (this.database.ListAllTeams.Count == 0)
            {
                return $"There are no teams";
            }

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine("All members in the team: ");
            sb.AppendLine(team.ReturnListOfMembers());

            return sb.ToString().TrimEnd();
        }
    }
}
