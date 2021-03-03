using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem_One___Mars_Rovers___Dale_Hartman.Model;

namespace MarsRoversUnitTest
{
    [TestClass]
    public class PlateauTests
    {
        [TestMethod]
        public void InBoundsTest()
        {
            Plateau plateau = new Plateau(3, 5);

            Assert.IsTrue(plateau.IsInBounds(2, 2));
            Assert.IsTrue(plateau.IsInBounds(3, 5));
        }

        [TestMethod]
        public void OutOfBoundsTest()
        {
            Plateau plateau = new Plateau(3, 5);

            Assert.IsFalse(plateau.IsInBounds(-1, 0));
            Assert.IsFalse(plateau.IsInBounds(5, 3));
        }
    }
}
