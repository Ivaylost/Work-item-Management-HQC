using System.Collections.Generic;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Engine.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Enums;
using WorkItemManagement.Models;
using WorkItemManagement.Models.Interfaces;
using WorkItemManagement.Models.WorkItems;

namespace WorkItemManagement.Core.Factories
{
    public class WIMFactory : IWIMFactory
    {
        public IMember CreateMember(string name)
        {
            return new Member(name);
        }

        public IBoard CreateBoard(string name)
        {
            return new Board(name);
        }

        public ITeam CreateTeam(string name)
        {
            return new Team(name);
        }

        public IBug
            CreateBug(string title, string description, PriorityType priorityType,
                   BugSeverityType bugSeverityType, IMember assignee, List<string> stepsToReproduce)
        {
            return new Bug(title, description, priorityType, bugSeverityType, assignee, stepsToReproduce);

        }

        public IStory CreateStory(string title, string description,
                     PriorityType priorityType, StorySizeType storySizeType,
                     StoryStatusType storyStatusType, IMember assignee)
        {
            return new Story(title, description, priorityType, storySizeType, storyStatusType, assignee);
        }

        public IFeedback CreateFeedback(string title, string description, int rating,
                       FeedbackStatusType feedbackStatusType)
        {
            return new FeedBack(title, description, feedbackStatusType, rating);
        }

        public IActivity CreateActivity(string description, IMember maker)
        {
            return new Activity(description, maker);
        }

        public IActivity CreateActivity(string description)
        {
            return new Activity(description);
        }

        public IComment CreateComment(string message, IMember author)
        {
            return new Comment(message, author);
        }

        
    }
}
