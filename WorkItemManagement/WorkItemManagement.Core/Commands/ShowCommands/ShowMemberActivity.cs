using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.ShowCommands
{
    public class ShowMemberActivity : ICommand
    {
        private readonly IDatabase database;

        public ShowMemberActivity(IDatabase database)
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

            if (!containsMember)
            {
                return (string.Format(GlobalConstants.MemberDoesNotExist, memberName));
            }

            var member = database.ListAllMembers.FirstOrDefault(m => m.Name == memberName);

            var result = member.ReturnMemberActivityToString();

            return result;
        }
    }
}
