using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.ChangeCommands;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.UnitTests.Commands
{
    [TestClass]
    public class ChangeBugSeverityShould
    {
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsNotValid()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var command = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);

            var input = new List<string>() { "Ivan Ivanov", "2" };

            var ex = Assert.ThrowsException<ArgumentException>(() => command.Execute(input));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenNoBoardFoundInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Low" };
            var fakeTeamList = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);
            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            mockedDatabase.Object.ListAllTeams.Add(mockedTeam.Object);
            var mockedBoard = new Mock<IBoard>();

            mockedBoard.Setup(x => x.Name).Returns("Team14B");

            var fakeListOfBoardsTeam = new List<IBoard>();

            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeListOfBoardsTeam);
           
            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);
            string expected = string.Format(GlobalConstants.BoardDoesNotExistInTeam, "Team14B", "Team14");

            Assert.AreEqual(expected, sut.Execute(parameters));
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenNoTeamFoundInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Low" };
            var fakeTeamList = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);
            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);
            string expected = string.Format(GlobalConstants.TeamDoesNotExist, "Team14");

            Assert.AreEqual(expected, sut.Execute(parameters));
        }

        [TestMethod]
        public void ExecuteMethodPassThrowChangeSeverityMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Minor" };

            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);

            var fakeTeamList = new List<ITeam>();
            var fakeTeamBoardList = new List<IBoard>();

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeTeamBoardList);
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            mockedDatabase.Object.ListAllTeams.Add(mockedTeam.Object);

            var mockedBoard = new Mock<IBoard>();
            mockedBoard.Setup(x => x.Name).Returns("Team14B");

            mockedTeam.Object.ListOfBoards.Add(mockedBoard.Object);

            var mockedBug = new Mock<IBug>();
            mockedBug.Setup(x => x.Title).Returns("BugInTeam14B");

            mockedBug.Setup(x => x.ChangeSeverity("Minor"));
            var fakeListWorkItemsOfBoard = new List<IWorkItem>();
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItemsOfBoard);

            mockedBoard.Object.ListOfWorkItems.Add(mockedBug.Object);

            sut.Execute(parameters);
            mockedBug.Verify(x => x.ChangeSeverity("Minor"), Times.Once);
        }

        [TestMethod]
        public void ExecuteMethodPassThrowCreateActivityMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Minor" };

            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);

            var fakeTeamList = new List<ITeam>();
            var fakeTeamBoardList = new List<IBoard>();

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeTeamBoardList);
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            mockedDatabase.Object.ListAllTeams.Add(mockedTeam.Object);

            var mockedBoard = new Mock<IBoard>();
            mockedBoard.Setup(x => x.Name).Returns("Team14B");

            mockedTeam.Object.ListOfBoards.Add(mockedBoard.Object);

            var mockedBug = new Mock<IBug>();
            mockedBug.Setup(x => x.Title).Returns("BugInTeam14B");

            var fakeListWorkItemsOfBoard = new List<IWorkItem>();
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItemsOfBoard);

            mockedBoard.Object.ListOfWorkItems.Add(mockedBug.Object);

            mockedBug.Setup(x => x.ChangeSeverity("Minor"));

            string expected = string.Format(GlobalConstants.BugSeverityWasChanged, "BugInTeam14B", "Minor");

            var mockedActivity = new Mock<IActivity>();
            sut.Execute(parameters);
            mockedFactory.Verify(x => x.CreateActivity(It.IsAny<string>()));
        }

        [TestMethod]
        public void ExecuteMethodPassThrowAddBoardActivityMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Minor" };


            var fakeTeamList = new List<ITeam>();
            var fakeTeamBoardList = new List<IBoard>();
            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeTeamBoardList);
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            mockedDatabase.Object.ListAllTeams.Add(mockedTeam.Object);

            var mockedBoard = new Mock<IBoard>();
            mockedBoard.Setup(x => x.Name).Returns("Team14B");

            mockedTeam.Object.ListOfBoards.Add(mockedBoard.Object);

            var mockedBug = new Mock<IBug>();
            mockedBug.Setup(x => x.Title).Returns("BugInTeam14B");

            var fakeListWorkItemsOfBoard = new List<IWorkItem>();
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItemsOfBoard);

            mockedBoard.Object.ListOfWorkItems.Add(mockedBug.Object);
            var mockedActivity = new Mock<IActivity>();
            
            mockedBug.Setup(x => x.ChangeSeverity("Minor"));

            string expected = string.Format(GlobalConstants.BugSeverityWasChanged, "BugInTeam14B", "Minor");

            mockedFactory.Setup(x => x.CreateActivity(It.IsAny<string>()));
            sut.Execute(parameters);
            mockedBoard.Object.AddBoardActivity(mockedActivity.Object);

        }

        [TestMethod]
        public void ExecuteMethodPassThrowAddTeamActivityMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Minor" };

            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);

            var fakeTeamList = new List<ITeam>();
            var fakeTeamBoardList = new List<IBoard>();

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeTeamBoardList);
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            mockedDatabase.Object.ListAllTeams.Add(mockedTeam.Object);

            var mockedBoard = new Mock<IBoard>();
            mockedBoard.Setup(x => x.Name).Returns("Team14B");

            mockedTeam.Object.ListOfBoards.Add(mockedBoard.Object);

            var mockedBug = new Mock<IBug>();
            mockedBug.Setup(x => x.Title).Returns("BugInTeam14B");

            var fakeListWorkItemsOfBoard = new List<IWorkItem>();
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItemsOfBoard);

            mockedBoard.Object.ListOfWorkItems.Add(mockedBug.Object);

            mockedBug.Setup(x => x.ChangeSeverity("Minor"));

            string expected = string.Format(GlobalConstants.BugSeverityWasChanged, "BugInTeam14B", "Minor");

            var mockedActivity = new Mock<IActivity>();

            mockedFactory.Setup(x => x.CreateActivity(It.IsAny<string>()));

            mockedTeam.Object.AddTeamActivity(mockedActivity.Object);

            sut.Execute(parameters);
            mockedTeam.Verify(x => x.AddTeamActivity(mockedActivity.Object));
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectResultWithCorrectInput()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Minor" };

            var sut = new ChangeBugSeverity(mockedFactory.Object, mockedDatabase.Object);

            var fakeTeamList = new List<ITeam>();
            var fakeTeamBoardList = new List<IBoard>();

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeTeamBoardList);
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            mockedDatabase.Object.ListAllTeams.Add(mockedTeam.Object);

            var mockedBoard = new Mock<IBoard>();
            mockedBoard.Setup(x => x.Name).Returns("Team14B");

            mockedTeam.Object.ListOfBoards.Add(mockedBoard.Object);

            var mockedBug = new Mock<IBug>();
            mockedBug.Setup(x => x.Title).Returns("BugInTeam14B");

            var fakeListWorkItemsOfBoard = new List<IWorkItem>();
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItemsOfBoard);

            mockedBoard.Object.ListOfWorkItems.Add(mockedBug.Object);

            mockedBug.Setup(x => x.ChangeSeverity("Minor"));

            string expected = string.Format(GlobalConstants.BugSeverityWasChanged, "BugInTeam14B", "Minor");

            Assert.AreEqual(expected, sut.Execute(parameters));
        }
    }
}
