using System.ComponentModel;
using NUnit.Framework;

namespace BookHelper.Tests
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void HowManyPagesLeft_When_given_few_read_ranges_Then_returns_correct_count_of_unread_pages()
        {
            // Arrange
            var book = new Book(10);
            book.AddRange(3, 4);
            book.AddRange(6, 8);

            // Act
            var leftPages = book.HowManyPagesLeft();

            // Assert
            Assert.That(leftPages, Is.EqualTo(5));
        }

        // TODO 2: Write test that checks that HowManyPagesLeft() correctly counts pages when overlapped ranges are added. Fix the code if test fails.
        // Task 2
        [Test]
        [TestCase(3, 10, 6, 12, 15, 5)]
        public void HowManyPagesLeft_When_overlapped_ranges_added_Then_returns_correct_count_of_unread_pages(int from1,
            int to1, int from2, int to2, int pageCount, int expected)
        {
            var book = new Book(pageCount);
            book.AddRange(from1, to1);
            book.AddRange(from2, to2);

            var leftPages = book.HowManyPagesLeft();

            Assert.That(leftPages, Is.EqualTo(expected));
        }

    }
}
