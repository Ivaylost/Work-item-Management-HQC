using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Add;
using WorkItemManagement.Core.Commands.Global.Contracts;
using WorkItemManagement.Core.Contracts.IO;
using WorkItemManagement.Core.Factories;
using WorkItemManagement.Core.Factory.Contracts;
using WorkItemManagement.Core.Utills;
using WorkItemManagement.Models;
using WorkItemManagement.Models.Interfaces;

namespace WorkItemManagement.UnitTests.Commands
{
    [TestClass]
    public class AddCommentToWorkItem_Should
    {
        
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsLargerThanExpected()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedInputReader = new Mock<IInputReader>();
            var mockedOutputWriter = new Mock<IOutputWriter>();

            var sut = new AddCommentToWorkItem(mockedFactory.Object,
                mockedDatabase.Object, mockedInputReader.Object, mockedOutputWriter.Object);

            var parameturs = new List<string>() { "A", "A", "A", "A", "A", "A" };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameturs));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);

        }
        [TestMethod]
        public void ExecuteMethodThrowExeptionWhenInputParametersCountIsSmalerThanExpected()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedInputReader = new Mock<IInputReader>();
            var mockedOutputWriter = new Mock<IOutputWriter>();

            var sut = new AddCommentToWorkItem(mockedFactory.Object,
                mockedDatabase.Object, mockedInputReader.Object, mockedOutputWriter.Object);

            var parameturs = new List<string>() { "A", "A", "A" };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameturs));

            Assert.AreEqual(GlobalConstants.ParametersCountInvalid, ex.Message);

        }
        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenTeamIsNotFoundInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedInputReader = new Mock<IInputReader>();
            var mockedOutputWriter = new Mock<IOutputWriter>();

            var sut = new AddCommentToWorkItem(mockedFactory.Object,
                mockedDatabase.Object, mockedInputReader.Object, mockedOutputWriter.Object);

            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(t => t.Name).Returns("IvanIvanov");
            var fakeList = new List<ITeam>();
            mockedDatabase.Setup(d => d.ListAllTeams).Returns(fakeList);
            fakeList.Add(mockTeam.Object);

            var parameters = new List<string>() { "IvanIvano", "A", "A" ,"A"};

            Assert.AreEqual(string.Format(GlobalConstants.TeamDoesNotExist, "IvanIvano"), sut.Execute(parameters));
        }
        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenBoardIsNotFoundInTeam()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedInputReader = new Mock<IInputReader>();
            var mockedOutputWriter = new Mock<IOutputWriter>();

            var sut = new AddCommentToWorkItem(mockedFactory.Object,
                mockedDatabase.Object, mockedInputReader.Object, mockedOutputWriter.Object);

            var fakeListOfBoards = new List<IBoard>();
            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(t => t.Name).Returns("Team14");
            mockTeam.Setup(t => t.ListOfBoards).Returns(fakeListOfBoards);
            

            var fakeList = new List<ITeam>();
            mockedDatabase.Setup(d => d.ListAllTeams).Returns(fakeList);
            fakeList.Add(mockTeam.Object);

            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.Name).Returns("Tea14B");

            fakeListOfBoards.Add(mockBoard.Object);

            var parameters = new List<string>() { "Team14", "Team14B", "A", "A" };

            var expected = sut.Execute(parameters);

            Assert.AreEqual(string.Format(GlobalConstants.BoardDoesNotExistInTeam, "Team14B", "Team14"), expected);
        }
        [TestMethod]
        public void ExecuteMethodReturnsCorrectMessageWhenMemberIsNotFoundInDatabase()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedInputReader = new Mock<IInputReader>();
            var mockedOutputWriter = new Mock<IOutputWriter>();

            var sut = new AddCommentToWorkItem(mockedFactory.Object,
                mockedDatabase.Object, mockedInputReader.Object, mockedOutputWriter.Object);

            var fakeListOfBoards = new List<IBoard>();
            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(t => t.Name).Returns("Team14");
            mockTeam.Setup(t => t.ListOfBoards).Returns(fakeListOfBoards);


            var fakeList = new List<ITeam>();
            mockedDatabase.Setup(d => d.ListAllTeams).Returns(fakeList);
            fakeList.Add(mockTeam.Object);

            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.Name).Returns("Team14B");

            fakeListOfBoards.Add(mockBoard.Object);

            var fakeListOfMembers = new List<IMember>();
            mockedDatabase.Setup(d => d.ListAllMembers).Returns(fakeListOfMembers);
            var mokedMember = new Mock<IMember>();
            mokedMember.Setup(m => m.Name).Returns("IvanIvano");
            fakeListOfMembers.Add(mokedMember.Object);

            var parameters = new List<string>() { "Team14", "Team14B", "IvanIvanov", "A" };

            var expected = sut.Execute(parameters);

            Assert.AreEqual(string.Format(GlobalConstants.MemberDoesNotExist, "IvanIvanov"), expected);
        }
        [TestMethod]
        public void ExecuteMethodCorectlyAssigneComment()
        {
            var mockedDatabase = new Mock<IDatabase>();
            var mockedFactory = new Mock<IWIMFactory>();
            var mockedInputReader = new Mock<IInputReader>();
            var mockedOutputWriter = new Mock<IOutputWriter>();

            var sut = new AddCommentToWorkItem(mockedFactory.Object,
                mockedDatabase.Object, mockedInputReader.Object, mockedOutputWriter.Object);

            var fakeListOfBoards = new List<IBoard>();
            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(t => t.Name).Returns("Team14");
            mockTeam.Setup(t => t.ListOfBoards).Returns(fakeListOfBoards);
            

            var fakeList = new List<ITeam>();
            mockedDatabase.Setup(d => d.ListAllTeams).Returns(fakeList);
            fakeList.Add(mockTeam.Object);


            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(b => b.Name).Returns("Team14B");

            fakeListOfBoards.Add(mockBoard.Object);

            var fakeListOfMembers = new List<IMember>();
            mockedDatabase.Setup(d => d.ListAllMembers).Returns(fakeListOfMembers);
            var mokedMember = new Mock<IMember>();
            mokedMember.Setup(m => m.Name).Returns("IvanIvanov");
            fakeListOfMembers.Add(mokedMember.Object);

            var parameters = new List<string>() { "Team14", "Team14B", "IvanIvanov", "WorkItemT" };


            mockedInputReader.Setup(i => i.ReadLine()).Returns("Comment");


            string commentdescription = "Comment";

            mockedFactory.Setup(f => f.CreateComment(commentdescription, mokedMember.Object)).Returns(new Comment(commentdescription, mokedMember.Object));
            var mockedcomment = new Mock<IComment>();
            
            var listofWorkItems = new List<IWorkItem>();
            var mockedWorkItem = new Mock<ITask>();


            mockedWorkItem.Setup(w => w.AddComment(mockedcomment.Object));
            mockedWorkItem.Setup(w => w.Title).Returns("WorkItemT");
            listofWorkItems.Add(mockedWorkItem.Object);
            mockBoard.Setup(b => b.ListOfWorkItems).Returns(listofWorkItems);
         

            var expected = sut.Execute(parameters);

            Assert.AreEqual(string.Format(GlobalConstants.CommentWasAddedToAWorkItem, "Comment", "WorkItemT"), expected);
        }


    }
}
