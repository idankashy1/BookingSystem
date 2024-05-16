using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BookingSystem._2._DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookingSystem._2._DAL.Repositories
{
    public class XmlBookRepository : IBookRepository
    {
        private readonly string _xmlFilePath;
        private readonly ILogger<XmlBookRepository> _logger;

        public XmlBookRepository(IConfiguration configuration, ILogger<XmlBookRepository> logger)
        {
            _xmlFilePath = configuration.GetValue<string>("BookData:XmlFilePath");
            _logger = logger;

            // Ensure the XML file exists or initialize it here
            if (!File.Exists(_xmlFilePath))
            {
                var initialBooks = new List<Book>
                {
                    new Book
                    {
                        ISBN = "9051234567897",
                        Title = "Harry Potter",
                        Authors = new List<string> { "J K. Rowling" },
                        Year = 2005,
                        Price = 29.99m,
                        Category = "children"
                    },
                    new Book
                    {
                        ISBN = "9031234567897",
                        Title = "XQuery Kick Start",
                        Authors = new List<string> { "James McGovern", "Per Bothner", "Kurt Cagle", "James Linn", "Vaidyanathan Nagarajan" },
                        Year = 2003,
                        Price = 49.99m,
                        Category = "web"
                    },
                    new Book
                    {
                        ISBN = "9043127323207",
                        Title = "Learning XML",
                        Authors = new List<string> { "Erik T. Ray" },
                        Year = 2003,
                        Price = 39.95m,
                        Category = "web",
                        Cover = "paperback"
                    }
                };
                SaveBooks(initialBooks);
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Bookstore));
                using (var reader = new StreamReader(_xmlFilePath))
                {
                    var bookstore = (Bookstore)serializer.Deserialize(reader);
                    return bookstore.Books;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading from XML file");
                throw;
            }
        }

        public Book GetBookByISBN(string isbn)
        {
            var books = GetAllBooks();
            return books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public void AddBook(Book book)
        {
            var books = GetAllBooks().ToList();
            books.Add(book);
            SaveBooks(books);
        }

        public bool UpdateBook(string originalIsbn, Book updatedBook)
        {
            var books = GetAllBooks().ToList();
            var existingBook = books.FirstOrDefault(b => b.ISBN == originalIsbn);

            if (existingBook != null)
            {
                existingBook.ISBN = updatedBook.ISBN;
                existingBook.Title = updatedBook.Title;
                existingBook.Authors = updatedBook.Authors;
                existingBook.Year = updatedBook.Year;
                existingBook.Price = updatedBook.Price;
                existingBook.Category = updatedBook.Category;
                existingBook.Cover = updatedBook.Cover;

                SaveBooks(books);
                return true;
            }
            return false;
        }

        public bool DeleteBook(string isbn)
        {
            var books = GetAllBooks().ToList();
            var bookToDelete = books.FirstOrDefault(b => b.ISBN == isbn);
            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                SaveBooks(books);
                return true;
            }
            return false;
        }

        private void SaveBooks(List<Book> books)
        {
            var bookstore = new Bookstore { Books = books };
            var serializer = new XmlSerializer(typeof(Bookstore));
            using (var writer = new StreamWriter(_xmlFilePath))
            {
                serializer.Serialize(writer, bookstore);
            }
        }
    }
}
