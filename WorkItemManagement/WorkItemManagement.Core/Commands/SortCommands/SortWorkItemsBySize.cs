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
    public class SortWorkItemsBySize : ICommand
    {
        private readonly IDatabase database;

        public SortWorkItemsBySize(IDatabase database)
        {
            this.database = database;
        }
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            var sortedItems = new List<Story>();

            sortedItems = database.ListAllTeams
                 .SelectMany(x => x.ListOfBoards)
                     .SelectMany(x => x.ListOfWorkItems)
                         .Where(x => x.GetType() == typeof(Story))
                             .Select(workItem => (Story)workItem)
                                   .OrderBy(storyToOrder => storyToOrder.Title)
                                         .ToList();

            var sb = new StringBuilder();

            foreach (var story in sortedItems)
            {
                sb.AppendLine($"Size: \"{story.StatusType}\", Title: \"{story.Title}\"");
            }

            return sb.ToString().Trim();
        }
    }
}