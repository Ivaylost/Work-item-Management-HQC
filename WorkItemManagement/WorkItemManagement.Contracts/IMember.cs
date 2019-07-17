using System.Collections.Generic;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Contracts
{
    public interface IMember

    {
        string Name { get; }

        ICollection<IWorkItem> ListOfMemberWorkItems { get; }

        ICollection<IActivity> ListOfMemberActivity { get; }

        void AssignMemberWorkItem(IWorkItem workItem);

        void UnAssignMemberWorkItem(IWorkItem workItem);

        void AddMemberActivity(IActivity activity);

        string ReturnMemberActivityToString();

        string ReturnMemberWorkItemsToString();

        string ReturnMemberWorkItemsToString(string workItemType);

        string ReturnMemberWorkItemsByStatusToString(string workItemStatus);

    }
}
