using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;

namespace WorkItemManagement.Core.Commands.Global.Contracts
{
    public interface IAllMembers
    {
        IList<IMember> ListAll { get; }

        void AddMemberToList(IMember member);

        bool IsPresentInCollection(string name);
    }
}
