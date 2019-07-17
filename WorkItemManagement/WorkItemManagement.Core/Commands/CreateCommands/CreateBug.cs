using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Abstract;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Contracts.IO;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Providers;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Enums;
using WorkItemManagement.Models.WorkItems;

namespace WorkItemManagement.Core.Commands.CreateCommands
{

    public class CreateBug :  ICommand
    {
        private readonly IInputReader inputReader;
        private readonly IOutputWriter outputWriter;
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public CreateBug(IWIMFactory factory, IDatabase database,
                         IInputReader inputReader, IOutputWriter outputWriter)
        {
            this.factory = factory;
            this.database = database;
            this.inputReader = inputReader;
            this.outputWriter = outputWriter;
        }
       
        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 6)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string teamName = parameters[0];
            string boardName = parameters[1];
            string bugTitle = parameters[2];
            string priorityType = parameters[3];
            string severityType = parameters[4];
            string memberName = parameters[5];

            outputWriter.WriteLine(GlobalConstants.EnterADescription);
            string bugDescription = inputReader.ReadLine();

            outputWriter.WriteLine("Please enter steps to reproduce!");
            string steps = inputReader.ReadLine();
            List<string> stepToReproduce = steps.Trim().Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

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
            if (board.ListOfWorkItems.Where(x => x.GetType() == typeof(Bug)).Any(y => y.Title == bugTitle))
            {
                return string.Format(GlobalConstants.BugAlreadyExistInBoard, bugTitle, boardName);
            }

            var member = database.ListAllMembers.Where(m => m.Name == memberName).FirstOrDefault();


            var bug = this.factory.CreateBug(bugTitle, bugDescription,
                                      (PriorityType)Enum.Parse(typeof(PriorityType), priorityType, true),
                                      (BugSeverityType)Enum.Parse(typeof(BugSeverityType), severityType, true), 
                                      member, stepToReproduce);


            board.AddBoardWorkItem(bug);
            member.AssignMemberWorkItem(bug);

            string result = string.Format(GlobalConstants.BugCreated, bugTitle, bugDescription, boardName);

            var activity = this.factory.CreateActivity(result, member);
            member.AddMemberActivity(activity);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
