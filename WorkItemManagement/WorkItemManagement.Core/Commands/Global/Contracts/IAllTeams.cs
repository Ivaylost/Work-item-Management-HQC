using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;

namespace WorkItemManagement.Core.Commands.Global.Contracts
{
    public interface IAllTeams
    {
        IList<ITeam> AllTeamsList { get; }
    }
}
