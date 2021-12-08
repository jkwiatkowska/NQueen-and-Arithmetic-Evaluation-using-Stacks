using NUnit.Framework;
using CO3402_Assignment;

namespace CO3402_Tests
{
    class NQueenTest
    {
        [Test]
        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 2)]
        [TestCase(5, 10)]
        [TestCase(6, 4)]
        [TestCase(7, 40)]
        [TestCase(8, 92)]
        [TestCase(9, 352)]
        [TestCase(10, 724)]
        [TestCase(11, 2680)]
        [TestCase(12, 14200)]
        [TestCase(13, 73712)]
        [TestCase(14, 365596)]
        [TestCase(15, 2279184)]
        [TestCase(16, 14772512)]
        [TestCase(17, 95815104)]
        public void NQueenSolutionCountTest(int n, int expectedResult)
        {
            int result = NQueen.SolveN(n, NQueen.eDisplayMode.None);

            Assert.AreEqual(result, expectedResult);
        }
    }
}
