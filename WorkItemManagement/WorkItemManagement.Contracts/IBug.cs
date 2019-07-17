using System.Collections.Generic;
using WorkItemManagement.Enums;

namespace WorkItemManagement.Contracts
{
    public interface IBug : IWorkItem, ITask
    {
        BugSeverityType SeverityType { get; }

        BugStatusType StatusType { get; }

        ICollection<string> StepsToReproduce { get; }

        //void ChangeStatus(BugStatusType bugStatusType);

        void ChangeSeverity(string newSeverity);

    }
}
