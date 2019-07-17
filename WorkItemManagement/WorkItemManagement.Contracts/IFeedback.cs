using WorkItemManagement.Enums;

namespace WorkItemManagement.Contracts
{
    public interface IFeedback : IWorkItem
    {
        int Rating { get; }

        FeedbackStatusType StatusType { get; }

        void ChangeRating(int rating);

        //void ChangeStatus(FeedbackStatusType feedbackStatusType);

    }
}
