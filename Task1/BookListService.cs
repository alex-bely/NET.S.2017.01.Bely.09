using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Collections;

namespace Task1
{
    /// <summary>
    /// Provides working with lists of books
    /// </summary>
    public class BookListService : IEnumerable<Book>
    {
        #region Private members
        /// <summary>
        /// Object of Logger for current class
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// List that contains Book objects
        /// </summary>
        private List<Book> BookList;
        #endregion


        /// <summary>
        /// Indexer of the current service
        /// </summary>
        /// <param name="index">Index of the list</param>
        /// <returns>Book of the given index</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index is out of range</exception>
        public Book this[int index]
        {
            get
            {
                if (index > BookList.Count - 1 || index < 0)
                {
                    logger.Error($"ArgumentOutOfRangeException: {index} is out of range");
                    throw new ArgumentOutOfRangeException("Index is out of range");
                }
                return BookList[index];
            }
        }

        #region Enumerators
        /// <summary>
        /// Returns an enumerator that iterates through a collection
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection</returns>
        public IEnumerator<Book> GetEnumerator()
        {
            return ((IEnumerable<Book>)BookList).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Book>)BookList).GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Creates new instance of the service
        /// </summary>
        public BookListService()
        {
            BookList = new List<Book>();
        }

        /// <summary>
        /// Adds book to the list
        /// </summary>
        /// <param name="book">Book for adding</param>
        /// <exception cref="ArgumentNullException">Argument is null referenced</exception>
        /// <exception cref="ArgumentException">List already contains the book</exception>
        public void AddBook(Book book)
        {
            if (ReferenceEquals(book, null)) throw new ArgumentNullException("Book is null referenced");
            if (BookList.Contains(book)) throw new ArgumentException("List already contains the book");
            BookList.Add(book);
            logger.Info($"Added book: {book.Title}");
        }

        /// <summary>
        /// Removes book from the list
        /// </summary>
        /// <param name="book">Book for removing</param>
        /// <exception cref="ArgumentNullException">Argument is null referenced</exception>
        /// <exception cref="ArgumentException">"List does not contain the book"</exception>
        public void RemoveBook(Book book)
        {
            if (ReferenceEquals(book, null)) throw new ArgumentNullException("Book is null referenced");
            if (!BookList.Remove(book)) throw new ArgumentException("List does not contain the book");
            logger.Info($"Deleted book: {book.Title}");
        }

        /// <summary>
        /// Sorts books in List accorting to tag
        /// </summary>
        /// <param name="tag">Object that contains a rule of comparing</param>
        public void SortBooksByTag(IComparer<Book> tag)
        {
            BookList.Sort(tag);
        }

        /// <summary>
        /// Find books according to tag
        /// </summary>
        /// <param name="tag">Object that contains a rule of searching</param>
        /// <returns>New <see cref="BookListService"/> object</returns>
        public BookListService FindBookByTag(ITagContainer tag)
        {
            BookListService temp = new BookListService();

            foreach (var t in BookList)
            {
                if (tag.Contain(t)) temp.AddBook(t);
            }

            return temp;
        }

        /// <summary>
        /// Saves current BookListService to given storage
        /// </summary>
        /// <param name="storage">Storage to save</param>
        public void Save(IBookStorage storage)
        {
            storage.SaveBookList(BookList, logger);
        }

        /// <summary>
        /// Load BookListService from the given storage
        /// </summary>
        /// <param name="storage">Storage for loading</param>
        public void Load(IBookStorage storage)
        {
            foreach (var book in storage.LoadBookList(logger))
            {
                AddBook(book);
            }
        }
    }
}
