using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Abstract;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Contracts.IO;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Enums;
using WorkItemManagement.Models.WorkItems;

namespace WorkItemManagement.Core.Commands.CreateCommands
{
    public class CreateStory :  ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;
        private readonly IInputReader inputReader;
        private readonly IOutputWriter outputWriter;

        public CreateStory(IWIMFactory factory, IDatabase database,
                           IInputReader inputReader, IOutputWriter outputWriter)
        {
            this.factory = factory;
            this.database = database;
            this.inputReader = inputReader;
            this.outputWriter = outputWriter;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 7)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string teamName = parameters[0];
            string boardName = parameters[1];
            string storyTitle = parameters[2];
            string priorityType = parameters[3];
            string sizeType = parameters[4];
            string statusType = parameters[5];
            string memberName = parameters[6];

            outputWriter.WriteLine(GlobalConstants.EnterADescription);
            string storyDescription = inputReader.ReadLine();

            bool containsTeam = this.database.ListAllTeams.Any(m => m.Name == teamName);

            if (!containsTeam)
            {
                return string.Format(GlobalConstants.TeamDoesNotExist, teamName);
            }

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            if (!team.ListOfBoards.Any(b => b.Name == boardName))
            {
                return string.Format(GlobalConstants.BoardDoesNotExistInTeam, boardName, teamName);
            }

            var board = team.ListOfBoards.Where(b => b.Name == boardName).FirstOrDefault();

            bool containsMember = this.database.ListAllMembers.Any(m => m.Name == memberName);

            if (!containsMember)
            {
                return string.Format(GlobalConstants.MemberDoesNotExistInTeam, memberName, teamName);
            }

            if (board.ListOfWorkItems.Where(x => x.GetType() == typeof(Story)).Any(y => y.Title == storyTitle))
            {
                return string.Format(GlobalConstants.StoryAlreadyExistInBoard, storyTitle, boardName);
            }

            var member = database.ListAllMembers.Where(m => m.Name == memberName).FirstOrDefault();

            var story = this.factory.CreateStory(storyTitle, storyDescription,
                                      (PriorityType)Enum.Parse(typeof(PriorityType), priorityType, true),
                                      (StorySizeType)Enum.Parse(typeof(StorySizeType), sizeType, true),
                                      (StoryStatusType)Enum.Parse(typeof(StoryStatusType), statusType, true), member);

            board.AddBoardWorkItem(story);
            member.AssignMemberWorkItem(story);

            string result = string.Format(GlobalConstants.StoryCreated, storyTitle, storyDescription, boardName);

            var activity = this.factory.CreateActivity(result, member);

            member.AddMemberActivity(activity);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
