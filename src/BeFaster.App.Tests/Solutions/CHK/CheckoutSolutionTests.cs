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
        public void ShouldReturnMinus1_WhenAtLeastOneItemIsNotValid()
        {
            //Given
            var skus =;

            // When
            var result = CheckoutSolution.ComputePrice(skus);

            //Then
            Assert.AreEqual(-1, result);
        }
    }
}

