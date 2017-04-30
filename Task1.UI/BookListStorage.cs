using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;
using NLog;
using System.IO;

namespace Task1.UI
{
    /// <summary>
    /// Provides loading and saving <see cref="BookListService"/> from/to binary file
    /// </summary>
    class BookListStorage : IBookStorage
    {
        #region Private members
        ILogger logger;
        private string FilePath { get; }
        #endregion

        /// <summary>
        /// Initializes the path of the storage
        /// </summary>
        /// <param name="path">Path to file</param>
        public BookListStorage(string path)
        {
            FilePath = path;
        }

        /// <summary>
        /// Load list of Books from the file
        /// </summary>
        /// <param name="logger">Reference to logger</param>
        /// <returns>List of loaded books</returns>        
        public List<Book> LoadBookList(ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");
            this.logger = logger;
            List<Book> list = new List<Book>();
            try
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
                {
                    while (binaryReader.PeekChar() > -1)
                    {
                        string Title = binaryReader.ReadString();
                        string Author = binaryReader.ReadString();
                        string Genre = binaryReader.ReadString();
                        int NumberOfPages = binaryReader.ReadInt32();
                        string Language = binaryReader.ReadString();
                        string Country = binaryReader.ReadString();
                        list.Add(new Book(Title, Author, Genre, NumberOfPages, Language, Country));
                    }
                    logger.Info("BookList is loaded");
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                logger.Error(ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                logger.Error(ex.Message);
            }

            return list;
        }

        /// <summary>
        /// Saves list of books to file
        /// </summary>
        /// <param name="bookList">List of books for saving</param>
        /// <param name="logger">Reference to logger</param>
        public void SaveBookList(IEnumerable<Book> bookList, ILogger logger)
        {
            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");
            this.logger = logger;
            try
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
                {
                    foreach (var book in bookList)
                    {
                        binaryWriter.Write(book.Title);
                        binaryWriter.Write(book.Author);
                        binaryWriter.Write(book.Genre);
                        binaryWriter.Write(book.NumberOfPages);
                        binaryWriter.Write(book.Language);
                        binaryWriter.Write(book.Country);
                    }
                    logger.Info("BookList is saved");
                }
            }
            catch (System.ArgumentNullException ex)
            {
                logger.Error(ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
