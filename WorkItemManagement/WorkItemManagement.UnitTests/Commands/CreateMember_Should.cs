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
    public class CreateMember_Should
    {

        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsNotValid()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateMember(mockedFactory.Object, mockedDatabase.Object);

            var parameters = new List<string>() { "Ivan Ivanov", "2" };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsSmaller()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateMember(mockedFactory.Object, mockedDatabase.Object);

            var parameters = new List<string>(){};

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);
        }

        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenMemberAlreadyExistsDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var fakeList = new List<IMember>();

            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeList);

            var command = new CreateMember(mockedFactory.Object, mockedDatabase.Object);
            var mockedMember = new Mock<IMember>();
            mockedMember.Setup(x => x.Name).Returns("IvanIvanov");
            fakeList.Add(mockedMember.Object);

            var parameters = new List<string> { "IvanIvanov" };
            var expected = string.Format(GlobalConstants.MemberAlreadyExist, "IvanIvanov");
            Assert.AreEqual(expected, command.Execute(parameters));
        }

        [TestMethod]
        public void ExecuteMethodPassThrowAddMemberToListMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateMember(mockedFactory.Object, mockedDatabase.Object);

            var mockedMember = new Mock<IMember>();

            var parameters = new List<string>() { "IvanIvanov" };
            var fakeListMembers = new List<IMember>();
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListMembers);

            mockedFactory.Setup(x => x.CreateMember("IvanIvanov")).Returns(mockedMember.Object);

            var expected = string.Format(GlobalConstants.MemberCreated, "IvanIvanov");

            var mockedActivity = new Mock<IActivity>();

            mockedFactory.Setup(x => x.CreateActivity(expected, mockedMember.Object));

            mockedMember.Setup(x => x.AddMemberActivity(mockedActivity.Object));

            sut.Execute(parameters);
            mockedDatabase.Verify(x => x.AddMemberToList(mockedMember.Object), Times.Once);

        }

        [TestMethod]
        public void ExecuteMethodPassCreateMemberMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateMember(mockedFactory.Object, mockedDatabase.Object);

            var mockedMember = new Mock<IMember>();

            var parameters = new List<string>() { "IvanIvanov" };
            var fakeListMembers = new List<IMember>();
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListMembers);

            mockedFactory.Setup(x => x.CreateMember("IvanIvanov")).Returns(mockedMember.Object);

            mockedDatabase.Setup(x => x.AddMemberToList(mockedMember.Object));

            var expected = string.Format(GlobalConstants.MemberCreated, "IvanIvanov");

            var mockedActivity = new Mock<IActivity>();

            mockedFactory.Setup(x => x.CreateActivity(expected, mockedMember.Object));

            mockedMember.Setup(x => x.AddMemberActivity(mockedActivity.Object));
            sut.Execute(parameters);

            mockedFactory.Verify(x => x.CreateMember(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ExecuteMethodPassCreateActivityMethod()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateMember(mockedFactory.Object, mockedDatabase.Object);

            var mockedMember = new Mock<IMember>();

            var parameters = new List<string>() { "IvanIvanov" };
            var fakeListMembers = new List<IMember>();
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListMembers);

            mockedFactory.Setup(x => x.CreateMember("IvanIvanov")).Returns(mockedMember.Object);

            mockedDatabase.Setup(x => x.AddMemberToList(mockedMember.Object));

            var expected = string.Format(GlobalConstants.MemberCreated, "IvanIvanov");

            var mockedActivity = new Mock<IActivity>();

            mockedMember.Setup(x => x.AddMemberActivity(mockedActivity.Object));
            sut.Execute(parameters);
            mockedFactory.Verify(x => x.CreateActivity(It.IsAny<string>(), mockedMember.Object),Times.Once);
        }

        public void ExecuteMethodReturnsCorrectResultWithCorrectInput()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();

            var sut = new CreateMember(mockedFactory.Object, mockedDatabase.Object);

            var mockedMember = new Mock<IMember>();

            var parameters = new List<string>() { "IvanIvanov" };
            var fakeListMembers = new List<IMember>();
            mockedDatabase.Setup(x => x.ListAllMembers).Returns(fakeListMembers);

            mockedFactory.Setup(x => x.CreateMember("IvanIvanov")).Returns(mockedMember.Object);

            mockedDatabase.Setup(x => x.AddMemberToList(mockedMember.Object));

            var expected = string.Format(GlobalConstants.MemberCreated, "IvanIvanov");

            var mockedActivity = new Mock<IActivity>();

            mockedFactory.Setup(x => x.CreateActivity(expected, mockedMember.Object));

            mockedMember.Setup(x => x.AddMemberActivity(mockedActivity.Object));

            Assert.AreEqual(expected, sut.Execute(parameters));
        }
    }
}

