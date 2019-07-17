using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorkItemManagement.Contracts;
using WorkItemManagement.Core.Commands.Global;

namespace WorkItemManagement.UnitTests.Global
{
    [TestClass]
    public class Database_Should
    {
        [TestMethod]
        public void ConstructorCorrectlyAssaignToListOfAllMembers()
        {
            //Arange
           
            var sut = new Database();

            //Act, Assert
            Assert.IsNotNull(sut.ListAllMembers);
        }

        [TestMethod]
        public void ConstructorCorrectlyAssaignToListOfAllTeams()
        {
            //Arange
            var sut = new Database();

            //Act, Assert
            Assert.IsNotNull(sut.ListAllTeams);
        }

        [TestMethod]
        public void CorrectlyAddMemberToListOfAllMembers()
        {
            //Arange
            var mockedMember = new Mock<IMember>();

            var sut = new Database();

            //Act
            sut.AddMemberToList(mockedMember.Object);

            //Assert
            Assert.AreEqual(1, sut.ListAllMembers.Count);
        }

        [TestMethod]
        public void CorrectlyAddTeamToListOfAllTeams()
        {
            //Arange
            var mockedTeam = new Mock<ITeam>();

            var sut = new Database();

            //Act
            sut.AddTeamToList(mockedTeam.Object);

            //Asser
            Assert.AreEqual(1, sut.ListAllTeams.Count);
        }
    }
}
