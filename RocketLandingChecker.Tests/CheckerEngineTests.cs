using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace RocketLandingChecker.Tests
{
    [TestClass]
    public class CheckerEngineTests
    {
        [TestMethod]
        public void CheckPosition_OkForLanding()
        {
            //Arrange
            CheckerEngine checker = new CheckerEngine();
            Point positionRocket1 = new Point(10, 10);

            //Act
            var result = checker.CheckPosition(1, positionRocket1);

            //Assert
            Assert.IsTrue(result == ReturnCodes.OkForLanding);
        }

        [TestMethod]
        public void CheckPosition_IsPositionOutsidePlatform()
        {
            //Arrange
            Point positionRocket1 = new Point(103, 10);
            CheckerEngine checker = new CheckerEngine();

            //Act
            var result = checker.CheckPosition(1, positionRocket1);

            //Assert
            Assert.IsTrue(result == ReturnCodes.OutOfPlatform);
        }

        [TestMethod]
        public void CheckPosition_HasPositionAlreadyBeenChecked()
        {
            //Arrange
            CheckerEngine checker = new CheckerEngine();
            Point positionRocket1 = new Point(10, 10);
            Point positionRocket2 = new Point(15, 15);
            Point positionRocket3 = new Point(10, 10);            
            checker.CheckPosition(1, positionRocket1);
            checker.CheckPosition(2, positionRocket2);

            //Act
            var result = checker.CheckPosition(3, positionRocket3);

            //Assert
            Assert.IsTrue(result == ReturnCodes.Clash);
        }


        [TestMethod]
        public void CheckPosition_IsPositionNextTo_Xminus1AndYminus1()
        {
            //Arrange
            Point positionRocket1 = new Point(10, 10);
            Point positionRocket2 = new Point(9, 9);
            CheckerEngine checker = new CheckerEngine();
            checker.CheckPosition(1, positionRocket1);

            //Act
            var result = checker.CheckPosition(2, positionRocket2);

            //Assert
            Assert.IsTrue(result == ReturnCodes.Clash);
        }

        [TestMethod]
        public void CheckPosition_IsPositionNextTo_XAndYminus1()
        {
            //Arrange
            Point positionRocket1 = new Point(10, 10);
            Point positionRocket2 = new Point(10, 9);
            CheckerEngine checker = new CheckerEngine();
            checker.CheckPosition(1, positionRocket1);

            //Act
            var result = checker.CheckPosition(2, positionRocket2);

            //Assert
            Assert.IsTrue(result == ReturnCodes.Clash);
        }
    }
}
