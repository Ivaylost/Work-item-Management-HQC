using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models.Abstract;
using WorkItemManagement.Models.WorkItems;

namespace WorkItemManagement.Core.Commands
{
    public class SortWorkItemsByPriority : ICommand
    {
        private readonly IDatabase database;

        public SortWorkItemsByPriority(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            var sortedItems = new List<Task>();

            sortedItems = database.ListAllTeams
                 .SelectMany(x => x.ListOfBoards)
                     .SelectMany(x => x.ListOfWorkItems)
                         .Where(x => x.GetType() == typeof(Bug) || x.GetType() == typeof(Story))
                             .Select(workItem => (Task)workItem)
                                   .OrderBy(bugToOrder => bugToOrder.Title)
                                         .ToList();

            var sb = new StringBuilder();

            foreach (var task in sortedItems)
            {
                sb.AppendLine($"Priority: \"{task.PriorityType}\", Title: \"{task.Title}\"");
            }

            return sb.ToString().Trim();
        }
    }
}
