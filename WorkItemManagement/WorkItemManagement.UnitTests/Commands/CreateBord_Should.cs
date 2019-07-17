using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.UnitTests.Commands
{
    [TestClass]
    public class CreateBord_Should
    {
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsNotValid()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var command = new CreateBoard(mockedFactory.Object, mockedDatabase.Object);

            var input = new List<string>() { "BoardName"};

            var ex = Assert.ThrowsException<ArgumentException>(() => command.Execute(input));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenTeamIsMissingInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var fakeList = new List<ITeam>();

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeList);

            var command = new CreateBoard(mockedFactory.Object, mockedDatabase.Object);
            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.Name).Returns("Teamteam");
            fakeList.Add(mockedTeam.Object);

            var parameters = new List<string> { "BoardName","Team14" };
            var expected = string.Format(GlobalConstants.TeamDoesNotExist, "Team14");
            Assert.AreEqual(expected, command.Execute(parameters));
        }

    }
}
