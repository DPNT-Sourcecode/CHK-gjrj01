using BeFaster.App.Solutions.CHK;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CheckoutSolutionTests
    {
        [TestCase("A, B, E")]
        [TestCase("1,B,C")]
        [TestCase("A-B+C")]
        [TestCase("ABCDa")]
        [TestCase("a")]
        public void ShouldReturnMinus1_WhenAtLeastOneItemIsNotValid(string skus)
        {
            // When
            var result = CheckoutSolution.ComputePrice(skus);

            //Then
            Assert.AreEqual(-1, result);
        }

        // I would again ask the PO how we are supposed to add the quantity to the sku string.
        // I'm assuming we could do just repeat the item in the string if the quantity is greater then 1

        [TestCase("DBCA", ExpectedResult = 115)]
        [TestCase("AA", ExpectedResult = 100)]
        public int ShouldReturnTotalWhenItemsAreAllValid(string skus)
        {
            // When
            var result = CheckoutSolution.ComputePrice(skus);

            // Then
            return result;
        }


        [TestCase("AAA", ExpectedResult = 130)]
        [TestCase("AAAA", ExpectedResult = 180)]
        [TestCase("ABAA", ExpectedResult = 160)]
        [TestCase("AAABB", ExpectedResult = 175)]
        [TestCase("AAAAA", ExpectedResult = 200)]
        [TestCase("AAAAABB", ExpectedResult = 245)]
        [TestCase("AAAAAAAAAA", ExpectedResult = 400)]
        [TestCase("BBBB", ExpectedResult = 90)]
        [TestCase("ABCDECBAABCABBAAAEEAA", ExpectedResult = 665)]
        [TestCase("FFF", ExpectedResult = 20)]
        [TestCase("FFFFFF", ExpectedResult = 40)]
        [TestCase("FFFAFFF", ExpectedResult = 90)]
        public int ShouldReturnDiscountedValue_IfThereIsASpecialOffer(string skus)
        {
            // When
            var result = CheckoutSolution.ComputePrice(skus);

            // Then
            return result;
        }

        [TestCase("EEBBB", ExpectedResult = 125)]
        [TestCase("EEEEBBBB", ExpectedResult = 205)]
        [TestCase("EEEEBBBB", ExpectedResult = 205)]
        [TestCase("STX", ExpectedResult = 45)]
        [TestCase("STXSTX", ExpectedResult = 90)]
        [TestCase("STXSTXT", ExpectedResult = 107)]
        public int ShouldApplyPromotions(string skus)
        {
            // When
            var result = CheckoutSolution.ComputePrice(skus);

            // Then
            return result;
        }
    }
}
