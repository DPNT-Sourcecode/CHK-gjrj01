using BeFaster.App.Solutions.CHK;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CheckoutSolutionTests
    {
        [TestCase("A, B, E")]
        [TestCase("1, B, C")]
        [TestCase("A- B+C")]
        public void ShouldReturnMinus1_WhenAtLeastOneItemIsNotValid(string skus)
        {
            // When
            var result = CheckoutSolution.ComputePrice(skus);

            //Then
            Assert.AreEqual(-1, result);
        }

        [TestCase("D,B,C,A", ExpectedResult = 115)]
        public int ShouldReturnTotalWhenItemsAreAllValid(string skus)
        {
            // When
            var result = CheckoutSolution.ComputePrice(skus);

            // Then
            return result;
        }
    }
}


