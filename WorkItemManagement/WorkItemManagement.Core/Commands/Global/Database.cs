using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;

namespace WorkItemManagement.Core.Commands.Global
{
    public class Database : IDatabase

    {
        private readonly IList<IMember> listAllMembers;

        private readonly IList<ITeam> listAllTeams;

        public Database()
        {
            this.listAllMembers = new List<IMember>();
            this.listAllTeams = new List<ITeam>();
        }

        public IList<IMember> ListAllMembers
        {
            get
            {
                return new List<IMember>(this.listAllMembers);
            }
        }

        public IList<ITeam> ListAllTeams
        {
            get
            {
                return new List<ITeam>(this.listAllTeams);
            }
        }

        public void AddMemberToList(IMember member)
        {
            this.listAllMembers.Add(member);
        }

        public bool IsMemberPresentInCollection(string name)
        {
            bool contains = this.listAllMembers.Any(m => m.Name == name);

            if (contains)
            {
                return true;
            }

            return false;
        }

        public void AddTeamToList(ITeam team)
        {
            this.listAllTeams.Add(team);
        }

        public bool IsTeamPresentInCollection(string name)
        {
            bool contains = this.listAllTeams.Any(m => m.Name == name);

            if (contains)
            {
                return true;
            }

            return false;
        }
    }
}
