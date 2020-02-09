using BeFaster.App.Solutions.SUM;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.SUM
{
    [TestFixture]
    public class SumSolutionTest
    {
        [TestCase(-4, -5)]
        [TestCase(1, -5)]
        [TestCase(-1, 5)]
        [TestCase(0, -7)]
        [TestCase(-7, 0)]
        [TestCase(101, 1)]
        [TestCase(1, 101)]
        public void ShouldReturnMinus1_When_InvalidInput(int x, int y)
        {
            // When
            var result = SumSolution.Sum(x, y);

            // then 
            Assert.AreEqual(-1, result);
        }

        [TestCase(1, 1, ExpectedResult = 2)]
        [TestCase(0, 1, ExpectedResult = 1)]
        [TestCase(1, 0, ExpectedResult = 1)]
        [TestCase(100, 100, ExpectedResult = 200)]
        public int ComputeSum_WhenInputIsValid(int x, int y)
        {
            //When
            var result = SumSolution.Sum(x, y);

            // Then
            return result;
        }
    }
}



