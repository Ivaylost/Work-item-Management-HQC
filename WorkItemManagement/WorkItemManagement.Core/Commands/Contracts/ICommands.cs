using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Core.Commands.Contracts
{
    public interface ICommands
    {
        string ReturnMemberName();

        string ReturnTeamName();

        string ReturnBoardName();

        string ReturnWorkItemTitle();

        string ReturnNewPriority();

        string ReturnNewStatus();

        string ReturnNewSeverity();

        string ReturnNewSize();

        string ReturnDescription();

        List<string> ReturnListOfSteps();

        string ReturnCurrentStatus();

        string ReturnCurrentPriority();

        string ReturnCurrentSeverity();

        string ReturnWorkItemAsignee();

        string ReturnCurrentSize();

        int ReturnCurrentRating();

        int ReturnNewRating();

        int RatingAsInt();
    }
}
