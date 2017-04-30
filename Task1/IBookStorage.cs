using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task1
{
    /// <summary>
    /// Provides methods for loading and saving list of books from/to file
    /// </summary>
    public interface IBookStorage
    {
        List<Book> LoadBookList(ILogger logger);
        void SaveBookList(IEnumerable<Book> bookList, ILogger logger);
    }
}
