using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;

namespace WorkItemManagement.Core.Commands.Global.Contracts
{
    public interface IDatabase
    {
        IList<IMember> ListAllMembers { get; }

        IList<ITeam> ListAllTeams { get; }

        void AddMemberToList(IMember member);

        bool IsMemberPresentInCollection(string name);

        void AddTeamToList(ITeam team);

        bool IsTeamPresentInCollection(string name);


    }
}
