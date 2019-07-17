using System.Collections.Generic;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Contracts
{
    public interface ITeam
    {
        string Name { get; }

        ICollection<IMember> ListOfMembers { get; }

        ICollection<IBoard> ListOfBoards { get; }

        ICollection<IActivity> ListOfActivity { get; }

        void AddTeamActivity(IActivity activity);

        void AddMemberToTeam(IMember member);

        void AddBoardInTeam(IBoard board);

        string ReturnListOfMembers();

        string ReturnListOfBoards();
    }
}
