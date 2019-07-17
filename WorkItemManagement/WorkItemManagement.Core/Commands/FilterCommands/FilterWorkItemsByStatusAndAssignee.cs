using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.FilterCommands
{
    public class FilterWorkItemsByStatusAndAssignee : ICommand
    {
        private readonly IDatabase database;

        public FilterWorkItemsByStatusAndAssignee(IDatabase database)
        {
            this.database = database;
        }
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            var workItemStatus = parameters[0];
            var memberName = parameters[1];

            bool containsMember = database.ListAllMembers.Any(x => x.Name == memberName);

            if (containsMember)
            {
                return (string.Format(GlobalConstants.MemberAlreadyExist, memberName));
            }

            var member = database.ListAllMembers.FirstOrDefault(m => m.Name == memberName);

            string result = member.ReturnMemberWorkItemsByStatusToString(workItemStatus);

            return result;
        }
    }
}
