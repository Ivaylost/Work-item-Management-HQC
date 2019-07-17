﻿using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands
{
    public class AssignWorkItemToMember : ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public AssignWorkItemToMember(IWIMFactory factory, IDatabase database)
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

            var newTask = (ITask)board.ListOfWorkItems.Where(w => w is ITask &&
                                                            w.Title == workItemTitle).FirstOrDefault();

            var member = database.ListAllMembers.Where(m => m.Name == memberName).FirstOrDefault();
            var oldMember = newTask.Assignee;

            member.AssignMemberWorkItem(newTask);
            oldMember.UnAssignMemberWorkItem(newTask);

            string result = string.Format(GlobalConstants.AssignWorkItemByMember, workItemTitle, memberName);

            var activity = this.factory.CreateActivity(result, member);
            member.AddMemberActivity(activity);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
