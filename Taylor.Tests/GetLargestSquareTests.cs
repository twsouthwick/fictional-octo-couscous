using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taylor.Tests
{
    [TestClass]
    public class GetLargestSquareTests_Naive : GetLargestSquareTestsBase
    {
        protected override LargestSquareResult GetResult(BoolMatrix matrix) => matrix.GetLargestSquare_Naive();
    }

    [TestClass]
    public class GetLargestSquareTests_Accumulating : GetLargestSquareTestsBase
    {
        protected override LargestSquareResult GetResult(BoolMatrix matrix) => matrix.GetLargestSquare_Accumulating();
    }

    public abstract class GetLargestSquareTestsBase
    {
        protected abstract LargestSquareResult GetResult(BoolMatrix matrix);

        [TestMethod]
        public void NoSquare_3_3()
        {
            var stringMatrix = @"
0 0 0
0 0 0
0 0 0".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(0, result.Size);
            Assert.AreEqual(-1, result.StartX);
            Assert.AreEqual(-1, result.StartY);
        }
        [TestMethod]
        public void Size1Square_3_3()
        {
            var stringMatrix = @"
0 0 0
0 1 0
0 0 0".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));
            
            Assert.AreEqual(1, result.Size);
            Assert.AreEqual(1, result.StartX);
            Assert.AreEqual(1, result.StartY);
        }


        [TestMethod]
        public void Size2Square_TopLeft_3_3()
        {
            var stringMatrix = @"
1 1 0
1 1 0
0 0 0".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(2, result.Size);
            Assert.AreEqual(0, result.StartX);
            Assert.AreEqual(0, result.StartY);
        }
        [TestMethod]
        public void Size2Square_TopRight_3_3()
        {
            var stringMatrix = @"
0 1 1 
0 1 1
0 0 0".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(2, result.Size);
            Assert.AreEqual(1, result.StartX);
            Assert.AreEqual(0, result.StartY);
        }
        [TestMethod]
        public void Size2Square_BottomLeft_3_3()
        {
            var stringMatrix = @"
0 0 0
1 1 0
1 1 0".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(2, result.Size);
            Assert.AreEqual(0, result.StartX);
            Assert.AreEqual(1, result.StartY);
        }
        [TestMethod]
        public void Size2Square_BottomRight_3_3()
        {
            var stringMatrix = @"
0 0 0
0 1 1
0 1 1".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(2, result.Size);
            Assert.AreEqual(1, result.StartX);
            Assert.AreEqual(1, result.StartY);
        }
        [TestMethod]
        public void Size3Square_3_3()
        {
            var stringMatrix = @"
1 1 1
1 1 1
1 1 1".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(3, result.Size);
            Assert.AreEqual(0, result.StartX);
            Assert.AreEqual(0, result.StartY);
        }

        [TestMethod]
        public void Size2Square_8_8()
        {
            var stringMatrix = @"
1 1 1 1 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(2, result.Size);
            Assert.AreEqual(5, result.StartX);
            Assert.AreEqual(0, result.StartY);
        }
        [TestMethod]
        public void Size4Square_8_8()
        {
            var stringMatrix = @"
1 1 1 1 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 0 0 1 1 0
1 0 0 1 1 1 1 0
1 0 0 1 1 1 1 0
1 0 0 1 1 1 1 0
1 0 0 1 1 1 1 0
".Trim();
            var result = GetResult(BoolMatrix.Parse(stringMatrix));

            Assert.AreEqual(4, result.Size);
            Assert.AreEqual(3, result.StartX);
            Assert.AreEqual(4, result.StartY);
        }

    }
}
