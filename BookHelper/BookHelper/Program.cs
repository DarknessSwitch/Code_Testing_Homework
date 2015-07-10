using System;

namespace BookHelper
{
    class Program
    {
        static void Main()
        {
            var book = new Book(20);
            book.AddRange(3, 4);
            book.AddRange(6, 12);

            var left = book.HowManyPagesLeft();
            Console.WriteLine("Left: " + left);
            Console.ReadKey();
        }
    }
}
