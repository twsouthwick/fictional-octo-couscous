using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taylor;

namespace Taylor.Tests
{
    [TestClass]
    public class ParseAndToStringTests
    {
        [TestMethod]
        public void RoundTrip_3_3_Identity()
        {
            var stringMatrix = @"1 0 0
0 1 0
0 0 1";

            var roundTrip = BoolMatrix.Parse(stringMatrix).ToString();

            Assert.AreEqual(stringMatrix, roundTrip);
        }

        // TODO - test input validation exceptions
    }
}
