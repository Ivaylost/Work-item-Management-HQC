using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Contracts.IO;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands.Add
{
    public class AddCommentToWorkItem : ICommand
    {
        private readonly IInputReader inputReader;
        private readonly IOutputWriter outputWriter;
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public AddCommentToWorkItem(IWIMFactory factory, IDatabase database,
                                    IInputReader inputReader, IOutputWriter outputWriter)
        {
            this.factory = factory;
            this.database = database;
            this.inputReader = inputReader;
            this.outputWriter = outputWriter;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 4)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }
            
            string teamName = parameters[0];
            string boardName = parameters[1];
            string memberName = parameters[2];
            string workItemTitle = parameters[3];

            outputWriter.WriteLine(GlobalConstants.EnterADescription);
            string commentDescription = inputReader.ReadLine();

            bool containsTeam = this.database.ListAllTeams.Any(m => m.Name == teamName);

            if (!containsTeam)
            {
                return string.Format(GlobalConstants.TeamDoesNotExist, teamName);
            }

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            bool containsBoard = team.ListOfBoards.Any(b => b.Name == boardName);

            if (!containsBoard)
            {
                return string.Format(GlobalConstants.BoardDoesNotExistInTeam, boardName, teamName);
            }

            bool containsMember = this.database.ListAllMembers.Any(m => m.Name == memberName);

            if (!containsMember)
            {
                return string.Format(GlobalConstants.MemberDoesNotExist, memberName);
            }

            var member = database.ListAllMembers.FirstOrDefault(m => m.Name == memberName);

            var board = team.ListOfBoards.Where(b => b.Name == boardName).FirstOrDefault();

            var workItem = board.ListOfWorkItems.Where(w => w is ITask &&
                                                            w.Title == workItemTitle).FirstOrDefault();
            IWorkItem newWorkItem = (IWorkItem)workItem;

            var comment = factory.CreateComment(commentDescription, member);

            newWorkItem.AddComment(comment);

            string result = string.Format(GlobalConstants.CommentWasAddedToAWorkItem, commentDescription, workItemTitle);
            var activity = this.factory.CreateActivity(result);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
