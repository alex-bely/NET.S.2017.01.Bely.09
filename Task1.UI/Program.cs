using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new NLogAdapter();

            //Creating books
            Book book1 = new Book("Война и мир", "Лев Толстой", "роман-эпопея", 1408, "русский", "Россия");
            Book book2 = new Book("Преступление и наказание", "Федор Достоевский", "роман", 608, "русский", "Россия");
            Book book3 = new Book("Собачье сердце", "Михаил Булгаков", "повесть", 384, "русский", "Россия");
            Book book4 = new Book("Тихий Дон", "Михаил Шолохов", "роман-эпопея", 1505, "русский", "Россия");
            Book book5 = new Book("Обломов", "Иван Гончаров", "роман", 640, "русский", "Россия");
            Book book6 = new Book("Идиот", "Федор Достоевский", "роман", 640, "русский", "Россия");
            Book book7 = new Book("Тихий дон", "Михаил Шолохов", "роман-эпопея", 1230, "английский", "Россия");

            //Creating blank BookListServices
            BookListService bookListService = new BookListService(logger);
            BookListService anotherBookListService = new BookListService(logger);
            BookListService bookListServiceFromBS = new BookListService(logger);
            BookListService bookListServiceFromXML = new BookListService(logger);

            //Filling in first BookListService
            bookListService.AddBook(book1);
            bookListService.AddBook(book2);
            bookListService.AddBook(book3);
            bookListService.AddBook(book4);
            bookListService.AddBook(book5);
            bookListService.AddBook(book6);
            bookListService.AddBook(book7);
            

            //Displaying of first element of the BookListService and removing of this element
            Console.WriteLine(bookListService[1].ToString());
            bookListService.RemoveBook(bookListService[1]);

            Console.WriteLine("****************Work with BookListSerializedStorage****************");
            //Creating BookListSerializedStorage with specified name
            BookListSerializedStorage bookListSerializedStorage = new BookListSerializedStorage("LibraryBS.dat");
            //Saving the BookListService to the BookListSerializedStorage
            bookListService.Save(bookListSerializedStorage);
            //Loading books from BookListSerializedStorage to another BookListService and displaying the books
            bookListServiceFromBS.Load(bookListSerializedStorage);
            foreach (Book temp in bookListServiceFromBS.GetBooks())
                Console.WriteLine(temp.ToString() + "\n");

            Console.WriteLine("****************Work with BookListXMLStorage****************");
            // Creating BookListXMLStorage with specified name
            BookListXMLStorage bookListXMLStorage = new BookListXMLStorage("LibraryXML.xml");
            //Saving the BookListService to the BookListXMLStorage
            bookListService.Save(bookListXMLStorage);
            //Loading books from BookListXMLStorage to another BookListService and displaying the books
            bookListServiceFromXML.Load(bookListXMLStorage);
            foreach (Book temp in bookListServiceFromXML.GetBooks())
                Console.WriteLine(temp.ToString() + "\n");

            Console.WriteLine("****************Work with BookListStorage****************");
            //Creating BookListStorage with specified name
            BookListStorage bookListStorage = new BookListStorage("Library.dat");
            //Saving the BookListService to the BookListStorage
            bookListService.Save(bookListStorage);
            //Loading books from BookListStorage to another BookListService and displaying the books
            anotherBookListService.Load(bookListStorage);
            foreach (Book temp in anotherBookListService.GetBooks())
                Console.WriteLine(temp.ToString()+"\n");

           
            //Sorting the books in order specified in Comparer object(by author)
            Console.WriteLine("****************SortedByAuthorList****************");
            IComparer<Book> authorComparer = new BookEqualityByAuthor();
            anotherBookListService.SortBooksByTag(authorComparer);
            foreach (Book temp in anotherBookListService.GetBooks())
                Console.WriteLine(temp.ToString()+ "\n");



            //Sorting the books in order specified in Comparer object(by number of pages)
            Console.WriteLine("****************SortedByNumberOfPages****************");
            IComparer<Book> numberOfPagesComparer = new BookEqualityByNumberOfPages();
            anotherBookListService.SortBooksByTag(numberOfPagesComparer);
            foreach (Book temp in anotherBookListService.GetBooks())
                Console.WriteLine(temp.ToString() + "\n");

            //Extracting books with the tag specified in TagContainer(title="дон")
            Console.WriteLine("****************BooksThatContainGivenTitle****************");
            ITagContainer tagContainer = new BookListContainTitle("дон");

            BookListService tempService = anotherBookListService.FindBookByTag(tagContainer);

             foreach (var temp in tempService.GetBooks())
                Console.WriteLine(temp.ToString() + "\n");

            //Extracting books with the tag specified in TagContainer(language="рус")
            Console.WriteLine("****************BooksThatContainGivenLanguage****************");
            tagContainer = new BookListContainLanguage("рус");

            tempService = anotherBookListService.FindBookByTag(tagContainer);

            foreach (var temp in tempService.GetBooks())
                Console.WriteLine(temp.ToString() + "\n");


            //Extracting books with the tag specified in TagContainer(language="рус")
            Console.WriteLine("****************BooksThatContainGivenLanguage****************");
            
            tempService = anotherBookListService.FindBookByTag(book=>book.Language.Contains("рус"));


            foreach (var temp in tempService.GetBooks())
                Console.WriteLine(temp.ToString() + "\n");
            
            Console.ReadLine();
        }
    }
}
