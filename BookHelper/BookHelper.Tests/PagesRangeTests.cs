using System;
using NUnit.Framework;

namespace BookHelper.Tests
{
    [TestFixture]
    public class PagesRangeTests
    {
        [Test]
        [TestCase(-5, 7)] // Documentation about TestCase attribute: http://www.nunit.org/index.php?p=testCase&r=2.5
        [TestCase(-7, -5)]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctr_When_Negative_pages_passed_Then_throws_exception(int from, int to)
        {
            // Act
            new PagesRange(from, to);
        }

        // TODO 1: Write test that checks that "from" should be less or equal than "to". Fix the code if test fails.
        // Task 1
        [Test]
        [TestCase(6, 5)]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_When_passed_from_greater_than_to_Then_throws_exception(int from, int to)
        {
            new PagesRange(from, to);
        }
        
    }
}