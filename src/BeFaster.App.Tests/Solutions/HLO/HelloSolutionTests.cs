using BeFaster.App.Solutions.HLO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Tests.Solutions.HLO
{
    [TestFixture]
    public class HelloSolutionTests
    {

        [TestCase("dave")]
        [TestCase("mike")]
        public void ShouldReturnANonEmptyString(string friendName)
        {
            // When
            var result = HelloSolution.Hello(friendName);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}
