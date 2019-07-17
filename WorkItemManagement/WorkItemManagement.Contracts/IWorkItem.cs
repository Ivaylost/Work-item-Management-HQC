using System;
using System.Collections.Generic;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Contracts
{
    public interface IWorkItem
    {
        Guid Id { get; }

        string Title { get; }

        string Description { get; }

        ICollection<string> ListOfChanges { get; }

        ICollection<IComment> ListOfComments { get; }

        void AddComment(IComment comment);

        Enum GetStatus();

        void ChangeStatus(string newStatus);

    }
}
