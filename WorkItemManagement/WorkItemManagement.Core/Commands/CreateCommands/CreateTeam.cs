using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkItemManagement.Core.Commands.Abstract;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.Core.Commands
{
    public class CreateTeam : ICommand
    {
        private readonly IDatabase database;
        private readonly IWIMFactory factory;

        public CreateTeam(IWIMFactory factory, IDatabase database)
            
        {
            this.factory = factory;
            this.database = database;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                throw new ArgumentException(GlobalConstants.ParametersCountInvalid);
            }


            string name = parameters[0];

            bool containsTeam = this.database.ListAllTeams.Any(m => m.Name == name);

            if (containsTeam)
            {
                return string.Format(GlobalConstants.TeamAlreadyExist, name);
            }
            var team = this.factory.CreateTeam(name);
            this.database.AddTeamToList(team);

            string result = string.Format(GlobalConstants.TeamCreated, name);

            var activity = this.factory.CreateActivity(result);
            team.AddTeamActivity(activity);

            return result;
        }
    }
}
