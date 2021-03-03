using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem_One___Mars_Rovers___Dale_Hartman.Model;

namespace MarsRoversUnitTest
{
    [TestClass]
    public class RoverTests
    {
        [TestMethod]
        public void BasicTurnTest()
        {
            Rover rover = new Rover(0, 0, Heading.E, null, 1, "");
            rover.Turn(Turn.R);

            Assert.AreEqual(rover.Heading, Heading.S);
        }

        [TestMethod]
        public void TurnWraparoundTest()
        {
            Rover rover1 = new Rover(0, 0, Heading.W, null, 1, "");
            Rover rover2 = new Rover(0, 0, Heading.N, null, 1, "");
            rover1.Turn(Turn.R);
            rover2.Turn(Turn.L);

            Assert.AreEqual(rover1.Heading, Heading.N);
            Assert.AreEqual(rover2.Heading, Heading.W);
        }

        [TestMethod]
        public void SafeLookTest()
        {
            Plateau plateau = new Plateau(3, 5);

            Rover rover = new Rover(1, 1, Heading.N, plateau, 1, "");

            Assert.AreEqual(rover.Look(), 0);
        }

        [TestMethod]
        public void UnsafeLookTest()
        {
            Plateau plateau = new Plateau(3, 5);

            Rover rover = new Rover(3, 5, Heading.N, plateau, 1, "");

            Assert.AreEqual(rover.Look(), 1);
        }

        [TestMethod]
        public void DriveTest()
        {
            Rover rover = new Rover(1, 1, Heading.N, null, 1, "");

            rover.Drive();

            Assert.AreEqual(rover.YCoord, 2);
        }

        [TestMethod]
        public void ReportCoordsAndHeadingTest()
        {
            Rover rover = new Rover(1, 1, Heading.N, null, 1, "");

            Assert.AreEqual(rover.ReportCoordsAndHeading(), "1 1 N");
        }

        [TestMethod]
        public void RunTest()
        {
            Plateau plateau = new Plateau(5, 5);

            Rover rover = new Rover(1, 2, Heading.N, plateau, 1, "LMLMLMLMM");

            rover.Run();

            Assert.AreEqual(rover.ReportCoordsAndHeading(), "1 3 N");
        }
    }
}
