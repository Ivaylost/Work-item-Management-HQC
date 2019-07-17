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
    public class CreateTeamShould
    {
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsNotValid()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);

            var parameters = new List<string>() { "Team14", "2" };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsSmaller()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);

            var parameters = new List<string>() { };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectWhenTeamAlreadyExistsInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var fakeList = new List<ITeam>();

            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeList);

            var command = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);
            var mockedTeam = new Mock<ITeam>();
            mockedTeam.Setup(x => x.Name).Returns("Team14");
            fakeList.Add(mockedTeam.Object);

            var parameters = new List<string> { "Team14" };
            var expected = string.Format(GlobalConstants.TeamAlreadyExist, "Team14");
            Assert.AreEqual(expected, command.Execute(parameters));
        }

        [TestMethod]
        public void ExecuteMethodPassThrowAddMemberToListMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);

            var mockedTeam = new Mock<ITeam>();

            var parameters = new List<string>() { "Team14" };
            var fakeListTeams = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeListTeams);

            mockedFactory.Setup(x => x.CreateTeam("Team14")).Returns(mockedTeam.Object);

            var expected = string.Format(GlobalConstants.MemberCreated, "Team14");

            var mockedActivity = new Mock<IActivity>();

            mockedFactory.Setup(x => x.CreateActivity(expected));

            mockedTeam.Setup(x => x.AddTeamActivity(mockedActivity.Object));

            sut.Execute(parameters);
            mockedDatabase.Verify(x => x.AddTeamToList(mockedTeam.Object), Times.Once);
        }

        [TestMethod]
        public void ExecuteMethodPassCreateMemberMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);

            var mockedTeam = new Mock<ITeam>();

            var parameters = new List<string>() { "Team14" };
            var fakeListTeams = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeListTeams);

            mockedFactory.Setup(x => x.CreateTeam("Team14")).Returns(mockedTeam.Object);

            mockedDatabase.Setup(x => x.AddTeamToList(mockedTeam.Object));

            var expected = string.Format(GlobalConstants.TeamCreated, "Team14");

            sut.Execute(parameters);

            mockedFactory.Verify(x => x.CreateTeam(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ExecuteMethodPassCreateActivityMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);

            var mockedTeam = new Mock<ITeam>();

            var parameters = new List<string>() { "Team14" };
            var fakeListTeams = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeListTeams);

            mockedFactory.Setup(x => x.CreateTeam("Team14")).Returns(mockedTeam.Object);

            mockedDatabase.Setup(x => x.AddTeamToList(mockedTeam.Object));

            var expected = string.Format(GlobalConstants.TeamCreated, "Team14");

            sut.Execute(parameters);
            mockedFactory.Verify(x => x.CreateActivity(It.IsAny<string>()), Times.Once);
        }

        public void ExecuteMethodReturnsCorrectResultWithCorrectInput()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateTeam(mockedFactory.Object, mockedDatabase.Object);

            var mockedTeam = new Mock<ITeam>();

            var parameters = new List<string>() { "Team14" };
            var fakeListTeams = new List<ITeam>();
            mockedDatabase.Setup(x => x.ListAllTeams).Returns(fakeListTeams);

            mockedFactory.Setup(x => x.CreateTeam("Team14")).Returns(mockedTeam.Object);

            mockedDatabase.Setup(x => x.AddTeamToList(mockedTeam.Object));

            var expected = string.Format(GlobalConstants.TeamCreated, "Team14");

            Assert.AreEqual(expected, sut.Execute(parameters));
        }
    }
}
