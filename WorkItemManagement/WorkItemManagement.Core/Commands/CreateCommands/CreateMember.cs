using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Abstract;
using WorkItemManagement.Core.Commands.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Engine;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands
{
    public class CreateMember :  ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public CreateMember(IWIMFactory factory, IDatabase database)
        {
            this.factory = factory;
            this.database = database;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string name = parameters[0];

            bool containsMember = this.database.ListAllMembers.Any(m => m.Name == name);

            if (containsMember)
            {
                return string.Format(GlobalConstants.MemberAlreadyExist, name);
            }

            var member = this.factory.CreateMember(name);

            database.AddMemberToList(member);

            string result = string.Format(GlobalConstants.MemberCreated, name);

            var activity = this.factory.CreateActivity(result, member);
            member.AddMemberActivity(activity);

            return result;
        }

    }
}
