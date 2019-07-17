using System.Collections.Generic;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Contracts
{
    public interface IBoard
    {
        string Name { get; }

        ICollection<IActivity> ListOfActivity { get; }

        ICollection<IWorkItem> ListOfWorkItems { get; }

        void AddBoardActivity(IActivity boardActivity);

        void AddBoardWorkItem(IWorkItem workItem);

        string ReturnBoardActivityToString();

        string ReturnBoardWorkItemsToString();

        string ReturnBoardWorkItemsToString(string workItem);

        string ReturnBoardWorkItemsByStatusToString(string workItemStatus);

    }
}
