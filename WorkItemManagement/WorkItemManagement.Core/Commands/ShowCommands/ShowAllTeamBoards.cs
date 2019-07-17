using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.ShowCommands
{
    public class ShowAllTeamBoards : ICommand
    {
        private readonly IDatabase database;

        public ShowAllTeamBoards(IDatabase database)
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

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            string result = team.ReturnListOfBoards();

            return "All boards in the team: \n" + result.Trim();
        }
    }
}
