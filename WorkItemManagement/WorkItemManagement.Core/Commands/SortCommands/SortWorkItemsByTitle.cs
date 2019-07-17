using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models.Abstract;

namespace WorkItemManagement.Core.Commands
{
    public class SortWorkItemsByTitle : ICommand
    {
        private readonly IDatabase database;

        public SortWorkItemsByTitle(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {

            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            var sortedItems = new List<WorkItem>();

            sortedItems = database.ListAllTeams
                 .SelectMany(x => x.ListOfBoards)
                     .SelectMany(x => x.ListOfWorkItems)
                             .Select(workItem => (WorkItem)workItem)
                                   .OrderBy(storyToOrder => storyToOrder.Title)
                                         .ToList();

            var sb = new StringBuilder();

            foreach (var workItem in sortedItems)
            {
                sb.AppendLine(workItem.Title);
            }

            return sb.ToString().Trim();
        }
    }
}
