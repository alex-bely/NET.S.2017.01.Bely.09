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
    /// Provides loading and saving <see cref="BookListService"/> from/to file
    /// </summary>
    class BookListStorage : IBookStorage
    {
        #region Private members
        Logger logger;
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
        public List<Book> LoadBookList(Logger logger)
        {
            this.logger = LogManager.GetLogger(logger.Name);
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
                    logger.Info("File is Loaded");
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                logger.Error(ex.Message);
                logger.Trace(ex);
            }
            catch (System.IO.IOException ex)
            {
                logger.Error(ex.Message);
                logger.Trace(ex);
            }

            return list;
        }

        /// <summary>
        /// Saves list of books to file
        /// </summary>
        /// <param name="bookList">List of books for saving</param>
        /// <param name="logger">Reference to logger</param>
        public void SaveBookList(IEnumerable<Book> bookList, Logger logger)
        {
            this.logger = LogManager.GetLogger(logger.Name);
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
                    logger.Info("File is saved");
                }
            }
            catch (System.ArgumentNullException ex)
            {
                logger.Error(ex.Message);
                logger.Trace(ex);
            }
            catch (System.IO.IOException ex)
            {
                logger.Error(ex.Message);
                logger.Trace(ex);
            }
        }
    }
}
