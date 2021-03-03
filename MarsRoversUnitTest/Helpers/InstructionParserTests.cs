using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem_One___Mars_Rovers___Dale_Hartman.Helpers;
using Problem_One___Mars_Rovers___Dale_Hartman.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarsRoversUnitTest
{
    [TestClass()]
    public class InstructionParserTests
    {
        [TestMethod()]
        public void ValidateAndParseTest()
        {
            StringBuilder testInput = new StringBuilder();
            testInput.AppendLine("5 5");
            testInput.AppendLine("1 2 N");
            testInput.AppendLine("LMLMLMLMM");
            testInput.AppendLine("3 3 E");
            testInput.AppendLine("MMRMMRMRRM");

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(testInput.ToString()));

            StreamReader sr = new StreamReader(ms);

            InstructionParser parser = new InstructionParser(sr);

            Assert.AreEqual(parser.ValidateAndParse(), 0);
            Assert.AreEqual(parser.GridCorner, (5, 5));
            Assert.AreEqual(parser.RoverInstructions.Count, 2);
            Assert.AreEqual(parser.RoverInstructions[0], ((1, 2, Heading.N), "LMLMLMLMM"));
            Assert.AreEqual(parser.RoverInstructions[1], ((3, 3, Heading.E), "MMRMMRMRRM"));
        }
    }
}