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
    public class BookListService
    {
        #region Private members
        /// <summary>
        /// Object of Logger for current class
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// List that contains Book objects
        /// </summary>
        private List<Book> bookList;
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
                if (index > bookList.Count - 1 || index < 0)
                {
                    logger.Error($"ArgumentOutOfRangeException: {index} is out of range");
                    throw new ArgumentOutOfRangeException("Index is out of range");
                }
                return bookList[index];
            }
        }

        
        /// <summary>
        /// Creates new instance of the service
        /// </summary>
        public BookListService()
        {
            bookList = new List<Book>();
        }

        /// <summary>
        /// Creates new instance of the service
        /// </summary>
        /// <param name="temp">Base BookListService instance</param>
        public BookListService(BookListService temp)
        {
            bookList = new List<Book>(temp.bookList);
        }

        /// <summary>
        /// Returns books of the booklistservice in array structure
        /// </summary>
        /// <returns>Array of books</returns>
        public Array GetBooks()
        {
            if(ReferenceEquals(bookList,null)) return new Book[] { };
            return bookList.ToArray();
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
            if (bookList.Contains(book)) throw new ArgumentException("List already contains the book");
            bookList.Add(book);
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
            if (!bookList.Remove(book)) throw new ArgumentException("List does not contain the book");
            logger.Info($"Deleted book: {book.Title}");
        }

        /// <summary>
        /// Sorts books in List accorting to tag
        /// </summary>
        /// <param name="tag">Object that contains a rule of comparing</param>
        public void SortBooksByTag(IComparer<Book> tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException();
            bookList.Sort(tag);
        }


        /// <summary>
        /// Sorts books in List accorting to tag
        /// </summary>
        /// <param name="tag">Object that contains a rule of comparing</param>
        public void SortBooksByTag(Comparison<Book> tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException();
            bookList.Sort(tag);
        }

        /// <summary>
        /// Finds books according to tag
        /// </summary>
        /// <param name="tag">Object that contains a rule of searching</param>
        /// <returns>New <see cref="BookListService"/> object with specified books</returns>
        public BookListService FindBookByTag(ITagContainer tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException();
            BookListService temp = new BookListService();

            foreach (var t in bookList)
            {
                if (tag.Contain(t)) temp.AddBook(t);
            }

            return temp;
        }

        /// <summary>
        /// Finds books according to tag
        /// </summary>
        /// <param name="tag">Object that contains a rule of searching</param>
        /// <returns>New <see cref="BookListService"/> object with specified books</returns>
        public BookListService FindBookByTag(Predicate<Book> tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException();
            BookListService temp = new BookListService();

            foreach (var t in bookList)
            {
                if (tag(t)) temp.AddBook(t);
            }

            return temp;
        }

       

        /// <summary>
        /// Saves current BookListService to given storage
        /// </summary>
        /// <param name="storage">Storage to save</param>
        public void Save(IBookStorage storage)
        {
            storage.SaveBookList(bookList, logger);
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
