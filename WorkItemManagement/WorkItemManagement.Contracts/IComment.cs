using WorkItemManagement.Contracts;

namespace WorkItemManagement.Models.Interfaces
{
    public interface IComment
    {
        string Message { get; set; }

        IMember Author { get; }


    }
}
