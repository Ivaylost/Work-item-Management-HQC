using WorkItemManagement.Enums;

namespace WorkItemManagement.Contracts
{
    public interface IStory : IWorkItem, ITask
    {
        StorySizeType SizeType { get; }

        StoryStatusType StatusType { get; }

        void ChangeSize(string storySizeType);

        //void ChangeStatus(StoryStatusType storyStatusType);
    }
}
