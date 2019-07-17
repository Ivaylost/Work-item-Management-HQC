using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models.WorkItems;

namespace WorkItemManagement.Core.Commands
{
    public class SortWorkItemsBySeverity : ICommand
    {

        private readonly IDatabase database;

        public SortWorkItemsBySeverity(IDatabase database)
        {
            this.database = database;
        }
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            var sortedItems = new List<Bug>();

            sortedItems = database.ListAllTeams
                 .SelectMany(x => x.ListOfBoards)
                     .SelectMany(x => x.ListOfWorkItems)
                         .Where(x => x.GetType() == typeof(Bug))
                             .Select(workItem => (Bug)workItem)
                                   .OrderBy(bugToOrder => bugToOrder.Title)
                                         .ToList();

            var sb = new StringBuilder();

            foreach (var bug in sortedItems)
            {
                sb.AppendLine($"Severity: \"{bug.SeverityType}\", Title: \"{bug.Title}\"");
            }

            return sb.ToString().Trim();
        }
    }
}