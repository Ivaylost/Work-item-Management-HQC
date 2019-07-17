using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Commands.ShowCommands;
using WorkItemManagement.Core.Utills;

namespace WorkItemManagement.UnitTests.Commands
{
    [TestClass]
    public class ShowAllTeamBoards_Should
    {
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsLargerThanExpected()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var sut =new ShowAllTeamBoards(mockedDatabase.Object);

            var parameters = new List<string>() { "A","A" };
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsSamlerThanExpected()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var sut = new ShowAllTeamBoards(mockedDatabase.Object);

            var parameters = new List<string>() {  };
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }
        [TestMethod]
        public void ExecuteMethodReturnsWhenTeamIsNotInListOfTeams()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var sut = new ShowAllTeamBoards(mockedDatabase.Object);

            var parameters = new List<string>() {"Team14"};

            var fakeListOfTeams = new List<ITeam>();
            var mokedTeam = new Mock<ITeam>();
            mokedTeam.Setup(t => t.Name).Returns("Team13");
            mockedDatabase.Setup(d => d.ListAllTeams).Returns(fakeListOfTeams);
            fakeListOfTeams.Add(mokedTeam.Object);

            var expected = sut.Execute(parameters);

            Assert.AreEqual(expected, string.Format(GlobalConstants.TeamDoesNotExist, "Team14"));
        }
        [TestMethod]
        public void ExecuteMethodReturnsRightValue()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var sut = new ShowAllTeamBoards(mockedDatabase.Object);

            var parameters = new List<string>() { "Team14" };

            var fakeListOfTeams = new List<ITeam>();
            var mokedTeam = new Mock<ITeam>();
            mokedTeam.Setup(t => t.Name).Returns("Team14");
            mockedDatabase.Setup(d => d.ListAllTeams).Returns(fakeListOfTeams);
            fakeListOfTeams.Add(mokedTeam.Object);

            var fakeListOfBoards = new List<IBoard>();
            var mokedBoard = new Mock<IBoard>();
            mokedBoard.Setup(b => b.Name).Returns("Team14B");
            fakeListOfBoards.Add(mokedBoard.Object);           
            mokedTeam.Setup(t => t.ListOfBoards).Returns(fakeListOfBoards);
            mokedTeam.Setup(t => t.ReturnListOfBoards()).Returns("Team14B");

            var expected = sut.Execute(parameters);

            Assert.AreEqual(expected, "All boards in the team: \n" + "Team14B");
            
        }

    }
}
