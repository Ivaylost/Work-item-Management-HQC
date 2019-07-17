using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.UnitTests.Commands
{
    [TestClass]
    public class AssignWorkItemToMember_Should
    {

        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsNotValid()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new AssignWorkItemToMember(mockedFactory.Object, mockedDatabase.Object);

            var input = new List<string>() { "Ivan Ivanov", "2" };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(input));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenNoTeamFoundInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "Low" };
            var fakeTeamList = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);
            var sut = new AssignWorkItemToMember(mockedFactory.Object, mockedDatabase.Object);
            string expected = string.Format(GlobalConstants.TeamDoesNotExist, "Team14");

            Assert.AreEqual(expected, sut.Execute(parameters));
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

            var sut = new AssignWorkItemToMember(mockedFactory.Object, mockedDatabase.Object);
            string expected = string.Format(GlobalConstants.BoardDoesNotExistInTeam, "Team14B", "Team14");

            Assert.AreEqual(expected, sut.Execute(parameters));
        }

        [TestMethod]
        public void ExecuteMethodPassThrowAssignMemberMethod()
        {
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedDatabase = new Mock<IDatabase>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "IvanIvanov" };

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            var fakeTeamList = new List<ITeam>() { mockedTeam.Object };

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedBoard = new Mock<IBoard>();
            var fakeListOfBoards = new List<IBoard>() { mockedBoard.Object };
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeListOfBoards);
            mockedBoard.Setup(x => x.Name).Returns("Team14B");
            var mockedTask = new Mock<ITask>();
            mockedTask.Setup(x => x.Title).Returns("BugInTeam14B");
            var fakeListWorkItems = new List<IWorkItem> { mockedTask.Object };
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItems);

            var mockedMember = new Mock<IMember>();
            mockedMember.Setup(x => x.AssignMemberWorkItem(mockedTask.Object));
            var fakeListOfMembers = new List<IMember>() { mockedMember.Object };
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListOfMembers);
            mockedMember.Setup(x => x.Name).Returns("IvanIvanov");
            var mockedOldMember = new Mock<IMember>();
            mockedOldMember.Setup(x => x.UnAssignMemberWorkItem(mockedTask.Object));
            mockedOldMember.Setup(x => x.Name).Returns("PeterPetrov");
            mockedTask.Setup(x => x.Assignee).Returns(mockedOldMember.Object);

            var sut = new AssignWorkItemToMember(mockedFactory.Object, mockedDatabase.Object);
            sut.Execute(parameters);
            mockedMember.Verify(x => x.AssignMemberWorkItem(mockedTask.Object));
        }

        [TestMethod]
        public void ExecuteMethodPassThrowAddMemberActivity()
        {
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedDatabase = new Mock<IDatabase>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "IvanIvanov" };

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            var fakeTeamList = new List<ITeam>() { mockedTeam.Object };

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedBoard = new Mock<IBoard>();
            var fakeListOfBoards = new List<IBoard>() { mockedBoard.Object };
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeListOfBoards);
            mockedBoard.Setup(x => x.Name).Returns("Team14B");
            var mockedTask = new Mock<ITask>();
            mockedTask.Setup(x => x.Title).Returns("BugInTeam14B");
            var fakeListWorkItems = new List<IWorkItem> { mockedTask.Object };
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItems);

            var mockedMember = new Mock<IMember>();
            var fakeListOfMembers = new List<IMember>() { mockedMember.Object };
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListOfMembers);
            mockedMember.Setup(x => x.Name).Returns("IvanIvanov");
            var mockedActivity = new Mock<IActivity>();

            mockedMember.Setup(x => x.AddMemberActivity(mockedActivity.Object)).Verifiable();
            mockedMember.Object.AddMemberActivity(mockedActivity.Object);
            var mockedOldMember = new Mock<IMember>();
            mockedOldMember.Setup(x => x.UnAssignMemberWorkItem(mockedTask.Object));
            mockedOldMember.Setup(x => x.Name).Returns("PeterPetrov");
            mockedTask.Setup(x => x.Assignee).Returns(mockedOldMember.Object);
        
            var sut = new AssignWorkItemToMember(mockedFactory.Object, mockedDatabase.Object);

            sut.Execute(parameters);
            mockedMember.Verify(x => x.AddMemberActivity(mockedActivity.Object), Times.Once);

        }


        [TestMethod]
        public void ExecuteMethodReturnsCorrectResultWithCorrectInput()
        {
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedDatabase = new Mock<IDatabase>();

            var parameters = new List<string>() { "Team14", "Team14B", "BugInTeam14B", "IvanIvanov" };

            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            var fakeTeamList = new List<ITeam>() { mockedTeam.Object };
           
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeTeamList);

            var mockedBoard = new Mock<IBoard>();
            var fakeListOfBoards = new List<IBoard>() { mockedBoard.Object };
            mockedTeam.Setup(x => x.ListOfBoards).Returns(fakeListOfBoards);
            mockedBoard.Setup(x => x.Name).Returns("Team14B");
            var mockedTask = new Mock<ITask>();
            mockedTask.Setup(x => x.Title).Returns("BugInTeam14B");
            var fakeListWorkItems = new List<IWorkItem> { mockedTask.Object };
            mockedBoard.Setup(x => x.ListOfWorkItems).Returns(fakeListWorkItems);

            var mockedMember = new Mock<IMember>();
            var fakeListOfMembers = new List<IMember>() { mockedMember.Object };
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListOfMembers);
            mockedMember.Setup(x => x.Name).Returns("IvanIvanov");
            mockedMember.Setup(x => x.AssignMemberWorkItem(mockedTask.Object));
            var mockedOldMember = new Mock<IMember>();
            mockedOldMember.Setup(x => x.UnAssignMemberWorkItem(mockedTask.Object));
            mockedOldMember.Setup(x => x.Name).Returns("PeterPetrov");
            mockedTask.Setup(x => x.Assignee).Returns(mockedOldMember.Object);

            var mockedActivity = new Mock<IActivity>();
            mockedFactory.Setup(x => x.CreateActivity(It.IsAny<string>()));

            string expected = string.Format(GlobalConstants.AssignWorkItemByMember, "BugInTeam14B", "IvanIvanov");
            var sut = new AssignWorkItemToMember(mockedFactory.Object, mockedDatabase.Object);

            Assert.AreEqual(expected, sut.Execute(parameters));
        }

    }
}
