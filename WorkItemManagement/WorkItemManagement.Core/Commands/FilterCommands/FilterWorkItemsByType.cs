using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands
{
    public class FilterWorkItemsByType : ICommand
    {
        private readonly IDatabase database;

        public FilterWorkItemsByType(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            var workItemType = parameters[0];
            var sb = new StringBuilder();

            foreach (var team in database.ListAllTeams)
            {
                foreach (var board in team.ListOfBoards)
                {
                    sb.AppendLine(board.ReturnBoardWorkItemsToString(workItemType));
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
