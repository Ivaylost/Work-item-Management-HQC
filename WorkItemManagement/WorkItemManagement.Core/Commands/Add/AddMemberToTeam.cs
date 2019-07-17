using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.Add
{
    public class AddMemberToTeam :  ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public AddMemberToTeam(IWIMFactory factory, IDatabase database)
        {
            this.factory = factory;
            this.database = database;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string memberName = parameters[0];
            string teamName = parameters[1];

            bool containsMember = this.database.ListAllMembers.Any(m => m.Name == memberName);

            if (!containsMember)
            {
                return string.Format(GlobalConstants.MemberDoesNotExistInTeam, memberName, teamName);
            }

            bool containsTeam = this.database.ListAllTeams.Any(m => m.Name == teamName);

            if (!containsTeam)
            {
                return string.Format(GlobalConstants.TeamDoesNotExist, teamName);
            }
            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            if (team.ListOfMembers.Any(m => m.Name == memberName))
            {
                return string.Format(GlobalConstants.MemberAlreadyExistInTeam, memberName);
            }

            var member = database.ListAllMembers.Where(m => m.Name == memberName).FirstOrDefault();

            team.AddMemberToTeam(member);

            string result = string.Format(GlobalConstants.MemberAddedToTeam, memberName, teamName);

            var history = this.factory.CreateActivity(result, member);
            member.AddMemberActivity(history);
            team.AddTeamActivity(history);

            return result;
        }
    }
}
