using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands
{
    public class UnAssignWorkItemToMember : ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public UnAssignWorkItemToMember(IWIMFactory factory, IDatabase database)
           
        {
            this.factory = factory;
            this.database = database;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 4)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string teamName = parameters[0];
            string boardName = parameters[1];
            string workItemTitle = parameters[2];
            string memberName = parameters[3];
            
            if (!this.database.IsTeamPresentInCollection(teamName))
            {
                return string.Format(GlobalConstants.TeamDoesNotExist, teamName);
            }

            var team = database.ListAllTeams.Where(t => t.Name == teamName).FirstOrDefault();

            bool containsTeam = team.ListOfBoards.Any(b => b.Name == boardName);

            if (!containsTeam)
            {
                return string.Format(GlobalConstants.BoardDoesNotExistInTeam, boardName, teamName);
            }

            var board = team.ListOfBoards.Where(b => b.Name == boardName).FirstOrDefault();

            var workItem = board.ListOfWorkItems.Where(w => w is ITask &&
                                                            w.Title == workItemTitle).FirstOrDefault();

            ITask newWorkItem = (ITask)workItem;

            var member = database.ListAllMembers.Where(m => m.Name == memberName).FirstOrDefault();
            var oldMember = newWorkItem.Assignee;

            member.UnAssignMemberWorkItem(newWorkItem);
            oldMember.AssignMemberWorkItem(newWorkItem);

            string result = string.Format(GlobalConstants.UnAssignWorkItemByMember, workItemTitle, memberName);

            var activity = this.factory.CreateActivity(result, member);
            member.AddMemberActivity(activity);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}

