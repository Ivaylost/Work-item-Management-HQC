using System;
using System.Collections.Generic;
using System.Text;

namespace WorkItemManagement.Core.Utills
{
    public static class GlobalConstants
    {
        public const string ParametersCountInvalid = "The count of input parameters is invalid!";
        public const string Delimiter = "####################";
        public const string InvalidCommand = "Invalid command name: {0}!";
        public const string TeamCreated = "Team with name \"{0}\" was created!";
        public const string TeamDoesNotExist = "Team with name \"{0}\" does not exist!";
        public const string TeamAlreadyExist = "Team with name \"{0}\" already exists!";
        public const string MemberDoesNotExist = "Member with name \"{0}\" does not exist!";
        public const string MemberCreated = "Member with name \"{0}\" was created!";
        public const string MemberAddedToTeam = "Member with name \"{0}\" was added to team \"{1}\"!";
        public const string MemberAlreadyExist = "Member with name \"{0}\" already exists!";
        public const string MemberAlreadyExistInTeam = "Member with name \"{0}\" already exists in the team!";
        public const string MemberDoesNotExistInTeam = "Member with name \"{0}\" does not exists in the team \"{1}\"!";
        public const string BoardAddedToTeam = "Board with name \"{0}\" was added to team \"{1}\"!";
        public const string BoardAlreadyExistInTeam = "Board with name \"{0}\" already exists in team!";
        public const string BoardDoesNotExistInTeam = "Board with name \"{0}\" does not exists in team \"{1}\"!";
        public const string BugCreated = "Bug with name \"{0}\", with descripion \n\"{1}\" was added to board \"{2}\"!";
        public const string BugAlreadyExistInBoard = "Bug with title \"{0}\" already exists in board \"{1}\"!";
        public const string StoryAlreadyExistInBoard = "Story with title \"{0}\" already exists in board \"{1}\"!";
        public const string StoryCreated = "Story with name \"{0}\", with descripion \n\"{1}\" was added to board \"{2}\"!";
        public const string StoryAlreadyExist = "Story with ID {0} already exists!";
        public const string FeedbackCreated = "Feedback with name \"{0}\", with descripion \n\"{1}\" was added to board \"{2}\"!";
        public const string FeedbackAlreadyExist = "Story with ID {0} already exists!";
        public const string AssignWorkItemByMember = "Work item with title \"{0}\" was \nassign by member with name \"{1}\"!";
        public const string UnAssignWorkItemByMember = "Work item with title \"{0}\" was \nunassign by member with name \"{1}\"!";
        public const string CommentWasAddedToAWorkItem = "Comment with description \"{0}\" \nwas added to a work item \"{1}\"!";
        public const string PriorityWasChanged = "{0} priority was changed to \"{1}\"!";
        public const string StatusWasChanged = "{0} status was changed to \"{1}\"!";
        public const string RatingWasChanged = "{0} rating was changed to \"{1}\"!";
        public const string BugSeverityWasChanged = "{0} severity was changed to \"{1}\"!";
        public const string StorySizeWasChanged = "{0} size was changed to \"{1}\"!";
        public const string ChangeFeedbackSize = "Story size was changed to \"{0}\"!";
        public const string InvalidPriorityType = "Invalid priority type!";
        public const string InvalidStatusType = "Invalid status type!";
        public const string InvalidSizeType = "Invalid size type!";
        public const string InvalidRating = "The rating should be a valid integer!";
        public const string EnterADescription = "Please enter a description: ";
        public const string EnterStepsToReproduce = "Please enter steps to reproduce: ";
        public const string WorkItemDoesNotExist = "WorkItem with name \"{0}\" does not exists!";
    }
}
