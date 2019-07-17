using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WorkItemManagement.Models;

namespace WorkItemManagement.UnitTests
{
    [TestClass]
    public class Member_Should
    {
        [TestMethod]
        public void ThrowWhenMemberNameIsSmallerThanMinValue()
        {
            // Arrange

            Assert.ThrowsException<ArgumentException>(() => new Member("Pe"));

            // Act

            // Assert
        }

        [TestMethod]
        public void ThrowWhenMemberNameIsLargerThanMaxValue()
        {
            // Arrange


            Assert.ThrowsException<ArgumentException>(() => new Member("Pesho Peshev Peshev"));

            // Act

            // Assert

        }
    }
}
