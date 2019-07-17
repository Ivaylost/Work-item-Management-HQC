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

namespace WorkItemManagement.Core.Commands
{
    public class CreateFeedback :  ICommand
    {
        private readonly IInputReader inputReader;
        private readonly IOutputWriter outputWriter;
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public CreateFeedback(IWIMFactory factory, IDatabase database,
                              IInputReader inputReader, IOutputWriter outputWriter)
            
        {
            this.factory = factory;
            this.database = database;
            this.inputReader = inputReader;
            this.outputWriter = outputWriter;
        }

        public  string Execute(IList<string> parameters)
        {
            if (parameters.Count != 5)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }

            string teamName = parameters[0];
            string boardName = parameters[1];
            string feedbackName = parameters[2];
            string statusType = parameters[3];

            bool checkRating = int.TryParse(parameters[4], out int rating);

            if (!checkRating)
            {
                throw new ArgumentException(GlobalConstants.InvalidRating);
            }

            outputWriter.WriteLine(GlobalConstants.EnterADescription);
            string feedbackDescription = inputReader.ReadLine();

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

            if (board.ListOfWorkItems.Where(x => x.GetType() == typeof(FeedBack)).Any(y => y.Title == feedbackName))
            {
                return string.Format(GlobalConstants.BugAlreadyExistInBoard, feedbackName, boardName);
            }

            var feedback = this.factory.CreateFeedback(feedbackName, feedbackDescription, rating,
                                      (FeedbackStatusType)Enum.Parse(typeof(FeedbackStatusType), statusType, true));

            board.AddBoardWorkItem(feedback);

            string result = string.Format(GlobalConstants.FeedbackCreated, feedbackName, feedbackDescription, boardName);

            var activity = this.factory.CreateActivity(result);
            board.AddBoardActivity(activity);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
