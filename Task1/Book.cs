using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Represents book
    /// </summary>
    public class Book : IComparable<Book>, IComparable, IEquatable<Book>
    {
        #region Private members
        /// <summary>
        /// Fields that describe certain book
        /// </summary>
        private string title;
        private string author;
        private string genre;
        private int numberOfPages;
        private string language;
        private string country;
        #endregion

        #region Properties
        /// <summary>
        /// Property for title of the book
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null referenced or blank</exception>
        public string Title
        {
            get { return title; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null referenced or empty string!");
                title = value;
            }
        }

        /// <summary>
        /// Property for author of the book
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null referenced or blank</exception>
        public string Author
        {
            get { return author; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null referenced or empty string!");
                author = value;
            }
        }
        /// <summary>
        /// Property for genre of the book
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null referenced or blank</exception>
        public string Genre
        {
            get { return genre; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null referenced or empty string!");
                genre = value;
            }
        }

        /// <summary>
        /// Property for number of pages of the book
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Value is out of range</exception>
        public int NumberOfPages
        {
            get { return numberOfPages; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Invalid number of pages!");
                numberOfPages = value;
            }
        }

        /// <summary>
        /// Property for language of the book
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null referenced or blank</exception>
        public string Language
        {
            get { return language; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null referenced or empty string!");
                language = value;
            }
        }

        /// <summary>
        /// Property for country of the author
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null referenced or blank</exception>
        public string Country
        {
            get { return country; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null referenced or empty string!");
                country = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates instance of the Book
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="author">Author</param>
        /// <param name="genre">Genre</param>
        /// <param name="numberOfPages">Number of pages</param>
        /// <param name="language">Language</param>
        /// <param name="country">Country</param>
        public Book(string title, string author, string genre, int numberOfPages, string language, string country)
        {
            Title = title;
            Author = author;
            Genre = genre;
            NumberOfPages = numberOfPages;
            Language = language;
            Country = country;
        }
        #endregion

        #region Public members
        /// <summary>
        /// Compares two books according to number of pages
        /// </summary>
        /// <param name="other">Book for comparing with current</param>
        /// <returns>A value that indicates the relative order of the books being compared.</returns>
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null)) return 1;
            return NumberOfPages.CompareTo(other.NumberOfPages);
        }

        /// <summary>
        /// Compares two objects if they are books
        /// </summary>
        /// <param name="obj">Object for comparing with current</param>
        /// <returns>A value that indicates the relative order of the books being compared.</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Book))
                throw new ArgumentException();

            Book comparerBook = (Book)obj;
            return CompareTo(comparerBook);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current Book object
        /// </summary>
        /// <param name="other">Book for comparing with current one</param>
        /// <returns>True if the specified object is equal to the current Book object; otherwise, false</returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;
            return title.Equals(other.title) && author.Equals(other.author) && genre.Equals(other.genre) &&
                numberOfPages.Equals(other.numberOfPages) && language.Equals(other.language) && country.Equals(other.country);
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current Book object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book)obj);
        }

        /// <summary>
        /// Calculates hash code for current book
        /// </summary>
        /// <returns>Hash code of the book</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                hashCode = Title.GetHashCode()*1 + Author.GetHashCode()*2 + Country.GetHashCode()*3;
            }
            return hashCode;
        }

        /// <summary>
        /// Represents the current book
        /// </summary>
        /// <returns>>String representation of book</returns>
        public override string ToString()
        {
            return $"Title: {Title}\nAuthor: {Author}\nGenre: {Genre} \nNumber of pages: {NumberOfPages}\nLanguage: {Language} \nCountry: {Country}";
        }


        /// <summary>
        /// Works like Equals.
        /// </summary>
        /// <param name="left"> Book type object for comparing. </param>
        /// <param name="right"> Book type object for comparing. </param>
        /// <returns> Boolean value indicates equality of the parameters.</returns>

        /// <summary>
        /// Compares two books
        /// </summary>
        /// <param name="lhs">First Book object for comparing</param>
        /// <param name="rhs">Second Book object for comparing</param>
        /// <returns>Result of comparison: true if variables are equal, false if they are not</returns>
        public static bool operator ==(Book lhs, Book rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Compares two books
        /// </summary>
        /// <param name="lhs">First Book object for comparing</param>
        /// <param name="rhs">Second Book object for comparing</param>
        /// <returns>Result of comparison: true if variables are not equal, false if the are</returns>
        public static bool operator !=(Book lhs, Book rhs)
        {
            return !(lhs == rhs);
        }
        #endregion

    }
}
