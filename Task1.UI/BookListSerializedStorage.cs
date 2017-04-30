using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Task1
{
    /// <summary>
    /// Provides loading and saving <see cref="BookListService"/> from/to binary file with serialization
    /// </summary>
    public class BookListSerializedStorage : IBookStorage
    {
        ILogger logger;
        private string FilePath { get; }

        /// <summary>
        /// Initializes the path of the storage
        /// </summary>
        /// <param name="filePath">Path to file</param>
        public BookListSerializedStorage(string filePath)
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
            BinaryFormatter formatter = new BinaryFormatter();
            if (!File.Exists(FilePath))
                throw new FileNotFoundException($"File \"{FilePath}\" not found.");

            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                tempList = (List<Book>)formatter.Deserialize(fs);
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
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, bookList);
            }
            logger.Info("BookList is saved");
        }
    }
}
