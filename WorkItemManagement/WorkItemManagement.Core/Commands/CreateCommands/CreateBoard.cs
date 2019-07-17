using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Abstract;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Engine;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands
{
    public class CreateBoard :  ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public CreateBoard(IWIMFactory factory, IDatabase database)
            
        {
            this.factory = factory;
            this.database = database;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string boardName = parameters[0];
            string teamName = parameters[1];

            bool containsTeam = this.database.ListAllTeams.Any(m => m.Name == teamName);

            if (!containsTeam)
            {
                return string.Format(GlobalConstants.TeamDoesNotExist, teamName);
            }

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            if (team.ListOfBoards.Any(b => b.Name == boardName))
            {
                return string.Format(GlobalConstants.BoardAlreadyExistInTeam, boardName);
            }

            var board = this.factory.CreateBoard(boardName);
            team.AddBoardInTeam(board);

            string result = string.Format(GlobalConstants.BoardAddedToTeam, boardName, teamName);

            var history = this.factory.CreateActivity(result);
            board.AddBoardActivity(history);
            team.AddTeamActivity(history);

            return result;
        }

    }
}
