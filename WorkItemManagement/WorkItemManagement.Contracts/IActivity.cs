using System;
using WorkItemManagement.Contracts;

namespace WorkItemManagement.Models.Interfaces
{
    public interface IActivity
    {
        string Description { get; }

        IMember Maker { get;  }
        
        DateTime DateOfChange { get; }

        //void print();
    }
}
