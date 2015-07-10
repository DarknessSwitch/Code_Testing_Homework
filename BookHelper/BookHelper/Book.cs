using System;
using System.Collections.Generic;
using System.Linq;

namespace BookHelper
{
    internal class Book
    {
        private readonly List<PagesRange> _readPages = new List<PagesRange>();

        public readonly int PagesCount;

        public Book(int pagesCount)
        {
            PagesCount = pagesCount;
        }

        public void AddRange(int from, int to)
        {
            _readPages.Add(new PagesRange(from, to));
        }

        public int HowManyPagesLeft()
        {
            // TODO 3: Improve/fix the code here.
            // Rewritten this method. The best I could think of. (task 3)
            List<PagesRange> tmp = new List<PagesRange>(_readPages);
            for(int i = 0; i < tmp.Count-1; )
            {
                for (int j = i+1; j < tmp.Count; j++)
                {
                    if ((tmp[i].From >= tmp[j].From && tmp[i].From <= tmp[j].To) ||
                        (tmp[i].To >= tmp[j].From && tmp[i].To <= tmp[j].To))
                    {
                        tmp[j] = new PagesRange(Math.Min(tmp[i].From, tmp[j].From), Math.Max(tmp[i].To, tmp[j].To));
                        tmp.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            var readPages = tmp.Sum(item => item.To - item.From + 1);
            var leftPages = PagesCount - readPages;
            return leftPages;
        }
    }
}