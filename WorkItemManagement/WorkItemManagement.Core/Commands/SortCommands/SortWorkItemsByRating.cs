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
    public class SortWorkItemsByRating : ICommand
    {
        private readonly IDatabase database;

        public SortWorkItemsByRating(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {

            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            var sortedItems = new List<FeedBack>();

            sortedItems = database.ListAllTeams
                 .SelectMany(x => x.ListOfBoards)
                     .SelectMany(x => x.ListOfWorkItems)
                         .Where(x => x.GetType() == typeof(FeedBack))
                             .Select(workItem => (FeedBack)workItem)
                                   .OrderBy(feedbackToOrder => feedbackToOrder.Title)
                                         .ToList();

            var sb = new StringBuilder();

            foreach (var feedback in sortedItems)
            {
                sb.AppendLine($"Rating: \"{feedback.Rating}\", Title: \"{feedback.Title}\"");
            }

            return sb.ToString().Trim();
        }
    }
}

