using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.ShowCommands
{
    public class ShowBoardActivity : ICommand
    {

        private readonly IDatabase database;

        public ShowBoardActivity(IDatabase database)
        {
            this.database = database;
        }
        
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            var teamName = parameters[0];
            var boardName = parameters[1];

            bool containsTeam = database.ListAllTeams.Any(x => x.Name == teamName);

            if (!containsTeam)
            {
                return (string.Format(GlobalConstants.TeamDoesNotExist, teamName));
            }

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            bool containsBoard = team.ListOfBoards.Any(b => b.Name == boardName);

            if (!containsBoard)
            {
                return string.Format(GlobalConstants.BoardDoesNotExistInTeam, boardName, teamName);
            }

            var board = team.ListOfBoards.Where(b => b.Name == boardName).FirstOrDefault();

            string result = board.ReturnBoardActivityToString();

            return result;
        }
    }
}
