using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.FilterCommands
{
    public class FilterWorkItemsByAssigniee : ICommand
    {
        private readonly IDatabase database;

        public FilterWorkItemsByAssigniee(IDatabase database)
        {
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            var memberName = parameters[0];

            bool containsMember = database.ListAllMembers.Any(x => x.Name == memberName);

            if (containsMember)
            {
                return (string.Format(GlobalConstants.MemberAlreadyExist, memberName));
            }

            var member = database.ListAllMembers.FirstOrDefault(m => m.Name == memberName);

            string result = member.ReturnMemberWorkItemsToString();

            return result;
        }
    }
}
