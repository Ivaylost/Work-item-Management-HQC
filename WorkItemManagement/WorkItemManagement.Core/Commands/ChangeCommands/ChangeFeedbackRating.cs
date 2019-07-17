using System;
using System.Collections.Generic;
using System.Linq;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models.WorkItems;

namespace WorkItemManagement.Core.Commands.ChangeCommands
{
    public class ChangeFeedbackRating : ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public ChangeFeedbackRating(IWIMFactory factory, IDatabase database)
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
            string feedbackTitle = parameters[2];
            string rating = parameters[3];

            bool checkRating = int.TryParse(parameters[3], out int newRating);

            if (!checkRating)
            {
                throw new ArgumentException(GlobalConstants.InvalidRating);
            }

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

            var feedbackItem = board.ListOfWorkItems.Where(x => x.GetType() ==
                      typeof(FeedBack)).FirstOrDefault(y => y.Title == feedbackTitle);

            IFeedback newFeedbackItem = (IFeedback)feedbackItem;

            newFeedbackItem.ChangeRating(newRating);
            string result = string.Format(GlobalConstants.RatingWasChanged, feedbackTitle, newRating);
            var activity = this.factory.CreateActivity(result);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
