using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.UI
{
    /// <summary>
    /// Implements a method to compare two books
    /// </summary>
    /// <see cref="IComparer{T}"/>
    public class BookEqualityByTitle : IComparer<Book>
    {
        /// <summary>
        /// Compares two books by its titles
        /// </summary>
        /// <param name="lhs">First book for comparison</param>
        /// <param name="rhs">Second book for comparison</param>
        /// <returns>A value that indicates the relative order of the books being compared</returns>
        /// <exception cref="ArgumentNullException">One of arguments is null referenced</exception>
        public int Compare(Book lhs, Book rhs)
        {
            if (lhs == null || rhs == null) throw new ArgumentNullException();
            return String.Compare(lhs.Title, rhs.Title, StringComparison.OrdinalIgnoreCase);
        }

    }

    public class BookEqualityByAuthor : IComparer<Book>
    {
        /// <summary>
        /// Compares two books by its authors
        /// </summary>
        /// <param name="lhs">First book for comparison</param>
        /// <param name="rhs">Second book for comparison</param>
        /// <returns>A value that indicates the relative order of the books being compared</returns>
        /// <exception cref="ArgumentNullException">One of arguments is null referenced</exception>
        public int Compare(Book lhs, Book rhs)
        {
            if (lhs == null || rhs == null) throw new ArgumentNullException();
            return String.Compare(lhs.Author, rhs.Author, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Compares two books by its numbers of pages
    /// </summary>
    /// <param name="lhs">First book for comparison</param>
    /// <param name="rhs">Second book for comparison</param>
    /// <returns>A value that indicates the relative order of the books being compared</returns>
    /// <exception cref="ArgumentNullException">One of arguments is null referenced</exception>
    public class BookEqualityByNumberOfPages : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            if (lhs == null || rhs == null) throw new ArgumentNullException();
            return lhs.NumberOfPages.CompareTo(rhs.NumberOfPages);
        }
    }

}
