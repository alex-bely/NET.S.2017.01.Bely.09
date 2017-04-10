using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.UI
{
    /// <summary>
    /// Implements method for making a decision if the book contains given Title
    /// </summary>
    public class BookListContainTitle : ITagContainer
    {
        public string Title { get; set; }
        public BookListContainTitle(string title)
        {
            Title = title;
        }
        /// <summary>
        /// Checks if book contains given title
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns>True if the specified book contains given title; otherwise, false</returns>
        public bool Contain(Book book)
        {
            if (book.Title.ToUpperInvariant().Contains(Title.ToUpperInvariant())) return true;
            else return false;
        }
    }

    /// <summary>
    /// Implements method for making a decision if the book contains given Author
    /// </summary>
    public class BookListContainAuthor : ITagContainer
    {
        public string Author { get; set; }
        public BookListContainAuthor(string author)
        {
            Author = author;
        }
        /// <summary>
        /// Checks if book contains given Author
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns>True if the specified book contains given Author; otherwise, false</returns>
        public bool Contain(Book book)
        {
            if (book.Author.ToUpperInvariant().Contains(Author.ToUpperInvariant())) return true;
            else return false;
        }
    }

    /// <summary>
    /// Implements method for making a decision if the book contains given Language
    /// </summary>
    public class BookListContainLanguage : ITagContainer
    {
        public string Language { get; set; }
        public BookListContainLanguage(string language)
        {
            Language = language;
        }
        /// <summary>
        /// Checks if book contains given Language
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns>True if the specified book contains given Language; otherwise, false</returns>
        public bool Contain(Book book)
        {
            if (book.Language.ToUpperInvariant().Contains(Language.ToUpperInvariant())) return true;
            else return false;
        }
    }

}
