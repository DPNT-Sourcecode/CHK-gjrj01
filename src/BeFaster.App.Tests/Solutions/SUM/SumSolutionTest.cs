using BeFaster.App.Solutions.SUM;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.SUM
{
    [TestFixture]
    public class SumSolutionTest
    {
        [TestCase()]
        public int InvalidInput(int x, int y)
        {
            // When
            var result = SumSolution.Sum(x, y);

            // then 
            Assert.AreEqual(-1, result);
        }


        [TestCase(1, 1, ExpectedResult = 2)]
        public int ComputeSum(int x, int y)
        {
            return SumSolution.Sum(x, y);
        }
    }
}

