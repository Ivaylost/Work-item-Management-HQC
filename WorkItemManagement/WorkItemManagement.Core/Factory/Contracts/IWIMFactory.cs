using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Enums;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.Core.Factory.Contracts
{
    public interface IWIMFactory
    {
        IMember CreateMember(string name);

        IBoard CreateBoard(string name);

        ITeam CreateTeam(string name);

        IBug
            CreateBug(string title, string description, PriorityType priorityType,
                   BugSeverityType bugSeverityType, IMember assignee, List<string> stepsToReproduce);

        IStory CreateStory(string title, string description,
                     PriorityType priorityType, StorySizeType storySizeType,
                     StoryStatusType storyStatusType, IMember assignee);

        IFeedback CreateFeedback(string title, string description, int rating,
                      FeedbackStatusType feedbackStatusType);

        IActivity CreateActivity(string description, IMember maker);

        IActivity CreateActivity(string description);

        IComment CreateComment(string message, IMember author);
       
    }
}
