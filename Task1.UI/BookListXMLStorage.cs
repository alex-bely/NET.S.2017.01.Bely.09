using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using NLog;
using System.Xml;
using System.Xml.Linq;

namespace Task1
{
    /// <summary>
    /// Provides loading and saving <see cref="BookListService"/> from/to XML-file
    /// </summary>
    public class BookListXMLStorage : IBookStorage
    {
        ILogger logger;
        private string FilePath { get; }

        /// <summary>
        /// Initializes the path of the storage
        /// </summary>
        /// <param name="filePath">Path to file</param>
        public BookListXMLStorage(string filePath)
        {
            FilePath = filePath;
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
            List<Book> tempList = new List<Book>();

            XDocument xdoc = XDocument.Load(FilePath);

            foreach (var item in xdoc.Element("BookList").Elements("Book"))
            {
                XElement title = item.Element("Title");
                XElement author = item.Element("Author");
                XElement genre = item.Element("Genre");
                XElement numberOfPages = item.Element("NumberOfPages");
                XElement language = item.Element("Language");
                XElement country = item.Element("Coutry");
                tempList.Add(new Book(title.Value,author.Value,genre.Value, int.Parse(numberOfPages.Value),language.Value,country.Value));
            }
            logger.Info("BookList is loaded");
            return tempList;
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
            
            XDocument document = new XDocument();
            document.Add(new XElement("BookList"));
            
            foreach (var book in bookList)
            {
                XElement node = new XElement("Book");
                //document.Root.Add(node);
                
                XElement title = new XElement("Title", book.Title);
                
                XElement author = new XElement("Author", book.Author);
                XElement genre = new XElement("Genre", book.Genre);
                XElement numberOfPages = new XElement("NumberOfPages", book.NumberOfPages);
                XElement language = new XElement("Language", book.Language);
                XElement country = new XElement("Coutry", book.Country);

                node.Add(title, author, genre, numberOfPages, language, country);

                document.Root.Add(node);
                
            }

            document.Save(FilePath);
            logger.Info("BookList is saved");
        }
    }
}
